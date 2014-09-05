using UnityEngine;
using System.Collections;

public class CardAIScript : MonoBehaviour {
	public  BattleControl _battleControl;

	public void EnemiesActionStart()
	{
		_battleControl.RoundEnd ();
	}

	void EnemiesActionOver()
	{
		_battleControl.RoundEnd ();
	}
}
