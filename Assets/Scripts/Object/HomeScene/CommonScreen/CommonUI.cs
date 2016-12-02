using UnityEngine;
using System.Collections;
using Unity.Linq;
using UnityEngine.UI;
using UniRx;

public class CommonUI : SingletonMonoBehaviour<CommonUI> {

	[SerializeField]
	private Button _backButton;

	[SerializeField]
	private BottomMenu _bottomMenu;

	void Reset(){
		_bottomMenu = this.gameObject.Descendants ().OfComponent<BottomMenu> ().First ();
	}

	protected override void Awake(){
		Init ();
	}

	void Start () {
		if (!_bottomMenu) {
			Reset ();
		}

		_bottomMenu.Init ();
	}

	public void Init(){
		_backButton.OnClickAsObservable ()
			.Subscribe (_ => OnClickBackButton());
	}

	public void OnClickBackButton(){
		ScreenManager.Instance.BackScreen ();
	}
}
