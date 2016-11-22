using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Home;

public class ScreenManager : SingletonMonoBehaviour<ScreenManager> {

	[SerializeField]
	private HomeScreenController _homeScreenController;

	[SerializeField]
	private MyRoomScreenController _myRoomScreenController;

	[SerializeField]
	private GuildScreenController _guildScreenController;

	private IScreenController _currentScreenController;
	private Dictionary<ScreenType, IScreenController> _screenControllerMap = new Dictionary<ScreenType, IScreenController>();

	// 初期化
	protected override void Awake(){
		SetScreenController ();
	}

	void Start(){
		// 初期スクリーン
		SetCurrentScreen (ScreenType.Home);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.A)) {
			SetCurrentScreen (ScreenType.Home);
		}

		if (Input.GetKeyDown (KeyCode.B)) {
			SetCurrentScreen (ScreenType.MyRoom);
		}

		if (Input.GetKeyDown (KeyCode.C)) {
			SetCurrentScreen (ScreenType.Guild);
		}
	}
	// スクリーンをセット
	void SetScreenController(){
		_screenControllerMap.Add (ScreenType.Home, _homeScreenController);
		_screenControllerMap.Add (ScreenType.MyRoom, _myRoomScreenController);
		_screenControllerMap.Add (ScreenType.Guild, _guildScreenController);
	}

	// スクリーンを変更
	void SetCurrentScreen(ScreenType newScreenType){
		if (_currentScreenController != null) {
			_currentScreenController.OnExit ();
		}
		_currentScreenController = _screenControllerMap [newScreenType];
		_currentScreenController.OnEnter ();
	}
}
