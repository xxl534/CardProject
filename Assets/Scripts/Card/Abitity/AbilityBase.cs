using UnityEngine;
using System.Collections;

/// <summary>
/// Generate a ability entity according to the ability that casted.
/// </summary>
public delegate AbilityEntity AbilityCast(Ability ability,BattleCard from,BattleCard to);


//public delegate Ability  AbilityLevelUpdater(Ability ability,int level);
public  class AbilityBase : MonoBehaviour {

	#region Instance members
	private int _id;
	private Rarity _rarity;
	private AbilityType _abilityType;
	private int _maxLevel;
	private string _name,
				 _description,
				 _targetAttr,
	_abilityCast,
				_effectCard;
	private string[] _variables;
	private int[][] _valueTable;
#endregion

#region Properties
	public int id
	{
		get{return _id;}
	}
	public AbilityType abilityType
	{
		get{return _abilityType;}
	}
	public int maxLevel
	{
		get{return _maxLevel;}
	}
	public string name
	{
		get{return _name;}
	}
	public string description
	{
		get{return _description;}
	}
	public string targetAttr
	{
		get{return _targetAttr;}
	}
	public int[][] valueTable
	{
		get{return _valueTable;}
	}
	public string abilityCast
	{
		get{return _abilityCast;}
	}
	public string effectCard
	{
		get{return _effectCard;}
	}
	public string[] variables
	{
		get{return  _variables;}
	}
#endregion
}
