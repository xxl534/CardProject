using UnityEngine;
using System.Collections;
using Holoville.HOTween;
public delegate void AbilityCastAnim();
public class AbilityBase:MonoBehaviour {
	public AbilityCastAnim ac;
	public enum CastTarget{
		Self,Enemy,Friendly
	};
	public enum CastArea{
		Single,Area
	};
	protected string _name;
	protected string _description;
	protected	CastArea _releaseArea;
	protected	CastTarget _releaseTarget;
	protected Cast _cast;
	protected CardAttr _targetAttr;
	protected int _value;
	public int test;
	/// <summary>
	/// the count of round for this ability effect.-1 for permanent until cast subject dies;
	/// 0 for immediately effect like take damage;other positive value for buff and debuff.
	/// </summary>
	protected int _round;
}
