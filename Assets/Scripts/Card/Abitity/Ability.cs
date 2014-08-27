using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Ability : MonoBehaviour {
	#region Instance members
	private AbilityBase _baseAbility;
	private Dictionary<string,int> _variableValues;
	private int _interval,_duration;

	private AbilityCast _abilityCast;
	private CardAttacked _cardAttacked;
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

	#endregion

	public int GetValue(string variableName)
	{
		return _variableValues[variableName];
	}
}
