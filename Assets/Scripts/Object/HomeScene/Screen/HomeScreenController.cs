using UnityEngine;
using System.Collections;
using Unity.Linq;

public class HomeScreenController : ScreenController, IScreenController {

	//private HomeModel _model;
	private HomeScreenView _view;

	public void OnEnter(){
		PushScreen ();
		_view = this.gameObject.Descendants ().OfComponent<HomeScreenView> ().First ();
		_view.Init ();

		Debug.Log ("Enter " + defaultScreen.name);
	}

	public void OnExit(){
		DestroyScreen ();
		Debug.Log ("Exit " + defaultScreen.name);
	}
}
