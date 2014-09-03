using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
/// <summary>
/// This delegate will be called when abilityEntity effects the target card.
/// Return the delta value of target attribute of target BattleCard
/// This delegate is owed by abilityEntity and called by target BattleCard
/// </summary>
public delegate int EffectCard(AbilityEntity abilityEntity);



public static class EffectCardStatic {
	static float physicalDamageDeductionBase=50f;
	static float magicalDamageDeductionBase=100f;
	static  Dictionary<string,EffectCard> effectCardTable;
	static EffectCardStatic()
	{
		effectCardTable=new Dictionary<string, EffectCard>();
		FieldInfo[] fieldInfos= typeof(EffectCardStatic).GetFields(BindingFlags.Public|BindingFlags.Static);
		foreach(FieldInfo fieldInfo in fieldInfos)
		{
			if(fieldInfo.FieldType==typeof (EffectCard)){
				string effectCardName=fieldInfo.Name.Substring(11);
				//				Debug .Log(effectCardName);
				effectCardTable.Add(effectCardName,fieldInfo.GetValue(null) as EffectCard);
			}
		}
	}
	public static bool HasEffectCard(string effectCardName)
	{
		return effectCardTable.ContainsKey(effectCardName);
	}
	
	public static EffectCard GetEffectCard(string effectCardName)
	{
		return effectCardTable[effectCardName];
	}
	public static EffectCard EffectCard_PhysicalDamage = delegate(AbilityEntity abilityEntity) {
		BattleCard to=abilityEntity.targetCard;
				int damage = abilityEntity.GetValue(AbilityVariable.value);
				damage =(int)(damage/( 1 + to.physicalDefense / physicalDamageDeductionBase));
				to.health -= damage;
		return -damage;
		};

	public static EffectCard EffectCard_MagicalDamage=delegate(AbilityEntity abilityEntity) {
		BattleCard to=abilityEntity.targetCard;
		int damage = abilityEntity.GetValue(AbilityVariable.value);
		damage =(int)(damage/( 1 + to.magicalDefense / magicalDamageDeductionBase));
		to.health -= damage;
		return -damage;
	};

	public static EffectCard EffectCard_Healing=delegate(AbilityEntity abilityEntity) {
		BattleCard target=abilityEntity.targetCard;
		int healing=abilityEntity.GetValue(AbilityVariable.value);
		target.health+=healing;
		return healing;
	};

	public static EffectCard EffectCard_GenerateDotOrHot = delegate(AbilityEntity abilityEntity) {
		BattleCard to=abilityEntity.targetCard,from=abilityEntity.castCard;
		int id=(from.GetHashCode ().ToString () + from.concreteCard.id.ToString ()).GetHashCode ();
		bool isDot=abilityEntity.GetValue(AbilityVariable.dot)==1;
		DotAndHot dotOrHot = new DotAndHot (id,abilityEntity.name,isDot, from,to,abilityEntity.abilityType,
		                               abilityEntity.GetValue(AbilityVariable.interval),abilityEntity.GetValue(AbilityVariable.duration));
		int value=abilityEntity.GetValue(AbilityVariable .value);
		if(isDot)
		{// Is a HOT
			dotOrHot.effectCard=EffectCard_Healing;
		}
		else
		{//Is a DOT
				if (abilityEntity.abilityType == AbilityType.Physical) {
			dotOrHot.effectCard = EffectCard_PhysicalDamage;
				} else if (abilityEntity.abilityType == AbilityType.Magical) {
			dotOrHot.effectCard = EffectCard_MagicalDamage;
				}
		}
		dotOrHot.SetValue (AbilityVariable.value,value);
		to.AddDotOrHot (dotOrHot);
		return 0;
		};

	public static EffectCard EffectCard_GenerateDebuffOrBuff= delegate(AbilityEntity abilityEntity) {
		BattleCard to=abilityEntity.targetCard,from=abilityEntity.castCard;
		PropertyInfo propertyInfo=typeof(BattleCard).GetProperty(abilityEntity.targetAttr);
		bool isDebuff=abilityEntity.GetValue(AbilityVariable.debuff)==1;
		DebuffAndBuff debuffOrBuff = new DebuffAndBuff (abilityEntity.abilityId,abilityEntity.name,isDebuff, from,to,
		                                                abilityEntity.abilityType,abilityEntity.GetValue(AbilityVariable.duration),propertyInfo);
		int valueDelta=abilityEntity.GetValue(AbilityVariable.value);
		debuffOrBuff.SetValue (AbilityVariable.restorativeValue, -valueDelta);
		int value =System.Convert.ToInt32(propertyInfo.GetValue(to,null))+valueDelta;
		return valueDelta;
	};
}
