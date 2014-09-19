using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

/// <summary>
/// Each base card is a data type of a role.Concrete cards which represent one role are generated from the same base card.
/// Each base card has a set of base attributes and abilities.
/// Value of concrete card's attributes are result from relative base card and card level. 
/// The value of instance member with postfix "Base" is attribute's value of concrete card with level 0.
/// </summary>
public class BaseCard {
	#region Static member
	private static  float _rate_strength_maxHealth,_rate_strength_physicalDefense,_rate_strength_physicalDamage,_rate_strength_healthResilience,
	_rate_agility_physicalDefense,_rate_agility_physicalCriticalChance,_rate_agility_evasion,
	_rate_magic_maxMana,_rate_magic_magicalDefense,_rate_magic_magicalCriticalChance,_rate_magic_magicalDamage,_rate_magic_magicResilience;

	public static List<int> _maxLevelTable;
	public static List<int> _experienceTable;
#endregion

	#region Instance member
	private int _id;
	private string _cardSprite;
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
	_price;
	private float _drapRate;
	private Rarity _cardRarity;
	private string _name;
	private string _description;

	public List<int> _abilitiesId;
#endregion

	#region Properties static member
	//static properties
	public static float rate_strength_maxHealth
	{
		get{return _rate_strength_maxHealth;}
		private set{
			if(value<0f)
			{
				Debug.Log("Static attribute rate_strength_maxHealth should not be negative");
				throw new ArgumentException("Static attribute rate_strength_maxHealth should not be negative");
			}
			_rate_strength_maxHealth=value;
		}
	}
	public static float rate_strength_physicalDefense
	{
		get{return _rate_strength_physicalDefense;}
		private set{
			if(value<0f)
			{
				Debug.Log("Static attribute rate_strength_physicalDefense should not be negative");
				throw new ArgumentException("Static attribute rate_strength_physicalDefense should not be negative");
			}
			_rate_strength_physicalDefense=value;
		}
	}
	public static float rate_strength_physicalDamage
	{
		get{return _rate_strength_physicalDamage;}
		private set{
			if(value<0f)
			{
				Debug.Log("Static attribute rate_strength_physicalDamage should not be negative");
				throw new ArgumentException("Static attribute rate_strength_physicalDamage should not be negative");
			}
			_rate_strength_physicalDamage=value;
		}
	}
	public static float rate_strength_healthResilience
	{
		get{return _rate_strength_healthResilience;}
		private set{
			if(value<0f)
			{
				Debug.Log("Static attribute rate_strength_healthResilience should not be negative");
				throw new ArgumentException("Static attribute rate_strength_healthResilience should not be negative");
			}
			_rate_strength_healthResilience=value;
		}
	}
	public static float rate_agility_physicalDefense
	{
		get{return _rate_agility_physicalDefense;}
		private set{
			if(value<0f)
			{
				Debug.Log("Static attribute rate_agility_physicalDefense should not be negative");
				throw new ArgumentException("Static attribute rate_agility_physicalDefense should not be negative");
			}
			_rate_agility_physicalDefense=value;
		}
	}
	public static float rate_agility_physicalCriticalChance
	{
		get{return _rate_agility_physicalCriticalChance;}
		private set{
			if(value<0f)
			{
				Debug.Log("Static attribute rate_agility_physicalCriticalChance should not be negative");
				throw new ArgumentException("Static attribute rate_agility_physicalCriticalChance should not be negative");
			}
			_rate_agility_physicalCriticalChance=value;
		}
	}
	public static float rate_agility_evasion
	{
		get{return _rate_agility_evasion;}
		private set{
			if(value<0f)
			{
				Debug.Log("Static attribute rate_agility_evasion should not be negative");
				throw new ArgumentException("Static attribute rate_agility_evasion should not be negative");
			}
			_rate_agility_evasion=value;
		}
	}
	public static float rate_magic_maxMana
	{
		get{return _rate_magic_maxMana;}
		private set{
			if(value<0f)
			{
				Debug.Log("Static attribute rate_magic_maxMana should not be negative");
				throw new ArgumentException("Static attribute rate_magic_maxMana should not be negative");
			}
			_rate_magic_maxMana=value;
		}
	}
	public static float rate_magic_magicalDefense
	{
		get{return _rate_magic_magicalDefense;}
		private set{
			if(value<0f)
			{
				Debug.Log("Static attribute rate_magic_magicalDefense should not be negative");
				throw new ArgumentException("Static attribute rate_magic_magicalDefense should not be negative");
			}
			_rate_magic_magicalDefense=value;
		}
	}
	public static float rate_magic_magicalCriticalChance
	{
		get{return _rate_magic_magicalCriticalChance;}
		private set{
			if(value<0f)
			{
				Debug.Log("Static attribute rate_magic_magicalCriticalChance should not be negative");
				throw new ArgumentException("Static attribute rate_magic_magicalCriticalChance should not be negative");
			}
			_rate_magic_magicalCriticalChance=value;
		}
	}
	public static float rate_magic_magicalDamage
	{
		get{return _rate_magic_magicalDamage;}
		private set{
			if(value<0f)
			{
				Debug.Log("Static attribute rate_magic_magicalDamage should not be negative");
				throw new ArgumentException("Static attribute rate_magic_magicalDamage should not be negative");
			}
			_rate_magic_magicalDamage=value;
		}
	}
	public static float rate_magic_magicResilience
	{
		get{return _rate_magic_magicResilience;}
		private set{
			if(value<0f)
			{
				Debug.Log("Static attribute rate_magic_magicResilience should not be negative");
				throw new ArgumentException("Static attribute rate_magic_magicResilience should not be negative");
			}
			_rate_magic_magicResilience=value;
		}
	}

