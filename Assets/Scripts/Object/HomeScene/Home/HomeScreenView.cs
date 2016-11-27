using UnityEngine;
using System.Collections;
using UniRx;

public class HomeScreenView : ScreenView {

	void Awake(){
		HomeScreenInitializer.Instance.OnInitScreen
			.Subscribe (_ => Init ());
	}
}
