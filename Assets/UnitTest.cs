using UnityEngine;
using System.Collections;

public class UnitTest : MonoBehaviour {
	public	GameController _gameController;

	bool _gan;
	// Use this for initialization
	void Start () {
		_gan=false;
	}
	
	// Update is called once per frame
//	void Update () {
//		if(Time.time>2f&&_gan==false){
//			_gameController._selectLevel=_gameController._levels[0];
//		_gameController.BattleStart(_gameController._selectLevel.GetComponent<LevelInfo>());
//			_gan=true;
//		}
//	}
}
