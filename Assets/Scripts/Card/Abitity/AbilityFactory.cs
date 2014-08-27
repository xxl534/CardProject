using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public static class AbilityFactory {
	static Dictionary <int,AbilityBase> BaseAbilityTable;

	static AbilityFactory()
	{
		BaseAbilityTable = new Dictionary<int, AbilityBase>();
	}
	public static void RegisterAbility(AbilityData abilityData)
	{

	}
//
//	public static Ability GeneAbility(AbilityBase baseAbility)
//	{
//		return null;
//	}

	public static Ability GeneAbility(int abilityId,int level)
	{
		return null;
	}
}
