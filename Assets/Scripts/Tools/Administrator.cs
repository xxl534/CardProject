using UnityEngine;
using System.Collections;

public class Administrator : MonoBehaviour {
//	public GameObject scripts;
	public string addScriptName;
	public string removeScriptName;
	[ContextMenu("Add Scripts")]
	void AddComponents()
	{
		System.Type T = System.Type.GetType (addScriptName);
		UIWidget[] childUI = GetComponentsInChildren<UIWidget> ();
		foreach (UIWidget widget in childUI) {
			widget.pivot= UIWidget.Pivot.TopLeft;
			if(widget.gameObject.GetComponent(T)==null)
			widget.gameObject.AddComponent(T);
				}
	}

	[ContextMenu("Remove Scripts")]
	void RemoveComponents()
	{
		Component com;
		System.Type T = System.Type.GetType (removeScriptName);
		UIWidget[] childUI = GetComponentsInChildren<UIWidget> ();
		foreach (UIWidget widget in childUI) {
			if((com=widget.gameObject.GetComponent(T))!=null)
				DestroyImmediate(com);
		}
	}


}
