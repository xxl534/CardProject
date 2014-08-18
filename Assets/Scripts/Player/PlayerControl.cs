using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {
	GameController _gameController;
	int _health,_maxHealth,_magic,_maxMagic;

	public int health{
		get{return _health;}
	}

	public int maxHealth{
		get{return _maxHealth;}
	}

	public int magic{
		get{return _magic;}
	}

	public int maxMagic{
		get{return _maxMagic;}
	}

	void Awake()
	{
		_gameController = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<GameController> ();
	}
	// Use this for initialization
	void Start () {
		LoadFromJson (_gameController._allInfoDict);
		LoadFromPlayerPrefs ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Save()
	{}

	public void LoadFromPlayerPrefs()
	{}

	public void LoadFromJson(Dictionary<string,object> dict)
	{}
}
