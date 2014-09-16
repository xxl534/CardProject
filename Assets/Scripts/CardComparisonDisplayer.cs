using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using Holoville.HOTween;

public class CardComparisonDisplayer : MonoBehaviour
{
		public UIWidget _container_firstAttr, _container_secondAttr, _container_comparison;
		public UIWidget _container_firstAbility, _container_secondAbility;
		public UISprite _icon_first, _icon_second;
		public UILabel _level_first, _level_second;
		private  AbilityDisplayer[] _firstAbilities, _secondAbilities;
		private UILabel[] _firstAttrs, _secondAttrs, _comparisonAttrs;
		private PropertyInfo[]  _attrs;
		private UIPanel _panel;
		private float _fadeDuration = 0.5f;
		private bool _canClick = false;

		void Awake ()
		{
				_panel = GetComponent<UIPanel> ();
				_firstAttrs = _container_firstAttr.GetComponentsInChildren<UILabel> ();
				System.Array.Sort<UILabel> (_firstAttrs, (x,y) => {
						return x.name.CompareTo (y.name);});
				_secondAttrs = _container_secondAttr.GetComponentsInChildren<UILabel> ();
				System.Array.Sort<UILabel> (_secondAttrs, (x,y) => {
						return x.name.CompareTo (y.name);});
				_comparisonAttrs = _container_comparison.GetComponentsInChildren<UILabel> ();
				System.Array.Sort<UILabel> (_comparisonAttrs, (x,y) => {
						return x.name.CompareTo (y.name);});

				_firstAbilities = _container_firstAbility.GetComponentsInChildren<AbilityDisplayer> ();
				_secondAbilities = _container_secondAbility.GetComponentsInChildren<AbilityDisplayer> ();
				System.Comparison<AbilityDisplayer> cmp = delegate(AbilityDisplayer a, AbilityDisplayer b) {
						int ax, ay, bx, by;
						ay = System.Convert.ToInt32 (a.transform.localPosition.y * 100);
						by = System.Convert.ToInt32 (b.transform.localPosition.y * 100);
						if (ay < by)
								return 1;
						if (ay > by)
								return -1;
						ax = System.Convert.ToInt32 (a.transform.localPosition.x * 100);
						bx = System.Convert.ToInt32 (b.transform.localPosition.x * 100);
						if (ax > bx)
								return 1;
						if (ax < bx)
								return -1;
						return 0;
				};
				System.Array.Sort<AbilityDisplayer> (_firstAbilities, cmp);
				System.Array.Sort<AbilityDisplayer> (_secondAbilities, cmp);

				_attrs = new PropertyInfo[_firstAttrs.Length];
				System.Type typeOfCard = typeof(ConcreteCard);
				for (int i = 0; i < _attrs.Length; i++) {
						_attrs [i] = typeOfCard.GetProperty (_firstAttrs [i].name);
				}

		}

		void Start ()
		{
				Clear ();
		}

		void OnClick ()
		{
				if (_canClick) {
						Hide ();
						_canClick = false;
				}
		}

		public void DisplayCardComparison (ConcreteCard second, ConcreteCard first=null)
		{
				int[] secondAttrValue = new int[_attrs.Length];
				int[] firstAttrValue = new int[_attrs.Length];
				for (int i = 0; i < _attrs.Length; i++) {
						secondAttrValue [i] = (int)_attrs [i].GetValue (second, null);
				}
				for (int i = 0; i < _attrs.Length; i++) {
						_secondAttrs [i].text = secondAttrValue [i].ToString ();
				}
				_container_secondAttr.gameObject.SetActive (true);
				if (first != null) {
						for (int i = 0; i < _attrs.Length; i++) {
								firstAttrValue [i] = (int)_attrs [i].GetValue (first, null);
						}
						for (int i = 0; i < _attrs.Length; i++) {
								_firstAttrs [i].text = firstAttrValue [i].ToString ();
						}

						for (int i = 0; i < _attrs.Length; i++) {
								UILabel attrLabel = _comparisonAttrs [i];
								int valueDelta = secondAttrValue [i] - firstAttrValue [i];
								string valueStr = "";
								if (valueDelta > 0) {
										attrLabel.color = Color.green;
										valueStr += "+";
								} else if (valueDelta < 0) {
										attrLabel.color = Color.red;
								} else {
										attrLabel.color = Color.white;
								}
								valueStr += valueDelta.ToString ();
								attrLabel.text = valueStr;
						}
						_container_comparison.gameObject.SetActive (true);
						_container_firstAttr.gameObject.SetActive (true);
				}

				for (int i = 0; i < second.abilities.Count; i++) {
						_secondAbilities [i].LoadAbility (second.abilities [i]);
				}
				if (first != null) {
						for (int i = 0; i < first.abilities.Count; i++) {
								Debug.Log ("first");
								_firstAbilities [i].LoadAbility (first.abilities [i]);
						}
				}

				_icon_second.spriteName = second.name;
				_icon_second.gameObject.SetActive (true);
				_level_second.text = second.level.ToString ();
				_level_second.gameObject.SetActive (true);
				_level_second.color = RarityColorStatic.rarityColors [(int)second.rarity];


				if (first != null) {
						_icon_first.spriteName = first.name;
						_icon_first.gameObject.SetActive (true);
						_level_first.text = first.level.ToString ();
						_level_first.gameObject.SetActive (true);
						_level_first.color = RarityColorStatic.rarityColors [(int)first.rarity];
				}

				gameObject.SetActive (true);
				HOTween.To (_panel, _fadeDuration, new TweenParms ().Prop ("alpha", 1f).Ease (EaseType.Linear).OnComplete (() => {
						_canClick = true;}));
		}

		void Hide ()
		{
				HOTween.To (_panel, _fadeDuration, new TweenParms ().Prop ("alpha", 0f).Ease (EaseType.Linear).OnComplete (delegate() {
						Clear ();
				}));
		}

		void Clear ()
		{
				_container_comparison.gameObject.SetActive (false);
				_container_firstAttr.gameObject.SetActive (false);
				_container_secondAttr.gameObject.SetActive (false);
				foreach (var item in _firstAbilities) {
						item.gameObject.SetActive (false);
				}
				foreach (var item in _secondAbilities) {
						item.gameObject.SetActive (false);
				}

				_icon_first.gameObject.SetActive (false);
				_icon_second.gameObject.SetActive (false);
				_level_first.gameObject.SetActive (false);
				_level_second.gameObject.SetActive (false);
				_panel.alpha = 0f;
				gameObject.gameObject.SetActive (false);
		}

}
