using UnityEngine;
using System.Collections;

public class BaseCard : MonoBehaviour {
	protected int 	_strength,
	_agility,
	_magic,
	_maxHealth,
	_maxMana,
	_physicalDefense,
	_magicalDefense,
	_physicalCriticalChance,
	_magicalCriticalChance,
	_physicalDamage,
	_magicalDamage,
	_healthResilience,
	_magicResilience,
	_drapRate,
	_price;
	public string _name;
	public string _description;
	protected	BaseAbility _normalAttack;
	protected	BaseAbility _baseAbility;
	protected	BaseAbility _AdvancedAbility;
	protected   BaseAbility _normalPassive;
	protected	BaseAbility _advancedPassive;


	protected BaseCard()
	{

	}
	protected BaseCard(BaseCard baseCard)
	{

	}


}
