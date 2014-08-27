using UnityEngine;
using System.Collections;

public static class AbilityEffectStatic{
//	public static bool Initialize(AbilityBase ability)
//	{
//		return false;
//	}

	public static 	AbilityEffect AbilityEffect_physicalDamage = delegate(BattleCard from, AbilityEntity ability) {
		int damage=Random.Range(ability._value[0],ability._value[1])+from.physicalDamage;
		int criticalChance=Random.Range(1,101);
		if(criticalChance<=from.physicalCriticalChance)
		{
			damage*=2;
		}
		ability._value=new int[]{damage};
		};
}
