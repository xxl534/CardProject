using UnityEngine;
using System.Collections;

/// <summary>
///This kind of card will only exist on battle field .
///The modification of attribute on BattleCard will not effect relative ConcreteCard 
/// </summary>
public class BattleCard : ConcreteCard {

	#region Instance member
	protected ConcreteCard _concreteCard;
	protected CardEffected _cardEffect;
	protected int _health;
	protected int _mana;
#endregion

	#region Properties
	/// <summary>
	/// If health less equal than zero ,this battle card will be dead and be moved from battle field.
	/// </summary>
	/// <value>The health.</value>
	public int health{
		get {return _health;}
		set{
			_health=value;
			if(_health<=0)
			{
				CardDead();
			}
		}
	}
	public int mana
	{
		get{return _mana;}
		set{_mana=value;}
	}
	public int maxHealth
	{
		get{return  _maxHealth;}
		set{_maxHealth=value;}
	}
	public int maxMana
	{
		get{return _maxMana;}
		set{_maxMana=value;}
	}
	public int magicalDefense
	{
		get{return _magicalDefense;}
		set{_magicalDefense=value;}
	}
	public int magicalCriticalChance
	{
		get{return _magicalCriticalChance;}
		set{_magicalCriticalChance=value;}
	}
	public int magicalDamage
	{
		get{return _magicalDamage;}
		set{_magicalDamage=value;}
	}
	public int healthResilience
	{
		get{return _healthResilience;}
		set{_healthResilience=value;}
	}
	public int magicResilience
	{
		get{return _magicResilience;}
		set{_magicResilience=value;}
	}
	public int evasion
	{
		get{return _evasion;}
		set{_evasion=value;}
	}
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

	//The implement, by accumulation, of 3 properties below are different from ConcreteCard. 
	//The reason is for attribute value restoration  mechanism in battle

	/// <summary>
	/// Max health,physical damage,physical defense and health resilience are changed along with strength
	/// </summary>
	/// <value>The strength.</value>
	public int strength
	{
		get{return _strength;}
		set{
			int valueDelta=value-_strength;
			_strength=value;
			_maxHealth+=(int)(valueDelta*BaseCard.rate_strength_maxHealth);
			_physicalDamage+=(int)(valueDelta*BaseCard.rate_strength_physicalDamage);
			_physicalDefense+=(int)(valueDelta*BaseCard.rate_strength_physicalDefense);
			_healthResilience+=(int)(valueDelta*BaseCard.rate_strength_healthResilience);
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
			int valueDelta=value-_agility;
			_agility=value;
			_physicalDefense+=(int)(valueDelta*BaseCard.rate_agility_physicalDefense);
			_physicalCriticalChance+=(int)(valueDelta*BaseCard.rate_agility_physicalCriticalChance);
			_evasion+=(int)(valueDelta*BaseCard.rate_agility_evasion);
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
			int valueDelta=value-_magic;
			_magic=value;
			_maxMana+=(int)(valueDelta*BaseCard.rate_magic_maxMana);
			_magicalDefense+=(int)(valueDelta*BaseCard.rate_magic_magicalDefense);
			_magicalCriticalChance+=(int)(valueDelta*BaseCard.rate_magic_magicalCriticalChance);
			_magicalDamage+=(int)(valueDelta*BaseCard.rate_magic_magicalDamage);
			_magicResilience+=(int)(valueDelta*BaseCard.rate_magic_magicResilience);
		}
	}
#endregion

	public BattleCard(ConcreteCard concreteCard)
	{

	}


	void CardDead()
	{

	}
}
