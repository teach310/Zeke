using UnityEngine;
using System.Collections;
using Unity.Linq;

public class MyRoomScreenController : ScreenController, IScreenController {

	private MyRoomScreenView _view;

	public void OnEnter(){
		PushScreen ();
		_view = this.gameObject.Descendants ().OfComponent<MyRoomScreenView> ().First();
		_view.Init ();

		Debug.Log ("Enter " + defaultScreen.name);
	}

	public void OnExit(){
		DestroyScreen ();
		Debug.Log ("Exit " + defaultScreen.name);
	}
}
