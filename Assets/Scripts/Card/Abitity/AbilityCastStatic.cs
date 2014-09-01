using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
public static class AbilityCastStatic
{
	static float _physicalCriticalFactor=2f,_magicalCriticalFactor=1.5f;
	static  Dictionary<string,AbilityCast> abilityCastTable;
	static AbilityCastStatic()
	{
		abilityCastTable=new Dictionary<string, AbilityCast>();
		FieldInfo[] fieldInfos= typeof(AbilityCastStatic).GetFields(BindingFlags.Public|BindingFlags.Static);
		foreach(FieldInfo fieldInfo in fieldInfos)
		{
			if(fieldInfo.FieldType==typeof (AbilityCast)){
			string abilityCastName=fieldInfo.Name.Substring(12);
//				Debug .Log(abilityCastName);
			abilityCastTable.Add(abilityCastName,fieldInfo.GetValue(null) as AbilityCast);
			}
		}
	}
	public static bool HasAbilityCast(string abilityCastName)
	{
		return abilityCastTable.ContainsKey(abilityCastName);
	}

	public static AbilityCast GetAbilityCast(string abilityCastName)
	{
		return abilityCastTable[abilityCastName];
	}

	public static AbilityCast AbilityCast_PhysicalDamage = delegate(Ability ability,BattleCard from, BattleCard to) {
				int damage = Random.Range (ability.GetValue (AbilityVariable.minValue), ability.GetValue (AbilityVariable.maxValue))
						+ from.physicalDamage;
				int criticalChance = Random.Range (1, 101);
				if (criticalChance <= from.physicalCriticalChance) {
						damage = (int)(damage*_physicalCriticalFactor);
				}
				AbilityEntity abilityEntity = new AbilityEntity (ability.baseAbility.id,ability.name,
		                                                 from, to, ability.abilityType,EffectCardStatic.EffectCard_PhysicalDamage);
				abilityEntity.SetValue (AbilityVariable.value, damage);
				return abilityEntity;
		};

	public static AbilityCast AbilityCast_MagicalDamage = delegate(Ability ability,BattleCard from, BattleCard to) {
		int damage = Random.Range (ability.GetValue (AbilityVariable.minValue), ability.GetValue (AbilityVariable.maxValue))
		+ from.magicalDamage;
		int criticalChance = Random.Range (1, 101);
		if (criticalChance <= from.magicalCriticalChance) {
			damage =(int)(damage*_magicalCriticalFactor);
		}
		AbilityEntity abilityEntity = new AbilityEntity (ability.baseAbility.id,ability.name,
		                                                 from, to, ability.abilityType,EffectCardStatic.EffectCard_MagicalDamage);
		abilityEntity.SetValue (AbilityVariable.value, damage);
		return abilityEntity;
	};

	public static AbilityCast AbilityCast_Healing= delegate(Ability ability, BattleCard from, BattleCard to) {
		int healing=Random.Range(ability.GetValue (AbilityVariable.minValue), ability.GetValue (AbilityVariable.maxValue))
			+ from.magicalDamage;
		AbilityEntity abilityEntity = new AbilityEntity (ability.baseAbility.id,ability.name,
		                                                 from, to, ability.abilityType,EffectCardStatic.EffectCard_Healing);
		abilityEntity.SetValue (AbilityVariable.value, healing);
		return abilityEntity;
	};

	public static AbilityCast AbilityCast_DotOrHot = delegate(Ability ability, BattleCard from, BattleCard to) {
		int value = Random.Range (ability.GetValue (AbilityVariable.minValue), ability.GetValue (AbilityVariable.maxValue));
		if(ability.abilityType== AbilityType.Physical)
		{
			value+=from.physicalDamage;
		}
		else if(ability.abilityType== AbilityType.Magical)
		{
			value+=from.magicalDamage;
		}
		AbilityEntity abilityEntity = new AbilityEntity (ability.baseAbility.id,ability.name,
		                                                 from, to, ability.abilityType, EffectCardStatic.EffectCard_GenerateDotOrHot);
		abilityEntity.SetValue(AbilityVariable.value,value);
		abilityEntity.SetValue(AbilityVariable.dot,ability.GetValue(AbilityVariable.dot));
		abilityEntity.SetValue(AbilityVariable.interval,ability.GetValue(AbilityVariable.interval));
		abilityEntity.SetValue(AbilityVariable.duration,ability.GetValue(AbilityVariable.duration));
				return abilityEntity;
		};

	public static AbilityCast AbilityCast_DebuffOrBuff = delegate(Ability ability, BattleCard from, BattleCard to) {
		AbilityEntity abilityEntity = new AbilityEntity (ability.baseAbility.id,ability.name,
		                                                 from, to, ability.abilityType, EffectCardStatic.EffectCard_GenerateDebuffOrBuff);
				abilityEntity.targetAttr = ability.target;
				abilityEntity.SetValue (AbilityVariable.value, ability.GetValue (AbilityVariable.value));
		abilityEntity.SetValue(AbilityVariable .debuff,ability.GetValue(AbilityVariable.debuff));
				abilityEntity.SetValue (AbilityVariable.interval, ability.GetValue (AbilityVariable.interval));
				abilityEntity.SetValue (AbilityVariable.duration, ability.GetValue (AbilityVariable.duration));
				return abilityEntity;
		};
}
