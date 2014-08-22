using UnityEngine;
using System.Collections;

public class RealAbility : MonoBehaviour,ICastAbility{
	/// <summary>
	///array about experience mount needed for every level to upgrade. 
	/// </summary>
	protected static int[] experienceSheet;

	/// <summary>
	/// Base ability information for "this" to levelup.
	/// </summary>
	protected BaseAbility _baseAbility;
	protected int _maxLevel;
	protected int _experience;
	protected int _level;
	protected int _value;
	protected ILevelUpgrader _levelUpgrader;
	public  void ExperienceIncrease(int quantity)
	{
		_experience += quantity;
		while (_level<_maxLevel&&_experience>experienceSheet[_level-1]) {
			_experience-=experienceSheet[_level-1];
			_levelUpgrader.Levelup(this);
				}
		if (_experience > experienceSheet [_level - 1]) {
			_experience=experienceSheet[_level-1];
				}
	}

	public virtual ICastAbility CastAbilityTo(BattleCard from,BattleCard to,RealAbility b)
	{
		_baseAbility._abilityEffect.AbilityEffect (from, to, this);
		return null;
	}
}
