using UnityEngine;
using System.Collections;
using System.Reflection;
public class DebuffAndBuff : DurativeState {
	protected PropertyInfo _targetProperty;
	protected bool _isDeBuff;
	protected int _remainTime;
	public int interval
	{
		get{return _interval;}
		set{_interval =value;}
	}
	public int duration
	{
		get{return _duration;}
	}
	public int remainTime
	{
		get{return _remainTime;}
	}
	public 	int id {
		get;
		set;
	}
	public bool isDebuff
	{
		get{return _isDeBuff;}
	}
	
	public DebuffAndBuff(int id,string name,bool isDebuff,BattleCard from,BattleCard to,
	                     AbilityType abilityType,int duration,PropertyInfo targetProperty)
	{
		this.id = id;
		_name=name;
		_isDeBuff=isDebuff;
		_castCard = from;
		_targetCard = to;
		_abilityType = abilityType;
		_duration = duration;
		_remainTime=duration;
		_targetProperty = targetProperty;
	}
	public override void RoundStart ()
	{

	}
	
	public override void RoundEnd ()
	{
		_remainTime--;
		if(_remainTime==0){
			Clear();
				}
	}

	public override void Clear()
	{
		object value=	_targetProperty.GetValue(_targetCard,null);
		value=(object)((int)value+GetValue(AbilityVariable.restorativeValue));
		_targetProperty.SetValue(_targetCard,value,null);
	}
}
