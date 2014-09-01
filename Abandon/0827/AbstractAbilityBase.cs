using UnityEngine;
using System.Collections;


public abstract class AbstractAbilityBase : MonoBehaviour {
	public string _name;
	public string _description;
	public string _targetAttr;
	public int _Value;
//public 	IAbilityEffect _abilityEffect;
	public abstract object CastAbility(ConcreteCard from,ConcreteCard to);

}
