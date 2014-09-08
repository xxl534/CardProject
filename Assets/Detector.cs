using UnityEngine;
using System.Collections;

public class Detector : MonoBehaviour {
	AbilityEntityShell  _abilityEntityShell;
	// Use this for initialization
	void Awake()
	{
		_abilityEntityShell=GetComponentInParent<AbilityEntityShell>();
	}


	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject==_abilityEntityShell.abilityEntity.targetCard.gameObject)
		{
			_abilityEntityShell.AttachTheTarget();
		}
	}
}
