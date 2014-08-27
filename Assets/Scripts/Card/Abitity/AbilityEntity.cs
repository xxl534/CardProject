using UnityEngine;
using System.Collections;

public class AbilityEntity :AbilityBase {

	private int _value;
	public AbilityCast _abilityCast;

	public int value
	{
		get{return _value;}
		set{_value=value;}
	}
}
