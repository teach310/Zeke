using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UniRx;

public class TableView<T> : ScrollBase {

	protected List<T> tableData = new List<T>(); // リスト項目のデータ所持

	public RectOffset padding; // スクロールさせる内容のパディング
	public float spacingHeight = 4.0f; // 各セルの間隔

	public GameObject cellBase; // コピー元のセル

	private LinkedList<TableViewCell<T>> cells = 
		new LinkedList<TableViewCell<T>>();

	private Rect _visibleRect; // リスト項目をセルとして表示する範囲を示す矩形
	public RectOffset visibleRectPadding; // visibleRectのパディング


	// リスト項目に対応するセルの高さを返すメソッド
	protected virtual float CellHeightAtIndex(int index)
	{
		return 0f;
	}

	protected override void Awake(){
		Init ();
	}

	protected virtual void Init(){
		this.OnValueChangedAsObservable ()
			.Subscribe (x => {
				// visibleRectを更新する
				UpdateVisibleRect();
				// スクロールした方向によって，セルを再利用して更新する
		});
	}

	// セルを再利用して表示を更新
	void ReuseCells(int scrollDirection){
		if (cells.Count < 1) {
			return;
		}

		if (scrollDirection > 0) {
			// 上にスクロールしている場合，visibleRectの範囲より上にあるセルを
			// 順に下に移動して内容を更新する
			TableViewCell<T> firstCell = cells.First.Value;
			while (firstCell.Bottom.y > _visibleRect.y) {
				TableViewCell<T> lastCell = cells.Last.Value;
				UpdateCellForIndex (firstCell, lastCell.dataIndex + 1);
				firstCell.Top = lastCell.Bottom + new Vector2 (0f, -spacingHeight);

				cells.AddLast (firstCell);
				cells.RemoveFirst ();
				firstCell = cells.First.Value;
			}

			// visibleRectの範囲内に空きがあればセルを作成する
			FillVisibleRectWithCells();
		}else if(scrollDirection < 0){
			// 下にスクロールしている場合、visibleRectの範囲より下にあるセルを
			// 順に上に移動して内容を更新する
			TableViewCell<T> lastCell = cells.Last.Value;
			while(lastCell.Top.y < _visibleRect.y - _visibleRect.height)
			{
				TableViewCell<T> firstCell = cells.First.Value;
				UpdateCellForIndex(lastCell, firstCell.dataIndex - 1);
				lastCell.Bottom = firstCell.Top + new Vector2(0.0f, spacingHeight);

				cells.AddFirst(lastCell);
				cells.RemoveLast();
				lastCell = cells.Last.Value;
			}
		}
	}

	// スクロールさせる内容全体の高さを更新する
	protected void UpdateContentSize()
	{
		// スクロールさせる内容全体の高さを算出させる
		float contentHeight = 0.0f;
		for (int i = 0; i < tableData.Count; i++) {
			contentHeight += CellHeightAtIndex (i);
			if (i > 0) {
				contentHeight += spacingHeight;
			}
		}

		// スクロールさせる内容の高さを設定する．
		Vector2 sizeDelta = content.sizeDelta;
		sizeDelta.y = padding.top + contentHeight + padding.bottom;
		content.sizeDelta = sizeDelta;
	}

	private  TableViewCell<T> CreateCellForIndex(int index)
	{
		// 生成
		GameObject obj = Instantiate(cellBase) as GameObject;
		obj.SetActive (true);
		TableViewCell<T> cell = obj.GetComponent<TableViewCell<T>> ();

		// 親要素の付け替えを行うとスケールやサイズが失われるため，変数に保持しておく
		Vector3 scale = cell.transform.localScale;
		Vector2 sizeDelta = cell.CachedRectTransform.sizeDelta;
		Vector2 offsetMin = cell.CachedRectTransform.offsetMin;
		Vector2 offsetMax = cell.CachedRectTransform.offsetMax;

		cell.transform.SetParent (cellBase.transform.parent);

		// セルのスケールやサイズを設定する
		cell.transform.localScale = scale;
		cell.CachedRectTransform.sizeDelta = sizeDelta;
		cell.CachedRectTransform.offsetMin = offsetMin;
		cell.CachedRectTransform.offsetMax = offsetMax;

		//指定したインデックスのリスト項目に対応するセルとして内容を更新する．
		UpdateCellForIndex(cell, index);
		cells.AddLast (cell);
		return cell;
	}

