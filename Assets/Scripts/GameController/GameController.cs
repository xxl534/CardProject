using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{

		PlayerControl _player;
		public List<LevelGateControl> _levels;
		LevelGateControl _selectLevel;
		public 	SwordControl _SwordForSelection;
		SceneFade _sceneFade;
		BattleControl _battleController;

		void Awake ()
		{
				_player = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerControl> ();
				_sceneFade = GameObject.FindGameObjectWithTag (Tags.sceneFader).GetComponent<SceneFade> ();
				_battleController = GameObject.FindGameObjectWithTag (Tags.battle).GetComponent<BattleControl> ();

				_levels = new List<LevelGateControl> ();
		}
	
		void Start ()
		{
				Init ();
				Load ();
				_sceneFade.ExitFading ();
		}

		// Update is called once per frame
		void Update ()
		{
		
		}
		/// <summary>
		/// Save imformation about game controller.
		/// </summary>
		void Save ()
		{
//				PlayerPrefs.SetInt (_uniqueIdentityString + "_mapWidth", _mapWidth);
//				//PlayerPrefs.SetInt (_uniqueIdentityString+ "_selectMapIndex", _selectMapIndex);
//				PlayerPrefs.SetInt (_uniqueIdentityString + "_firstLockIndex", _lastUnlockIndex);
//				PlayerPrefs.SetInt (_uniqueIdentityString, 1);
		}

		/// <summary>
		///  Auto save after complete battle.This method may be called by star counter
		/// </summary>
		public void SaveAfterCompleteBattle ()
		{//Should Save data of game controller,player controller,select level and of star counter
				Save ();

				_player.Save ();

				_selectLevel.Save ();
		}

		/// <summary>
		/// Load this data of game contorller and proccess relative operations
		/// </summary>
		void Load ()
		{
		}

		/// <summary>
		/// Loads data from json and do proccessing.
		/// </summary>
		void LoadFromJson (Dictionary<string,object> dict)
		{

		}


		/// <summary>
		/// Loads data from playerprefs and do proccessing.
		/// </summary>
		void LoadFromPlayerPrefs ()
		{

		}

		public void LightPathPoints (int levelIndex)
		{
				if (levelIndex - 1 >= 0) {
						_levels [levelIndex - 1].LightPathPoints ();
				}
		}

		/// <summary>
		/// Initialize game controller.
		/// </summary>
		void Init ()
		{
				foreach (GameObject go in GameObject.FindGameObjectsWithTag (Tags.levelGate)) {
						_levels.Add (go.GetComponent<LevelGateControl> ());
				}
				_levels.Sort ();
				_levels [0].Unlock ();
		LevelButtonClick(_levels[0]);
		}

		public void LevelButtonClick (LevelGateControl level)
		{
				if (level != _selectLevel) {
						_SwordForSelection.Show (level.transform.position);
						_selectLevel = level;
						
				} else {
						BattleStart (level.GetComponent<LevelInfo> ());
				}
		}

		public void BattleStart (LevelInfo levelInfo)
		{
				_sceneFade.BeginFading ();
				gameObject.AddComponent<RunOnCondition> ().RunWhenBoolChange (_sceneFade.IsOpeque, true, delegate {
						_battleController.LoadLevel (levelInfo);
						_battleController.gameObject.SetActive (true);
						_sceneFade.ExitFading ();
//						_battleController.gameObject.AddComponent<RunOnCondition> ().RunWhenBoolChange (delegate {
//								return _sceneFade.gameObject.activeSelf;
//						},
//			                                                                              false, delegate {
//								_battleController.RotateShells ();
//						});
				});
		}

		public void BattleComplete ()
		{
//				Debug.Log ("GC_battleComplete1");
				gameObject.AddComponent<RunOnCondition> ().RunWhenBoolChange (delegate {
						return _sceneFade.gameObject.activeSelf;
				},
			false, delegate {
						_selectLevel.LevelComplete ();
						SaveAfterCompleteBattle ();
				}
				);
		}

		void AllSave ()
		{

		}
}
