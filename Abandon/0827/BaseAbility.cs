using UnityEngine;
using System.Collections;


public abstract class BaseAbility : MonoBehaviour {
	public AbilityType _abilityType;
	public TargetType _targetType;
	public string _name;
	public string _description;
	public string _targetAttr;
	public object _Value;

//public 	IAbilityEffect _abilityEffect;
	public abstract void CastAbility(RealCard card);
//
//	public virtual 
}
