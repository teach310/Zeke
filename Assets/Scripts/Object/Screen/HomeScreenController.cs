using UnityEngine;
using System.Collections;

public class HomeScreenController : ScreenController, IScreenController {


	public void OnEnter(){
		AnimateIn ();
		Debug.Log ("Enter " + defaultScreen.name);
	}

	public void OnExit(){
		AnimateOut ();
		Debug.Log ("Exit " + defaultScreen.name);
	}
}
