using UnityEngine;
using System.Collections;

public abstract class ScreenController : MonoBehaviour {

	public GameObject defaultScreen;
	protected GameObject _currentScreen;

	protected void PushScreen(){
		_currentScreen = Instantiate (defaultScreen, this.transform) as GameObject;
	}

	protected virtual void DestroyScreen(){
		Destroy (_currentScreen);
	}

}
