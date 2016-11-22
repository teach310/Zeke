using UnityEngine;
using System.Collections;
using UnityEditor;
using Common.Animation;

[CustomEditor(typeof(AnimateInModel))]
	public class AnimationUIModelEditor : Editor {

	private AnimateInModel ui;
	private Transform handleTransform;
	private Quaternion handleRotation;

	private void OnSceneGUI()
	{
		ui = target as AnimateInModel;
		handleTransform = ui.transform;
		handleRotation = Tools.pivotRotation == PivotRotation.Local ?
			handleTransform.rotation : Quaternion.identity;

		Vector3 p0 = ShowPoint();
		Vector3 p1 = handleTransform.position;

		Handles.color = Color.yellow;
		Handles.DrawLine(p0, p1);
	}

	// Handleを作成する
	Vector3 ShowPoint()
	{
		Vector3 point = handleTransform.TransformPoint(ui.startPoint);
		EditorGUI.BeginChangeCheck();
		point = Handles.DoPositionHandle(point, handleRotation);
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(ui, "Move Point");
			EditorUtility.SetDirty(ui);
			ui.startPoint = handleTransform.InverseTransformPoint(point);
		}
		return point;
	}
}
