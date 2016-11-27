using UnityEngine;
using System.Collections;
using UniRx;


public abstract class ScreenInitializer<T> : SingletonMonoBehaviour<T>
	where T : MonoBehaviour
{

	private Subject<Unit> _initSubject = new Subject<Unit>();
	public IObservable<Unit> OnInitScreen{
		get{return _initSubject;}
	}

	protected virtual void Start(){
		Init ();
	}

	protected virtual void Init(){
		_initSubject.OnNext (Unit.Default);
		_initSubject.OnCompleted ();
	}

	// Loadが終わったことを検知
	// Loadまで待つ
}
