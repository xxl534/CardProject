using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using System.Collections.Generic;

public class BattleCardShell : MonoBehaviour
{
	#region Static fields
		static float _toggleDistance = 0.3f;
		static float _clickInterval = 0.5f;
		static float _getHurtTime = 0.2f;
		static float _castTime = 0.4f;
		static float _shellRotateTime = 1f;
	#endregion

	#region Instancial fields
		private BattleControl _battleController;
		public ShellType  _shellType;
		public GameObject _roleMesh;
		public GameObject _shellMesh;
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
		private bool _displayed;
		private float _displayTimer = 0f;
	private BattleCardShell[]  _shellQueue;
		private static float _showTime = 0.3f;
		private static float _displayTime = 2f;
		private static float _firstRowAbilityShowTime = 0.2f;
		private static float _secondRowAbilityShowTime = 0.35f;
		private static float _firstRowAbilityShowDistance = 0.8f;
		private static float _secondRowAbilityShowDistance = 1.4f;
	private static float _deadTime = 0.5f;
		private static int _abilityCountPerRow = 3;
		float _clickTimer = 0f;
	#endregion

	#region Properties
		public Vector3 originalLocalPosition {
				get{ return _originalLocalPosition;}
		}

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

		public BattleCardShell[] shellQueue {
				get{ return _shellQueue;}
		}
	#endregion

		void Awake ()
		{
				_battleController = GetComponentInParent<BattleControl> ();
				_originalLocalPosition = transform.localPosition;
				_battleCard = GetComponent<BattleCard> ();
				_shellQueue = (_shellType == ShellType.Player) ? _battleController._playerCardShellSet : _battleController._enemyCardShellSet;
				_vacant = true;
				_displayed = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
				_clickTimer += Time.deltaTime;
				_displayTimer += Time.deltaTime;
				if (_displayed && _displayTimer > _displayTime) {
						_battleController.cardDetailDisplayer.DisplayCardDetail (_battleCard);
						_displayTimer = -1000f;
				}
		}

//	void OnMouseOver()
//	{
//		Debug.Log("on");
//			_showCardTimer+=Time.deltaTime;
//	}
		void OnHover (bool IsOver)
		{
				_displayed = IsOver;
				if (_displayed) {
						_displayTimer = 0f;
				}
		}

		void OnClick ()
		{
				if (_clickTimer > _clickInterval) {
						MouseClick ();
						_clickTimer = 0f;
				}
		}

		void OnTriggerEnter (Collider other)
		{
				AbilityEntityShell abilityES = other.GetComponentInParent<AbilityEntityShell> ();
				if (abilityES != null && abilityES.abilityEntity.targetCard == _battleCard) {
						_battleCard.cardEffected (abilityES);
				}
		}

