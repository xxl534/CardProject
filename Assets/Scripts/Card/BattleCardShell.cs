using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class BattleCardShell : MonoBehaviour {
	static float _toggleDistance=0.3f;
	public int _slotIndex;
	public BattleControl _battleController;
	public ShellType  _shellType;
	private BattleCard _battleCard;
	Vector3 _origLocalPosition;
	/// <summary>
	///If the card has been used,this shell is vacant,When reload a new card to this battle card shell "vacant" is false; 
	/// </summary>
	private bool _vacant;
	/// <summary>
	/// Every card has a chance to cast an ability when a new round started.
	/// If a card has casted an ability,it's 'hasCast' =true.
	/// </summary>
	private bool _hasCast;
	public GameObject _role;
	public GameObject _shell;
	public GameObject _glowEdge;
	public UILabel _label_hp,_label_mp;
	/// <summary>
	/// The card toggle .
	/// </summary>
	bool _toggle=false;
	float _showCardTimer=0f;
	float _showCardTime=2f;

	float _clickTimer=0f;
	float _clickInterval=0.5f;

	public bool vacant
	{
		get{return _vacant;}
//		set{_vacant=value;}
	}
	public bool hasCast
	{
		get{return _hasCast;}
	}
	public BattleCard battleCard
	{
		get{return _battleCard;}
	}
	// Use this for initialization
	void Awake()
	{
		_battleController = GetComponentInParent<BattleControl> ();
		_origLocalPosition=transform.localPosition;
		_battleCard = GetComponent<BattleCard> ();
	}
	
	// Update is called once per frame
	void Update () {
		_clickTimer += Time.deltaTime;
		if (_showCardTimer > _showCardTime)
						_battleController.ShowCardDetail (_battleCard);
	}

//	void OnMouseOver()
//	{
//		Debug.Log("on");
//			_showCardTimer+=Time.deltaTime;
//	}
	void OnHover()
	{
		Debug.Log("hover");
	}
	void OnClick()
	{
		Debug.Log("click");
		MouseClick();
	}
//	void OnMouseExit()
//	{
//		Debug.Log("mouseOut");
//	_showCardTimer=0;
//		_battleController.StopShowCardDetail();
//	}


 	void MouseClick()
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
			HOTween.To (transform,0.5f,new TweenParms().Prop("localPosition",_origLocalPosition+new Vector3(0,_toggleDistance,0)).Ease(EaseType.EaseInOutQuad).OnComplete(delegate(){
				_glowEdge.SetActive(true);
			}));

				} else {
			HOTween.To (transform,0.5f,new TweenParms().Prop("localPosition",_origLocalPosition).Ease(EaseType.EaseInOutQuad).OnComplete(delegate(){
				_glowEdge.SetActive(false);
		}
				));
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
		_battleCard.LoadConcreteCard (concreteCard);

		if( _origLocalPosition!=null)
			transform.localPosition=_origLocalPosition;
		_vacant = false;
		_toggle = false;
		_shell.renderer.material=_battleController._shellMaterials[(int)concreteCard.rarity];
		_role.renderer.material.mainTexture=concreteCard.roleTexture;
		_label_hp.text=_battleCard.health.ToString();
		_label_mp.text=_battleCard.mana.ToString();
		transform.localRotation=Quaternion.Euler(new Vector3(0,180f,0));
		gameObject.SetActive(true);
	}

	public void CardRoleDead()
	{
		_vacant = true;
		gameObject.SetActive (false);
		_battleController.CheckVacantShell ();
	}

	public void RoundStart()
	{}

	public void RoundEnd()
	{}

	public void HighLight()
	{}
}
