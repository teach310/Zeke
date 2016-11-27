using UnityEngine;
using System.Collections;
using UniRx;

public class MyRoomScreenView : ScreenView {
	void Awake(){
		MyRoomScreenInitializer.Instance.OnInitScreen
			.Subscribe (_ => Init ());
	}
}
