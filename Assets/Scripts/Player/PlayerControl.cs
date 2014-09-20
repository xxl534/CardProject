using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{
		public BagManagement _bagManagement;
		private  List<ConcreteCard> _cardBag;
		private List<ConcreteCard> _playCardSet;
		private GameController _gameController;
		private CardFactory _cardFactory;
		private int _coins, _experience, _level, _maxLevel;
		private string _name, _spriteName;

	public BagManagement bagManagement
	{
		get{return _bagManagement;}
	}
		public List<ConcreteCard> cardBag {
				get{ return _cardBag;}
		}

		public List<ConcreteCard> playCardSet {
				get{ return _playCardSet;}
		}
		
		public int coins {
				get{ return _coins;}
				set{ _coins = value;}
		}

		public string playerName {
				get{ return _name;}
		}

		public int level {
				get{ return _level;}
		}

		public int experience {
				get{ return _experience;}
				set {
						if (value < _experience) {
								Debug.Log ("Decrease of experience is forbidden");
								throw new System.ArgumentException ("experience");
						}
						_experience = value;
						int newLevel = level;
						while (newLevel<BaseCard._experienceTable.Count&&_experience>BaseCard._experienceTable[newLevel-1]) {
								_experience -= BaseCard._experienceTable [newLevel - 1];
								newLevel++;
						}
						if (_experience > BaseCard._experienceTable [newLevel - 1]) {//When experience exceed the max,it equals the max
								_experience = BaseCard._experienceTable [newLevel - 1];
						}
						_level = newLevel;
				}
		}
		
		public string spriteName {
				get{ return _spriteName;}
				set{ _spriteName = value;}
		}

		void Awake ()
		{
//				_gameController = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<GameController> ();
				_playCardSet = new List<ConcreteCard> ();
				_cardBag = new List<ConcreteCard> ();
				_cardFactory = CardFactory.GetCardFactory ();
				Load ();
				
		}
		// Use this for initialization
		void Start ()
		{
				_bagManagement.Load ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void Save ()
		{
		}

		void Load ()
		{
				LoadFromJson ();
				LoadFromPlayerPrefs ();
		}

		public void LoadFromPlayerPrefs ()
		{
		}

		//when player first enter the game,load information from json and all default card are with level 1.
		public void LoadFromJson ()
		{
				TextAsset textAsset = Resources.Load<TextAsset> (ResourcesFolderPath.json_player + "/player");
				if (textAsset == null) {
						Debug.Log ("Player json information file access error");
						throw new System.Exception ("Player json information file access error");
				}
				string json = textAsset.text;
				Dictionary<string,object> dict = MiniJSON.Json.Deserialize (json)as Dictionary<string,object>;
				Dictionary<string,object> playerInfo = dict ["player"] as Dictionary<string,object>;
				List<string> CardSet = (playerInfo ["cardSet"] as List<object>).ConvertAll (x => x.ToString ());
				foreach (string str in CardSet) {
//			Debug.Log(str);
						try {
								if (_cardFactory.ContainsCard (str)) {
										_cardBag.Add (_cardFactory.GeneConcreteCard (str));
								} else {
										_cardBag.Add (_cardFactory.GeneConcreteCard (System.Convert.ToInt32 (str)));

								}
						} catch (System.Exception e) {
								Debug.Log (string.Format ("Card:{0}  access error", str));
								throw e;
						}
				}

				for (int i = 0; i < 5; i++) {
						if (_cardBag.Count > 0) {
								_playCardSet.Add (_cardBag [0]);
								_cardBag.RemoveAt (0);
						} else {
								_playCardSet.Add (null);
						}
				}

				_coins = System.Convert.ToInt32 (playerInfo ["coins"]);
				_name = playerInfo ["playerName"].ToString ();
		_spriteName = _name;
				_experience = 0;
				_level = 1;
		}

		public void Sell (List<ConcreteCard> sellList)
		{
				foreach (var item in sellList) {
						_coins += item.price;
						_cardBag.Remove (item);
				}
		}

	public void GainNewCard(ConcreteCard  newCard)
	{
		_cardBag.Add (newCard);
		_bagManagement.AddNewCardToBag (newCard);
	}
}
