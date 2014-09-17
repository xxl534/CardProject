using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardAIScript : MonoBehaviour
{
		private  BattleControl _battleControl;
		private GameObject _shieldPanelGO;
		private List<BattleCardShell> _candidateCards;
		private bool _run;
		public bool run
	{
		get{return _run;}
		set{_run=value;}
	}
		void Awake ()
		{
				_battleControl = GetComponent<BattleControl> ();
				_run = false;
				_candidateCards = new List<BattleCardShell> ();
		}

	void Start()
	{
		_shieldPanelGO = _battleControl.shieldPanel.gameObject;
	}
		void Update ()
		{
				
		if (_run && !_shieldPanelGO.activeInHierarchy) {Debug.Log("1");
						BattleCardShell activeShell = GetRandomActivaEnemy ();
						if (activeShell == null) {//No active enemy
								EnemiesActionOver ();
								return;
						}
						List<Ability> abilities = activeShell.battleCard.abilities;
						Dictionary<Ability,int> cdTable = activeShell.battleCard.abilityCDTable;
						for (int i = abilities.Count-1; i >=0; i--) {
								Ability ability = abilities [i];
								if (cdTable [ability] >= ability.cooldown && ability.mana <= activeShell.battleCard.mana) {
										BattleCardShell targetShell;
										switch (ability.targetType) {
										case TargetType.All:
												{
														int ran = Random.Range (0, 1);
														if (ran == 0) {
																targetShell = GetRandomShell (_battleControl._enemyCardShellSet);
														} else {
																targetShell = GetRandomShell (_battleControl._playerCardShellSet);
														}
														break;
												}
										case TargetType.Friend:
												{
														targetShell = GetRandomShell (_battleControl._enemyCardShellSet);
														break;
												}
										case TargetType.Enemy:
												{
														targetShell = GetRandomShell (_battleControl._playerCardShellSet);
														break;
												}
										case TargetType.Self:
												{
														targetShell = activeShell;
														break;
												}
										default:
												targetShell = null;
												break;
										}
										if (targetShell != null) {
												if (ability.targetArea == TargetArea.Area) {//If ability is AOE,cast at the middle of enemies;
														activeShell.CastAbility (ability, targetShell.shellQueue [0].battleCard);
												} else {
														activeShell.CastAbility (ability, targetShell.battleCard);
												}
										}
								}
						}
				}
		}

		public void EnemiesActionStart ()
		{
				_run = true;
		}

		void EnemiesActionOver ()
		{
				_run = false;
				_battleControl.RoundEnd ();
		}

		BattleCardShell GetRandomActivaEnemy ()
		{
				foreach (var item in _battleControl._enemyCardShellSet) {
						if (item.vacant == false && item.hasCast == false) {
								_candidateCards.Add (item);
						}
				}
				if (_candidateCards.Count == 0)
						return null;
				int index = Random.Range (0, _candidateCards.Count - 1);
				BattleCardShell returnCard = _candidateCards [index];
				_candidateCards.Clear ();
				return returnCard;
		}

		/// <summary>
		/// Gets the random shell from shell set.
		/// </summary>
		/// <returns>The random shell.</returns>
		/// <param name="shellSet">Player set or enemy set.</param>
		BattleCardShell GetRandomShell (BattleCardShell[] shellSet)
		{
				foreach (var item in shellSet) {
						if (item.vacant == false) {
								_candidateCards.Add (item);
						}
				}
				if (_candidateCards.Count == 0)
						return null;
				int index = Random.Range (0, _candidateCards.Count - 1);
				BattleCardShell returnCard = _candidateCards [index];
				_candidateCards.Clear ();
				return returnCard;
		}

}
