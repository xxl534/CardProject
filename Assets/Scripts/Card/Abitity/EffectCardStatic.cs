using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
/// <summary>
/// This delegate will be called when abilityEntity effects the target card.
/// Return the delta value of target attribute of target BattleCard
/// This delegate is owed by abilityEntity and called by target BattleCard
/// </summary>
public delegate int EffectCard(AbilityEntity abilityEntity,BattleCard to);



public static class EffectCardStatic {
	static float physicalDamageDeductionBase=50f;

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
	public static EffectCard EffectCard_PhysicalDamage = delegate(AbilityEntity abilityEntity, BattleCard to) {
				int damage = abilityEntity.GetValue(AbilityVariable.damage);
				damage =(int)(damage/( 1 + to.physicalDefense / physicalDamageDeductionBase));
				to.health -= damage;
		return -damage;
		};

}
