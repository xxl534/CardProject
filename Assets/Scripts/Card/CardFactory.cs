using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class CardFactory
{
		private static CardFactory _cardFactoryInstance;
		private Dictionary<string,int> _baseCardIdTable;
		private Dictionary<int,BaseCard> _baseCardTable;
		private Dictionary<string,Texture> _cardRoleSpriteTable;
		private AbilityFactory _abilityFactory;
		
		public bool ContainsCard (string cardName)
		{
				return _baseCardIdTable.ContainsKey (cardName);
		}

		public bool ContainsCard (int cardId)
		{
				return _baseCardTable.ContainsKey (cardId);
		}

		public ConcreteCard GeneConcreteCard (string cardName, int level=1, int[] abilitiesLevel=null)
		{
				int cardId = _baseCardIdTable [cardName];
				BaseCard baseCard = _baseCardTable [cardId];
//				Debug.Log ("Card name:" + cardName + ";  Card id:" + cardId + "; BaseCard:" + (baseCard == null));
				return GeneConcreteCard (baseCard, level, abilitiesLevel);
		}

		public ConcreteCard GeneConcreteCard (int cardId,int level=1, int[] abilitiesLevel=null)
		{
				BaseCard baseCard = _baseCardTable [cardId];
				return GeneConcreteCard (baseCard,  level, abilitiesLevel);
		}
		/// <summary>
		/// Generate  ConcreteCard based on BaseCard.
		/// </summary>
		/// <returns>The generated ConcreteCard.</returns>
		/// <param name="baseCard">BaseCard.</param>
		/// <param name="rarity">ConcreteCard's rarity.</param>
		/// <param name="level">ConcreteCard level.</param>
		/// <param name="abilitiesLevel">level of ConcreteCard's abilities.</param>
		public ConcreteCard GeneConcreteCard (BaseCard baseCard, int level, int[] abilitiesLevel=null)
		{
				List<Ability> abilityList = new List<Ability> ();
				//That's for not to exam whether the array is null every for-loop
				if (abilitiesLevel == null)
						abilitiesLevel = new int[0];
				for (int i=0; i!= baseCard._abilitiesId.Count; i++) {
						int abilityLevel = 1;
						if (i < abilitiesLevel.Length) {
								abilityLevel = abilitiesLevel [i];
						}
						if (abilityLevel < 1) {
								Debug.Log ("Level of ability should be positive.");
								throw new System.ArgumentException ("abilitiesLevel");
						}

						Ability ability = _abilityFactory.GeneAbility (baseCard._abilitiesId [i], abilityLevel);
						abilityList.Add (ability);
				}
				return new ConcreteCard (baseCard,  level, abilityList,_cardRoleSpriteTable[baseCard.cardSprite]);
	
		}

		public static CardFactory  GetCardFactory ()
		{
				if (_cardFactoryInstance == null)
						_cardFactoryInstance = new CardFactory ();
				return _cardFactoryInstance;
		}

		private CardFactory ()
		{
				_baseCardIdTable = new Dictionary<string, int> ();
				_baseCardTable = new Dictionary<int, BaseCard> ();
				_cardRoleSpriteTable = new Dictionary<string, Texture> ();
				_abilityFactory = AbilityFactory.GetAbilityFactory ();
		LoadCardTexture();
				LoadCards ();
				LoadCardStatic ();
//		Debug.Log (_baseCardIdTable.Count + "    " + _baseCardTable.Count);
		}

		/// <summary>
		/// Loads the BaseCard from resources.
		/// </summary>
		void LoadCards ()
		{
				TextAsset[] textAssets = Resources.LoadAll<TextAsset> ("Json/Card");
				foreach (TextAsset tAsset in textAssets) {
						string jsonString = tAsset.text;
						var dict = Json.Deserialize (jsonString) as Dictionary<string,object>;
						if (dict.ContainsKey ("card")) {
								Dictionary<string,object> cardInfos = dict ["card"] as Dictionary<string,object>;
								foreach (string cardName in cardInfos.Keys) {
										//Detect existence of cardName
										if (_baseCardIdTable.ContainsKey (cardName)) {
												Debug.Log (string.Format ("Card name has existed -name:{0} in folder: 'Resources/Json/Card/{1}'", cardName, tAsset.name));
												throw new System.Exception (string.Format ("Card name has existed -name:{0} in folder: 'Resources/Json/Card/{1}'", cardName, tAsset.name));
										}
										KeyValuePair<string,Dictionary<string,object>> cardInfo
					= new KeyValuePair<string, Dictionary<string, object>> (cardName, cardInfos [cardName]as Dictionary<string,object>);
				
										BaseCard baseCard;
										try {
												baseCard = BaseCard.GeneBaseCard (cardInfo);
										} catch (System.Exception e) {
												Debug.Log (string.Format ("Fail to Generate BaseCard via card data that named:{0}", cardName));
												throw e;
										}
										int id = baseCard.id;
										//Detect existence of baseCard's id
										if (_baseCardTable.ContainsKey (id)) {
												Debug.Log (string.Format ("Card id has existed -name:{0} in folder: 'Resources/Json/Card/{1}'", cardName, tAsset.name));
												throw new System.Exception (string.Format ("Card id has existed -name:{0} in folder: 'Resources/Json/Card/{1}'", cardName, tAsset.name));
										}
										//Check the role and background sprites
										try {
												CheckSprite (baseCard);
										} catch (System.Exception e) {
												Debug.Log (string.Format ("Fail to check sprite texture of BaseCard named:{0}", baseCard.name));
												throw e;
										}
										_baseCardIdTable.Add (cardName, id);
										_baseCardTable.Add (id, baseCard);
								}
						}
				}
		}

		void LoadCardStatic ()
		{
				TextAsset textAsset = Resources.Load<TextAsset> (ResourcesFolderPath.json_card + "/cardStatic");
				if (textAsset == null) {
						Debug.Log ("card static infomation file access error");
						throw new System.Exception ("card static infomation file access error");
				}
				string json = textAsset.text;
				Dictionary<string,object> dict = MiniJSON.Json.Deserialize (json) as Dictionary<string,object>;
				Dictionary<string,object> cardStatics = dict ["cardStatic"] as Dictionary<string,object>;
				BaseCard.LoadStaticFields (cardStatics);

				if (!BaseCard.CheckStaticFields ()) {
						Debug.Log ("BaseCard static fields incomplete error");
						throw new System.Exception ("BaseCard static fields incomplete error");
				}
		}
		/// <summary>
		/// Checks the legality of BaseCard's cardSprite and backgroundSprite.
		/// </summary>
		/// <param name="baseCard">The Basecard for checking.</param>
		void CheckSprite (BaseCard baseCard)
		{
				//Check role texture.
				if (baseCard.cardSprite == null) {
						baseCard.cardSprite = baseCard.name;
				}
				string roleSpriteName = baseCard.cardSprite;
				if (!_cardRoleSpriteTable.ContainsKey (roleSpriteName)) {
								Debug.Log (string.Format ("Illegal cardSprite name:{0} ", roleSpriteName));
								throw new System.ArgumentException (string.Format ("Illegal cardSprite name:{0} ", roleSpriteName));
				}
		}

	void LoadCardTexture()
	{
		Texture[] textures=Resources.LoadAll<Texture>(ResourcesFolderPath.textures_role);
		foreach (var item in textures) {
			_cardRoleSpriteTable.Add(item.name,item);
				}
	}


}
