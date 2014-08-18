using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class StarSlotControl : MonoBehaviour {
	public StarControl _childStar;
	LevelGateControl _level;
	// Use this for initialization
	void Awake(){
		_childStar = GetComponentInChildren<StarControl> ();
		_level = GetComponentInParent<LevelGateControl> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FillStar(bool fill)
	{
		_childStar.gameObject.SetActive (fill);
	}

	public void GainStar()
	{
		_childStar.Activate ();
	}

	public Vector3[] GetFlyPath()
	{
		Debug.Log ("_level" + _level.name);
		return _level.GetStarFlyPath (this);
	}

	public void Unlock()
	{
		gameObject.SetActive (true);
		GetComponentInChildren<ParticleSystem> ().Play();
		UISprite sprite= GetComponent<UISprite> ();
		sprite.alpha = 0;
		HOTween.To (sprite, 1f, new TweenParms ().Prop ("alpha", 1f).Ease (EaseType.Linear));

	}
}