	/// <summary>
	/// maxLevelTable has same number item as Enum Rarity does 
	/// It should be sorted
	/// </summary>
	/// <value>The max level table.</value>
	public static List<object> maxLevelTable
	{
		get{return _maxLevelTable.ConvertAll(x=>(object)x);}
		private set{
			_maxLevelTable=value.ConvertAll(x=>System.Convert.ToInt32(x));
			if(_maxLevelTable.Count!=Enum.GetValues(typeof(Rarity)).Length)
			{
				Debug.Log("maxLevelTable count error");
				throw new System.ArgumentException("maxLevelTable count error");
			}
			if(!IsIntListSorted(_maxLevelTable))
			{
				Debug.Log("maxLevelTable item sequence error");
				throw new System.ArgumentException("maxLevelTable item sequence error");
			}
		}
	}
	/// <summary>
	/// ExperienceTable should be consistent with the max maxLevel and be sorted.
	/// </summary>
	/// <value>The experience table.</value>
	public static List<object> experienceTable
	{
		get{return _experienceTable.ConvertAll(x=>(object)x);}
		private set{
			_experienceTable=value.ConvertAll(x=>System.Convert.ToInt32(x));
			if(!IsIntListSorted(_experienceTable))
			{
				Debug.Log("ExperienceTable item sequence error");
				throw new System.ArgumentException("ExperienceTable item sequence error");
			}
		}
	}
#endregion

	#region Properties instance member
	//member properties
	public int id
	{
		 get{return _id;}
		private set{
			if(value<=0)
			{
				Debug.Log("Attribute id should not be negative");
				throw new ArgumentException("Attribute id should not be negative");
			}
			_id=value;}
	}

