using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LevelGateControl : MonoBehaviour,IComparable<LevelGateControl>
{
		LevelInfo _levelInfo;
		public	StarSlotControl _starOne, _starTwo, _starThree;
		GameController	_gameController;
		public  PathPointControl _nextPathPoint;
		GameObject _lock;
		public GameObject _mainButton;
		string _uniqueIdentityString;
		StarFlyPath _starPath;

		// Use this for initialization
		void Awake ()
		{

				_uniqueIdentityString = transform.parent.name + name;
				_gameController = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<GameController> ();
				_levelInfo = GetComponent<LevelInfo> ();
//				foreach (StarSlotControl starControl in GetComponentsInChildren<StarSlotControl>()) {
//						switch (starControl.name) {
//						case "starEmpty_01":
//								_starOne = starControl;
//								break;
//						case "starEmpty_02":
//								_starTwo = starControl;
//								break;
//						case "starEmpty_03":
//								_starThree = starControl;
//								break;
//						default:
//								break;
//						}
//				}
				foreach (Transform trans in GetComponentsInChildren<Transform>()) {
						switch (trans.name) {
						case "starEmpty_01":
								_starOne = trans.GetComponent<StarSlotControl> ();
								break;
						case "starEmpty_02":
								_starTwo = trans.GetComponent<StarSlotControl> ();
								break;
						case "starEmpty_03":
								_starThree = trans.GetComponent<StarSlotControl> ();
								break;
						case "btn_main":
								_mainButton = trans.gameObject;
								break;
						case "icon_lock":
								_lock = trans.gameObject;
								break;
						default:
								break;
						}
				}

				_starPath = gameObject.AddComponent<StarFlyPath> ();
				_starPath._levelControl = this;
		}

		void Start ()
		{
				LoadFromJson (_gameController._allInfoDict);
				LoadFromPlayerPrefs ();
				Init ();
		}
		// Update is called once per frame
		void Update ()
		{
	
		}

		public int CompareTo (LevelGateControl other)
		{
				if (transform.parent.name.Equals (other.transform.parent.name))
						return name.CompareTo (other.name);
				return transform.parent.name.CompareTo (other.transform.parent.name); 
		}
/// <summary>
/// Unlock this level and process relative events.
/// </summary>
		public void Unlock ()
		{
				_levelInfo.Unlock ();
				_mainButton.GetComponentInChildren<ParticleSystem> ().Play ();
				gameObject.AddComponent<RunOnCondition> ().RunDelay (1.5f, UnlockButton);
				
//				Transform[] childTransforms = GetComponentsInChildren<Transform> ();
//		Debug.Log (childTransforms.Length);
//		foreach (Transform trans in GetComponentsInChildren<Transform> ()) {
//			Debug.Log(trans.name);
//						if (trans.name == "icon_lock") {
////				Debug.Log("icon_lock");
//								trans.gameObject.SetActive (false);
//						} else if (trans.name == "btn_main") {
////				Debug.Log("btn_main");
//								trans.GetComponent<UISprite> ().spriteName = "iconStage_01";
//						}
//				}
				Debug.Log ("Unlock");
				
		}

		void UnlockButton ()
		{
				_mainButton.GetComponent<UISprite> ().spriteName = "iconStage_01";
				_lock.SetActive (false);
				_mainButton.GetComponent<UIPlayAnimation> ().enabled = true;
		_starOne.Unlock ();
		_starTwo.Unlock ();
		_starThree.Unlock ();
		_gameController.UnlockLevel ();
		}

		/// <summary>
		/// Unlocks the next level through path points.
		/// </summary>
		public void UnlockNextLevel ()
		{
				_nextPathPoint.Activate ();
		}

		public void GainStar (StarNum starNum)
		{
				_levelInfo.GainStar (starNum);
				if (starNum >= StarNum.ONE)
						_starOne.GainStar ();
				if (starNum >= StarNum.TWO) {
						gameObject.AddComponent<RunOnCondition> ().RunDelay (0.35f, _starTwo.GainStar);
//						_starTwo.GainStar ();
				}
				if (starNum >= StarNum.TRHEE) {
						gameObject.AddComponent<RunOnCondition> ().RunDelay (0.7f, _starThree.GainStar);
//						_starThree.GainStar ();
				}
				Debug.Log (name + "GainStar");
				UnlockNextLevel ();
		}

		public void SetLevelIndex (int index)
		{
				_levelInfo.SetLevelIndex (index);
		}

		public int GetLevelIndex ()
		{
				return _levelInfo.GetLevelIndex ();
		}

		public void MainButtonMouseEnter ()
		{
		
		}

		public void MainButtonMouseLeave ()
		{

		}

		public void MainButtonMouseClick ()
		{

				if (_levelInfo.unlocked) {
						_gameController.LevelButtonClick (this);
				}
		}

		public void Save ()
		{
				if (_levelInfo.unlocked) {
						PlayerPrefs.SetInt (_uniqueIdentityString, 1);
						PlayerPrefs.SetInt (_uniqueIdentityString + "_starNum", _levelInfo.GetStarNum ());
				}
		}

		public void LoadFromPlayerPrefs ()
		{
		}

		public void LoadFromJson (Dictionary<string,object> dict)
		{
		}

		void Init ()
		{
				if (!_levelInfo.unlocked) {
						_starOne.FillStar (false);
						_starOne.gameObject.SetActive (false);
						_starTwo.FillStar (false);
						_starTwo.gameObject.SetActive (false);
						_starThree.FillStar (false);
						_starThree.gameObject.SetActive (false);

				} else {
						_mainButton.GetComponent<UISprite> ().spriteName = "iconStage_01";
						_lock.SetActive (false);
						int starNum = _levelInfo.GetStarNum ();
						if (starNum < 3)
								_starThree.FillStar (false);
						if (starNum < 2)
								_starTwo.FillStar (false);
						if (starNum < 1)
								_starOne.FillStar (false);
				}
		}

		public Vector3[] GetStarFlyPath (StarSlotControl  starSlot)
		{
				Vector3[] path = null;
				if (starSlot == _starOne) {
						path = _starPath.GetPathOne ();
				} else if (starSlot == _starTwo) {
						path = _starPath.GetPathTwo ();
				} else if (starSlot == _starThree) {
						path = _starPath.GetPathThree ();
				}
				for (int i=0; i!=path.Length; i++)
						path [i] += _mainButton.transform.position;
				return path;
		}

		public MapLayerControl GetMapLayer ()
		{
				return GetComponentInParent<MapLayerControl> ();
		}

		public GameObject GetMainButton ()
		{
				return _mainButton;
		}
}
