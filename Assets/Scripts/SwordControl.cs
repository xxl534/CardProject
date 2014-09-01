using UnityEngine;
using System.Collections;
using Holoville.HOTween;
public class SwordControl : MonoBehaviour {
	ParticleSystem _particle;
	UISprite _sword;

	float _anim_time=1.5f;
	// Use this for initialization
	void Awake()
	{
		_particle = GetComponentInChildren<ParticleSystem> ();
		_sword = GetComponentInChildren<UISprite> ();
		foreach (ParticleSystem ps in _particle.GetComponentsInChildren<ParticleSystem>()) {
			ps.startLifetime=_anim_time;
				}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Disappear(Operation oper)
	{
		Holoville.HOTween.Core.TweenDelegate.TweenCallback callback =delegate {
			oper();
	} ;
		_particle.Play ();
		gameObject.AddComponent<RunOnCondition> ().RunDelay (0.5f * _anim_time, delegate {
			HOTween.To(_sword.transform,0.5f*_anim_time,new TweenParms().Prop("localScale",Vector3.zero).Ease(EaseType.Linear).OnComplete(callback));
				});
	}

	public void Show()
	{
		_sword.transform.localScale = Vector3.zero;
		_particle.Play ();

		HOTween.To(_sword.transform,0.5f*_anim_time,new TweenParms().Prop("localScale",Vector3.one).Ease(EaseType.Linear));
	
	}
}
