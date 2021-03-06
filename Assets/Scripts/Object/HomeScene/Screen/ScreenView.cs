﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Linq;
using Common.Animation;

public class ScreenView : MonoBehaviour {



	protected virtual void Init(){
		AnimateIn ();
	}

	public void AnimateIn(){
		float duration = 0.8f;

		var list = this.gameObject.Descendants ().OfComponent<AnimateInModel> ();
		foreach (var item in list) {
			item.Init ();
			item.transform.DOMove (item.EndPoint, duration).SetEase (Ease.InOutQuart);
		}
	}
}
