using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(QuestTableView))]
public class QuestTableViewEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();
	}
}
