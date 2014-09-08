using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using System.Collections.Generic;

public class BattleCardShell : MonoBehaviour
{
	#region Static fields
		static float _toggleDistance = 0.3f;
		static float _showCardTimer = 0f;
		static float _showCardTime = 2f;
		static float _clickInterval = 0.5f;
	#endregion

	#region Instancial fields
		public BattleControl _battleController;
		public ShellType  _shellType;
		public GameObject _role;
		public GameObject _shell;
		public UILabel _label_hp, _label_mp;
		public AbilityShell[] _firstRowAbilityShells;
		public AbilityShell[] _secondRowAbilityShells;
		private BattleCard _battleCard;
		Vector3 _originalLocalPosition;
		/// <summary>
		///If the card has been used,this shell is vacant,When reload a new card to this battle card shell "vacant" is false; 
		/// </summary>
		private bool _vacant;
		/// <summary>
		/// Every card has a chance to cast an ability when a new round started.
		/// If a card has casted an ability,it's 'hasCast' =true.
		/// </summary>
		private bool _hasCast;
		private static float _showTime = 0.3f;
		private static float _firstRowAbilityShowTime = 0.2f;
		private static float _secondRowAbilityShowTime = 0.35f;
		private static float _firstRowAbilityShowDistance = 0.8f;
		private static float _secondRowAbilityShowDistance = 1.4f;
		private static int _abilityCountPerRow = 3;
		float _clickTimer = 0f;
	#endregion

	#region Properties
		public bool vacant {
				get{ return _vacant;}
//		set{_vacant=value;}
		}

		public bool hasCast {
				get{ return _hasCast;}
		}

		public BattleCard battleCard {
				get{ return _battleCard;}
		}

		public BattleControl battleController {
				get{ return _battleController;}
		}

		public ShellType shellType {
				get{ return _shellType;}
		}
	#endregion

		void Awake ()
		{
				_battleController = GetComponentInParent<BattleControl> ();
				_originalLocalPosition = transform.localPosition;
				_battleCard = GetComponent<BattleCard> ();

				GetComponentsInChildren<AbilityShell> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				_clickTimer += Time.deltaTime;
				if (_showCardTimer > _showCardTime)
						_battleController.ShowCardDetail (_battleCard);
		}

//	void OnMouseOver()
//	{
//		Debug.Log("on");
//			_showCardTimer+=Time.deltaTime;
//	}
		void OnHover ()
		{
//		Debug.Log("hover");
		}

		void OnClick ()
		{
				Debug.Log ("BattleCardShell click : " + name);
				MouseClick ();
		}
//	void OnMouseExit()
//	{
//		Debug.Log("mouseOut");
//	_showCardTimer=0;
//		_battleController.StopShowCardDetail();
//	}

//	void OnMouseOver()
//	{
//		Debug.Log("over");
//	}
//	void OnMouseExit()
//	{
//		Debug.Log("ex");
//	}
		void MouseClick ()
		{
				if (_clickTimer > _clickInterval) {
						_battleController.CardClick (this);
						_clickTimer = 0f;

				}
		}

		public void Show ()
		{
				HOTween.To (transform, _showTime, new TweenParms ().Prop ("localPosition", _originalLocalPosition + new Vector3 (0, _toggleDistance, 0)).Ease (EaseType.EaseInOutQuad)
		            .OnStart (delegate() {
						_battleController.shieldPanel.Activate ();
				}).OnComplete (delegate() {
						foreach (var item in _firstRowAbilityShells) {
								if (item.vacant == false) {
										item.Show (_firstRowAbilityShowDistance, _firstRowAbilityShowTime);
								}
						}
						foreach (var item in _secondRowAbilityShells) {
								if (item.vacant == false) {
										item.Show (_secondRowAbilityShowDistance, _secondRowAbilityShowTime);
								}
						}
						_battleController.shieldPanel.Deactivate ();
				}
				));
		}

