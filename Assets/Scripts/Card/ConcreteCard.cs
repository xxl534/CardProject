using UnityEngine;
using System.Collections;

public class ConcreteCard : BaseCard {
//	protected int 	_strength,
//	_agility,
//	_magic,
//	_maxHealth,
//	_maxMana,
//	_physicalDefense,
//	_magicalDefense,
//	_physicalCriticalChance,
//	_magicalCriticalChance,
//	_physicalDamage,
//	_magicalDamage,
//	_healthResilience,
//	_magicResilience,
//	_drapRate,
//	_price;
	public int physicalDamage
	{
		get{return _physicalDamage;}
		set{_physicalDamage=value;}
	}

	public int physicalCriticalChance
	{
		get{return _physicalCriticalChance;}
		set{_physicalCriticalChance=value;}
	}

	public int physicalDefense
	{
		get{return _physicalDefense;}
		set{_physicalDefense=value;}
	}
}
