using UnityEngine;
using System.Collections;

public class Test4 : MonoBehaviour {
	public bool b;
	public  UIGrid grid; 
	void Awake()
	{
//		Debug.Log("awke"+name);
//		label = GetComponent<UILabel> ();
//		label.text=string.Join("\n",s.Split(new char[]{' '}, System.StringSplitOptions.RemoveEmptyEntries));
	
	}

	void Update()
	{
		if (b) {
			b=false;
			grid.AddChild(transform);
		}
	}
}