		public void Hide ()
		{
				if (_battleCard.concreteCard.abilities.Count <= _abilityCountPerRow) {//Only one row of 'AbilityShell'.
						foreach (var item in _firstRowAbilityShells) {
								if (item.vacant == false) {
										item.Hide (_firstRowAbilityShowTime);
								}
						}
						StartCoroutine (HideAfterTime (_firstRowAbilityShowTime));
				} else {//Have two rows of 'AbilityShell'.
						foreach (var item in _secondRowAbilityShells) {
								if (item.vacant == false) {
										item.Hide (_secondRowAbilityShowTime);
								}
						}
						StartCoroutine (HideFirstRowAbilityAndCard (_secondRowAbilityShowTime - _firstRowAbilityShowTime));
				}
		}

		IEnumerator HideAfterTime (float time)
		{
				yield return new WaitForSeconds (time);
				HOTween.To (transform, _showTime, new TweenParms ().Prop ("localPosition", _originalLocalPosition).Ease (EaseType.EaseInOutQuad)
		            .OnStart (delegate() {
						_battleController.shieldPanel.Activate ();
				}).OnComplete (delegate() {
						_battleController.shieldPanel.Deactivate ();
				}));
				yield return null;
		}

		IEnumerator HideFirstRowAbilityAndCard (float time)
		{
				yield return new WaitForSeconds (time);
				foreach (var item in _firstRowAbilityShells) {
						if (item.vacant == false) {
								item.Hide (_firstRowAbilityShowTime);
						}
				}
				StartCoroutine (HideAfterTime (_firstRowAbilityShowTime));
				yield return null;
		}

		public bool CardInteraction (Component other)
		{
				if (this == other)
						return true;
//		if(other.GetType()==typeof(BattleCardShell))
//		{
//			Debug.Log("action deny");
//			return false;
//		}
//		else if(other.GetType()==typeof(BossControl))
//		{
//			Debug.Log("attack boss");
//			BossControl boss=other as BossControl;
//			boss.Injure(3);
//			_vacant=true;
//			gameObject.SetActive(false);
//			return true;
//		}
				return false;
		}

		public void LoadCard (ConcreteCard concreteCard)
		{
				_battleCard.LoadConcreteCard (concreteCard);
				_vacant = false;
				_hasCast = false;
				_shell.renderer.material = _battleController._shellMaterials [(int)concreteCard.rarity];
				_role.renderer.material.mainTexture = concreteCard.roleTexture;
				_label_hp.text = _battleCard.health.ToString ();
				_label_mp.text = _battleCard.mana.ToString ();
				transform.localRotation = Quaternion.Euler (new Vector3 (0, 180f, 0));
				if (_shellType == ShellType.Player) {//Load 'AbilityShell'
						if (_battleCard.abilities.Count <= _abilityCountPerRow) {
								for (int i = 0; i < _battleCard.abilities.Count; i++) {
										_firstRowAbilityShells [i].LoadAbility (_battleCard.abilities [i]);
								}
						} else {
								int index = 0;
								for (int i = 0; i < _abilityCountPerRow; i++) {
										_secondRowAbilityShells [i].LoadAbility (_battleCard.abilities [index]);
										index++;
								}
								for (int i = 0; i < _abilityCountPerRow&&index<_battleCard.abilities.Count; i++) {
										_firstRowAbilityShells [i].LoadAbility (_battleCard.abilities [index]);
										index++;
								}
						}
				}
				transform.localPosition = _originalLocalPosition;
				gameObject.SetActive (true);
		}

		public void CardRoleDead ()
		{
				_vacant = true;
				gameObject.SetActive (false);
				_battleController.CheckVacantShell ();
		}

		public void RoundStart ()
		{
		}

		public void RoundEnd ()
		{
		}

		public void HighLight ()
		{
		}

		public bool HasAvailableAbility ()
		{
				foreach (var item in _firstRowAbilityShells) {
						if (!item.vacant && item.available) {
								return true;
						}
				}
				if (battleCard.abilities.Count > _abilityCountPerRow) {
						foreach (var item in _secondRowAbilityShells) {
								if (!item.vacant && item.available) {
										return true;
								}
						}
				}
				return false;
		}

		public void Deny ()
		{
		}
}
