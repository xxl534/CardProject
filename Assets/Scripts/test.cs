using System;
using System.Reflection;
using UnityEngine;


public class Test:MonoBehaviour
{

	void Start()
	{

//
//		C1 c=new C1();
////		object ob=(object)(new int[]{20,3});
//		object ob=(object)14;
//		typeof(C1).GetProperty("si").SetValue(null,ob,null);	
//		Debug.Log(C1.si);
	 Debug.Log(	AbilityCastStatic.HasAbilityCast("physicalDamage"));
//		foreach(int i in c.i)
//			Debug.Log(i);
//		Debug.Log(ob==20);
//		Debug.Log((int)Enum.Parse(typeof(E1),"e1"));

	}
}

public enum E1
{
	e1,e2,e3
};
public class C1
{
	static int _si;
	E1 _e;
	int[] _i;
	public int[]  i
	{
		get{return _i;}
		set{_i=value;}
	}

	public static int si
	{
		get{return _si;}
		set{_si=value;}
	}
	public E1 e
	{
		get;
		set;
	}
}