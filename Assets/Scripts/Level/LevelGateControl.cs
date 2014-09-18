using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Holoville.HOTween;

public class LevelGateControl : MonoBehaviour,IComparable<LevelGateControl>
{

	private static float _unlockDuration=1f,
		_hoverFadeDuration=0.3f;
		LevelInfo _levelInfo;
		GameController	_gameController;
		public  PathPointControl _nextPathPoint;
		public UISprite _mainButton,_whiteEdge;
	public UILabel _levelName;
	public int _levelIndex;
	private float _clickTimer = 0,_clickThreshold=0.3f;	

	public int levelIndex
	{
		get{return _levelIndex;}
	}
		// Use this for initialization
		void Awake ()
		{
				_gameController = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<GameController> ();
				_levelInfo = GetComponent<LevelInfo> ();
		}

		void Start ()
		{
		_levelInfo.Load ();
		Init ();
		}
		// Update is called once per frame
		void Update ()
		{
		_clickTimer += Time.deltaTime;
		}
		
		void OnClick()
	{
		if (_clickTimer > _clickThreshold) {
			_clickTimer=0f;
			_gameController.LevelButtonClick(this);
				}
	}

	void OnHover(bool isOver)
	{
		if (_levelInfo.unlocked) {
			if(isOver){
				HOTween.To(_whiteEdge,_hoverFadeDuration,new TweenParms().Prop("alpha",1f).OnStart(()=>{_whiteEdge.gameObject.SetActive(true);}));
				HOTween.To(_levelName,_hoverFadeDuration,new TweenParms().Prop("effectDistance",new Vector2(6f,6f)).OnStart(()=>{_levelName.effectStyle= UILabel.Effect.Shadow;}));
			}
			else{
				HOTween.To(_whiteEdge,_hoverFadeDuration,new TweenParms().Prop("alpha",0f).OnComplete(()=>{_whiteEdge.gameObject.SetActive(false);}));
				HOTween.To(_levelName,_hoverFadeDuration,new TweenParms().Prop("effectDistance",Vector2.zero).OnComplete(()=>{_levelName.effectStyle= UILabel.Effect.None;}));}
				}
	}
		public int CompareTo (LevelGateControl other)
		{
				return _levelIndex-other._levelIndex; 
		}
/// <summary>
/// Unlock this level and process relative events.
/// </summary>
		public void Unlock ()
		{
				_levelInfo.Unlock ();
				_mainButton.GetComponentInChildren<ParticleSystem> ().Play ();
		HOTween.To (_mainButton, _unlockDuration, new TweenParms ().Prop ("color", Color.white).Ease (EaseType.Linear));
		HOTween.To (_levelName, _unlockDuration, new TweenParms ().Prop ("alpha", 1f).Ease (EaseType.Linear).OnStart(()=>{_levelName.gameObject.SetActive(true);}));
				
		}

//		void UnlockButton ()
//		{
//				_mainButton.GetComponent<UISprite> ().spriteName = "iconStage_01";
//				_mainButton.GetComponent<UIPlayAnimation> ().enabled = true;
//		_gameController.UnlockLevel ();
//		}

		/// <summary>
		/// Unlocks the next level through path points.
		/// </summary>
		public void UnlockNextLevel ()
		{
				_nextPathPoint.Activate ();
		}

//		public void SetLevelIndex (int index)
//		{
//				_levelInfo.SetLevelIndex (index);
//		}
//
//		public int GetLevelIndex ()
//		{
//				return _levelInfo.GetLevelIndex ();
//		}
		

		public void Save ()
		{
		_levelInfo.Save ();
		}

	public void LevelComplete()
	{
		_nextPathPoint.Activate ();
	}
	void Init()
	{
		bool unlock = _levelInfo.unlocked;
		if (unlock) {
						_mainButton.alpha = 1;
			_levelName.gameObject.SetActive(true);
						_levelName.alpha = 1;
						_nextPathPoint.Init (_levelInfo.unlocked);
				} else {
			_mainButton.color=new Color(0.5f,0.5f,0.5f,0.75f);
			_levelName.alpha=0f;
			_levelName.gameObject.SetActive(false);
				}
		_whiteEdge.alpha = 0f;
		_whiteEdge.gameObject.SetActive (false);
		_levelName.effectStyle = UILabel.Effect.None;
		_levelName.effectDistance = Vector2.zero;
	}

}
