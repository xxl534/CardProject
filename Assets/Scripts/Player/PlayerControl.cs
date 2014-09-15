using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{
		private  List<ConcreteCard> _cardBag;
		private List<ConcreteCard> _playCardSet;
		private GameController _gameController;
		private CardFactory _cardFactory;
		private int _coins;

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

		void Awake ()
		{
//				_gameController = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<GameController> ();
		_playCardSet = new List<ConcreteCard>();
				_cardBag = new List<ConcreteCard> ();
				_cardFactory = CardFactory.GetCardFactory ();
				LoadFromPlayerPrefs ();
		}
		// Use this for initialization
		void Start ()
		{
				
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void Save ()
		{
		}

		public void LoadFromPlayerPrefs ()
		{
				if (!PlayerPrefs.HasKey (PlayerPrefKeys.player)) {
						LoadFromJson ();
				}
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
						if (_cardBag.Count > 0){
								_playCardSet.Add(_cardBag [0]);
				_cardBag.RemoveAt(0);
			}
			else{
				_playCardSet.Add(null);
			}
				}

				if (playerInfo.ContainsKey ("coins")) {
						_coins = System.Convert.ToInt32 (playerInfo ["coins"]);
				}
		}
}
