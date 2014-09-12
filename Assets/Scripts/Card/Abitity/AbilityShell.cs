using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class AbilityShell : MonoBehaviour {


//	private static Color _lightColor=Color.white;
//	private static Color _darkColor=new Color(0.4f,0.4f,0.4f);
	private static float _clickInterval=0.3f;

	
	public BattleCardShell _battleCardShell;
	public GameObject _glowEdge;
	public AbilityDetailDisplayer _abilityDetailDisplayer;


	private Ability _ability;
	private Vector3 _localPosition;

	private float _timeToShowDetail=2f;
	private float _showDetailTimer=float.NegativeInfinity;
	private bool _detailDisplayed=false;
	/// <summary>
	/// Recording how how many rounds have passed since last time the ability has cast.
	/// </summary>
	private int _cooldownTimer;

	/// <summary>
	/// For setting shader value.
	/// </summary>
	private float _cdPercent;
	/// <summary>
	/// Indicate if the shell is filled by abililty.
	/// </summary>
	private bool _vacant;

	/// <summary>
	/// Indicate whether the card is capable to cast the ability..
	/// </summary>
	private bool _available;

	private float _clickTimer;

	public bool vacant
	{
		get{return _vacant;}
	}
	public bool available
	{
		get{return _available;}
	}
	public Ability ability
	{
		get{return _ability;}
//		set{_ability =value;}
	}

	void Awake()
	{
		_localPosition=transform.localPosition;
		_vacant=true;
		_available=false;
		_clickTimer=0;
		_glowEdge.SetActive(false);

	}

	void Update()
	{
		_clickTimer+=Time.deltaTime;
		renderer.material.SetFloat("cooldown",_cdPercent);
		_showDetailTimer+=Time.deltaTime;
		if(_showDetailTimer>_timeToShowDetail&&_detailDisplayed==false)
		{
			_abilityDetailDisplayer.DisplayerAbilityDetail(_ability,transform.position.y+1f,_cooldownTimer);
			_detailDisplayed=true;
		}
	}
	void OnHover(bool isOver)
	{
		if(isOver)
		{
			_showDetailTimer=0;
		}
		else{
			if(_detailDisplayed)
			{
				_abilityDetailDisplayer.HideAbilityDetail();
			}
			_detailDisplayed=false;
			_showDetailTimer=float.NegativeInfinity;
		}
	}
	void OnClick()
	{
		if (_clickTimer > _clickInterval) {
						_clickTimer = 0;
						MouseClick ();
				}
	}

	void MouseClick()
	{
//		Debug.Log("Ability activate : "+_ability.name);
		if(_available)
		{
			_glowEdge.SetActive(true);
			_battleCardShell.battleController.AbilityClick(this);
		}
		else if(_cooldownTimer<_ability.cooldown)
		{
			_battleCardShell.battleController.dynamicTextAdmin.DynamicText(transform.position,"Not Available");
		}
		else
		{
			_battleCardShell.battleController.dynamicTextAdmin.DynamicText(transform.position,"Out Of Mana");
		}

	}
	public void LoadAbility(Ability ability)
	{
		gameObject.SetActive(false);
		_vacant=false;
		_ability=ability;
		renderer.material.mainTexture=_ability.icon;
		_cooldownTimer=0;
		if (_cooldownTimer == 0) {
			_available=true;
				}
	}

	public void Clear()
	{
		_ability=null;
		_available=false;
		_vacant=true;
		_glowEdge.SetActive(false);
		_cooldownTimer=0;
	}

	public void Show(float distance,float duration)
	{
		gameObject.SetActive(true);
		if(vacant)
		{
			Debug.Log("Empty AbilityShell,'Show' method is unsupported");
			throw new System.MethodAccessException("Empty AbilityShell,'Show' method is unsupported");
		}
		if(_cooldownTimer<_ability.cooldown)
		{	//Is still in cooldown.
			//_ability.cooldown must be positive due to _clickTimer is less than it and _clickTimer is not negative.
			_available=false;
			_cdPercent=(_cooldownTimer%_ability.cooldown)/_ability.cooldown;
		}
		else if(_battleCardShell.battleCard.mana<_ability.mana)
		{//Not enough mana.
			_available=false;
			_cdPercent=0;
		}
		else
		{	//Ability is available to cast
			_available=true;
			_cdPercent=1f;
		}

		HOTween.To(transform,duration,new TweenParms().Prop("localPosition",transform.localPosition+new Vector3(0,distance,0)).Ease(EaseType.EaseOutQuad)
		           .OnStart(delegate (){
			_battleCardShell.battleController.shieldPanel.Activate();
		          }).OnComplete(delegate() {
			_battleCardShell.battleController.shieldPanel.Deactivate();
		}));

	}

	public void Hide(float duration)
	{

		_glowEdge.SetActive(false);
		HOTween.To(transform,duration,new TweenParms().Prop("localPosition",_localPosition).Ease(EaseType.Linear)
		           .OnStart(delegate (){
			_battleCardShell.battleController.shieldPanel.Activate();
		}).OnComplete(delegate() {
			gameObject.SetActive(false);
			_battleCardShell.battleController.shieldPanel.Deactivate();
		}));
	}

	public void NewRound()
	{
	_cooldownTimer+=1;
	}

	public void UpdateCDTimer()
	{
		_cooldownTimer=0;
	}
}
