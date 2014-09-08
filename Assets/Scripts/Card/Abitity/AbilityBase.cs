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
	private TargetType _targetType;
	private TargetArea _targetArea;
		private int _maxLevel;
		private string _name,
				_description,
				_icon,
				_targetAttr,
	_abilityCast;
//				_effectCard;
		public List<string> _variables;
		public List<List<int>> _variableValueTable;
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
	public string icon
	{
		get{return _icon;}
		private set{_icon=value;}
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
		private set{_abilityType=value;}
	}
		
	public TargetType targetType
	{
		get{return _targetType;}
		private set{_targetType=value;}
	}

	public TargetArea targetArea
	{
		get{return _targetArea;}
		private set{_targetArea=value;}
	}

		public int maxLevel {
		get{ return _maxLevel;}
		private set{
			if(value<0)
			{
				Debug.Log("Attribute maxLevel should not be negative");
				throw new ArgumentException("Attribute maxLevel should not be negative");
			}
			_maxLevel=value;}
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

		public List<object> variableValueTable {
		get{ return _variableValueTable.ConvertAll(x=>(object)x);}
		private set{
			_variableValueTable=value.ConvertAll(x=>{
				List<int> intList=(x as List<object>).ConvertAll(y=>System.Convert.ToInt32(y));
				return intList;});
		}
	}

		public string abilityCast {
		get{ return _abilityCast;}
		private set{_abilityCast=value;}
	}

//		public string effectCard {
//		get{ return _effectCard;}
//		private set{_effectCard=value;}
//	}

		public List<object> variables {
		get{ return  _variables.ConvertAll(x=>(object)x);}
		private set{	_variables=value.ConvertAll(x=>x.ToString());
		}
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
//			Debug.Log(propertyInfo.Name);
//			Debug.Log(valueOb.GetType()+","+propertyInfo.PropertyType);
			if(valueOb.GetType()!=propertyInfo.PropertyType)
			{
				valueOb=System.Convert.ChangeType (valueOb,propertyInfo.PropertyType);
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
//		if(_effectCard==null)
//		{
//			Debug.Log("Lack of effectCard data");
//			bPass=false;
//		}
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
