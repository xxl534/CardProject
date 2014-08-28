using UnityEngine;
using System.Collections;

public static class AbilityCastStatic{
//	public static bool Initialize(AbilityBase ability)
//	{
//		return false;
//	}

	public static 	AbilityCast AbilityCast_physicalDamage = delegate(Ability ability,BattleCard from, BattleCard to) {
		int damage=Random.Range(ability.GetValue(AbilityVariable.minDamage),ability.GetValue(AbilityVariable.maxDamage))
					+from.physicalDamage;
		int criticalChance=Random.Range(1,101);
		if(criticalChance<=from.physicalCriticalChance)
		{
			damage*=2;
		}
		AbilityEntity abilityEntity=new AbilityEntity(from ,to,ability.abilityType);
		abilityEntity.SetValue(AbilityVariable.damage,damage);
		return abilityEntity;
		};

}
