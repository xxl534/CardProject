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
		private Dictionary<string,Texture> _cardBgSpriteTable;
		private AbilityFactory _abilityFactory;

	public ConcreteCard GeneConcreteCard(int cardId,Rarity rarity=Rarity.Normal,int level=1,int[] abilitiesLevel=null)
	{
		BaseCard baseCard=_baseCardTable[cardId];
		return GeneConcreteCard(baseCard,rarity,level,abilitiesLevel);
	}
		/// <summary>
		/// Generate  ConcreteCard based on BaseCard.
		/// </summary>
		/// <returns>The generated ConcreteCard.</returns>
		/// <param name="baseCard">BaseCard.</param>
		/// <param name="rarity">ConcreteCard's rarity.</param>
		/// <param name="level">ConcreteCard level.</param>
		/// <param name="abilitiesLevel">level of ConcreteCard's abilities.</param>
	public ConcreteCard GeneConcreteCard (BaseCard baseCard, Rarity rarity, int level, int[] abilitiesLevel=null)
		{
				List<Ability> abilityList = new List<Ability> ();
				//That's for not to exam whether the array is null every for-loop
				if (abilitiesLevel == null)
						abilitiesLevel = new int[0];
				for (int i=0; i!= baseCard.abilitiesId.Count; i++) {
						int abilityLevel = 1;
						if (i < abilitiesLevel.Length) {
								abilityLevel = abilitiesLevel [i];
						}
						if (abilityLevel < 1) {
								Debug.Log ("Level of ability should be positive.");
								throw new System.ArgumentException ("abilitiesLevel");
						}

						Ability ability = _abilityFactory.GeneAbility (baseCard.abilitiesId [i], abilityLevel);
						abilityList.Add (ability);
				}
				return new ConcreteCard (baseCard, rarity, level, abilityList);
	
		}

		public static CardFactory  GetCardFactory ()
		{
				if (_cardFactoryInstance == null)
						_cardFactoryInstance = new CardFactory ();
				return _cardFactoryInstance;
		}

		private CardFactory ()
		{
		_baseCardIdTable=new Dictionary<string, int>();
		_baseCardTable=new Dictionary<int, BaseCard>();
		_cardBgSpriteTable=new Dictionary<string, Texture>();
		_cardRoleSpriteTable=new Dictionary<string, Texture>();
		_abilityFactory=AbilityFactory.GetAbilityFactory();
				LoadCards ();

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
						Dictionary<string,object> cardStatics = dict ["cardStatics"] as Dictionary<string,object>;
						if (cardStatics != null)
								BaseCard.LoadStaticFields (cardStatics);
				}
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
				string roleSpriteName = baseCard.cardSprite;
				if (!_cardRoleSpriteTable.ContainsKey (roleSpriteName)) {
						string roleSpritePath = ResourcesFolderPath.textures_role + roleSpriteName;
						Texture roleSprite = Resources.Load<Texture> (roleSpritePath);
						if (roleSprite == null) {
								Debug.Log (string.Format ("Illegal cardSprite name:{0} ", roleSpriteName));
								throw new System.ArgumentException (string.Format ("Illegal cardSprite name:{0} ", roleSpriteName));
						}
						_cardRoleSpriteTable.Add (roleSpriteName, roleSprite);
				}

				//Check background texture.
				string bgSpriteName = baseCard.backgroundSprite;
				if (!_cardBgSpriteTable.ContainsKey (bgSpriteName)) {
						string bgSpritePath = ResourcesFolderPath.textures_background + bgSpriteName;
						Texture bgSprite = Resources.Load<Texture> (bgSpritePath);
						if (bgSprite == null) {
								Debug.Log (string.Format ("Illegal background texture name:{0} ", bgSpriteName));
								throw new System.ArgumentException (string.Format ("Illegal background texture name:{0} ", bgSpriteName));
						}
						_cardBgSpriteTable.Add (bgSpriteName, bgSprite);
				}
		}


}
