using UnityEngine;
using System.Collections;

public delegate void CardAttacked(AbilityEntity abilityEntity,BattleCard to);
public static class CardAttackedStatic {
	static int physicalDamageDeductionBase=50;
	public static CardAttacked CardAttacked_PhysicalDamage = delegate(AbilityEntity abilityEntity, BattleCard to) {
				int damage = abilityEntity.value;
				damage /= 1 + to.physicalDefense / physicalDamageDeductionBase;
				to.health -= damage;
		};

}
