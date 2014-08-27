using UnityEngine;
using System.Collections;

/// <summary>
/// Each base card is a data type of a role.Concrete cards which represent one role are generated from the same base card.
/// Each base card has a set of base attributes and abilities.
/// Value of concrete card's attributes are result from relative base card and card level. 
/// The value of instance member with postfix "Base" is attribute's value of concrete card with level 0.
/// </summary>
public class BaseCard : MonoBehaviour {
	#region Static member
	private static  float _rate_strength_maxHealth,_rate_strength_physicalDefense,_rate_strength_physicalDamage,_rate_strength_healthResilience,
	_rate_agility_physicalDefense,_rate_agility_physicalCriticalChance,_rate_agility_evasion,
	_rate_magic_maxMana,_rate_magic_magicalDefense,_rate_magic_magicalCriticalChance,_rate_magic_magicalDamage,_rate_magic_magicResilience;

	private static int[] _maxLevelViaRarityTable;
#endregion

	#region Instance member
	private int 	_strengthBase;private float _strengthGrowth;
	private int  _agilityBase;private float _agilityGrowth;
	private int _magicBase;private float _magicGrowth;
	private int _maxHealthBase,
	_maxManaBase,
	_physicalDefenseBase,
		_magicalDefenseBase,
		_physicalCriticalChanceBase,
	_magicalCriticalChanceBase,
	_physicalDamageBase,
	_magicalDamageBase,
	_healthResilienceBase,
	_magicResilienceBase,
	_evasionBase,
	_drapRate,
	_price;
	private CardRarity _cardRarity;
	private string _name;
	private string _description;
	private int[] _abilitiesId;
#endregion

	#region Properties static
	//static properties
	public float rate_strength_maxHealth
	{
		get{return _rate_strength_maxHealth;}
	}
	public float rate_strength_physicalDefense
	{
		get{return _rate_strength_physicalDefense;}
	}
	public float rate_strength_physicalDamage
	{
		get{return _rate_strength_physicalDamage;}
	}
	public float rate_strength_healthResilience
	{
		get{return _rate_strength_healthResilience;}
	}
	public float rate_agility_physicalDefense
	{
		get{return _rate_agility_physicalDefense;}
	}
	public float rate_agility_physicalCriticalChance
	{
		get{return _rate_agility_physicalCriticalChance;}
	}
	public float rate_agility_evasion
	{
		get{return _rate_agility_evasion;}
	}
	public float rate_magic_maxMana
	{
		get{return _rate_magic_maxMana;}
	}
	public float rate_magic_magicalDefense
	{
		get{return _rate_magic_magicalDefense;}
	}
	public float rate_magic_magicalCriticalChance
	{
		get{return _rate_magic_magicalCriticalChance;}
	}
	public float rate_magic_magicalDamage
	{
		get{return _rate_magic_magicalDamage;}
	}
	public float rate_magic_magicResilience
	{
		get{return _rate_magic_magicResilience;}
	}
	public int[] maxLevelViaRarityTable
	{
		get{return _maxLevelViaRarityTable;}
	}
#endregion

	#region Properties instance member
	//member properties
	public int 	strengthBase
	{
		get{return _strengthBase;}
	}
	public float strengthGrowth
	{
		get{return _strengthGrowth;}
	}
	public int  agilityBase
	{
		get{return _agilityBase;}
	}
	public float agilityGrowth
	{
		get{return _agilityGrowth;}
	}
	public int magicBase
	{
		get{return _magicBase;}
	}
	public float magicGrowth
	{
		get{return _magicGrowth;}
	}
	public int maxHealthBase
	{
		get{return _maxHealthBase;}
	}
	public int maxManaBase
	{
		get{return _maxManaBase;}
	}
	public int physicalDefenseBase
	{
		get{return _physicalDefenseBase;}
	}
	public int magicalDefenseBase
	{
		get{return _magicalDefenseBase;}
	}
	public int physicalCriticalChanceBase
	{
		get{return _physicalCriticalChanceBase;}
	}
	public int magicalCriticalChanceBase
	{
		get{return _magicalCriticalChanceBase;}
	}
	public int physicalDamageBase
	{
		get{return _physicalDamageBase;}
	}
	public int magicalDamageBase
	{
		get{return _magicalDamageBase;}
	}
	public int healthResilienceBase
	{
		get{return _healthResilienceBase;}
	}
	public int magicResilienceBase
	{
		get{return _magicResilienceBase;}
	}
	public int evasionBase
	{
		get{return _evasionBase;}
	}
	public int drapRate
	{
		get{return _drapRate;}
	}
	public int price
	{
		get{return _price;}
	}
	public CardRarity cardRarity
	{
		get{return _cardRarity;}
	}
	public string name
	{
		get{return _name;}
	}
	public string description
	{
		get{return _description;}
	}
	public int[] abilitiesId
	{
		get{return _abilitiesId;}
	}
#endregion
	


}
