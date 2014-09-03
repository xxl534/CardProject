using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum StarNum{
	ZERO=0,
	ONE,TWO,TRHEE
};

public class LevelInfo:MonoBehaviour {
	//Level name
	string _name;
	
	StarNum _starNum;
	bool _unlocked;

	  int _index;

	private LevelData _levelData;

	public LevelData leveldata
	{
		get{return _levelData;}
	}

	public bool unlocked
	{
		get{return _unlocked;}
	}

//	void Awake()
//	{
//		LoadFromJson ();
//	}
	/// <summary>
	/// Initializes a new instance of the <see cref="Level_editMode"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	public void Init(string name)
	{
		_name = name;
		_starNum = StarNum.ZERO;
		_unlocked = false;
	}
	public  string levelName{
		get{
			return _name;
		}
		set{
			_name=value;
		}
	}

	public void Unlock()
	{
		_unlocked = true;
	}

	public void GainStar(StarNum starNum)
	{
		if (starNum > _starNum)
						_starNum = starNum;
	}

	public int GetLevelIndex()
	{
		return _index;
	}

	public void SetLevelIndex(int index)
	{
		_index = index;
	}

	public int GetStarNum()
	{
		return (int)_starNum;
	}

	public void LoadFromJson()
	{
		TextAsset text = Resources.Load <TextAsset>(string.Format("{0}/level_{1:000}",ResourcesFolderPath.json_level ,_index+1));
		string json = text.text;
		Dictionary<string ,object> dict = MiniJSON.Json.Deserialize (json)as Dictionary<string,object>;
		_levelData=LevelData.GetLevelData(dict["level"]as Dictionary<string,object>);
		Debug.Log ("experience:"+_levelData.experience);
		Debug.Log ("level:"+_levelData.level);
		foreach (var item in _levelData.enemiesId)
						Debug.Log (item);
		foreach (var item in _levelData.bossIndices) {
			Debug.Log(item);
				}
	}

	public void LoadFromPlayerPrefs()
	{}
}