	public string cardSprite
	{
		get{return _cardSprite;}
		set{
			_cardSprite=value;
		}
	}
//	public string backgroundSprite
//	{
//		get{return _backgroundSprite;}
//		private set{
//			_backgroundSprite=value;
//		}
//	}
	public int 	strengthBase
	{
		get{return _strengthBase;}
		private set{
			if(value<0)
			{
				Debug.Log("Attribute strengthBase should not be negative");
				throw new ArgumentException("Attribute strengthBase should not be negative");
			}
			_strengthBase=value;}
	}
	public float strengthGrowth
	{
		get{return _strengthGrowth;}
		private set{
			if(value<0)
			{
				Debug.Log("Attribute strengthGrowth should not be negative");
				throw new ArgumentException("Attribute strengthGrowth should not be negative");
			}
			_strengthGrowth=value;}
	}
	public int  agilityBase
	{
		get{return _agilityBase;}
		private set{
			if(value<0)
			{
				Debug.Log("Attribute agilityBase should not be negative");
				throw new ArgumentException("Attribute agilityBase should not be negative");
			}
			_agilityBase=value;}
	}
	public float agilityGrowth
	{
		get{return _agilityGrowth;}
		private set{
			if(value<0)
			{
				Debug.Log("Attribute agilityGrowth should not be negative");
				throw new ArgumentException("Attribute agilityGrowth should not be negative");
			}
			_agilityGrowth=value;}
	}
	public int magicBase
	{
		get{return _magicBase;}
		private set{
			if(value<0)
			{
				Debug.Log("Attribute magicBase should not be negative");
				throw new ArgumentException("Attribute magicBase should not be negative");
			}
			_magicBase=value;}
	}
	public float magicGrowth
	{
		get{return _magicGrowth;}
		private set{
			if(value<0)
			{
				Debug.Log("Attribute magicGrowth should not be negative");
				throw new ArgumentException("Attribute magicGrowth should not be negative");
			}
			_magicGrowth=value;}
	}
	public int maxHealthBase
	{
		get{return _maxHealthBase;}
		private set{
			if(value<0)
			{
				Debug.Log("Attribute maxHealthBase should not be negative");
				throw new ArgumentException("Attribute maxHealthBase should not be negative");
			}
			_maxHealthBase=value;}
	}
	public int maxManaBase
	{
		get{return _maxManaBase;}
		private set{
			if(value<0)
			{
				Debug.Log("Attribute maxManaBase should not be negative");
				throw new ArgumentException("Attribute maxManaBase should not be negative");
			}
			_maxManaBase=value;}
	}
	public int physicalDefenseBase
	{
		get{return _physicalDefenseBase;}
		private set{
//			if(value<0)
//			{
//				Debug.Log("Attribute physicalDefenseBase should not be negative");
//				throw new ArgumentException("Attribute physicalDefenseBase should not be negative");
//			}
			_physicalDefenseBase=value;}
	}
	public int magicalDefenseBase
	{
		get{return _magicalDefenseBase;}
		private set{
//			if(value<0)
//			{
//				Debug.Log("Attribute magicalDefenseBase should not be negative");
//				throw new ArgumentException("Attribute magicalDefenseBase should not be negative");
//			}
			_magicalDefenseBase=value;}
	}
	public int physicalCriticalChanceBase
	{
		get{return _physicalCriticalChanceBase;}
		set{_physicalCriticalChanceBase=value;}
	}
	public int magicalCriticalChanceBase
	{
		get{return _magicalCriticalChanceBase;}
		set{_magicalCriticalChanceBase=value;}
	}
	public int physicalDamageBase
	{
		get{return _physicalDamageBase;}
		private set{
			if(value<0)
			{
				Debug.Log("Attribute physicalDamageBase should not be negative");
				throw new ArgumentException("Attribute physicalDamageBase should not be negative");
			}
			_physicalDamageBase=value;}
	}
	public int magicalDamageBase
	{
		get{return _magicalDamageBase;}
		private set{
			if(value<0)
			{
				Debug.Log("Attribute magicalDamageBase should not be negative");
				throw new ArgumentException("Attribute magicalDamageBase should not be negative");
			}
			_magicalDamageBase=value;}
	}
	public int healthResilienceBase
	{
		get{return _healthResilienceBase;}
		private set{
			if(value<0)
			{
				Debug.Log("Attribute healthResilienceBase should not be negative");
				throw new ArgumentException("Attribute healthResilienceBase should not be negative");
			}
			_healthResilienceBase=value;}
	}
	public int magicResilienceBase
	{
		get{return _magicResilienceBase;}
		private set{
			if(value<0)
			{
				Debug.Log("Attribute magicResilienceBase should not be negative");
				throw new ArgumentException("Attribute magicResilienceBase should not be negative");
			}
			_magicResilienceBase=value;}
	}
	public int evasionBase
	{
		get{return _evasionBase;}
		private set{
//			if(value<0)
//			{
//				Debug.Log("Attribute evasionBase should not be negative");
//				throw new ArgumentException("Attribute evasionBase should not be negative");
//			}
			_evasionBase=value;}
	}
	public float drapRate
	{
		get{return _drapRate;}
		private set{
			if(value<0)
			{
				Debug.Log("Attribute drapRate should not be negative");
				throw new ArgumentException("Attribute drapRate should not be negative");
			}
			_drapRate=value;}
	}
	public int price
	{
		get{return _price;}
		private set{
			if(value<0)
			{
				Debug.Log("Attribute price should not be negative");
				throw new ArgumentException("Attribute price should not be negative");
			}
			_price=value;}
	}
	public Rarity rarity
	{
		get{return _cardRarity;}
		private set{
					_cardRarity=value;
		}
	}
	public string name
	{
		get{return _name;}
	}
	public string description
	{
		get{return _description;}
		set{_description=value;}
	}
	public List<object> abilitiesId
	{
//		get{return _abilitiesId;}
		set{_abilitiesId=value.ConvertAll(x=>System.Convert.ToInt32(x));}
	}
#endregion

