using UnityEngine;
using System.Collections;

public delegate void AbilityEffect(ConcreteCard from,ConcreteCard to,AbilityBase ability); 
public  class AbilityBase : MonoBehaviour {
	public AbilityType _abilityType;
	public string _name;
	public string _description;
	public string _targetAttr;
	public string _EffectOrBuff;
	public int _interval;
	public int _duration;
	public object _value;
	public AbilityEffect _abilityEffect;


}