	// セルの内容を更新するメソッド
	void UpdateCellForIndex(TableViewCell<T> cell, int index){
		// セルに対応するリスト項目にインデックスを設定する
		cell.dataIndex = index;

		if (cell.dataIndex >= 0 && cell.dataIndex <= tableData.Count - 1) {
			// セルに対応するリスト項目があればセルをアクティブにして内容を更新し，高さを設定する
			cell.gameObject.SetActive (true);
			cell.UpdateContent (tableData [cell.dataIndex]);
			cell.Height = CellHeightAtIndex (cell.dataIndex);
		} else {
			// セルに対応するリスト項目がなかったら，セルを非アクティブにして表示しない．
		}
	}

	// visibleRectを更新する
	void UpdateVisibleRect()
	{
		// visibleRectの位置はスクロールさせる内容の基準点からの相対位置
		_visibleRect.x = content.anchoredPosition.x + visibleRectPadding.left;
		_visibleRect.y = -content.anchoredPosition.y + visibleRectPadding.top;

		// visibleRectのサイズはスクロールビューのサイズ + パディング
		_visibleRect.width = CachedRectTransform.rect.width + visibleRectPadding.left + visibleRectPadding.right;
		_visibleRect.height = CachedRectTransform.rect.height + visibleRectPadding.top + visibleRectPadding.bottom;
	}

	protected void UpdateContents(){
		UpdateContentSize (); // スクロールさせる内容のサイズを更新する
		UpdateVisibleRect (); // visibleRectを更新する．

		if (cells.Count < 1) {
			// セルが1つもない場合，visibleRectの範囲に入る最初のリスト項目を探して,
			// それに対応するセルを作成する
			Debug.Log("p");
			Vector2 cellTop = new Vector2 (0f, -padding.top);
			for (int i = 0; i < tableData.Count; i++) {
				Debug.Log ("a");
				float cellHeight = CellHeightAtIndex (i);
				Vector2 cellBottom = cellTop + new Vector2 (0f, -cellHeight);
				if ((cellTop.y <= _visibleRect.y && cellTop.y >= _visibleRect.y - _visibleRect.height) ||
				    (cellBottom.y <= _visibleRect.y && cellBottom.y >= _visibleRect.y - _visibleRect.height)) {
					TableViewCell<T> cell = CreateCellForIndex (i);
					cell.Top = cellTop;
					break;
				}
				cellTop = cellBottom + new Vector2 (0f, spacingHeight);
			}

			// visibleRectの範囲内に空きがあればセルを作成する．
			FillVisibleRectWithCells();
		} else {
			// すでにセルがある場合，先頭のセルから順に対応するリスト項目の
			// インデックスを設定し直し，位置と内容を更新する
			LinkedListNode<TableViewCell<T>> node = cells.First;
			UpdateCellForIndex (node.Value, node.Value.dataIndex);
			node = node.Next;

			while (node != null) {
				UpdateCellForIndex (node.Value, node.Previous.Value.dataIndex + 1);
				node.Value.Top = node.Previous.Value.Bottom + new Vector2 (0f, -spacingHeight);
				node = node.Next;
			}

			// visibleRectの範囲内に空きがあればセルを作成する
			FillVisibleRectWithCells();
		}
	}

	// visibleRectの範囲内に表示される分のセルを作成する
	void FillVisibleRectWithCells()
	{
		// セルがなければ何もしない
		if (cells.Count < 1) {
			return;
		}

		// 表示されている最後のセルに対応するリスト項目の次のリスト項目があり，
		// かつ，そのセルがvisibleRectの範囲内にはいるようであれば対応するセルを作成する
		TableViewCell<T> lastCell = cells.Last.Value;
		int nextCellDataIndex = lastCell.dataIndex + 1;
		Vector2 nextCellTop = lastCell.Bottom + new Vector2 (0f, -spacingHeight);

		while (nextCellDataIndex < tableData.Count
			&& nextCellTop.y >= _visibleRect.y - _visibleRect.height) {
			TableViewCell<T> cell = CreateCellForIndex (nextCellDataIndex);
			cell.Top = nextCellTop;

			lastCell = cell;
			nextCellDataIndex = lastCell.dataIndex + 1;
			nextCellTop = lastCell.Bottom + new Vector2 (0f, -spacingHeight);
		}
	}
}
