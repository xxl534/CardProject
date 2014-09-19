using UnityEngine;
using System.Collections;

public class CompleteSlot : MonoBehaviour {
	private static float _expIncDuration=3.0f;

	public UISprite _cardIcon;
	public UILabel _cardName, _cardLevelLabel, _experienceLabel;
	public UISlider _experienceSlider;
	public ConcreteCard _card;

	private int _level,_experience,_expUpBound;

	public bool vacant
	{ get; set;}
	public void LoadConcreteCard(ConcreteCard concreteCard)
	{
		vacant = false;
		_card = concreteCard;
		_cardIcon.spriteName = concreteCard.name;
		_cardName .text= concreteCard.name;
		_level = concreteCard.level;
		_cardLevelLabel.text = "lv." + _level.ToString ();

		_experience = concreteCard.experience;
		_expUpBound=BaseCard._experienceTable[concreteCard.level-1];
		_experienceSlider.value = _experience / (float)_expUpBound;
		_experienceLabel.text = _experience.ToString () + "/" + _expUpBound.ToString ();
		gameObject.SetActive (true);
	}

	public void GainExperience(int experience)
	{
		_card.experience += experience;
		StartCoroutine (GainExperienceDynamic (experience, _expIncDuration));
	}

	IEnumerator GainExperienceDynamic(int experience,float duration)
	{
		float fExp = (float)_experience;
		float perSecond = experience / duration;
				int dstLevel = _card.level, dstExp = _card.experience;
				while (_level<dstLevel||_experience<dstExp) {
						fExp += perSecond*Time.deltaTime;
			_experience=(int)fExp;
						if (_experience >= _expUpBound) {
								while (_level<dstLevel) {
					_level++;
					_experience-=_expUpBound;
					fExp-=(float)_expUpBound;
					_expUpBound=BaseCard._experienceTable[_level-1];
								}
					
						}
			_cardLevelLabel.text = "lv." + _level.ToString ();
			_experienceSlider.value = _experience / (float)_expUpBound;
			_experienceLabel.text = _experience.ToString () + "/" + _expUpBound.ToString ();
			yield return null;
				}
				if (_experience > dstExp) {
			_experience=dstExp;
				}
		_cardLevelLabel.text = "lv." + _level.ToString ();
		_experienceSlider.value = _experience / (float)_expUpBound;
		_experienceLabel.text = _experience.ToString () + "/" + _expUpBound.ToString ();
		yield return null;
		}

	public void GainExperienceInstant()
	{
		StopCoroutine ("GainExperienceDynamic");
		_level=_card .level;
		_experience=_card.experience;
		_expUpBound=BaseCard._experienceTable[_level-1];
		_cardLevelLabel.text = "lv." + _level.ToString ();
		_experienceSlider.value = _experience / (float)_expUpBound;
		_experienceLabel.text = _experience.ToString () + "/" + _expUpBound.ToString ();
	}
}
