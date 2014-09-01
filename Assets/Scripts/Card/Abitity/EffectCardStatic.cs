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

	public static EffectCard EffectCard_GenerateDotOrHot = delegate(AbilityEntity abilityEntity) {
		BattleCard to=abilityEntity.targetCard,from=abilityEntity.castCard;
		int id=(from.GetHashCode ().ToString () + from.baseCard.id.ToString ()).GetHashCode ();
		DotAndHot dotOrHot = new DotAndHot (id,from,to,abilityEntity.abilityType,
		                               abilityEntity.GetValue(AbilityVariable.interval),abilityEntity.GetValue(AbilityVariable.duration));
		int value=abilityEntity.GetValue(AbilityVariable .value);
		if(abilityEntity.GetValue(AbilityVariable.dot)!=1)
		{
			value*=-1;
		}
				dotOrHot.SetValue (AbilityVariable.value,value);
		
				if (abilityEntity.abilityType == AbilityType.Physical) {
			dotOrHot.effectCard = EffectCard_PhysicalDamage;
				} else if (abilityEntity.abilityType == AbilityType.Magical) {
			dotOrHot.effectCard = EffectCard_MagicalDamage;
				}
				to.AddDotOrHot (dotOrHot);
		return 0;
		};

	public static EffectCard EffectCard_GenerateBuffOrDebuff = delegate(AbilityEntity abilityEntity) {
		BattleCard to=abilityEntity.targetCard,from=abilityEntity.castCard;
		PropertyInfo propertyInfo=typeof(BattleCard).GetProperty(abilityEntity.targetAttr);
		BuffAndDebuff buffOrDebuff = new BuffAndDebuff (abilityEntity.abilityId,from,to,abilityEntity.abilityType,
		                               abilityEntity.GetValue(AbilityVariable.duration),propertyInfo);
		buffOrDebuff.SetValue (AbilityVariable.value, abilityEntity.GetValue (AbilityVariable.value));
		
		if (abilityEntity.abilityType == AbilityType.Physical) {
			buffOrDebuff.effectCard = EffectCard_PhysicalDamage;
		} else if (abilityEntity.abilityType == AbilityType.Magical) {
			buffOrDebuff.effectCard = EffectCard_MagicalDamage;
		}
		to.AddBuffOrDebuff (buffOrDebuff);
		return 0;
	};
}
