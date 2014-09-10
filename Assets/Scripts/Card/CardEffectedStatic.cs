using UnityEngine;
using System.Collections;
/// <summary>
/// This delegate is owed by BattleCard and is called when abilityEntity hit the BattleCard
/// </summary>
public delegate void  CardEffected(AbilityEntityShell abilityES);
public static class CardEffectedStatic {

	public static CardEffected CardEffected_Normal = delegate(AbilityEntityShell abilityES) {
		if(abilityES.abilityEntity.abilityType== AbilityType.Physical)
		{
			int odds=Random.Range(0,100);
			if(odds<abilityES.abilityEntity.targetCard.evasion)
			{
				abilityES.Abort();
				abilityES.abilityEntity.targetCard.Evade();
				return;
			}
		}
		abilityES.Hit();
	};
}
