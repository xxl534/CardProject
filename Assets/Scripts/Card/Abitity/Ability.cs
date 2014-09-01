using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Ability : MonoBehaviour {
	#region Instance members
	private AbilityBase _baseAbility;
	private Dictionary<string,int> _variableValues;
	private int _level;
//	private int _interval,_duration;

	private AbilityCast _abilityCast;
	private EffectCard _effectCard;
#endregion

	#region Properties
	public string name
	{
		get{return _baseAbility.name;}
	}
	public string description
	{
		get{return _baseAbility.description;}
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
			List<int> variableValue=_baseAbility.variableValueTable[_level-1];
			for (int i = 0; i < _baseAbility.variables.Count; i++) {
				_variableValues[_baseAbility.variables[i]]=variableValue[i];
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
	public EffectCard effectCard
	{
		get{return _effectCard;}
	}

	#endregion

	public int GetValue(string variableName)
	{
		return _variableValues[variableName];
	}

	public  Ability(AbilityBase abilityBase,int level,AbilityCast abilityCast,EffectCard effectCard,Dictionary<string,int> variables)
	{
		_baseAbility=abilityBase;
		_level=level;
		_abilityCast=abilityCast;
		_effectCard=effectCard;
		_variableValues=variables;
	}
}
