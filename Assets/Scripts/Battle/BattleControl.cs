using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleControl : MonoBehaviour
{
		/// <summary>
		/// The level difference between boss and normal enemy.
		/// </summary>
		private int _bossLevelDelta = 2;
		private int _bossAbilityLevelDelta = 1;
		private CardFactory _cardFactory;
		public BattleCardShell[] _playerCardShellSet;
		public BattleCardShell[] _enemyCardShellSet;
		private List<ConcreteCard> _enemyCard;
		private List<ConcreteCard> _playerCard;
		private PlayerControl _player;
		private int _enemyWave = 0, enemyPerWave = 5;
		/// <summary>
		///Launcher of event(attack boss,merge another card or provide buff....)  
		/// </summary>
		BattleCardShell _currentActiveCard;
		GameController _gameController;

		void Awake ()
		{
				_player = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerControl> ();
				_gameController = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<GameController> ();
				_enemyCard = new List<ConcreteCard> ();
				_cardFactory = CardFactory.GetCardFactory ();
		}
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		/// <summary>
		/// Loads the battle layer elements by level information.
		/// </summary>
		/// <param name="levelInfo">The level to load.</param>
		public void LoadLevel (LevelInfo levelInfo)
		{
				//Rules:	1.Every level has at least one boss;
				//			 	2.In each wave of enemies,at most one boss could appear;
				//				3.The last wave of enemie must has boss shown.
				//             4.Every level has at least one enemy will have odds to drop a whole card or a card fragment out.
				//				5.Every player card will gain some experience after win the battle.
				LoadEnemyCard (levelInfo);
				_playerCard = _player.playCardSet;
				for (int i = 0; i < _enemyCardShellSet.Length; i++) {
						if (i < _enemyCard.Count) {
								_enemyCardShellSet [i].gameObject.SetActive (true);
								_enemyCardShellSet [i].LoadCard (_enemyCard [i]);
						} else {
								_enemyCardShellSet [i].gameObject.SetActive (false);
						}
				}
				for (int i = 0; i < _playerCardShellSet.Length; i++) {
						if (i < _playerCard.Count) {
								_playerCardShellSet [i].gameObject.SetActive (true);
								_playerCardShellSet [i].LoadCard (_playerCard [i]);
						} else {
								_playerCardShellSet [i].gameObject.SetActive (false);
						}
				}
		}

		void LoadEnemyCard (LevelInfo levelInfo)
		{
				_enemyCard.Clear ();
				LevelData levelData = levelInfo.leveldata;
				List<int> enemyLevels = new List<int> ();
				List<int> enemyAbilityLevels = new List<int> ();
				for (int i = 0; i < levelData.enemiesId.Count; i++) {
						enemyLevels.Add (levelData.level);
						enemyAbilityLevels.Add (levelData.enemiesAbilityLevel);
				}
				foreach (int i in levelData.boss) {
						enemyLevels [i] += _bossLevelDelta;
						enemyAbilityLevels [i] += _bossAbilityLevelDelta;
				}
				for (int i = 0; i < levelData.enemiesId.Count; i++) {
						ConcreteCard enemyCard = _cardFactory.GeneConcreteCard (levelData.enemiesId [i],
			                                                     levelData.enemiesRarity [i], enemyLevels [i]);
						foreach (var ability in enemyCard.abilities) {
								int abilityLevel = enemyAbilityLevels [i];
								if (abilityLevel > ability.maxLevel) {
										abilityLevel = ability.maxLevel;
								}
								ability.level = abilityLevel;
						}	
						_enemyCard.Add (enemyCard);
				}
		}

	void RoundStart()
	{}
	void RoundEnd()
	{}
		/// <summary>
		/// Freeze battle layer so that no operation is available.
		/// </summary>
		public void Freeze ()
		{
		}
		/// <summary>
		/// Player can operate after this method is called
		/// </summary>
		public void Defreeze ()
		{
		}

		public void CardClick (BattleCardShell card)
		{
				if (_currentActiveCard == null) {
						_currentActiveCard = card;
						_currentActiveCard.Toggle ();
				} else if (_currentActiveCard == card) {
						_currentActiveCard.Toggle ();
						_currentActiveCard = null;

				} else {
						if (_currentActiveCard.CardInteraction (card))
								_currentActiveCard = null;
				}
		}

		public void ShowCardDetail (BattleCard card)
		{

		}

		public void StopShowCardDetail ()
		{
		}

		public void ShowBossDetail (bool show)
		{
		}
//	public void BossClick()
//	{
//		if(_currentActiveCard==null){
//			_boss.Showoff();
//		}
//		else{
//			if(_currentActiveCard.CardInteraction(_boss))
//				_currentActiveCard=null;
//		}
//	}

		void SupplyNewCard ()
		{

		}

		public void BattleComplete ()
		{
				_gameController.BattleComplete (StarNum.TRHEE);
		}

		void BattleAbort ()
		{
				_gameController.BattleComplete (StarNum.ZERO);
		}

}
