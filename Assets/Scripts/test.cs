using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	void Awake()
	{
		C1 c1 = new C1 ();
		C2 c2 = new C2 ();
//		Debug.Log (c2.name);
		c2.print ();
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public class C1
{
	public string name;
	public C1()
	{
		name="baseClass";
	}
}

public class C2:C1
{
	public C2()
	{
		this.name="DerivedClass";
	}

	public void print()
	{
		Debug.Log (base.name);
		Debug.Log (name);
	}
}
