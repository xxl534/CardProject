using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
public static class AbilityVariable  {
	private static HashSet<string> variableStringSet;
	public static string damage="damage";
	public static string maxDamage="maxDamage";
	public static string minDamage="minDamage";

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
