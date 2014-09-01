using UnityEngine;
using System.Collections;

public class FlyStar : MonoBehaviour {
	// Use this for initialization
	Vector3 _lastPosition;
	ParticleSystem _particle_blow;
	ParticleSystem _particle_tail;
	void Awake()
	{
		foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>()) {
			if(ps.name.Equals("particle_blow"))
				_particle_blow=ps;
			else if(ps.name.Equals("particle_tail"))
				_particle_tail=ps;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (_lastPosition != null) {
			transform.up=(transform.position-_lastPosition).normalized;
				}
		_lastPosition = transform.position;
	}

	public void TurnOffTail()
	{
		_particle_tail.Stop ();
	}

	public void Blow()
	{
		_particle_blow.Play ();
	}
}
