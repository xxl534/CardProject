using UnityEngine;
using System.Collections;
/// <summary>
/// This delegate is owed by BattleCard and is called when abilityEntity hit the BattleCard
/// </summary>
public delegate void  CardEffected(BattleCard target,AbilityEntity abilityEntity);
public static class CardEffectedStatic {

	public static CardEffected cardEffected_normal = delegate( BattleCard to,AbilityEntity abilityEntity) {
		abilityEntity.effectCard(abilityEntity,to);
	};
}
