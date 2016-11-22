using UnityEngine;
using System.Collections;

public class GuildScreenController : ScreenController, IScreenController {

	public void OnEnter(){
		AnimateIn ();
		Debug.Log ("Enter " + defaultScreen.name);
	}

	public void OnExit(){
		AnimateOut ();
		Debug.Log ("Exit " + defaultScreen.name);
	}
}
