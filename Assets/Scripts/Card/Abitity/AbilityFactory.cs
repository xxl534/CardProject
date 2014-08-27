using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public static class AbilityFactory {
	static List<AbilityBase> BaseAbilityTable;

	static AbilityFactory()
	{
		BaseAbilityTable = new List<AbilityBase> ();
	}
	public static void RegisterAbility(AbilityData abilityData)
	{

	}

	public static Ability GeneAbility(AbilityBase baseAbility)
	{
		return null;
	}
}
