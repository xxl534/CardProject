using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
public static class AbilityVariable  {
	private static HashSet<string> variableStringSet;
	public static string maxValue="maxValue";
	public static string minValue="minValue";
	public static string interval="interval";
	public static string duration="duration";
	public static string restorativeValue="restorativeValue";
	public static string value="value";
	public static string dot="dot";

	static AbilityVariable()
	{
		variableStringSet=new HashSet<string>();
		FieldInfo[] fields=typeof(AbilityVariable).GetFields( BindingFlags.Public|BindingFlags.Static);
		foreach (var item in fields) {
			if(item.FieldType==typeof(string))
			{
				variableStringSet.Add(item.Name);
			}
				}
	}
	public static bool HasVariableString(string variableString)
	{
		return variableStringSet.Contains(variableString);
	}
}
