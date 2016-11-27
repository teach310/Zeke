using UnityEngine;
using System.Collections;

public abstract class TableViewCell<T> : ViewBase {

	// セルに対応するリスト項目のインデックスを保持
	public int dataIndex;

	// セルの内容を更新
	public abstract void UpdateContent (T itemData);

	// セルの高さを取得，設定するプロパティー
	public float Height
	{
		get{ return CachedRectTransform.sizeDelta.y; }
		set{
			Vector2 sizeDelta = CachedRectTransform.sizeDelta;
			sizeDelta.y = value;
			CachedRectTransform.sizeDelta = sizeDelta;
		}
	}

	// セルの上端の一を取得，設定するプロパティー
	public Vector2 Top
	{
		get{
			Vector3[] corners = new Vector3[4];
			CachedRectTransform.GetLocalCorners (corners);
			return CachedRectTransform.anchoredPosition + new Vector2 (0.0f, corners [1].y);
		}
		set{
			Vector3[] corners = new Vector3[4];
			CachedRectTransform.GetLocalCorners (corners);
			CachedRectTransform.anchoredPosition = value - new Vector2 (0.0f, corners [1].y);
		}
	}

	// セルの下端の一を取得，設定するプロパティー
	public Vector2 Bottom
	{
		get{
			Vector3[] corners = new Vector3[4];
			CachedRectTransform.GetLocalCorners (corners);
			return CachedRectTransform.anchoredPosition + new Vector2 (0.0f, corners [3].y);
		}
		set{
			Vector3[] corners = new Vector3[4];
			CachedRectTransform.GetLocalCorners (corners);
			CachedRectTransform.anchoredPosition = value - new Vector2 (0.0f, corners [3].y);
		}
	}

}
