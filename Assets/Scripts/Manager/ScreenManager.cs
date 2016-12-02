using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Home;
using UniRx;
using System.Linq;

public class ScreenManager : SingletonMonoBehaviour<ScreenManager> {

	[SerializeField]
	private HomeScreenController _homeScreenController;

	[SerializeField]
	private MyRoomScreenController _myRoomScreenController;

	[SerializeField]
	private GuildScreenController _guildScreenController;

	//private IScreenController _currentScreenController;

	private Dictionary<ScreenType, IScreenController> _screenControllerMap = new Dictionary<ScreenType, IScreenController>();

	static Stack<ScreenType> _beforeScreen = new Stack<ScreenType>();

	private ReactiveProperty<ScreenType> _currentScreen = new ReactiveProperty<ScreenType> ();

	public ScreenType CurrentScreen{
		get { return _currentScreen.Value; }
	}

	public IObservable<ScreenType> OnScreenChanged {
		get { return _currentScreen; }
	}

	//private ReactiveProperty<bool> _isBack = new ReactiveProperty<bool>();

	// 初期化
	protected override void Awake(){
		Init ();
	}

	void Init(){
		SetScreenController ();

		// 初回
		OnScreenChanged
			.First ()
			.Subscribe (x => _screenControllerMap [x].OnEnter ());

		OnScreenChanged
			.Pairwise ()
			.Subscribe (x => {
				_screenControllerMap[x.Previous].OnExit();
				_screenControllerMap[x.Current].OnEnter();
		});

//		OnScreenChanged
//			.Where (_ => _isBack.Value == false)
//			.Pairwise ()
//			.Subscribe (x => _beforeScreen.Push(x.Previous));


//		_isBack
//			.Where (x => x == true)
//			.Subscribe (_=>{
//				_currentScreen.Value = _beforeScreen.Pop();
//				_isBack.Value = false;
//			});
		//_currentScreen
//		_currentScreenController.Value = _screenControllerMap [newScreenType];
//			.Subscribe()
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
	void SetCurrentScreen(ScreenType newScreenType){
		_currentScreen.Value = newScreenType;
	}

	// 新しいスクリーンをプッシュ
	public void PushScreen(ScreenType newScreenType){
		// 旧スクリーンを保存
		_beforeScreen.Push (_currentScreen.Value);
		// 新スクリーンに切り替え
		SetCurrentScreen (newScreenType);
	}


	public void BackScreen(){

		// _beforeScreenを使って戻らない場合の処理

		// wip アニメーション中はタッチを無効

		PopScreen ();

		//_isBack.Value = true;
	}

	// 以前のスクリーンに戻る
	void PopScreen(){
		if (_beforeScreen.Count < 1) {
			// 前の階層がないので何もしない
			return;
		}

		var newScreenType = _beforeScreen.Pop ();
		SetCurrentScreen (newScreenType);
	}


}
