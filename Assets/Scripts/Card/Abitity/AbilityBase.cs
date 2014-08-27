using UnityEngine;
using System.Collections;

/// <summary>
/// Description of how an ability entity effect the target card
/// </summary>
public delegate void AbilityEffect(BattleCard from,AbilityEntity abilityEntity);


//public delegate Ability  AbilityLevelUpdater(Ability ability,int level);
public  class AbilityBase : MonoBehaviour {
	public AbilityType _abilityType;
	public string _name;
	public string _description;
	public string _targetAttr;
	public string _EffectOrBuff;
//	public int _interval;
//	public int _duration;
	public int[] _value;
	public AbilityEffect _abilityEffect;
	public CardAttacked _cardAttacked;

}
