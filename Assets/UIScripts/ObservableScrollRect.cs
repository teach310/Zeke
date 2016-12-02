using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;

public class ObservableScrollRect : ScrollRect{


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
		this.OnDestroyAsObservable ()
			.Subscribe (_ => {
				_onBeginDragSubject.OnCompleted();
				_onEndDragSubject.OnCompleted();
			});
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
}
