using UnityEngine;
using System.Collections;
public class StarCounterControl : MonoBehaviour {
	UILabel _counter;
	int _sum;
	int _count;
	// Use this for initialization
	void Awake()
	{
		_counter = GetComponent<UILabel> ();
		_sum = GameObject.FindGameObjectsWithTag (Tags.levelGate).Length * 3;
		_count = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public	void GainStar()
	{
		_count++;
		_counter.text = _count + "/" + _sum;
	}

	public void Save()
	{

	}

	public void LoadFromPlayerPrefs()
	{}
}
