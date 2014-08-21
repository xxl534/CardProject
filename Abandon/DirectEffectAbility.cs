using UnityEngine;
using System.Collections;


public class DirectEffectAbility : AbilityBase {
	protected static int[] experiencePerLevel;

	protected int _maxLevel;
	public int maxLevel
	{
		get{return _maxLevel;}
		set{_maxLevel=value;}
	}

	protected int _experience;
//	protected int _maxExperienceViaLevel;
	protected int _level;

	protected object _pureValue;

	public DirectEffectAbility (AbilityBase baseAbility):base(baseAbility)
	{
		_maxLevel = 1;
		_experience = 0;
		_level = 1;
		_pureValue = base.baseValue;
		}

	void ExperienceIncrease(int amount)
	{
		_experience += amount;
		while (_level<_maxLevel&&_experience>=experiencePerLevel[_level-1]) {
			_experience-=experiencePerLevel[_level-1];
			LevelIncrease();
				}
		if (_experience > experiencePerLevel [_level - 1])
						_experience = experiencePerLevel [_level - 1];

	}

	void LevelIncrease()
	{

	}
//	public void attack(Card card)
//	{
//
//		CastAbility (card, targetAttr, baseValue, relative,proportional);
//	}

	public static void SetExperiencePerLevel(int[] experienceArray)
	{
		experiencePerLevel = experienceArray;
//		maxLevel = experienceArray.Length;
	}
}
