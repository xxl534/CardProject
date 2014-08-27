using UnityEngine;
using System.Collections;

public static class AbilityCastStatic{
//	public static bool Initialize(AbilityBase ability)
//	{
//		return false;
//	}

	public static 	AbilityCast AbilityCast_physicalDamage = delegate(BattleCard from, Ability ability) {
		int damage=Random.Range(ability.GetValue(AbilityVariable.minDamage),ability.GetValue(AbilityVariable.maxDamage))
					+from.physicalDamage;
		int criticalChance=Random.Range(1,101);
		if(criticalChance<=from.physicalCriticalChance)
		{
			damage*=2;
		}

		};
}
