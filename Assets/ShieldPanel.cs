using UnityEngine;
using System.Collections;

public class ShieldPanel : MonoBehaviour {
	private int _counter;
	void Awake()
	{
		_counter=0;
		gameObject.SetActive(false);
	}

	public void Activate()
	{
		if(_counter==0)
			gameObject.SetActive(true);
		_counter++;
	}

	public void Deactivate()
	{
		if(_counter==1)
		{
			gameObject.SetActive(false);
		}
		if(--_counter<0)
		{
			_counter=0;
		}
	}
}
