using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

public class BattleControl : MonoBehaviour
{
		static float _shellRotateTime = 1f;

	#region Fields
		/// <summary>
		/// The level difference between boss and normal enemy.
		/// </summary>
		///
		private CardAIScript _enemyAI;
//		public UICamera _uiCamera;
		private int _bossLevelDelta = 2;
		private int _bossAbilityLevelDelta = 1;
		private CardFactory _cardFactory;
		public BattleCardShell[] _playerCardShellSet;
		public BattleCardShell[] _enemyCardShellSet;
		public GameObject _background;
		public Material[] _shellMaterials;
		public ShieldPanel _shieldPanel;
		private PlayerControl _player;
		private GameController _gameController;
		private List<ConcreteCard> _enemyCard;
		private List<ConcreteCard> _playerCard;
		private int _enemyIndex;
		private Dictionary<LevelInfo,Texture> _backgroundTextureTable;
		/// <summary>
		///Launcher of event(attack boss,merge another card or provide buff....)  
		/// </summary>
		private BattleCardShell _currentActiveCard;
		private Ability _currentActiveAbility;
		private DynamicTextAdmin _dynamicTextAdmin;
	#endregion

	#region Properties
		public ShieldPanel shieldPanel {
				get{ return _shieldPanel;}
		}

		public DynamicTextAdmin dynamicTextAdmin {
				get{ return _dynamicTextAdmin;}
		}

	#endregion

		void Awake ()
		{
				_player = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerControl> ();
				_gameController = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<GameController> ();
				_dynamicTextAdmin = GetComponentInChildren<DynamicTextAdmin> ();
				_enemyCard = new List<ConcreteCard> ();
				_playerCard = new List<ConcreteCard> ();
				_cardFactory = CardFactory.GetCardFactory ();
				_enemyAI = GetComponent<CardAIScript> ();
				_backgroundTextureTable = new Dictionary<LevelInfo, Texture> ();
		}
		
