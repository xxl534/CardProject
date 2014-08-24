using UnityEngine;
using System.Collections;
/// <summary>
/// This interface defines how an ability acting on target.
/// It modifies target one or some attributes(determined by the ability) to some value(determined by the ability too). 
/// </summary>
public interface IAbilityEffect {
	 void AbilityEffect (ConcreteCard from, ConcreteCard to, InstantAbilityBase ability);

}
