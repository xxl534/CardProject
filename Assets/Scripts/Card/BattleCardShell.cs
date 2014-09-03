using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class BattleCardShell : MonoBehaviour {
	public int _slotIndex;
	public BattleControl _battleController;
	public BattleCard _battleCard;
	Vector3 _origLocalPosition;
	/// <summary>
	///If the card has been used,this shell is vacant,When reload a new card to this battle card shell "vacant" is false; 
	/// </summary>
	public bool _vacant = true;
	public Material _material_role;
	public GameObject _shell;
	public GameObject _glowEdge;
	/// <summary>
	/// The card toggle .
	/// </summary>
	bool _toggle=false;
	float _showCardTimer=0f;
	float _showCardTime=2f;

	float _clickTimer=0f;
	float _clickInterval=0.5f;



	// Use this for initialization
	void Awake()
	{
		_battleController = GetComponentInParent<BattleControl> ();
		_origLocalPosition=transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		_clickTimer += Time.deltaTime;
		if (_showCardTimer > _showCardTime)
						_battleController.ShowCardDetail (_battleCard);
	}

	void OnMouseOver()
	{
			_showCardTimer+=Time.deltaTime;
	}

	void OnMouseExit()
	{
	_showCardTimer=0;
		_battleController.StopShowCardDetail();
	}

public 	void MouseClick()
	{
			if(_clickTimer>_clickInterval){
			_battleController.CardClick(this);
				_clickTimer=0f;

		}
	}

	public void Toggle()
	{
		_toggle = !_toggle;
		if (_toggle) {
			HOTween.To (transform,0.5f,new TweenParms().Prop("localPosition",_origLocalPosition+new Vector3(0,70f,0)).Ease(EaseType.EaseInOutQuad));

				} else {
			HOTween.To (transform,0.5f,new TweenParms().Prop("localPosition",_origLocalPosition).Ease(EaseType.EaseInOutQuad));
				}
	}

	public bool CardInteraction(Component other)
	{
		if(this==other)
			return true;
//		if(other.GetType()==typeof(BattleCardShell))
//		{
//			Debug.Log("action deny");
//			return false;
//		}
//		else if(other.GetType()==typeof(BossControl))
//		{
//			Debug.Log("attack boss");
//			BossControl boss=other as BossControl;
//			boss.Injure(3);
//			_vacant=true;
//			gameObject.SetActive(false);
//			return true;
//		}
		return false;
	}

	public void LoadCard(ConcreteCard concreteCard)
	{

	}
}
