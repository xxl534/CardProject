using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
public static class AbilityCastStatic
{

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

		public static AbilityCast AbilityCast_physicalDamage = delegate(Ability ability,BattleCard from, BattleCard to) {
				int damage = Random.Range (ability.GetValue (AbilityVariable.minDamage), ability.GetValue (AbilityVariable.maxDamage))
						+ from.physicalDamage;
				int criticalChance = Random.Range (1, 101);
				if (criticalChance <= from.physicalCriticalChance) {
						damage *= 2;
				}
				AbilityEntity abilityEntity = new AbilityEntity (from, to, ability.abilityType);
				abilityEntity.SetValue (AbilityVariable.damage, damage);
				return abilityEntity;
		};
}
