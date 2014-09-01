using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LevelData {
	private int _level,_experience,_enemiesAbilityLevel;
	private List<int> _enemiesId,_boss;
	private List<Rarity> _enemiesRarity;
	public int level
	{
		get{return _level;}
	}
	public int experience
	{
		get{return _experience;}
	}
	public int enemiesAbilityLevel
	{
		get{return _enemiesAbilityLevel;}
	}
	public List<int> enemiesId
	{
		get{return _enemiesId;}
	}
	public List<int> boss
	{
		get{return _boss;}
	}
	public List<Rarity> enemiesRarity
	{
		get{return _enemiesRarity;}
	}
	private LevelData()
	{}

	public static LevelData GetLevelData(Dictionary<string,object> data)
	{
		LevelData levelData=new LevelData();
		return levelData;
	}
}
