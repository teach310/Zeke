using UnityEngine;
using System.Collections;
using Unity.Linq;

public class GuildScreenController : ScreenController, IScreenController {

	private GuildScreenView _view;

	public void OnEnter(){
		PushScreen ();
		_view = this.gameObject.Descendants ().OfComponent<GuildScreenView> ().First ();
		_view.Init ();

		Debug.Log ("Enter " + defaultScreen.name);
	}

	public void OnExit(){
		DestroyScreen ();
		Debug.Log ("Exit " + defaultScreen.name);
	}
}
