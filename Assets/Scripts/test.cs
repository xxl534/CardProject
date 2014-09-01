using System;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;

public class Test:MonoBehaviour
{

	void Start()
	{
		Dictionary<C1,int> dic = new Dictionary<C1, int> ();
		C1 c1 = new C1 ();
		C1 c2 = new C1 ();

		dic [c1] = 1;
		dic [c2] = 2;
//		dic.Add (c1, 1);
//		dic.Add (c1, 2);
		Debug.Log (dic.Count);
//		C1 c=new C1();
////		object ob=(object)(new int[]{20,3});
//		object ob=(object)14;
//		typeof(C1).GetProperty("si").SetValue(null,ob,null);	
//		Debug.Log(C1.si);
//	 Debug.Log(	AbilityCastStatic.HasAbilityCast("physicalDamage"));
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