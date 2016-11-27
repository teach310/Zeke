using UnityEngine;
using System.Collections;
using UniRx;


/// <summary>
/// スクリーンの捜査
/// </summary>
public abstract class ScreenController<T>: MonoBehaviour, IScreenController 
	where T:IScreenController
{
	public GameObject defaultScreen;
	protected GameObject _currentScreen;

//	private TView _view;
	// スクリーンがEnterするときのストリーム
	private Subject<Unit> _onEnterSubject = new Subject<Unit>();
	public IObservable<Unit> OnEnterAsObservable {
		get { return _onEnterSubject; }
	}

	// スクリーンがExitするときのストリーム
	private Subject<Unit> _onExitSubject = new Subject<Unit>();
	public IObservable<Unit> OnExitAsObservable {
		get { return _onExitSubject; }
	}

	public virtual void Awake(){
		Init ();
	}

	// 初期化
	public virtual void Init(){
		
		_onEnterSubject
			.Subscribe (_ =>{
				// スクリーンの生成
				PushScreen ();
			});

		_onExitSubject
			.Subscribe (_ => DestroyScreen ());
	}

	// Enter
	public void OnEnter(){
		_onEnterSubject.OnNext (Unit.Default);
	}

	// Exit
	public void OnExit(){
		_onExitSubject.OnNext (Unit.Default);
	}

	// スクリーン生成
	protected void PushScreen(){
		_currentScreen = Instantiate (defaultScreen, this.transform) as GameObject;
	}

	// スクリーン削除
	protected virtual void DestroyScreen(){
		Destroy (_currentScreen);
	}
}
