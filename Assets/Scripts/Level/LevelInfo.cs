using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelInfo:MonoBehaviour
{
		LevelGateControl _gateControl;
		string _name;
		string _uniqueIdentifer;
		bool _unlocked;
		private LevelData _levelData;

		public LevelData leveldata {
				get{ return _levelData;}
		}

		public bool unlocked {
				get{ return _unlocked;}
		}

		public int index {
				get{ return _gateControl.levelIndex;}
		}

		void Awake ()
		{
				_gateControl = GetComponent<LevelGateControl> ();
		}

		public void Unlock ()
		{
				_unlocked = true;
		}

		public void LoadFromJson ()
		{
				TextAsset text = Resources.Load <TextAsset> (string.Format ("{0}/level_{1:000}", ResourcesFolderPath.json_level, index));
				if (text == null)
						return;
				string json = text.text;
				Dictionary<string ,object> dict = MiniJSON.Json.Deserialize (json)as Dictionary<string,object>;
				_levelData = LevelData.GetLevelData (dict ["level"]as Dictionary<string,object>);
//		Debug.Log ("experience:"+_levelData.experience);
//		Debug.Log ("level:"+_levelData.level);
//		foreach (var item in _levelData.enemiesId)
//						Debug.Log (item);
//		foreach (var item in _levelData.bossIndices) {
//			Debug.Log(item);
//				}
		}

		public void LoadFromPlayerPrefs ()
		{

				int iUnlock = PlayerPrefs.GetInt (_uniqueIdentifer);
				if (iUnlock > 0) {
						_unlocked = true;
				}
		}

		public void Load ()
		{
				_uniqueIdentifer = string.Format ("level_{0:000}", index);
				LoadFromJson ();
				LoadFromPlayerPrefs ();
		}

		public void Save ()
		{
				PlayerPrefs.SetInt (_uniqueIdentifer, _unlocked ? 1 : 0);
		}

}
