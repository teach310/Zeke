using UnityEngine;
using System.Collections;

public class Barrier : SingletonMonoBehaviour<MonoBehaviour> {

	private GameObject _barrier;

	protected override void Awake(){
		_barrier = this.transform.GetChild (0).gameObject;
	}

	public void Generate(){
		_barrier.SetActive (true);
	}

	public void Hide(){
		_barrier.SetActive (false);
	}
}
