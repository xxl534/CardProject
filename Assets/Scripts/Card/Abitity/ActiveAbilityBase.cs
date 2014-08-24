using UnityEngine;
using System.Collections;

public class ActiveAbilityBase :AbilityBase {

	public void CastAbility(ConcreteCard from,ConcreteCard to)
	{
		_abilityEffect(from,to,this);
	}
	
}
