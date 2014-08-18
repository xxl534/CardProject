using UnityEngine;
using System.Collections;

public class BattleControl : MonoBehaviour {
	BattleCardControl[] _cardSet;
	BossControl _boss;
	/// <summary>
	///Launcher of event(attack boss,merge another card or provide buff....)  
	/// </summary>
	BattleCardControl _currentActiveCard;
	GameController _gameController;
	void Awake(){
		_cardSet = GetComponentsInChildren<BattleCardControl> ();
		_boss=GetComponentInChildren<BossControl>();
		_gameController=GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();
		}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Loads the battle layer elements by level information.
	/// </summary>
	/// <param name="levelInfo">The level to load.</param>
	public void LoadLevel(LevelInfo levelInfo)
	{

	}

	/// <summary>
	/// Freeze battle layer so that no operation is available.
	/// </summary>
	public void Freeze(){
		}
	/// <summary>
	/// Player can operate after this method is called
	/// </summary>
	public void Defreeze(){}

	public void CardClick(BattleCardControl card)
	{
		if (_currentActiveCard == null) {
						_currentActiveCard = card;
						_currentActiveCard.Toggle ();
				} else if (_currentActiveCard == card) {
			_currentActiveCard.Toggle();
			_currentActiveCard=null;

				}
		else
		{
			if(_currentActiveCard.CardInteraction(card))
				_currentActiveCard=null;
		}
	}

	public void ShowCardDetail(CardInfo card)
	{

	}

	public void StopShowCardDetail()
	{}

	public void ShowBossDetail (bool show)
	{}
	public void BossClick()
	{
		if(_currentActiveCard==null){
			_boss.Showoff();
		}
		else{
			if(_currentActiveCard.CardInteraction(_boss))
				_currentActiveCard=null;
		}
	}

	void SupplyNewCard()
	{

	}

	public void BattleComplete()
	{
		_gameController.BattleComplete(StarNum.TRHEE);
	}

	void BattleAbort()
	{
		_gameController.BattleComplete(StarNum.ZERO);
	}

}
