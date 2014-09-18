using UnityEngine;
using System.Collections;
using System;

public class MapLayerControl : MonoBehaviour ,IComparable<MapLayerControl> {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int CompareTo(MapLayerControl other)
	{
		return name.CompareTo (other.name);
	}
}
