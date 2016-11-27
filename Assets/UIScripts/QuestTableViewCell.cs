using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestTableViewCell : TableViewCell<QuestData> {

	[SerializeField] private Text _nameLabel; // クエスト名

	public override void UpdateContent (QuestData itemData)
	{
		_nameLabel.text = itemData.questName;
	}
}
