using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Home;
using UniRx;

public class ScreenManager : SingletonMonoBehaviour<ScreenManager> {

	[SerializeField]
	private HomeScreenController _homeScreenController;

	[SerializeField]
	private MyRoomScreenController _myRoomScreenController;

	[SerializeField]
	private GuildScreenController _guildScreenController;

	private ReactiveProperty<IScreenController> _currentScreenController = new ReactiveProperty<IScreenController>();
	public IObservable<IScreenController> OnScreenChanged {
		get { return _currentScreenController; }
	}
	private Dictionary<ScreenType, IScreenController> _screenControllerMap = new Dictionary<ScreenType, IScreenController>();

	// 初期化
	protected override void Awake(){
		Init ();
	}

	void Init(){
		SetScreenController ();

		_currentScreenController
			.Pairwise ()
			.Subscribe (x => {
				if(x.Previous != null){
					x.Previous.OnExit();
				}
				x.Current.OnEnter();
		});
	}

	// スクリーンをセット
	void SetScreenController(){
		_screenControllerMap.Add (ScreenType.Home, _homeScreenController);
		_screenControllerMap.Add (ScreenType.MyRoom, _myRoomScreenController);
		_screenControllerMap.Add (ScreenType.Guild, _guildScreenController);
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


	// スクリーンを変更
	public void SetCurrentScreen(ScreenType newScreenType){
		_currentScreenController.Value = _screenControllerMap [newScreenType];
	}
}
