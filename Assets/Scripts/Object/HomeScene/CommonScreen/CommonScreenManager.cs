using UnityEngine;
using System.Collections;
using Unity.Linq;

public class CommonScreenManager : MonoBehaviour {
	[SerializeField]
	private BottomMenu _bottomMenu;

	void Reset(){
		_bottomMenu = this.gameObject.Descendants ().OfComponent<BottomMenu> ().First ();
	}
		
	void Start () {
		if (!_bottomMenu) {
			Reset ();
		}

		_bottomMenu.Init ();
	}
}
