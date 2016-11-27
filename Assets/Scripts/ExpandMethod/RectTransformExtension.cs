using UnityEngine;

public static class RectTransformExtension {

	private static Vector2 vector2;

	#region SetAnchoredPosition
	public static void SetAnchoredPosition(this RectTransform transform, float x, float y) {
		vector2.Set(x, y);
		transform.anchoredPosition = vector2;
	}
	public static void SetAnchoredPositionX(this RectTransform transform, float x) {
		vector2.Set(x, transform.anchoredPosition.y);
		transform.anchoredPosition = vector2;
	}
	public static void SetAnchoredPositionY(this RectTransform transform, float y) {
		vector2.Set(transform.anchoredPosition.x, y);
		transform.anchoredPosition = vector2;
	}
	#endregion
}