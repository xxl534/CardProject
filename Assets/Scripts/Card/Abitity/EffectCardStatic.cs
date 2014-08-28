using UnityEngine;
using System.Collections;

/// <summary>
/// This delegate will be called when abilityEntity effects the target card.
/// Return the delta value of target attribute of target BattleCard
/// This delegate is owed by abilityEntity and called by target BattleCard
/// </summary>
public delegate int EffectCard(AbilityEntity abilityEntity,BattleCard to);



public static class EffectCardStatic {
	static float physicalDamageDeductionBase=50f;

	public static EffectCard EffectCard_PhysicalDamage = delegate(AbilityEntity abilityEntity, BattleCard to) {
				int damage = abilityEntity.GetValue(AbilityVariable.damage);
				damage =(int)(damage/( 1 + to.physicalDefense / physicalDamageDeductionBase));
				to.health -= damage;
		return -damage;
		};

}
