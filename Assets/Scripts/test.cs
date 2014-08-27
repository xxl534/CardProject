using System;
using System.Reflection;
using UnityEngine;


public class Test:MonoBehaviour
{
	public class MyFieldClassA
	{
		public string Field = "A Field";
	}
	
	public class MyFieldClassB
	{
		private string field = "B Field";
		public string Field 
		{
			get
			{
				return "hi";
			}
			set
			{
				if (field!=value)
				{
					field=value;
				}
			}
		}
	}
	 void Start()
	{
		MyFieldClassB myFieldObjectB = new MyFieldClassB();
		MyFieldClassA myFieldObjectA = new MyFieldClassA();
		
		Type myTypeA = typeof(MyFieldClassA);
		FieldInfo myFieldInfo = myTypeA.GetField("Field");
		
		Type myTypeB = typeof(MyFieldClassB);
		FieldInfo[] fields = myTypeB.GetFields (BindingFlags.NonPublic|BindingFlags.Instance);
		Debug.Log (fields.Length);
		foreach (FieldInfo fie in fields) {
					Debug.Log(string.Format("The value of the private field is: '{0}'", 
					                  fie.GetValue(myFieldObjectB)));

				}
//		FieldInfo myFieldInfo1 = myTypeB.GetField("field", 
//		                                          BindingFlags.NonPublic | BindingFlags.Instance);
//		
//		Debug.Log(string.Format( "The value of the public field is: '{0}'", 
//		                  myFieldInfo.GetValue(myFieldObjectA)));
//		Debug.Log(string.Format("The value of the private field is: '{0}'", 
//		                  myFieldInfo1.GetValue(myFieldObjectB)));
	}
}
