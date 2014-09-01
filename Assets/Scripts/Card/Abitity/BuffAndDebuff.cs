using UnityEngine;
using System.Collections;
using System.Reflection;
public class BuffAndDebuff : DurativeState {
	protected PropertyInfo _targetProperty;
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
		get;
		set;
	}
	
	public BuffAndDebuff(int id,BattleCard from,BattleCard to,AbilityType abilityType,int duration,PropertyInfo targetProperty)
	{
		this.id = id;
		_castCard = from;
		_targetCard = to;
		_abilityType = abilityType;
		_duration = duration;
		_targetProperty = targetProperty;
	}
	public override void RoundStart ()
	{

	}
	
	public override void RoundEnd ()
	{
		duration--;
		if(duration==0){
		 object value=	_targetProperty.GetValue(_targetCard,null);
			value=(object)((int)value+GetValue(AbilityVariable.restorativeValue));
			_targetProperty.SetValue(_targetCard,value,null);
				}
	}
}
