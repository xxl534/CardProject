using UnityEngine;
using System.Collections;

public interface ICastAbility {
	ICastAbility CastAbilityTo(ICastAbility from,BattleCard to,RealAbility ability);
}

