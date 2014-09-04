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
		public Material[] _shellMaterials;
		private PlayerControl _player;
		private GameController _gameController;
		private List<ConcreteCard> _enemyCard;
		private List<ConcreteCard> _playerCard;
		private int _enemyIndex;
		/// <summary>
		///Launcher of event(attack boss,merge another card or provide buff....)  
		/// </summary>
		private BattleCardShell _currentActiveCard;

		void Awake ()
		{
				_player = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerControl> ();
				_gameController = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<GameController> ();
				_enemyCard = new List<ConcreteCard> ();
				_playerCard = new List<ConcreteCard> ();
				_cardFactory = CardFactory.GetCardFactory ();
				Debug.Log ("battleControlAwake");
		}
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void Clear ()
		{
				_enemyCard.Clear ();
				_playerCard.Clear ();
				_enemyIndex = 0;
				_currentActiveCard = null;
				foreach (var item in _enemyCardShellSet) {
						item.gameObject.SetActive (false);
				}
				foreach (var item in _playerCardShellSet) {
						item.gameObject.SetActive (false);
				}
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

				Clear ();
				LoadEnemyConcreteCard (levelInfo);
				LoadPlayerConcreteCard ();
				LoadEnemyWave ();
				LoadPlayerBattleCard ();
		}
		
		/// <summary>
		/// Loads the enemy ConcreteCard via level imformation.
		/// </summary>
		/// <param name="levelInfo">Level information.</param>
		void LoadEnemyConcreteCard (LevelInfo levelInfo)
		{
				LevelData levelData = levelInfo.leveldata;
				List<int> enemyLevels = new List<int> ();
				List<int> enemyAbilityLevels = new List<int> ();
				for (int i = 0; i < levelData.enemiesId.Count; i++) {
						enemyLevels.Add (levelData.level);
						enemyAbilityLevels.Add (levelData.enemiesAbilityLevel);
				}
				foreach (int i in levelData.bossIndices) {
						enemyLevels [i] += _bossLevelDelta;
						enemyAbilityLevels [i] += _bossAbilityLevelDelta;
						
				}
				for (int i = 0; i < levelData.enemiesId.Count; i++) {
						ConcreteCard enemyCard = _cardFactory.GeneConcreteCard (levelData.enemiesId [i], enemyLevels [i]);
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

		void LoadPlayerConcreteCard ()
		{
				for (int i = 0; i < _player.playCardSet.Count; i++) {
						_playerCard.Add (_player.playCardSet [i]);
				}
		}

		/// <summary>
		/// Loads a wave of enemy battleCard.
		/// </summary>
		void LoadEnemyWave ()
		{
				bool emptyWave = true;
				for (int i = 0; i < _enemyCardShellSet.Length; i++) {
						if (_enemyIndex + i < _enemyCard.Count) {
								if (_enemyCard [i + _enemyIndex] != null) {
										emptyWave = false;
										_enemyCardShellSet [i].LoadCard (_enemyCard [i + _enemyIndex]);
								}
								_enemyIndex++;
						} else {
								break;
						}
				}

				//In case of a wave of empty enemy
				if (emptyWave) {
						Debug.Log ("Load a wave of empty enemy");
						LoadEnemyWave ();
				}
		}

		/// <summary>
		/// Loads the player's concrete card into card shells.
		/// </summary>
		void LoadPlayerBattleCard ()
		{
				for (int i = 0; i < _playerCardShellSet.Length; i++) {
						if (i < _playerCard.Count) {
								if (_playerCard [i] != null) {
										_playerCardShellSet [i].LoadCard (_playerCard [i]);
								}
						}
				}
		}

		public void CheckVacantShell ()
		{
				bool enemyWaveDead = true, playerRolesDead = true;
				for (int i = 0; i < _enemyCardShellSet.Length; i++) {
						if (_enemyCardShellSet [i].vacant == false) {
								enemyWaveDead = false;
								break;
						}
				}
				for (int i = 0; i < _playerCardShellSet.Length; i++) {
						if (_playerCardShellSet [i].vacant == false) {
								playerRolesDead = false;
								break;
						}
				}
				if (enemyWaveDead) {
						if (_enemyIndex < _enemyCard.Count) {
								LoadEnemyWave ();
						} else {
								BattleComplete ();
								return;
						}
				}
				if (playerRolesDead) {
						BattleAbort ();
				}
		}

		void RoundStart ()
		{
		}

		void RoundEnd ()
		{
		}
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
