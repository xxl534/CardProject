using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelInfo:MonoBehaviour {
	//Level name
	LevelGateControl _gateControl;
	string _name;
	string _uniqueIdentifer;
	bool _unlocked;

	private LevelData _levelData;

	public LevelData leveldata
	{
		get{return _levelData;}
	}

	public bool unlocked
	{
		get{return _unlocked;}
	}

	public int index
	{
		get{return _gateControl.levelIndex;}
	}

	void Awake()
	{
		_gateControl = GetComponent<LevelGateControl> ();
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="Level_editMode"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
//	public  string levelName{
//		get{
//			return _name;
//		}
//		set{
//			_name=value;
//		}
//	}

	public void Unlock()
	{
		_unlocked = true;
	}

//	public void GainStar(StarNum starNum)
//	{
//		if (starNum > _starNum)
//						_starNum = starNum;
//	}

//	public int GetLevelIndex()
//	{
//		return _index;
//	}

//	public void SetLevelIndex(int index)
//	{
//		_index = index;
//	}

	public void LoadFromJson()
	{
		TextAsset text = Resources.Load <TextAsset>(string.Format("{0}/level_{1:000}",ResourcesFolderPath.json_level ,index));
		if (text == null)
						return;
		string json = text.text;
		Dictionary<string ,object> dict = MiniJSON.Json.Deserialize (json)as Dictionary<string,object>;
		_levelData=LevelData.GetLevelData(dict["level"]as Dictionary<string,object>);
//		Debug.Log ("experience:"+_levelData.experience);
//		Debug.Log ("level:"+_levelData.level);
//		foreach (var item in _levelData.enemiesId)
//						Debug.Log (item);
//		foreach (var item in _levelData.bossIndices) {
//			Debug.Log(item);
//				}
	}

	public void LoadFromPlayerPrefs()
	{
		 int iUnlock=	PlayerPrefs.GetInt(_uniqueIdentifer);
		_unlocked = iUnlock > 0;
	}

	public void Load()
	{
			_uniqueIdentifer = string.Format ("level_{0:000}", index);
		LoadFromJson ();
		LoadFromPlayerPrefs ();
	}
	public void Save()
	{
		PlayerPrefs.SetInt (_uniqueIdentifer, _unlocked ? 1 : 0);
	}

}