		void Clear ()
		{
				_enemyCard.Clear ();
				_playerCard.Clear ();
				_enemyIndex = 0;
				_currentActiveCard = null;
				foreach (var item in _enemyCardShellSet) {
						item.Clear ();
						item.gameObject.SetActive (false);
				}
				foreach (var item in _playerCardShellSet) {
						item.Clear ();
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
				LoadBackground (levelInfo);
				LoadEnemyConcreteCard (levelInfo);
				LoadPlayerConcreteCard ();
				LoadEnemyWave ();
				LoadPlayerBattleCard ();
		}
		
		void LoadBackground (LevelInfo levelInfo)
		{
				Texture backgroundTex;
				if (_backgroundTextureTable.ContainsKey (levelInfo)) {
						backgroundTex = _backgroundTextureTable [levelInfo];
				} else {
						backgroundTex = Resources.Load<Texture> (ResourcesFolderPath.textures_background + "/" + levelInfo.leveldata.background);
						_backgroundTextureTable.Add (levelInfo, backgroundTex);
				}
				_background.renderer.material.mainTexture = backgroundTex;
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
		Debug.Log("checkVacant");
				bool enemyWaveDead = true, playerRolesDead = true;
				for (int i = 0; i < _enemyCardShellSet.Length; i++) {
						if (_enemyCardShellSet [i].vacant == false) {
				Debug.Log("i:"+i);
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
				Debug.Log("haiyou");
								LoadEnemyWave ();
						} else {
				Debug.Log("BattleComplete");
								BattleComplete ();
								return;
						}
				}
				if (playerRolesDead) {
						BattleAbort ();
				}
		}
		
		public 	void RotateShells ()
		{
				foreach (var item in _playerCardShellSet) {
						if (item.gameObject.activeSelf == true && item.transform.localRotation != Quaternion.identity) {
				item.TurnToFront();
//								HOTween.To (item.transform, _shellRotateTime, new TweenParms ().Prop ("localRotation", Quaternion.identity).Ease (EaseType.Linear).OnStart (delegate() {
//										_shieldPanel.Activate ();
//								}).OnComplete (delegate() {
//										_shieldPanel.Deactivate ();
//								}));
						}
				}
				foreach (var item in _enemyCardShellSet) {
						if (item.gameObject.activeSelf == true && item.transform.localRotation != Quaternion.identity) {
//								HOTween.To (item.transform, _shellRotateTime, new TweenParms ().Prop ("localRotation", Quaternion.identity).Ease (EaseType.Linear).OnStart (delegate() {
//										_shieldPanel.Activate ();
//								}).OnComplete (delegate() {
//										_shieldPanel.Deactivate ();
				item.TurnToFront();
//								}));
						}
				}
		}

		void RoundStart ()
		{
				foreach (var shell in _playerCardShellSet) {
						if (shell.vacant == false) {
								shell.RoundStart ();
						}
				}
				foreach (var shell in _enemyCardShellSet) {
						if (shell.vacant == false) {
								shell.RoundStart ();
						}
				}
				StartCoroutine ("CRCheckPlayerCardActivity");
		}
		
		IEnumerator CRCheckPlayerCardActivity ()
		{
				while (_shieldPanel.gameObject.activeSelf) {
						yield return null;
				}
				CheckPlayerCardShellSetActivity ();
				yield return null;
		}
		
		/// <summary>
		/// Check all the player cards' activity.
		/// </summary>
		void CheckPlayerCardShellSetActivity ()
		{
				bool playerRoundOver = true;
				foreach (var shell in _playerCardShellSet) {
						if (CheckPlayerCardActivity (shell)) {
								shell.HighLight ();
								playerRoundOver = false;
						}
				}
				if (playerRoundOver) {
						ExecuteEnemyCards ();
				}
		}

		bool CheckPlayerCardActivity (BattleCardShell cardShell)
		{
				if (cardShell.shellType == ShellType.Enemy || cardShell.vacant || cardShell.hasCast) {
						return false;
				}
				return cardShell.HasAvailableAbility ();
		}

		void ExecuteEnemyCards ()
		{
				_enemyAI.EnemiesActionStart ();
		}

		public	void RoundEnd ()
		{
				foreach (var item in _playerCardShellSet) {
						if (item.vacant == false) {
								item.RoundEnd ();
						}
				}	
				foreach (var item in _enemyCardShellSet) {
						if (item.vacant == false) {
								item.RoundEnd ();
						}
				}
				StartCoroutine ("CRRoundStart");
		}

		IEnumerator CRRoundStart ()
		{
				while (_shieldPanel.gameObject.activeSelf) {
						yield return null;
				}
				RoundStart ();
				yield return null;
		}
		
		public void AbilityClick (Ability ability)
		{
				_currentActiveAbility = ability;
		}

		public void CardClick (BattleCardShell card)
		{
				if (_currentActiveAbility == null) {
//						Debug.Log (1);
						if (CheckPlayerCardActivity (card)) {
								{
//										Debug.Log (2);
										if (_currentActiveCard == card) {
//												Debug.Log (3);
												_currentActiveCard.Hide ();
												_currentActiveCard = null;
										} else {
//												Debug.Log (4);
												_currentActiveCard = card;
												card.Show ();
										}
								}
						} else {
//								Debug.Log (5);
								card.Deny ();
						}
				} else {
//			Debug.Log(6);
						bool canCast = false;
						switch (_currentActiveAbility.targetType) {
						case TargetType.All:
								{
										canCast = true;
										break;
								}
						case TargetType.Friend:
								{
										if (card.shellType == ShellType.Player) {canCast = true;}
										break;
								}
						case TargetType.Enemy:
								{
//				Debug.Log(7);
										if (card.shellType == ShellType.Enemy) {canCast = true;}
										break;
								}
						case TargetType.Self:
								{
										if (card == _currentActiveCard) {canCast = true;}
										break;
								}
						default:
								break;
						}
						if (canCast) {
//				Debug.Log(8);
				if(_currentActiveAbility.targetArea== TargetArea.Area)
				{//If ability is AOE,cast at the middle of enemies;
					_currentActiveCard.CastAbility(_currentActiveAbility,_enemyCardShellSet[0].battleCard);
				}
				else{
						_currentActiveCard.CastAbility(_currentActiveAbility,card.battleCard);
				}
				_currentActiveAbility=null;
				_currentActiveCard=null;
						} else {
								card.Deny ();
						}
				}
		}
		
		public void BackgroundClick ()
		{
				if (_currentActiveCard != null) {
						_currentActiveCard.Hide ();
						_currentActiveCard = null;
						_currentActiveAbility = null;
				}
		}

		public void ShowCardDetail (BattleCard card)
		{

		}

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
