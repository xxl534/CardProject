using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
		/// <summary>
		/// The json data file.
		/// </summary>
		public TextAsset _jsonDataFile;
		public int _mapWidth;
		PlayerControl _player;
		StarCounterControl _starCounter;
		List<MapLayerControl> _maps;
		int  _selectMapIndex;
		public List<LevelGateControl> _levels;
		public LevelGateControl _selectLevel;
		string _uniqueIdentityString;
		/// <summary>
		/// The index of the last unlocked level.If all the levels are unlocked ,this param equals _levels.Count-1
		/// </summary>
		int _lastUnlockIndex;
		LevelGateControl _lastUnlockLevel;
		public 	SwordControl _SwordForSelection;
		List<PathPointControl> _pathPoints;
		SceneFade _sceneFade;
		BattleControl _battleController;
		public Dictionary<string,object> _allInfoDict;

		void Awake ()
		{
				if (_jsonDataFile != null)
						_allInfoDict = MiniJSON.Json.Deserialize (_jsonDataFile.text) as Dictionary<string ,object>;
				_uniqueIdentityString = transform.parent.name + name + GetType ().ToString ();
				_player = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerControl> ();
				_starCounter = GameObject.FindGameObjectWithTag (Tags.starCounter).GetComponentInChildren<StarCounterControl> ();
				_sceneFade = GameObject.FindGameObjectWithTag (Tags.sceneFader).GetComponent<SceneFade> ();
				_battleController = GameObject.FindGameObjectWithTag (Tags.battle).GetComponent<BattleControl> ();
				//_battleController.gameObject.SetActive (false);

				_maps = new List<MapLayerControl> ();
				_levels = new List<LevelGateControl> ();
				_pathPoints = new List<PathPointControl> ();
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
				PlayerPrefs.SetInt (_uniqueIdentityString + "_mapWidth", _mapWidth);
				//PlayerPrefs.SetInt (_uniqueIdentityString+ "_selectMapIndex", _selectMapIndex);
				PlayerPrefs.SetInt (_uniqueIdentityString + "_firstLockIndex", _lastUnlockIndex);
				PlayerPrefs.SetInt (_uniqueIdentityString, 1);
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
			
				LoadFromJson (_allInfoDict);
				if (PlayerPrefs.GetInt (_uniqueIdentityString) == 1)
						LoadFromPlayerPrefs ();
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
				//Load data
				_mapWidth = PlayerPrefs.GetInt (_uniqueIdentityString + "_mapWidth");
				_lastUnlockIndex = PlayerPrefs.GetInt (_uniqueIdentityString + "_firstLockIndex");
				
				//process data
				//Locate map layers position
				_maps [_selectMapIndex].transform.localPosition = Vector3.zero;
				for (int i = 0; i < _maps.Count; i++) {
						if (i < _selectMapIndex)
								_maps [i].transform.localPosition = new Vector3 (-_mapWidth, 0, 0);
						else if (i > _selectMapIndex)
								_maps [i].transform.localPosition = new Vector3 (_mapWidth, 0, 0);
				}

				//active path points 
				for (int i = 0; i < _pathPoints.Count; i++) {
						_pathPoints [i].SetActivated ();
						if (_pathPoints [i]._nextLevelGate != null && _lastUnlockIndex < _levels.Count && _pathPoints [i]._nextLevelGate == _levels [_lastUnlockIndex])
								break;
				}
		}

		public	void UnlockLevel ()
		{
				_lastUnlockIndex++;
		}

		

		/// <summary>
		/// Initialize game controller.
		/// </summary>
		void Init ()
		{
				
				foreach (GameObject go in GameObject.FindGameObjectsWithTag (Tags.mapLayer)) {
						_maps.Add (go.GetComponent<MapLayerControl> ());
				}
				_maps.Sort ();
				_selectMapIndex = 0;

				foreach (GameObject go in GameObject.FindGameObjectsWithTag (Tags.levelGate)) {
						_levels.Add (go.GetComponent<LevelGateControl> ());
				}
				_levels.Sort ();
				for (int i = 0; i < _levels.Count; i++) {
						_levels [i].SetLevelIndex (i);
				}
				_selectLevel = null;
				//level No.1 start unlocked
				_levels [0].GetComponent<LevelInfo> ().Unlock ();
				_lastUnlockIndex = 0;

				foreach (GameObject go in GameObject.FindGameObjectsWithTag(Tags.pathPoint)) {
						_pathPoints.Add (go.GetComponent<PathPointControl> ());
				}
				_pathPoints.Sort ();
				for (int i = 0; i < _pathPoints.Count-1; i++) {
						if (_pathPoints [i]._pointPostfix == 1 && _pathPoints [i]._levelPostfix > 0)
								_levels [_pathPoints [i]._levelPostfix - 1]._nextPathPoint = _pathPoints [i];
			
						if (_pathPoints [i]._pointPostfix == _pathPoints [i + 1]._pointPostfix - 1) {
								_pathPoints [i]._nextPathPoint = _pathPoints [i + 1];
						} else {
								if (_pathPoints [i]._levelPostfix < _levels.Count)
										_pathPoints [i]._nextLevelGate = _levels [_pathPoints [i]._levelPostfix];
						}
				}
				if (_pathPoints [_pathPoints.Count - 1]._levelPostfix < _levels.Count)
						_pathPoints [_pathPoints.Count - 1]._nextLevelGate = _levels [_pathPoints [_pathPoints.Count - 1]._levelPostfix];

				_battleController.gameObject.SetActive (false);
		}

		public void LevelButtonClick (LevelGateControl level)
		{
				if (level != _selectLevel) {
						if (_selectLevel != null && _selectLevel.GetMapLayer () == _maps [_selectMapIndex]) {
								_selectLevel = level;
								_SwordForSelection.Disappear (ShowSword);
						} else {
								_selectLevel = level;
								ShowSword ();
						}
				} else {
						BattleStart (level.GetComponent<LevelInfo> ());
				}
		}

		void ShowSword ()
		{
			
//				Debug.Log ("showSword");
				_SwordForSelection.transform.parent = _selectLevel.GetMainButton ().transform;
				_SwordForSelection.transform.localPosition = Vector3.zero;
				_SwordForSelection.transform.localScale = Vector3.one;
				_SwordForSelection.Show ();
			
		}

		public void BattleStart (LevelInfo levelInfo)
		{
				_sceneFade.BeginFading ();
				gameObject.AddComponent<RunOnCondition> ().RunWhenBoolChange (_sceneFade.IsOpeque, true, delegate {
						_battleController.LoadLevel (levelInfo);
						_maps [_selectMapIndex].gameObject.SetActive (false);
						_battleController.gameObject.SetActive (true);
						_sceneFade.ExitFading ();
				});
		}

		public void BattleComplete (StarNum starNum)
		{
				Debug.Log ("GC_battleComplete1");
				_sceneFade.BeginFading ();
				gameObject.AddComponent<RunOnCondition> ().RunWhenBoolChange (_sceneFade.IsOpeque, true, delegate {
						_maps [_selectMapIndex].gameObject.SetActive (true);
						_battleController.gameObject.SetActive (false);
						_sceneFade.ExitFading ();
						gameObject.AddComponent<RunOnCondition> ().RunWhenBoolChange (delegate {
								return _sceneFade.gameObject.activeSelf;
						},
			false, delegate {
								_selectLevel.GainStar (starNum);
								SaveAfterCompleteBattle ();
						}
						);
				}
				);
				Debug.Log ("GC_battleComplete");
//				_selectLevel.GainStar (starNum);
//				SaveAfterCompleteBattle ();
		}
	
		public void MapLayerLeftButtonClick ()
		{
				if (_selectMapIndex > 0) {
						_maps [_selectMapIndex - 1].transform.localPosition += new Vector3 (_mapWidth, 0, 0);
						_maps [_selectMapIndex].transform.localPosition += new Vector3 (_mapWidth, 0, 0);
						_selectMapIndex--;
				}
		}

		public void MapLayerRightButtonClick ()
		{
				if (_selectMapIndex + 1 < _maps.Count) {
						_maps [_selectMapIndex + 1].transform.localPosition -= new Vector3 (_mapWidth, 0, 0);

						_maps [_selectMapIndex].transform.localPosition -= new Vector3 (_mapWidth, 0, 0);
						_selectMapIndex++;
				}
		}

		void AllSave ()
		{

		}
}
