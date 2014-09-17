using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

public class BagManagement : MonoBehaviour
{
		
		static System.Comparison<Transform> gridSort_byLevel, gridSort_byLevelReverse,
				gridSort_byRarity, gridSort_byRarityReverse;
		public UILabel _playerName, _coin, _level, _experience;
		public UISlider _experienceSlider;
		public GameObject _bagSlotPrefab;
		public UIGrid _grid;
		public List<BattleSlot> _battleSlots;
		public ShieldPanel _shieldPanel;
		private List<BagSlot> _bagSlots;
		private UIScrollView _scrollView;
		private PlayerControl _player;
		private BagSlot _selectBagSlot;
		private BattleSlot _selectBattleSlot;
		private CardComparisonDisplayer _cardComparisonDisplayer;
		private float _fadeDuration = 0.3f;
		private int _coinAmount;

		public int coinAmount {
				get{ return _coinAmount;}
				set{ _coinAmount = value;}
		}

		static BagManagement ()
		{
				gridSort_byLevel = delegate(Transform x, Transform y) {
						ConcreteCard cardX = x.GetComponent<BagSlot> ().concreteCard;
						ConcreteCard cardY = y.GetComponent<BagSlot> ().concreteCard;
						if (cardX.level > cardY.level) {
								return 1;
						}
						if (cardX.level < cardY.level) {
								return -1;
						}
						if (cardX.experience > cardY.experience) {
								return 1;
						}
						if (cardX.experience < cardY.experience) {
								return -1;
						}
						return 0;
				};
				gridSort_byLevelReverse = delegate(Transform x, Transform y) {
						return -gridSort_byLevel (x, y);
				};
				gridSort_byRarity = delegate(Transform x, Transform y) {
						ConcreteCard cardX = x.GetComponent<BagSlot> ().concreteCard;
						ConcreteCard cardY = y.GetComponent<BagSlot> ().concreteCard;
						if (cardX.rarity > cardY.rarity) {
								return 1;
						}
						if (cardX.rarity < cardY.rarity) {
								return -1;
						}
						return gridSort_byLevel (x, y);
				};
				gridSort_byRarityReverse = delegate(Transform x, Transform y) {
						return -gridSort_byRarity (x, y);
				};
		}

		void Awake ()
		{
				_player = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerControl> ();
				_bagSlots = new List<BagSlot> ();
				_scrollView = GetComponentInChildren<UIScrollView> ();
				_cardComparisonDisplayer = GetComponentInChildren<CardComparisonDisplayer> ();
		}

		public void Load ()
		{
				_playerName.text = _player.playerName;
				_coinAmount = _player.coins;
				_coin.text = _coinAmount.ToString ();
				_level.text = "Lv." + _player.level.ToString ();
				_experience.text = _player.experience.ToString () + "/" + BaseCard.experienceTable [_player.level - 1].ToString ();
				_experienceSlider.value = _player.experience / (float)(BaseCard._experienceTable [_player.level - 1]);
				
				for (int i = 0; i < _player.playCardSet.Count; i++) {
						if (_player.playCardSet [i] != null) {
								_battleSlots [i].LoadConcreteCard (_player.playCardSet [i]);
						}
						_battleSlots [i].Index = i;
				}

				for (int i = 0; i < _player.cardBag.Count; i++) {
						GameObject bagSlotGO = Instantiate (_bagSlotPrefab)as GameObject;
						bagSlotGO.GetComponent<UIDragScrollView> ().scrollView = _scrollView;
						BagSlot bagSlot = bagSlotGO.GetComponent<BagSlot> ();
						bagSlot.LoadConcreteCard (_player.cardBag [i]);
						_grid.AddChild (bagSlotGO.transform);
						bagSlotGO.transform.localScale = Vector3.one;
						_bagSlots.Add (bagSlot);
				}
		}

		public void SlotClick (BattleSlot battleSlot)
		{
				if (_selectBattleSlot == null) {
						if (_selectBagSlot != null) {
								_selectBagSlot.Deselect ();
								_selectBagSlot = null;
						}
						_selectBattleSlot = battleSlot;
						battleSlot.Select ();
				} else {//has selected a battleSlot
						if (_selectBattleSlot.Index == battleSlot.Index) {//Click the same slot,deselect
								battleSlot.Deselect ();
								_selectBattleSlot = null;
						} else if (_selectBattleSlot.concreteCard == null) {
								_selectBattleSlot.Deselect ();
								_selectBattleSlot = battleSlot;
								_selectBattleSlot.Select ();
						} else {
								_selectBattleSlot.Deselect ();
								ConcreteCard first = _selectBattleSlot.concreteCard, second = battleSlot.concreteCard;
								int indexFst = _selectBattleSlot.Index, indexSec = battleSlot.Index;
								_player.playCardSet [indexFst] = second;
								_player.playCardSet [indexSec] = first;
								HOTween.To (_selectBattleSlot.slotBody, _fadeDuration, new TweenParms ().Prop ("alpha", 0f).Ease (EaseType.Linear).OnStart (() => {
										_shieldPanel.Activate ();}).OnComplete (() => {  
										if (second != null) {
												_selectBattleSlot.LoadConcreteCard (second);
												HOTween.To (_selectBattleSlot.slotBody, _fadeDuration, new TweenParms ().Prop ("alpha", 1f).Ease (EaseType.Linear).OnComplete (() => {  
														_shieldPanel.Deactivate ();
														_selectBattleSlot = null;
												}));
										} else {
												_shieldPanel.Deactivate ();
												_selectBattleSlot.Unload ();
												_selectBattleSlot = null;
										}
								}));
								HOTween.To (battleSlot.slotBody, _fadeDuration, new TweenParms ().Prop ("alpha", 0f).Ease (EaseType.Linear).OnStart (() => {
										_shieldPanel.Activate ();}).OnComplete (() => {  
										battleSlot.LoadConcreteCard (first);
										HOTween.To (battleSlot.slotBody, _fadeDuration, new TweenParms ().Prop ("alpha", 1f).Ease (EaseType.Linear).OnComplete (() => {  
												_shieldPanel.Deactivate ();
										}));
								}));
						}
				} 
		}

