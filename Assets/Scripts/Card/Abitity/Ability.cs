using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Ability {
	#region Instance members
	private AbilityBase _baseAbility;
	private int _mana;
	private Dictionary<string,int> _variableValues;
	private int _level;
//	private int _interval,_duration;

	private AbilityCast _abilityCast;
//	private EffectCard _effectCard;
#endregion

	#region Properties
	public string name
	{
		get{return _baseAbility.name;}
	}
	public int mana
	{
		get{return _mana;}
	}
	public string description
	{
		get{return _baseAbility.description;}
	}
	public TargetType targetType
	{
		get{return _baseAbility.targetType;}
	}
	public TargetArea targetArea
	{
		get{return _baseAbility.targetArea;}
	}
	public int level
	{
		get{return _level;}
		set{
			if(value<1||value>maxLevel)
			{
				Debug.Log(string.Format("Value illegal when set level of ability '{0}'",name));
				throw new System.ArgumentException(string.Format("Value illegal when set level of ability '{0}'",name));
			}
			_level=value;
			List<int> variableValue=_baseAbility._variableValueTable[_level-1];
			for (int i = 0; i < _baseAbility.variables.Count; i++) {
				_variableValues[_baseAbility._variables[i]]=variableValue[i];
			}
		}
	}
	public int maxLevel
	{
		get{return _baseAbility.maxLevel;}
	}
	public AbilityBase baseAbility
	{
		get{return _baseAbility;}
	}
	public AbilityType abilityType
	{
		get{return _baseAbility.abilityType;}
	}
	public string target
	{
		get{return _baseAbility.targetAttr;}
	}
//	public EffectCard effectCard
//	{
//		get{return _effectCard;}
//	}

	#endregion

	public int GetValue(string variableName)
	{
		return _variableValues[variableName];
	}

	public  Ability(AbilityBase abilityBase,int level,AbilityCast abilityCast,Dictionary<string,int> variables)
	{
		_baseAbility=abilityBase;
		_level=level;
		_abilityCast=abilityCast;
//		_effectCard=effectCard;
		_variableValues=variables;
		if(_variableValues.ContainsKey(AbilityVariable .mana))
		{
			_mana=_variableValues[AbilityVariable .mana];
		}
		else{
			_mana=0;
		}
	}
}
