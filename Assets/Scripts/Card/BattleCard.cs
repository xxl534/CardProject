using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
/// <summary>
///This kind of card will only exist on battle field .
///The modification of attribute on BattleCard will not effect relative ConcreteCard 
/// </summary>
public class BattleCard : MonoBehaviour {

	#region Instance member

	public  BattleCardShell _shell;
	protected ConcreteCard _concreteCard;
	protected CardEffected _cardEffect;

	protected Dictionary<int,DotAndHot> _DotAndHotTable;
	protected Dictionary<int,DebuffAndBuff> _debuffAndBuffTable;
	/// <summary>
	/// If a card doesn't have enough mana to cast an ability,it's 'ableToCast'= false;
	/// </summary>
	protected bool _ableToCast;
	protected int _health,
	_mana,	
	_strength,
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
	_evasion;
#endregion

	#region Properties
	public bool ableToCast
	{
		get{return _ableToCast;}
	}

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
				CardRoleDead();
			}
		}
	}
	public int mana
	{
		get{return _mana;}
		set{_mana=value;
			foreach (var item in _concreteCard.abilities) {
				if(_mana>=item.mana)
				{
					_ableToCast=true;
					return;
				}
			}
		}
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

	public ConcreteCard concreteCard
	{
		get{return _concreteCard;}
	}
#endregion

	public void LoadConcreteCard(ConcreteCard concreteCard)
	{
		_concreteCard = concreteCard;
		_ableToCast = false;
		_strength = concreteCard.strength;
		_agility = concreteCard.agility;
		_magic = concreteCard.magic;
		_maxHealth = concreteCard.maxHealth;
		_maxMana = concreteCard.maxMana;
		_health = _maxHealth;
		_mana = 0;
		_physicalDefense = concreteCard.physicalDefense;
		_magicalDefense = concreteCard.magicalDefense;
		_physicalCriticalChance = concreteCard.physicalCriticalChance;
		_magicalCriticalChance = concreteCard.magicalCriticalChance;
		_physicalDamage = concreteCard.physicalDamage;
		_magicalDamage = concreteCard.magicalDamage;
		_healthResilience = concreteCard.healthResilience;
		_magicResilience = concreteCard.magicResilience;
		_evasion = concreteCard.evasion;
	}

	void Awake()
	{
		_DotAndHotTable = new Dictionary<int, DotAndHot> ();
		_debuffAndBuffTable = new Dictionary<int, DebuffAndBuff> ();
		_cardEffect = CardEffectedStatic.CardEffected_Normal;
	}
	public void Clear()
	{
		FieldInfo[] fields = typeof(BattleCard).GetFields (BindingFlags.NonPublic | BindingFlags.Instance);
		foreach (var item in fields) {
			if(item.FieldType==typeof(int))
			{
				item.SetValue(this,0);
			}
		}
		_debuffAndBuffTable.Clear ();
		_DotAndHotTable.Clear ();
		}
	void CardRoleDead()
	{
		Clear ();
		_shell.CardRoleDead ();
	}

	public void AddDotOrHot(DotAndHot dotOrHot)
	{
		_DotAndHotTable[dotOrHot.id]=dotOrHot;
	}

	public void AddDebuffOrBuff(DebuffAndBuff debuffOrBuff)
	{
		_debuffAndBuffTable [debuffOrBuff.id] = debuffOrBuff;
	}
}
