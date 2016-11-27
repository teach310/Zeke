using UnityEngine;
using System.Collections;
using UniRx;

public class GuildScreenView : ScreenView {

	void Awake(){
		GuildScreenInitializer.Instance.OnInitScreen
			.Subscribe (_ => Init ());
	}
}
