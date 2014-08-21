using UnityEngine;
using System.Collections;

public delegate void Cast(Card from,Card to); 
public class BaseAbility : MonoBehaviour {
	public string name;
	public string description;
	public int value;
	public Cast cast;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
