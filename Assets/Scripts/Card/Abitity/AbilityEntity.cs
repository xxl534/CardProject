using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AbilityEntity {
	#region Instance menbers
	protected int _id;
	protected string _name;
	protected Dictionary<string ,int> _variables;
	protected string _targetAttr;
	protected BattleCard _targetCard;
	protected BattleCard _castCard;
	protected AbilityType _abilityType;
	protected EffectCard _effectCard;
#endregion

	#region Properties
	public int abilityId
	{
		get{return _id;}
	}
	public string name
	{
		get{return _name;}
	}
	public BattleCard castCard
	{
		get{return _castCard;}
		set{_castCard=value;}
	}
	public BattleCard targetCard
	{
		get{return _targetCard;}
		set{_targetCard=value;}
	}
	public AbilityType abilityType
	{
		get{return _abilityType;}
		set{_abilityType=value;}
	}
	public EffectCard effectCard
	{
		get{return _effectCard;}
		set{_effectCard=value;}
	}
	public string targetAttr
	{
		get{return _targetAttr;}
		set{_targetAttr=value;}
	}
	#endregion


	protected AbilityEntity()
	{
		_variables = new Dictionary<string, int> ();
	}

	public AbilityEntity(int abilityId,string name,BattleCard from ,BattleCard to,AbilityType abilityType,EffectCard effectCard,string targetAttr=null):this()
	{
		castCard = from;
		targetCard = to;
		this.abilityType = abilityType;
		this.effectCard = effectCard;
		this.targetAttr = targetAttr;
		_id = abilityId;
		_name=name;
	}

	/// <summary>
	/// Sets or adds variable to this AbilityEntity.
	/// </summary>
	/// <param name="name">Variable name.</param>
	/// <param name="value">Variable value.</param>
	public void SetValue(string name,int value)
	{
		_variables[name]=value;
	}
	/// <summary>
	/// Gets a variable of this AbilityEntity by name.
	/// </summary>
	/// <returns>Variablie value.</returns>
	/// <param name="name">Variable name.</param>
	public int GetValue(string name)
	{
		return _variables [name];
	}

	void OnTriggleEnter(Collider other)
	{
		BattleCard targetBattleCard=other.GetComponent<BattleCard>();
		if (targetBattleCard == targetCard) {

				}
	}
}
