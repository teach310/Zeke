using UnityEngine;
using System.Collections;

public abstract class ScreenController : MonoBehaviour {

	public GameObject defaultScreen;
	protected GameObject _currentScreen;

	protected void AnimateIn(){
		_currentScreen = Instantiate (defaultScreen, this.transform) as GameObject;
	}

	protected void AnimateOut(){
		
		Destroy (_currentScreen);
	}

}
