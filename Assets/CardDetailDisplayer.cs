using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using Holoville.HOTween;

public class CardDetailDisplayer : MonoBehaviour {
	public UILabel _label_name,_label_level,_label_hp,_label_mp,_label_description;
	public Renderer _renderer_frame,_renderer_role;
	public AbilityDisplayer[] _abilitiesDisplayer;
	public GameObject _labelContainer;
	public Material[] _shellMaterials;
	public float _showTime;
	private UILabel[] _attrLabels;
	private Type _typeofBattleCard;
	private PropertyInfo[] _propertiesOfBattleCard;
	private Vector3 _position;
	void Awake()
	{
		_position=transform.position;
		_attrLabels = _labelContainer.GetComponentsInChildren<UILabel> ();
		_typeofBattleCard = typeof(BattleCard);
		_propertiesOfBattleCard=new PropertyInfo[_attrLabels.Length];
		for (int i = 0; i < _attrLabels.Length; i++) {
			_propertiesOfBattleCard[i]=_typeofBattleCard.GetProperty(_attrLabels[i].name);
				}

	}
	void Start()
	{
		gameObject.SetActive(false);
	}
	void OnClick()
	{
		HideCardDetail();
	}
	public void DisplayCardDetail(BattleCard card)
	{
		string cardName = card.concreteCard.name;
		cardName= string.Join ("\n", cardName.Split (new char[]{' '}, System.StringSplitOptions.RemoveEmptyEntries));
		_label_name.text = cardName;
		_label_level.text = card.concreteCard.level.ToString();
		_label_hp.text = card.health.ToString();
		_label_mp.text = card.mana.ToString();
		_label_description.text = card.concreteCard.description;
		for (int i = 0; i < _attrLabels.Length; i++) {
			_attrLabels[i].text=_propertiesOfBattleCard[i].GetValue(card,null).ToString();
				}
		_renderer_frame.material = _shellMaterials [(int)card.concreteCard.rarity];
		_renderer_role.material.mainTexture = card.concreteCard.roleTexture;
		for (int i = 0; i < card.abilities.Count&&i<_abilitiesDisplayer.Length; i++) {
			_abilitiesDisplayer[i].LoadAbility(card.abilities[i]);
				}

		gameObject.SetActive (true);
		HOTween.To(transform,_showTime,new TweenParms().Prop("position",new Vector3(6f,0,0),true).Ease(EaseType.Linear));
	}

	public void HideCardDetail()
	{
		HOTween.To (transform,_showTime*(transform.position-_position).magnitude/6f,new TweenParms().Prop("position",_position).Ease(EaseType.Linear).OnComplete(
			delegate() {
			foreach (var item in _abilitiesDisplayer) {
				item.gameObject.SetActive(false);
			}
			gameObject.SetActive (false);
		}));
	}




}
