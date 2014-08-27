using UnityEngine;
using System.Collections;

public class SetAbilityCast : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[ContextMenu("SetCastMethod")]
	public void SetCastMethod()
	{
		BaseAbility ba = GetComponent<BaseAbility> ();
		
	}


}
