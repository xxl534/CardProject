using UnityEngine;
using System.Collections;

public class BaseCard : MonoBehaviour {
	protected int _maxHealth,
	_maxMana,
	_agility,
	_defense,
	_damage,
	_drapRate,
	_healthResilience,
	_magicResilience,
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