	private BaseCard()
	{}

	public static BaseCard GeneBaseCard(KeyValuePair<string,Dictionary<string,object>> cardInfo)
	{
		BaseCard baseCard=new BaseCard();
		baseCard._name=cardInfo.Key;
		Type type=typeof(BaseCard);
		Dictionary<string,object> cardAttr=cardInfo.Value;
		foreach(string attribute in cardAttr.Keys)
		{
			PropertyInfo propertyInfo= type.GetProperty(attribute);
			if(propertyInfo==null)
			{
				Debug.Log(string.Format("Illegal field:Card attribute '{0}' does not exist",attribute));
				throw new System.Exception(string.Format("Illegal field:Card attribute '{0}' does not exist",attribute));
			}
			object valueOb=cardAttr[attribute];
			if(propertyInfo.PropertyType.IsEnum)
			{
				try{
					System.Convert.ToInt32(valueOb);
				}catch{
					valueOb=	Enum.Parse(propertyInfo.PropertyType,(string)valueOb);
				}
				if(!Enum.IsDefined(propertyInfo.PropertyType,valueOb))
				{
					Debug.Log(string.Format("Illegal {0} value :{1}" ,propertyInfo.PropertyType,valueOb));
					throw new System.ArgumentException(string.Format("Illegal {0} value :{1}" ,propertyInfo.PropertyType,valueOb));
				}
			}
			if(valueOb.GetType()!=propertyInfo.PropertyType)
			{
				valueOb=System.Convert.ChangeType(valueOb,propertyInfo.PropertyType);
			}
			propertyInfo.SetValue(baseCard,valueOb,null);
		}
		if(!baseCard.CheckInstanceFields())
		{
			throw new System.ArgumentException(string.Format("The card data is deficient in card that named:{0}",baseCard.name));
		}
		return baseCard;
	}

	public static void LoadStaticFields(Dictionary<string,object> staticFields)
	{
		Type type=typeof(BaseCard);
		foreach(string attribute in staticFields.Keys)
		{
			PropertyInfo propertyInfo= type.GetProperty(attribute,BindingFlags.Static|BindingFlags.Public);
			if(propertyInfo==null)
			{
				Debug.Log(string.Format("Illegal static field:Card static attribute '{0}' does not exist",attribute));
				throw new System.FieldAccessException(string.Format("Illegal static field:Card static attribute '{0}' does not exist",attribute));
			}
			object valueOb=staticFields[attribute];
//			Debug.Log("property name:"+propertyInfo.Name+";property type:"+propertyInfo.PropertyType);
//			Debug.Log(valueOb.GetType());
			if(valueOb.GetType()!=propertyInfo.PropertyType)
			{
				valueOb= System.Convert.ChangeType(valueOb,propertyInfo.PropertyType);
			}
			propertyInfo.SetValue(null,valueOb,null);
		}
	}


	public static bool CheckStaticFields()
	{
		bool bPass=true;
		foreach(PropertyInfo propertyInfo in typeof(BaseCard).GetProperties(BindingFlags.Static|BindingFlags.Public))
		{
//			Debug.Log(propertyInfo.Name+":"+propertyInfo.GetValue(null,null));
			if(propertyInfo.PropertyType==typeof(float))
			{
				if(propertyInfo.GetValue(null,null)==(object)0f){
					Debug.Log(string.Format("lack of static field: '{0}'",propertyInfo.Name));
					bPass=false;
				}
			}
		}
		if(_maxLevelTable==null)
		{
			Debug.Log("lack of static field: maxLevelTable");
			bPass=false;
		}
		if(_experienceTable==null)
		{
			Debug.Log("lack of static field: experienceTable");
			bPass=false;
		}
		if(_maxLevelTable!=null&&_experienceTable!=null)
		{
			if(_experienceTable.Count!=_maxLevelTable[_maxLevelTable.Count-1])
			{
				Debug.Log("experienceTable and maxLevelTable are not consistent");
				bPass=false;
			}
		}
		return bPass;
	}

	bool  CheckInstanceFields()
	{
		bool bPass=true;
		if(_id<=0){
			Debug.Log("Lack of id data");
			bPass=false;
		}
		return bPass;
	}

	static bool  IsIntListSorted(List<int> intList)
	{
		bool bPass=true;
		for(int i=1;i!=intList.Count;i++)
		{
			if(intList[i-1]>intList[i])
				bPass=false;
		}
		return bPass;
	}
}
