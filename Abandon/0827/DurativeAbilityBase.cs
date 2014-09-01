using UnityEngine;
using System.Collections;

public class DurativeAbilityBase : AbstractAbilityBase {
	protected int _effectInterval;
	protected int _duration;
	public  override object CastAbility(ConcreteCard from,ConcreteCard to)
	{
		return DurativeAbilityMonitor(from,to);
	}

	public object DurativeAbilityMonitor(ConcreteCard from,ConcreteCard to)
	{
		return  null;
	}
}
