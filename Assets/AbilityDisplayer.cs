using UnityEngine;
using System.Collections;

public class AbilityDisplayer : MonoBehaviour {
public 	UILabel _abilityLevel;
 public 	Renderer _renderer_icon;
	bool _displayed;
	// Use this for initialization
	void Awake()
	{
		_displayed = false;

		}
	void Start()
	{
		gameObject.SetActive (false);
	}
	// Update is called once per frame
	void Update () {
	
	}
	public void LoadAbility(Ability ability)
	{
		Debug.Log("Load");
		_renderer_icon.material.mainTexture = ability.icon;
		_abilityLevel.text = ability.level.ToString ();
		gameObject.SetActive (true);
	}

//	void OnHover()
//	{
//		if (!_displayed) {
//				}
//	}
}
