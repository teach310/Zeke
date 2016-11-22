using UnityEngine;
using System.Collections;
using Unity.Linq;
using UnityEngine.UI;
using UniRx;
using System;
public class BottomMenu : MonoBehaviour {

	[System.Serializable]
	public class BottomMenuIcon{
		public Toggle toggle;
		public Home.ScreenType screenType;

		public BottomMenuIcon(){
		}

		public BottomMenuIcon(Toggle toggle, Home.ScreenType screenType){
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
		bottomMenuSubject
			.ThrottleFirstFrame(9)
			.Subscribe (x=>ScreenManager.Instance.SetCurrentScreen(x))
			.AddTo(this.gameObject);

		for(int i=0;i<_bottomMenuIcon.Length;i++){
			var icon = _bottomMenuIcon [i];
			icon.toggle
				.OnValueChangedAsObservable ()
				.Where(b=>b)
				.Subscribe (_=>bottomMenuSubject.OnNext(icon.screenType));
		}
	}
}
