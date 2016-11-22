using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonTest : MonoBehaviour {

	public void OnClick(){
		Sequence sequence = DOTween.Sequence ();
		sequence.Append(this.transform.DOScale (0.9f, 0.1f));
		sequence.Append (this.transform.DOScale (1.0f, 0.1f));
	}
}
