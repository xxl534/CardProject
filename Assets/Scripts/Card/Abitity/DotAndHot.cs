using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DotAndHot :DurativeState {
	protected bool _isDot;

	public bool isDot
	{
		get{return _isDot;}
	}
	public int interval
	{
		get{return _interval;}
		set{_interval =value;}
	}
	public int duration
	{
		get{return _duration;}
		set{_duration=value;
		}
	}
 public 	int id {
		get{return _id;}
		set{_id=value;}
	}

	public DotAndHot(int id,string name,bool isDot,BattleCard from,BattleCard to,
	                 AbilityType abilityType,int interval,int duration)
	{
		this.id = id;
		_name=name;
		_isDot=isDot;
		_castCard = from;
		_targetCard = to;
		_abilityType = abilityType;
		_interval = interval;
		_duration = duration;
		_roundCounter = 0;
		}
	public override void RoundStart ()
	{
		_roundCounter++;
	}

	public override void RoundEnd ()
	{
		if (_roundCounter == interval) {
			_roundCounter=0;
			effectCard(this);
				}
		duration--;

	}
	public override void Clear()
	{}
}
