using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {
	private  List<ConcreteCard> _cardBag;
	private List<ConcreteCard> _playCardSet;
	private GameController _gameController;

	public List<ConcreteCard> cardBag
	{
		get{return _cardBag;}
	}
	public List<ConcreteCard> playCardSet
	{
		get{return _playCardSet;}
	}
	void Awake()
	{
		_gameController = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<GameController> ();
		_playCardSet=new List<ConcreteCard>();
		_cardBag=new List<ConcreteCard>();
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
