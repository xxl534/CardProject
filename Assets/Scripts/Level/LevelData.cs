using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
public class LevelData {
	private int _level,_experience,_enemiesAbilityLevel;
	private List<int> _enemiesId,_bossIndices,_dropCards,_dropRates;

	private Rarity _enemiesRarity;
	private string _background;
	public int level
	{
		get{return _level;}
		private set{
			_level=value;
		}
	}
	public int experience
	{
		get{return _experience;}
		private set{_experience=value;}
	}
	public int enemiesAbilityLevel
	{
		get{return _enemiesAbilityLevel;}
		private set{_enemiesAbilityLevel=value;}
	}
	public string background
	{
		get{return _background;}
		private set{_background=value;}
	}
	public List<int> enemiesId
	{
		get{return _enemiesId;}
	}
	private List<object> enemies
	{
		set{_enemiesId=value.ConvertAll(x=>System.Convert.ToInt32(x));}
	}
	public List<int> dropCards
	{
		get{return _dropCards;}
	}
	private List<object> dropCard
	{
		set{_dropCards=value.ConvertAll(x=>System.Convert.ToInt32(x));}
	}
	public List<int> dropRates
	{
		get{return _dropRates;}
	}
	private List<object> dropRate
	{
		set{_dropRates=value.ConvertAll(x=>System.Convert.ToInt32(x));}
	}
	public List<int> bossIndices
	{
		get{return _bossIndices;}
	}
	private List<object> bosses
	{
		set{_bossIndices=value.ConvertAll(x=>System.Convert.ToInt32(x));}
	}
	public Rarity enemiesRarity
	{
		get{return _enemiesRarity;}
		private set{_enemiesRarity=value;}
	}
	private LevelData()
	{}

	public static LevelData GetLevelData(Dictionary<string,object> data)
	{
		LevelData levelData=new LevelData();
		Type type=typeof(LevelData);
		foreach(string attribute in data.Keys)
		{
			PropertyInfo propertyInfo= type.GetProperty(attribute,BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance);
			if(propertyInfo==null)
			{
				Debug.Log(string.Format("Illegal property in Level : '{0}'",attribute));
				throw new System.Exception(string.Format("Illegal property in Level : '{0}'",attribute));
			}
			object valueOb=data[attribute];
			if(propertyInfo.PropertyType.IsEnum)
			{
				try{
					System.Convert.ToInt32(valueOb);
				}catch{
					valueOb=	Enum.Parse(propertyInfo.PropertyType,(string)valueOb);
				}
				if(!Enum.IsDefined(propertyInfo.PropertyType,valueOb))
				{
					Debug.Log(string.Format("Illegal {0} value :{1}" ,propertyInfo.PropertyType,valueOb));
					throw new System.ArgumentException(string.Format("Illegal {0} value :{1}" ,propertyInfo.PropertyType,valueOb));
				}
			}
			if(valueOb.GetType()!=propertyInfo.PropertyType)
			{
				valueOb=System.Convert.ChangeType(valueOb,propertyInfo.PropertyType);
			}
			propertyInfo.SetValue(levelData,valueOb,null);
		}
		if (!levelData.CheckFields ()) {
			Debug.Log("Failing to pass fields test");
			throw new System.Exception("Failing to pass fields test");
				}
		return levelData;
	}

	bool CheckFields()
	{
		bool bPass = true;
		if (_level <= 0) {
			Debug.Log("Level should be positive");
			bPass=false;
				}
		if (_experience <= 0) {
			Debug.Log("Experience should be positive");
			bPass=false;
				}
		if (_enemiesId == null) {
			Debug.Log("EnemiesId list should not be null");
			bPass=false;
				}
		if (_bossIndices == null) {
						Debug.Log ("BossIndices list should not be null");
						bPass = false;
				}
		return bPass;
	}
}
