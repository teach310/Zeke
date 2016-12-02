using UnityEngine;
using System.Collections;
using Unity.Linq;
using UnityEngine.UI;
using UniRx;
using System;
using System.Linq;
using Home;

public class BottomMenu : MonoBehaviour {

	[System.Serializable]
	public class BottomMenuIcon{
		public Toggle toggle;
		public ScreenType screenType;

		public BottomMenuIcon(){
		}

		public BottomMenuIcon(Toggle toggle, ScreenType screenType){
			this.toggle = toggle;
			this.screenType = screenType;
		}
	}

	[SerializeField]
	private BottomMenuIcon[] _bottomMenuIcon;

	void Reset(){
		// 自動セット
		var toggles = this.gameObject.Descendants ().OfComponent<Toggle> ().ToArray();
		_bottomMenuIcon = new BottomMenuIcon[toggles.Length];
		for (int i = 0; i < toggles.Length; i++) {
			_bottomMenuIcon[i] = new BottomMenuIcon (toggles[i], (Home.ScreenType) i);
		}
	}

	public void Init(){

		var bottomMenuSubject = new Subject<Home.ScreenType> ();
		// ボタンを押されたらスクリーン遷移
		bottomMenuSubject
			.ThrottleFirstFrame(9)
			.Subscribe (x=>ScreenManager.Instance.PushScreen(x))
			.AddTo(this.gameObject);

		// イベント発行
		for(int i=0;i<_bottomMenuIcon.Length;i++){
			var icon = _bottomMenuIcon [i];
			icon.toggle
				.OnValueChangedAsObservable ()
				.Where(b=>b)
				.Where(_=>icon.screenType != ScreenManager.Instance.CurrentScreen)
				.Subscribe (_=>bottomMenuSubject.OnNext(icon.screenType));
		}

		// イベント登録
		ScreenManager.Instance.OnScreenChanged
			.Subscribe (x => SetMenu (x));
	}

	public void SetMenu(ScreenType newScreenType){
		var menuIcon = _bottomMenuIcon.Where (x => x.screenType == newScreenType).First();
		menuIcon.toggle.isOn = true;
	}
}