		public void SlotClick (BagSlot bagSlot)
		{
				if (_selectBattleSlot == null) {
						if (_selectBagSlot != null) {
								_selectBagSlot.Deselect ();
						}
						_selectBagSlot = bagSlot;
						_selectBagSlot.Select ();
				} else {
						_selectBattleSlot.Deselect ();
						ConcreteCard first = _selectBattleSlot.concreteCard, second = bagSlot.concreteCard;
						int index = _selectBattleSlot.Index;
						_player.playCardSet [index] = second;
						_player.cardBag.Remove (second);
						if (first != null) {
								_player.cardBag.Add (first);
						}
						HOTween.To (_selectBattleSlot.slotBody, _fadeDuration, new TweenParms ().Prop ("alpha", 0f).Ease (EaseType.Linear).OnStart (() => {
								_shieldPanel.Activate ();}).OnComplete (() => {  
								_selectBattleSlot.LoadConcreteCard (second);
								HOTween.To (_selectBattleSlot.slotBody, _fadeDuration, new TweenParms ().Prop ("alpha", 1f).Ease (EaseType.Linear).OnComplete (() => {  
										_shieldPanel.Deactivate ();
										_selectBattleSlot = null;
								}));
						}));
						Debug.Log (bagSlot.slotBody == null);
						HOTween.To (bagSlot.slotBody, _fadeDuration, new TweenParms ().Prop ("alpha", 0f).Ease (EaseType.Linear).OnStart (() => {
								_shieldPanel.Activate ();}).OnComplete (() => { 
								if (first != null) {
										bagSlot.LoadConcreteCard (first);
										HOTween.To (bagSlot.slotBody, _fadeDuration, new TweenParms ().Prop ("alpha", 1f).Ease (EaseType.Linear).OnComplete (() => {  
												_shieldPanel.Deactivate ();
										}));
								} else {
										_shieldPanel.Deactivate ();
										_grid.RemoveChild (bagSlot.transform);
										_bagSlots.Remove (bagSlot);
										Destroy (bagSlot.gameObject);
								}
						}));
				}
		}

		public void SlotShowDetail (BattleSlot battleSlot)
		{
				SlotShowDetail (battleSlot.concreteCard);		
		}

		public void SlotShowDetail (BagSlot bagSlot)
		{
				SlotShowDetail (bagSlot.concreteCard);
		}

		void SlotShowDetail (ConcreteCard card)
		{
				ConcreteCard second = card;
				ConcreteCard first = null;
				if (_selectBagSlot != null) {
						first = _selectBagSlot.concreteCard;
				} else if (_selectBattleSlot != null) {
						first = _selectBattleSlot.concreteCard;
				}
				if (first == null) {
						Debug.Log (second == null);
						_cardComparisonDisplayer.DisplayCardComparison (second);
				} else {
						_cardComparisonDisplayer.DisplayCardComparison (second, first);
				}
		}

		public void SellSelectedCards ()
		{
				List<ConcreteCard> sellList = new List<ConcreteCard> ();
//				List<Transform> gridChildren = _grid.GetChildList ();
				for (int i = _bagSlots.Count-1; i >=0; i--) {
						if (_bagSlots [i].isSell) {
								Debug.Log ("sell");
								sellList.Add (_bagSlots [i].concreteCard);
								BagSlot bagSlot = _bagSlots [i];
								HOTween.To (bagSlot.slotBody, _fadeDuration, new TweenParms ().Prop ("alpha", 0).Ease (EaseType.Linear).OnComplete (() => {
										Destroy (bagSlot.gameObject);}));
								bagSlot.transform.parent = null;
								_bagSlots.RemoveAt (i);
						}
				}
				StartCoroutine (GridRepositionDelay (_fadeDuration));

				if (sellList.Count == 0) {
						return;
				}
				_player.Sell (sellList);
				HOTween.To (this, 2f, new TweenParms ().Prop ("coinAmount", _player.coins).Ease (EaseType.Linear));
				StartCoroutine (CoinIncrease ());
		}

		IEnumerator GridRepositionDelay (float delay)
		{
				yield return new WaitForSeconds (delay);
				_grid.Reposition ();
				yield return null;
		}

		IEnumerator CoinIncrease ()
		{
				while (_coinAmount!=_player.coins) {
						_coin.text = _coinAmount.ToString ();
						yield return null;
				}
				_coin.text = _player.coins.ToString ();
				yield return null;
		}

		public void SortByLevel ()
		{
				if (_grid.onCustomSort == gridSort_byLevelReverse) {
						_grid.onCustomSort = gridSort_byLevel;
				} else {
						_grid.onCustomSort = gridSort_byLevelReverse;
				}
				_grid.Reposition ();
		}

		public void SortByRarity ()
		{
				if (_grid.onCustomSort == gridSort_byRarityReverse) {
						_grid.onCustomSort = gridSort_byRarity;
				} else {
						_grid.onCustomSort = gridSort_byRarityReverse;
				}
				_grid.Reposition ();
		}
}
