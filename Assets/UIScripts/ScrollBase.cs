using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScrollBase : ScrollRect {

	//Rect Transformコンポーネントをキャッシュ
	private RectTransform _cachedRectTransform;
	public RectTransform CachedRectTransform{
		get{
			return _cachedRectTransform ?? (_cachedRectTransform = this.GetComponent<RectTransform> ());
		}
	}

}
