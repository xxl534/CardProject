using UnityEngine;
using System.Collections;
/// <summary>
/// This delegate is owed by BattleCard and is called when abilityEntity hit the BattleCard
/// </summary>
public delegate void  CardEffected(AbilityEntity abilityEntity);
public static class CardEffectedStatic {

	public static CardEffected cardEffected_normal = delegate(AbilityEntity abilityEntity) {
		abilityEntity.effectCard(abilityEntity);
	};
}
