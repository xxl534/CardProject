using UnityEngine;
using System.Collections;

public class AbilityDisplayer : MonoBehaviour
{
		public 	UILabel _abilityLevel;
		public 	UISprite _icon;
		public AbilityDetailDisplayer _abilityDetailDisplayer;
		Ability _ability;
		bool _displayed = false;
		float _timeToShowDetail = 2f;
		float _ShowTimer = float.NegativeInfinity;
		
//		void Start ()
//		{
//				gameObject.SetActive (false);
//		}
		// Update is called once per frame
		void Update ()
		{
				_ShowTimer += Time.deltaTime;
				if (_ShowTimer > _timeToShowDetail && _displayed == false) {
						_abilityDetailDisplayer.DisplayerAbilityDetail (_ability, transform.position.y);
						_displayed = true;
				}
		}

		public void LoadAbility (Ability ability)
		{
				_ability = ability;
				_icon.spriteName=ability.name;
				_abilityLevel.text = ability.level.ToString ();
				gameObject.SetActive (true);
//		Debug.Log("loadabili");
		}

		void OnHover (bool isOver)
		{
				if (isOver) {
						_ShowTimer = 0;
				} else {
						if (_displayed) {
								_abilityDetailDisplayer.HideAbilityDetail ();
						}
						_displayed = false;
						_ShowTimer = float.NegativeInfinity;
				}
		}
}
