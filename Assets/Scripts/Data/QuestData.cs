using UnityEngine;
using System.Collections;

public class QuestData {

	public string questName; // クエスト名
	public int ID;

	public QuestData(string questName, int ID){
		this.questName = questName;
		this.ID = ID;
	}
}