		void MouseClick ()
		{
//				Debug.Log ("BattleCardShell click : " + name);
				_battleController.CardClick (this);
	
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
						StartCoroutine (HideFirstRowAbility (_secondRowAbilityShowTime - _firstRowAbilityShowTime));
						StartCoroutine (HideAfterTime (_secondRowAbilityShowTime));
				}
		}

		void HideAbilitiesOnly ()
		{
				if (_battleCard.concreteCard.abilities.Count <= _abilityCountPerRow) {//Only one row of 'AbilityShell'.
						foreach (var item in _firstRowAbilityShells) {
								if (item.vacant == false) {
										item.Hide (_firstRowAbilityShowTime);
								}
						}
				} else {//Have two rows of 'AbilityShell'.
						foreach (var item in _secondRowAbilityShells) {
								if (item.vacant == false) {
										item.Hide (_secondRowAbilityShowTime);
								}
						}
						StartCoroutine (HideFirstRowAbility (_secondRowAbilityShowTime - _firstRowAbilityShowTime));
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

		IEnumerator HideFirstRowAbility (float time)
		{
				yield return new WaitForSeconds (time);
				foreach (var item in _firstRowAbilityShells) {
						if (item.vacant == false) {
								item.Hide (_firstRowAbilityShowTime);
						}
				}
//				StartCoroutine (HideAfterTime (_firstRowAbilityShowTime));
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

		public void Clear ()
		{
				_vacant = true;
				gameObject.SetActive (false);
				_battleCard.Clear ();
				foreach (var item in _firstRowAbilityShells) {
						item.Clear ();
						item.gameObject.SetActive (false);
				}
				foreach (var item in _secondRowAbilityShells) {
						item.Clear ();
						item.gameObject.SetActive (false);
				}
		}

		public void LoadCard (ConcreteCard concreteCard)
		{
				_battleCard.LoadConcreteCard (concreteCard);
				_vacant = false;
				_hasCast = false;
				_shellMesh.renderer.material = _battleController._shellMaterials [(int)concreteCard.rarity];
				_roleMesh.renderer.material.mainTexture = concreteCard.roleTexture;
				_label_hp.text = _battleCard.health.ToString ();
				_label_mp.text = _battleCard.mana.ToString ();
				_label_hp.alpha = 0;
				_label_mp.alpha = 0;
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

		public void TurnToFront ()
		{
				HOTween.To (transform, _shellRotateTime, new TweenParms ().Prop ("localRotation", Quaternion.identity).Ease (EaseType.Linear).OnStart (delegate() {
						_battleController.shieldPanel.Activate ();
				}).OnComplete (delegate() {
						_battleController.shieldPanel.Deactivate ();
						HOTween.To (_label_hp, 0.5f, new TweenParms ().Prop ("alpha", 1f).Ease (EaseType.Linear));
						HOTween.To (_label_mp, 0.5f, new TweenParms ().Prop ("alpha", 1f).Ease (EaseType.Linear));
				}));
		}
	
		public void CardRoleDead ()
		{
		HOTween.To (transform, _deadTime, new TweenParms ().Prop ("localScale", Vector3.one*0.001f).Ease (EaseType.Linear).OnStart (()=>{_battleController.shieldPanel.Activate();}).OnComplete (()=>{
			if(_shellType== ShellType.Enemy){
				_battleController.Drop(this);
			}
			Clear();
			_battleController.CheckVacantShell ();
			_battleController.shieldPanel.Deactivate();
		}));
		}


		public void RoundStart ()
		{
		_battleCard.health += _battleCard.healthResilience;
		_battleCard.mana += _battleCard.magicResilience;
		_hasCast = false;
		foreach (var item in _battleCard.abilities) {
			_battleCard.abilityCDTable[item]++;
				}
		}

		public void RoundEnd ()
		{
		}

		public void HighLight ()
		{
		}

		public bool HasAvailableAbility ()
		{
				foreach (var item in _battleCard.abilityCDTable) {
						if (item.Value >= item.Key.cooldown && item.Key.mana <= _battleCard.mana) {
								return true;
						}
				}
				return false;
		}

		public void Deny ()
		{
		}

		public void CastAbility (Ability ability, BattleCard target)
		{
//		Debug.Log(9);
				float time = 0f;
				if (_shellType == ShellType.Player) {
						HideAbilitiesOnly ();
						if (_firstRowAbilityShells.Length == 0) {
								time = 0;
						} else if (_battleCard.abilities.Count > _abilityCountPerRow) {
								time = _secondRowAbilityShowTime;
						} else {
								time = _firstRowAbilityShowTime;
						}
				}
				if (ability.targetArea == TargetArea.Multiple) {
						foreach (var item in target.shell._shellQueue) {
								if (item.vacant == false) {
										StartCoroutine (CastAbilityAfter (ability, item.battleCard, time));
								}
						}
				} else {
						StartCoroutine (CastAbilityAfter (ability, target, time));
				}
				_hasCast = true;
				StartCoroutine (HideAfterTime (time + _castTime));
				_battleCard.abilityCDTable [ability] = 0;
		}

		IEnumerator CastAbilityAfter (Ability ability, BattleCard target, float time)
		{
//		Debug.Log(10);
		_battleController.shieldPanel.Activate ();
//		Debug.Log("ya");
				yield return new WaitForSeconds (time);
				AbilityEntityShell prefab = Resources.Load<AbilityEntityShell> (ResourcesFolderPath.prefabs_ability + "/" + ability.name);
				AbilityEntityShell abilityEntityShell = Instantiate (prefab) as AbilityEntityShell;
//		Debug.Log(abilityEntityShell.name);
				abilityEntityShell.Init (ability.abilityCast (ability, _battleCard, target));
		_battleController.shieldPanel.Deactivate ();
//		Debug.Log("ha");
				yield return null;
		}

		public void GetHurt ()
		{
//				if (transform.localPosition == _originalLocalPosition) {
//						StartCoroutine (Reposition ());
//				}
//				HOTween.To (transform, _getHurtTime, new TweenParms ().Prop ("position", Random.onUnitSphere * 0.4f, true).Ease (EaseType.Linear));
//		HOTween.To (transform, _getHurtTime, new TweenParms ().Prop ("rotation", Quaternion.Euler(new Vector3(30f,0,0)), true).Loops(2, LoopType.Yoyo).Ease( EaseType.Linear));
		HOTween.To (transform, _getHurtTime, new TweenParms ().Prop ("localRotation", Quaternion.Euler(new Vector3(30f,0,0)), true).Ease( EaseType.Linear).OnComplete(()=>{
			HOTween.To (transform,_getHurtTime,new TweenParms().Prop("localRotation",Quaternion.identity).Ease(EaseType.Linear));
            		}));
		}

//		IEnumerator Reposition ()
//		{
//				yield return new WaitForSeconds (0.1f);
//				while (transform.localPosition!=_originalLocalPosition) {
//						transform.localPosition = Vector3.Lerp (transform.localPosition, _originalLocalPosition, 0.3f);
//						yield return null;
//				}
//				yield return null;
//		}
}
