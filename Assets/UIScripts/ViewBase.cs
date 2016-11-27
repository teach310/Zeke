using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class ViewBase : MonoBehaviour {

	//Rect Transformコンポーネントをキャッシュ
	private RectTransform _cachedRectTransform;
	public RectTransform CachedRectTransform{
		get{
			return _cachedRectTransform ?? (_cachedRectTransform = this.GetComponent<RectTransform> ());
		}
	}

	// ビューのタイトルを取得，設定するプロパティ
	public virtual string Title{ get {return string.Empty;} set{}}
}
