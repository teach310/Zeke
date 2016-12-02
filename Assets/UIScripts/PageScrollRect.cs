using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using DG.Tweening;

// ページスクロールビュー
public class PageScrollRect : ScrollRect {

	private ReactiveProperty<int> _pageIndex = new ReactiveProperty<int> (0);
	public IObservable<int> OnPageChanged{
		get{ return _pageIndex; }
	}


	private Subject<PointerEventData> _onBeginDragSubject = new Subject<PointerEventData>();
	public IObservable<PointerEventData> OnBeginDragAsObservable {
		get { return _onBeginDragSubject; }
	}

	private Subject<PointerEventData> _onEndDragSubject = new Subject<PointerEventData>();
	public IObservable<PointerEventData> OnEndDragAsObservable {
		get { return _onEndDragSubject; }
	}

	protected override void Awake(){
		base.Awake ();
		Init ();
	}

	void Init(){
		
		OnEndDragAsObservable
			.Where(_=>content)
			.Subscribe (x => Snap (x))
			.AddTo (this.gameObject);

	}

	public override void OnBeginDrag (PointerEventData eventData)
	{
		base.OnBeginDrag (eventData);
		_onBeginDragSubject.OnNext (eventData);
	}

	public override void OnEndDrag (PointerEventData eventData)
	{
		base.OnEndDrag (eventData);
		_onEndDragSubject.OnNext (eventData);
	}

	void Snap(PointerEventData eventData){
		GridLayoutGroup grid = content.GetComponent<GridLayoutGroup> ();
		// 1ページの幅を取得.
		float pageWidth = grid.cellSize.x + grid.spacing.x;

		StopMovement ();

		// 四捨五入して次の位置を決定 左に行ったら+なので-をかける
		int newPageIndex = -Mathf.RoundToInt(content.anchoredPosition.x / pageWidth);
		if (newPageIndex == _pageIndex.Value && Mathf.Abs (eventData.delta.x) >= 4) {
			// sign ... 正負を返す
			newPageIndex += (int)Mathf.Sign (-eventData.delta.x);
		}
//		 先頭や末尾のページの場合，それ以上先にスクロールしないようにする
		if (newPageIndex < 0) {
			newPageIndex = 0;
		} else if (newPageIndex > grid.transform.childCount - 1) {
			newPageIndex = grid.transform.childCount - 1;
		}

		float destX = (-1) * newPageIndex * pageWidth;

		content.DOAnchorPosX (destX, 0.5f);
		_pageIndex.Value = newPageIndex;
	}
	

}
