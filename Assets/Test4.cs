using UnityEngine;
using System.Collections;

public class Test4 : MonoBehaviour {
	UILabel label;
	string s="hello   the       world";
	void Awake()
	{
		label = GetComponent<UILabel> ();
		label.text=string.Join("\n",s.Split(new char[]{' '}, System.StringSplitOptions.RemoveEmptyEntries));
	
	}
}
