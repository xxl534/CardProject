using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AbilityEntity :MonoBehaviour {
	#region Instance menbers
	private Dictionary<string ,int> _variables;
	private BattleCard _targetCard;
	private BattleCard _castCard;
	private AbilityType _abilityType;
	private EffectCard _effectCard;
#endregion

	#region Properties
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
	}
	#endregion

	/// <summary>
	/// Initializes a new instance of the <see cref="AbilityEntity"/> class.
	/// </summary>
	/// <param name="from">BattleCard casts this AbilityEntity.</param>
	/// <param name="to">Target BattleCard.</param>
	public AbilityEntity(BattleCard from ,BattleCard to,AbilityType abilityType)
	{
		castCard = from;
		targetCard = to;
		this.abilityType = abilityType;
	}

	/// <summary>
	/// Sets or adds variable to this AbilityEntity.
	/// </summary>
	/// <param name="name">Variable name.</param>
	/// <param name="value">Variable value.</param>
	public void SetValue(string name,int value)
	{
		_variables.Add (name, value);
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
