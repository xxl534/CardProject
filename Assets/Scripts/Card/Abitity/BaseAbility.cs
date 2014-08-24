﻿using UnityEngine;
using System.Collections;


public abstract class BaseAbility : MonoBehaviour {
	public string _name;
	public string _description;
	public string _targetAttr;
	public int _baseValue;
//public 	IAbilityEffect _abilityEffect;
	public abstract void CastAbility(RealCard card);

}
