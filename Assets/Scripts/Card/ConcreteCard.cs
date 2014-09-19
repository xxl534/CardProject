using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
/// <summary>
/// A concrete card based on an particular role with specilized level and rarity.
/// This card are used as battle levels' enemies or player storage which is used for battle. 
/// </summary>
public class ConcreteCard {

	#region Instance member
	protected BaseCard _baseCard;
	protected Texture _cardSprite;
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
	_price,
	_maxLevel,
	/// <summary>
	/// Level should be positive
	/// </summary>
	_level,
	_experience;

	/// <summary>
	/// Active abilities
	/// </summary>
	protected List<Ability> _abilities;
#endregion

	#region Properties
	public int id
	{
		get{return _baseCard.id;}
	}
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
			_maxHealth=_baseCard.maxHealthBase+(int)(_strength*BaseCard.rate_strength_maxHealth);
			_physicalDamage=_baseCard.physicalDamageBase+(int)(_strength*BaseCard.rate_strength_physicalDamage);
			_physicalDefense=_baseCard.physicalDefenseBase+(int)(_strength*BaseCard.rate_strength_physicalDefense);
			_healthResilience=_baseCard.healthResilienceBase+(int)(_strength*BaseCard.rate_strength_healthResilience);
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
			_physicalDefense=_baseCard.physicalDefenseBase+(int)(_agility*BaseCard.rate_agility_physicalDefense);
			_physicalCriticalChance=_baseCard.physicalCriticalChanceBase+(int)(_agility*BaseCard.rate_agility_physicalCriticalChance);
			_evasion=_baseCard.evasionBase+(int)(_agility*BaseCard.rate_agility_evasion);
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
			_maxMana=_baseCard.maxManaBase+(int)(_magic*BaseCard.rate_magic_maxMana);
			_magicalDefense=_baseCard.magicalDefenseBase+(int)(_magic*BaseCard.rate_magic_magicalDefense);
			_magicalCriticalChance=_baseCard.magicalCriticalChanceBase+(int)(_magic*BaseCard.rate_magic_magicalCriticalChance);
			_magicalDamage=_baseCard.magicalDamageBase+(int)(_magic*BaseCard.rate_magic_magicalDamage);
			_magicResilience=_baseCard.magicResilienceBase+(int)(_magic*BaseCard.rate_magic_magicResilience);
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
	public float dropRate
	{
		get{return _baseCard.drapRate;}
	}
	public int price
	{
		get{return _price;}
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
	public Rarity rarity
	{
		get{return _baseCard.rarity;}
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
			while(newLevel<maxLevel&&_experience>BaseCard._experienceTable[newLevel-1])
			{
				_experience-=BaseCard._experienceTable[newLevel-1];
				newLevel++;
			}
			if(_experience>BaseCard._experienceTable[newLevel-1])
			{//When experience exceed the max,it equals the max
				_experience=BaseCard._experienceTable[newLevel-1];
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
	public List<Ability> abilities
	{
		get{return _abilities;}
	}
	public Texture roleTexture
	{
		get{return _cardSprite;}
	}
#endregion

	public ConcreteCard()
	{

	}

	public ConcreteCard(BaseCard baseCard,int level,List<Ability> abilities,Texture cardSprite)
	{
		if (baseCard == null) {
						throw new System.ArgumentNullException ("baseCard");
				}
		_baseCard = baseCard;


		_maxLevel = BaseCard._maxLevelTable [(int)rarity];
	

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

		_cardSprite=cardSprite;
	
	}


}
