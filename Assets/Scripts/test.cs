﻿using System;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;

public class Test:MonoBehaviour
{

	void Start()
	{
		CardFactory cf=CardFactory.GetCardFactory();
		//		Int64 i=1;
//		int l=System.Convert.ToInt32(i);
//		typeof(C1).GetProperty("si").SetValue(null,l,null);

	}
}

public enum E1
{
	e1,e2,e3
};
public class C1
{
	public C1(){}
	public C1(int i)
	{
		typeof(C1).GetProperty("e").SetValue(this,i,null);
	}
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
		private set;
	}
}