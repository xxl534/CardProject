using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System;
/// <summary>
/// A concrete card based on an particular role with specilized level and rarity.
/// This card are used as battle levels' enemies or player storage which is used for battle. 
/// </summary>
public class ConcreteCard : MonoBehaviour {
	private BaseCard _baseCard;
	private int 	_strength,
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
	_evasion,
	_drapRate,
	_price,
	_maxLevel,
	/// <summary>
	/// Level should be positive
	/// </summary>
	_level,
	_experience;
	private CardRarity _cardRarity;
	private List<Ability> _abilities;

	#region Properties
	public string name
	{
		get{return _baseCard.name;}
	}

	public string description
	{
		get{return _baseCard.description;}
	}
	/// <summary>
	/// Max health,physical damage,physical defense and health resilience are changed along with strength
	/// </summary>
	/// <value>The strength.</value>
	public int strength
	{
		get{return _strength;}
		set{
			_strength=value;
			_maxHealth=_baseCard.maxHealthBase+(int)(_strength*_baseCard.rate_strength_maxHealth);
			_physicalDamage=_baseCard.physicalDamageBase+(int)(_strength*_baseCard.rate_strength_physicalDamage);
			_physicalDefense=_baseCard.physicalDefenseBase+(int)(_strength*_baseCard.rate_strength_physicalDefense);
			_healthResilience=_baseCard.healthResilienceBase+(int)(_strength*_baseCard.rate_strength_healthResilience);
		}
	}

	/// <summary>
	/// Physical defense ,physical critical chance and evasion are changed along with agility
	/// </summary>
	/// <value>The agility.</value>
	public int agility
	{
		get{return _agility;}
		set{
			_agility=value;
			_physicalDefense=_baseCard.physicalDefenseBase+(int)(_agility*_baseCard.rate_agility_physicalDefense);
			_physicalCriticalChance=_baseCard.physicalCriticalChanceBase+(int)(_agility*_baseCard.rate_agility_physicalCriticalChance);
			_evasion=_baseCard.evasionBase+(int)(_agility*_baseCard.rate_agility_evasion);
		}
	}

/// <summary>
/// Max mana,magical defense,magical critical chance,magical damage and magic resiliense are changed along with magic
/// </summary>
/// <value>The magic.</value>
	public int magic
	{
		get{return _magic;}
		set{
			_magic=value;
			_maxMana=_baseCard.maxManaBase+(int)(_magic*_baseCard.rate_magic_maxMana);
			_magicalDefense=_baseCard.magicalDefenseBase+(int)(_magic*_baseCard.rate_magic_magicalDefense);
			_magicalCriticalChance=_baseCard.magicalCriticalChanceBase+(int)(_magic*_baseCard.rate_magic_magicalCriticalChance);
			_magicalDamage=_baseCard.magicalDamageBase+(int)(_magic*_baseCard.rate_magic_magicalDamage);
			_magicResilience=_baseCard.magicResilienceBase+(int)(_magic*_baseCard.rate_magic_magicResilience);
		}
	}

	public int physicalDamage
	{
		get{return _physicalDamage;}
//		set{_physicalDamage=value;}
	}

	public int physicalCriticalChance
	{
		get{return _physicalCriticalChance;}
//		set{_physicalCriticalChance=value;}
	}

	public int physicalDefense
	{
		get{return _physicalDefense;}
//		set{_physicalDefense=value;}
	}

	/// <summary>
	/// Strength,agility and magic are changed along with level
	/// </summary>
	/// <value>The level.</value>
	public int level{
		get{return _level;}
		set{
			_level=value;
			strength = _baseCard.strengthBase +(int)(_level * _baseCard.strengthGrowth);
			agility = _baseCard.agilityBase + (int)(_level * _baseCard.agilityGrowth);
			magic = _baseCard.magicBase + (int)(_level * _baseCard.magicGrowth);
		}
	}
#endregion

	public ConcreteCard()
	{
		
	}

	public ConcreteCard(BaseCard baseCard,CardRarity rarity,int level,List<Ability> abilities)
	{
		if (baseCard == null) {
						throw new System.ArgumentNullException ("baseCard");
				}
		_baseCard = baseCard;

		if (rarity > _baseCard.cardRarity||rarity<0) {
			Debug.Log("The rarity of concrete card should not be less than 0 nor be larger than base card's rarity.");
			throw new System.ArgumentException("rarity");
				}
		_cardRarity = rarity;
		_maxLevel = _baseCard.maxLevelViaRarityTable [(int)_cardRarity];

		if (level > _maxLevel || level < 1) {
			Debug.Log("The level of card should not be less than 1 nor be larger than card's max level.");
			throw new System.ArgumentException("level");
				}
		this.level = level;

		if (abilities == null) {
			throw new System.ArgumentNullException ("abilities");
				}
		_abilities = abilities;
	}

}
