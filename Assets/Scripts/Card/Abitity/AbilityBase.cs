using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

/// <summary>
/// Generate a ability entity according to the ability that casted.
/// </summary>
public delegate AbilityEntity AbilityCast (Ability ability,BattleCard from,BattleCard to);


//public delegate Ability  AbilityLevelUpdater(Ability ability,int level);
public  class AbilityBase
{

	#region Instance members
		private int _id;
		private Rarity _rarity;
		private AbilityType _abilityType;
		private int _maxLevel;
		private string _name,
				_description,
				_targetAttr,
				_abilityCast,
				_effectCard;
		private List<string> _variables;
		private List<List<int>> _variableValueTable;
#endregion

#region Properties
		public int id {
				get{ return _id;}
		private set{
			if(value<=0)
			{
				Debug.Log("Attribute id should not be negative");
				throw new ArgumentException("Attribute id should not be negative");
			}
			_id=value;}
		}

	public Rarity rarity
	{
		get{return _rarity;}
		private set{
			Array array=	Enum.GetValues( typeof(Rarity));
			for (int i = 0; i < array.Length; i++) {
				if((Rarity)array.GetValue(i)==value)
				{
					_rarity=value;
					return;
				}
			}
			Debug.Log(string.Format("Illegal ability rarity value :{0}" ,value));
			throw new System.ArgumentException(string.Format("Illegal ability rarity value :{0}" ,value));
		}
	}
		public AbilityType abilityType {
		get{ return _abilityType;}
		private set{
			Array array=	Enum.GetValues( typeof(AbilityType));
			for (int i = 0; i < array.Length; i++) {
				if((AbilityType)array.GetValue(i)==value)
				{
					_abilityType=value;
					return;
				}
			}
			Debug.Log(string.Format("Illegal abilityType value :{0}" ,value));
			throw new System.ArgumentException(string.Format("Illegal abilityType value :{0}" ,value));
		}
	}

		public int maxLevel {
		get{ return _maxLevel;}
		private set{
			if(value<0)
			{
				Debug.Log("Attribute maxLevel should not be negative");
				throw new ArgumentException("Attribute maxLevel should not be negative");
			}
			_id=value;}
	}

		public string name {
		get{ return _name;}
	}

		public string description {
		get{ return _description;}
		private set{_description=value;}
	}

		public string targetAttr {
		get{ return _targetAttr;}
		private set{_targetAttr=value;}
	}

		public List<List<int>> variableValueTable {
		get{ return _variableValueTable;}
		private set{_variableValueTable=value;}
	}

		public string abilityCast {
		get{ return _abilityCast;}
		private set{_abilityCast=value;}
	}

		public string effectCard {
		get{ return _effectCard;}
		private set{_effectCard=value;}
	}

		public List<string> variables {
		get{ return  _variables;}
		private set{_variables=value;}
	}
#endregion

	private AbilityBase()
	{}
	public static AbilityBase GeneAbilityBase(KeyValuePair<string,Dictionary<string,object>> abilityInfo)
	{
		AbilityBase abilityBase=new AbilityBase();
		abilityBase._name=abilityInfo.Key;
		Type type=typeof(AbilityBase);
		Dictionary<string,object> abilityFeilds=abilityInfo.Value;
		foreach(string feildName in abilityFeilds.Keys)
		{
			PropertyInfo propertyInfo= type.GetProperty(feildName);
			if(propertyInfo==null)
			{
				Debug.Log(string.Format("Illegal field:ability feild '{0}' does not exist",feildName));
				throw new System.FieldAccessException(string.Format("Illegal field:ability feild '{0}' does not exist",feildName));
			}
			object valueOb=abilityFeilds[feildName];
			if(propertyInfo.PropertyType.IsEnum)
			{
				try{
					int value=Convert.ToInt32(valueOb);
				}catch{
					valueOb=	Enum.Parse(propertyInfo.PropertyType,(string)valueOb);
				}
			}
			propertyInfo.SetValue(abilityBase,valueOb,null);
		}
		if(!abilityBase.CheckInstanceFields())
		{
			throw new System.ArgumentException(string.Format("The ability data is deficient in ability that named:{0}",abilityBase.name));
		}
		return abilityBase;
	}

	bool CheckInstanceFields()
	{
		bool bPass=true;
		if(_id<=0){
			Debug.Log("Lack of id data");
			bPass=false;
		}
		if(_effectCard==null)
		{
			Debug.Log("Lack of effectCard data");
			bPass=false;
		}
		if(_abilityCast==null)
		{
			Debug.Log("Lack of abilityCast data");
			bPass=false;
		}
		if(_maxLevel<=0)
		{
			Debug.Log("Lack of maxLevel data");
			bPass=false;
		}
		if(_variableValueTable==null||_variableValueTable.Count==0)
		{
			Debug.Log("Lack of variableValueTable data");
			bPass=false;
		}else{
			if(_variableValueTable.Count!=_maxLevel)
			{
				Debug.Log("Count of variableValueTable is incosistent with maxLevel");
				bPass=false;
			}
		}
		if(_variables==null||_variables.Count==0)
		{
			Debug.Log("Lack of variables data");
			bPass=false;
		}else if(_variableValueTable!=null&&_variableValueTable.Count>=1)
		{
			if(_variables.Count!=_variableValueTable[0].Count)
			{
				Debug.Log("Count of variables is incosistent with item of variableValueTable");
				bPass=false;
			}
		}
		return bPass;
	}
}
