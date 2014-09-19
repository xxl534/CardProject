using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

public class BattleCompleteDisplayer : MonoBehaviour {
	public CompleteSlot[] _completeSlots;
	public UIGrid _grid;
	public UISlider _playerExperienceSlider;
	public UILabel _playerExperienceLabel;
	public UILabel _playerLevelLabel;
	public UILabel _playerCoinLabel;
	public UISprite _playerIcon;
	public GameObject _cardIconPrefab;

	public GameObject _returnButton;
	public Vector3 _newCardPosition;
	private PlayerControl _player;
private	UIPanel _panel;
	private int _level,_experience,_expUpBound,_coin;
	private float _fadeTime=0.5f,_addTimeInterval=0.25f, _expIncDuration=3.0f;
	private List<ConcreteCard> _trophyCards;

	private	SceneFade _fader;
	private GameController _gamerController;
	private BattleControl _battleController;
	private bool _canClick;
	void Awake()
	{
		_player = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerControl> ();
		_panel = GetComponent<UIPanel> ();
		_fader= GameObject.FindGameObjectWithTag (Tags.sceneFader).GetComponent<SceneFade> ();
		_gamerController = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<GameController> ();
		_battleController = GameObject.FindGameObjectWithTag (Tags.battle).GetComponent<BattleControl> ();
	}

	void Start()
	{
		Clear ();
	}

	void OnClick()
	{
		if (_canClick) {
			DynamicToInstant();
			_canClick=false;
				}
	}
	public void DisplayBattleComplete(LevelData levelData,List<ConcreteCard> achieveCards)
	{
		_trophyCards = achieveCards;
		_playerIcon.spriteName = _player.spriteName;
		LoadPlayerInfo ();
	  IEnumerator slotIter	=_completeSlots.GetEnumerator () ;
		for (int i = 0; i < _player.playCardSet.Count; i++) {
			if(_player.playCardSet[i]!=null)
				slotIter.MoveNext();
				((CompleteSlot)slotIter.Current).LoadConcreteCard(_player.playCardSet[i]);
				}
		gameObject.SetActive (true);
		_panel.alpha = 0f;
		foreach (var item in achieveCards) {
			_player.GainNewCard(item);
				}
		HOTween.To (_panel, _fadeTime, new TweenParms ().Prop ("alpha", 1f).Ease (EaseType.Linear).OnComplete (delegate() {
			Debug.Log("al=1");
			PlayerGainExperience(levelData.experience);
			foreach (var item in _completeSlots) {
				if(item.vacant==false)
				{
					item.GainExperience(levelData.experience);
				}
			}
			StartCoroutine(gainCardsDynamic());
			_returnButton.SetActive(true);
			_canClick=true;
	}));
	}

	void LoadPlayerInfo()
	{
		_level = _player.level;
		_playerLevelLabel.text = "Lv." +_level.ToString ();
		_experience = _player.experience;
		_expUpBound = BaseCard._experienceTable [_level-1];
		_playerExperienceLabel.text = _experience.ToString () + "/" + _expUpBound.ToString ();
		_playerExperienceSlider.value = _experience / (float)_expUpBound;
		_coin = _player.coins;
		_playerCoinLabel.text = _coin.ToString ();
	}

	void PlayerGainExperience(int experience)
	{
		_player.experience += experience;
		StartCoroutine (PlayerGainExperienceDynamic (experience, _expIncDuration));
	}

	IEnumerator PlayerGainExperienceDynamic(int experience,float duration)
	{
		float fExperience=(float)_experience;
		float perSecond = experience / duration;
		int dstLevel = _player.level, dstExp =_player.experience;

		while (_level<dstLevel||_experience<dstExp) {
			fExperience +=perSecond*Time.deltaTime;
//			Debug.Log(perSecond.ToString()+"|"+fExperience.ToString());
			_experience=(int)fExperience;
			if (_experience >= _expUpBound) {
				while (_level<dstLevel) {
					_level++;
					_experience-=_expUpBound;
					fExperience-=(float)_expUpBound;
					_expUpBound=BaseCard._experienceTable[_level-1];
				}
				
			}
			_playerLevelLabel.text = "lv." + _level.ToString ();
			_playerExperienceSlider.value = _experience / (float)_expUpBound;
			_playerExperienceLabel.text = _experience.ToString () + "/" + _expUpBound.ToString ();
//			Debug.Log(_level.ToString()+"|"+_experience.ToString()+"|"+dstLevel.ToString()+"|"+dstExp.ToString());
			yield return null;
		}
		if (_experience > dstExp) {
			_experience=dstExp;
		}
		_playerLevelLabel.text = "lv." + _level.ToString ();
		_playerExperienceSlider.value = _experience / (float)_expUpBound;
		_playerExperienceLabel.text = _experience.ToString () + "/" + _expUpBound.ToString ();
		yield return null;
	}
	void PlayerGainCoins (int coins)
	{
		_player.coins += coins;
		StartCoroutine (gainCoinsDynamic (coins, _expIncDuration));
	}

	IEnumerator gainCoinsDynamic(int coins,float duration)
	{
		float perSecond=coins/duration;
		while (_coin<_player.coins) {
			_coin+=(int)(perSecond*Time.deltaTime);
			_playerCoinLabel.text=_coin.ToString();
			yield return null;
				}
		_coin = _player.coins;
		_playerCoinLabel.text=_coin.ToString();
		yield return null;
	}
	IEnumerator gainCardsDynamic()
	{
		while (_trophyCards.Count >0) {
			ConcreteCard card=_trophyCards[_trophyCards.Count-1];
			_trophyCards.RemoveAt(_trophyCards.Count-1);
			GameObject newCard=	Instantiate(_cardIconPrefab) as GameObject;
			newCard.transform.localPosition=_newCardPosition;
			newCard.GetComponent<UISprite>().spriteName=card.name;
			_grid.AddChild(newCard.transform);
			yield return new WaitForSeconds(_addTimeInterval);
				}
		yield return null;
	}
	void Clear()
	{
		foreach (var item in _completeSlots) {
			item.vacant=true;
			item.gameObject.SetActive(false);
				}
		List<Transform> trophies = _grid.GetChildList ();
		for (int i = trophies.Count-1; i >=0 ; i--) {
			Destroy(trophies[i].gameObject);
				}
		_canClick = false;
		gameObject.SetActive (false);
//		Debug.Log("clear");
	}

	void DynamicToInstant()
	{
		StopCoroutine("PlayerGainExperienceDynamic");
		StopCoroutine ("gainCoinsDynamic");
		StopCoroutine("gainCardsDynamic");
		foreach (var item in _completeSlots) {
			if(item.vacant==false)
				item.GainExperienceInstant();
				}
		LoadPlayerInfo ();
		_grid.animateSmoothly = false;
		for (int i = _trophyCards.Count-1; i >=0;i--) {

			ConcreteCard card=_trophyCards[i];
			GameObject newCard=	Instantiate(_cardIconPrefab) as GameObject;
			newCard.GetComponent<UISprite>().spriteName=card.name;
			_grid.AddChild(newCard.transform);
		}
		_grid.animateSmoothly = true;
	}

	public void EndDisplay()
	{
		_returnButton.SetActive (false);
		if (_canClick) {
			DynamicToInstant();
			_canClick=false;
				}
		_fader.BeginFading (()=>{
			Clear();
			_battleController.gameObject.SetActive(false);
			_fader.ExitFading(()=>{_gamerController.BattleComplete();});
		});
	}
}
