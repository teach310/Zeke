using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestTableView : TableView<QuestData> {

	public float defaultCellHeight = 128.0f;

	protected override void Start () {
		LoadData ();
	}
	
	void LoadData(){
		// 通常データはデータソースから取得しますが、ここではハードコードで定義する
		tableData = new List<QuestData>();
		for (int i = 0; i < 5; i++) {
			string questName = "Quest" + (i+1).ToString ();
			tableData.Add (new QuestData (questName, i));
		}
		UpdateContents ();
	}

	protected override float CellHeightAtIndex (int index)
	{
		return defaultCellHeight;
	}


}
