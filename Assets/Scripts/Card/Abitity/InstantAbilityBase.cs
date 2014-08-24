using UnityEngine;
using System.Collections;


/// <summary>
///This kind of abliity directly affect the targer,like take damage,weaken defense. 
/// </summary>
public class InstantAbilityBase : AbstractAbilityBase{
	protected IAbilityEffect ablityEffect;
	public override  object CastAbility(ConcreteCard from,ConcreteCard to)
	{
		ablityEffect.AbilityEffect(from,to,this);
			return null;
	}
	
}
