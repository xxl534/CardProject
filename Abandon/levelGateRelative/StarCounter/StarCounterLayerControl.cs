using UnityEngine;
using System.Collections;

public class StarCounterLayerControl : MonoBehaviour {
	public BigStarControl _bigStar;
	StarCounterControl _starCounter;

	void Awake()
	{
		_bigStar = GetComponentInChildren<BigStarControl> ();
		_starCounter = GetComponentInChildren<StarCounterControl> ();
	}

	public void GainStar()
	{
		_starCounter.GainStar ();
	}
}
