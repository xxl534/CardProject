using UnityEngine;
using System.Collections;

public class Aura : DurativeState {
	public Aura (int id,string name,BattleCard from,
	             AbilityType abilityType,int interval)
	{
		_id=id;
		_name=name;

	}
	public override void RoundStart ()
	{
		throw new System.NotImplementedException ();
	}
	public override void RoundEnd ()
	{
		throw new System.NotImplementedException ();
	}
	public override void Clear ()
	{
		throw new System.NotImplementedException ();
	}

}
