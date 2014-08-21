using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using System;
using System.Reflection;

public delegate void CastAbility(Card from,Card to);
public enum CastTarget
{
	Self,
	Enemy,
	Friendly
}
;

public enum CastArea
{
	Single,
	Area
}
;
public class AbilityBase:MonoBehaviour
{
	public  CastAbility cast;

		public  string _name;

		public string name {
				get{ return _name;}
				set{ _name = value;}
		}

		public string _description;

		public string description {
				get{ return _description;}
				set{ _description = value;}
		}

//		protected	CastArea _releaseArea;
//
//		public CastArea releaseArea {
//				get{ return _releaseArea;}
//				set{ _releaseArea = value;}
//		}

//		protected	CastTarget _releaseTarget;
//
//		public CastTarget releaseTarget {
//				get{ return _releaseTarget;}
//				set{ _releaseTarget = value;}
//		}

		public  string  _targetAttr;

//		public string targetAttr {
//				get{ return _targetAttr;}
//				set{ _targetAttr = value;}
//		}

		public object _baseValue;

		public object baseValue {
				get{ return _baseValue;}
				set{ _baseValue = value;}
		}

		public bool _relative;

		public bool relative {
				set{ _relative = value;}
				get {
						return _relative;
				}
		}
		
	public bool _proportional;

	public bool proportional
	{
		get{return _proportional;}
		set{_proportional=value;}
	}
		/// <summary>
		/// the count of round for this ability effect.-1 for permanent until cast subject dies;
		/// 0 for immediately effect like take damage;other positive value for buff and debuff.
		/// </summary>
		protected int _baseRound;
	public int baseRound
	{
		get{return _baseRound;}
		set{_baseRound=value;}
	}
	public AbilityBase(){}
	protected AbilityBase(AbilityBase baseAbility)
	{
		_name = baseAbility.name;
		_description = baseAbility.description;
		_relative = baseAbility.relative;
		_proportional = baseAbility.proportional;
		_baseValue = baseAbility.baseValue;
		_baseRound = baseAbility.baseRound;
		}
		public static void CastAbility (object target, string field, object value, bool relative, bool proportional)
		{
				Type t = target.GetType ();
				FieldInfo fieldInfo;
				PropertyInfo propertyInfo;
				if ((fieldInfo = t.GetField (field)) != null) {
						if (relative) {
								object orgValue = fieldInfo.GetValue (target);
								Type valueType = orgValue.GetType ();
								MethodInfo operMethod;
								if (proportional) {
										operMethod = valueType.GetMethod ("op_Multiply");
								} else {
										operMethod = valueType.GetMethod ("op_Addition");
								}
								if (operMethod != null && operMethod.IsSpecialName) {
										try {
												object finalValue = operMethod.Invoke (null, new object[] {
														orgValue,
														value
												});

												fieldInfo.SetValue (target, finalValue);
										} catch {
												Debug.Log ("the value parameter '" + value.ToString () + "' does not have the same type as field '" + fieldInfo.Name + "' of " + t.Name + " '" + target.ToString () + "' does.");
												return;
										}

								} else {
										Debug.Log ("The type of field '" + fieldInfo.Name + "' of class " + t.Name + " doesn't implement add operator,parameter relative 'true' is illegal.");
								}
						} else {
								try {
										fieldInfo.SetValue (target, value);
								} catch {
										Debug.Log ("The value parameter '" + value.ToString () + "' cannot be converted and stored in the field '" + fieldInfo.Name + "' of " + t.Name + " '" + target.ToString () + "'.");
										return;
								}
						}
				} else if ((propertyInfo = t.GetProperty (field)) != null) {
						MethodInfo setMethod = propertyInfo.GetSetMethod ();
						if (setMethod == null) {
								Debug.Log ("The property '" + propertyInfo.Name + "' of class '" + t.Name + "'doesn't have set method");
								return;
						}
						if (relative) {

								MethodInfo getMethod = propertyInfo.GetGetMethod ();
								if (getMethod == null) {
										Debug.Log ("The property '" + propertyInfo.Name + "' of class '" + t.Name + "'doesn't have get method");
										return;
								}
								object orgValue = getMethod.Invoke (target, null);
								MethodInfo operMethod;
								if (proportional) {
										operMethod = orgValue.GetType ().GetMethod ("op_Multiply");
								} else {
										operMethod = orgValue.GetType ().GetMethod ("op_Addition");
								}
								if (operMethod != null && operMethod.IsSpecialName) {
										try {
												object finalValue = operMethod.Invoke (null, new object[] {
														orgValue,
														value
												});
												setMethod.Invoke (target, new object[]{finalValue});
										} catch {
												Debug.Log ("the value parameter '" + value.ToString () + "' does not have the same type as property  '" + propertyInfo.Name + "' of " + t.Name + " '" + target.ToString () + "' does.");
												return;
										}
								} else {
										Debug.Log ("The type of property '" + fieldInfo.Name + "' of class " + t.Name + " doesn't implement add operator,parameter relative 'true' is illegal.");
								}
						} else {
								try {
										setMethod.Invoke (target, new object[]{ value});
								} catch {
										Debug.Log ("The value parameter '" + value.ToString () + "' cannot be converted and set into the property '" + propertyInfo.Name + "' of " + t.Name + " '" + target.ToString () + "'.");
										return;
								}
						}
				} else {
						Debug.Log ("The class '" + t.Name + "' doesn't have field or property '" + field + "'");
						return;
				}
		}
//
//		public virtual void AffectTarget (Card card)
//		{
//				CastAbility (card, targetAttr, baseValue, relative,pr);
//		}
}