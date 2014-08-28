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
public class ConcreteCard :MonoBehaviour{

	#region Instance member
	protected BaseCard _baseCard;
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
	_evasion,
	_drapRate,
	_price,
	_maxLevel,
	/// <summary>
	/// Level should be positive
	/// </summary>
	_level,
	_experience;
	private Rarity _cardRarity;
	protected List<Ability> _abilities;
#endregion

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

	public int maxHealth
	{
		get{return  _maxHealth;}
	}
	public int maxMana
	{
		get{return _maxMana;}
	}
	public int magicalDefense
	{
		get{return _magicalDefense;}
	}
	public int magicalCriticalChance
	{
		get{return _magicalCriticalChance;}
	}
	public int magicalDamage
	{
		get{return _magicalDamage;}
	}
	public int healthResilience
	{
		get{return _healthResilience;}
	}
	public int magicResilience
	{
		get{return _magicResilience;}
	}
	public int evasion
	{
		get{return _evasion;}
	}
	public int drapRate
	{
		get{return _drapRate;}
	}
	public int price
	{
		get{return price;}
	}
	public int maxLevel
	{
		get{return _maxLevel;}
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
	/// The increase of experience may affect level value.
	/// </summary>
	/// <value>The experience.</value>
	public int experience
	{
		get {return _experience;}
		set{
			if(value<_experience)
			{
				Debug.Log("Decrease of experience is forbidden");
				throw new System.ArgumentException("experience");
			}
			_experience=value;
			int newLevel=level;
			while(newLevel<maxLevel&&_experience>baseCard.experienceTable[newLevel-1])
			{
				_experience-=baseCard.experienceTable[newLevel-1];
				newLevel++;
			}
			if(_experience>baseCard.experienceTable[newLevel-1])
			{//When experience exceed the max,it equals the max
				_experience=baseCard.experienceTable[newLevel-1];
			}
			level=newLevel;
		}
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
	public BaseCard baseCard
	{
		get{return _baseCard;}
	}
#endregion

	public ConcreteCard()
	{

	}

	public ConcreteCard(BaseCard baseCard,Rarity rarity,int level,List<Ability> abilities)
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

		_price=baseCard.price;
	}


}