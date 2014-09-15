using UnityEngine;
using System.Collections;

public class BagSlot : MonoBehaviour {
	private static Color[] _bgColor;

	public UISprite _Icon;
	public UILabel _name,_level,_price,_experience;
	public UIToggle _sell;
	public UISlider _experienceSlider;
	public UISprite _slotEdge,_background;
	public UIWidget _slotBody;
	private ConcreteCard _concreteCard;
	private BagManagement _bagManagement;

	private float _clickTiming=0.3f,_clickTimer=0,_hoverTiming=4f,_hoverTimer=float.NegativeInfinity;

	public ConcreteCard concreteCard
	{
		get{return _concreteCard;}
	}
	public UIWidget slotBody
	{
		get{return _slotBody;}
	}
	public bool isSell {
				get;
				set;
	}

	static BagSlot()
	{
		_bgColor = new Color[5];
		_bgColor [0] = new Color (0.53f,0.53f,0.53f);
		_bgColor [1] = new Color (0.177f,1f,0.345f);
		_bgColor [2] = new Color (0.169f,0.38f,1f);
		_bgColor [3] = new Color (0.718f,0.247f,0.88f);
		_bgColor [4] = new Color (1f,0.518f,0f);
	}
	// Use this for initialization
	void Start () {
		_bagManagement = GetComponentInParent<BagManagement> ();
		_sell.instantTween = false;

	}
	
	// Update is called once per frame
	void Update () {
		_clickTimer += Time.deltaTime;
		_hoverTimer += Time.deltaTime;
		if (_hoverTimer > _hoverTiming) {
			_bagManagement.SlotShowDetail(this);
			_hoverTimer=float.NegativeInfinity;
				}
	}

	void OnClick()
	{
		if (_clickTimer > _clickTiming) {
			_bagManagement.SlotClick(this);
			_clickTimer=0f;
				}
	}
	void OnHover(bool isOver)
	{
		if (isOver) {
						_hoverTimer = 0;
				} else {
			_hoverTimer=float.NegativeInfinity;
				}

		if (isOver) {
						_background.alpha = 1f;
				} else {
			_background.alpha=0.66f;
				}
	}

	public void LoadConcreteCard(ConcreteCard card)
	{
		_concreteCard = card;
		_Icon.spriteName = card.name;
		_name.text = card.name;
		_level.text = "Lv." + card.level.ToString ();
		_price.text = card.price.ToString ();
		_experience .text= card.experience.ToString () + "/" + BaseCard.experienceTable [card.level-1];
		_sell.value=false;
		_experienceSlider.value = card.experience / (float)BaseCard.experienceTable [card.level - 1];
		_slotEdge.color = Color.white;
		_background.color = _bgColor [(int)card.rarity];
		Deselect ();
	}

	public void Refresh()
	{
		_level.text = "Lv." + _concreteCard.level.ToString ();
		_price.text = _concreteCard.price.ToString ();
		_experience .text= _concreteCard.experience.ToString () + "/" + BaseCard.experienceTable [_concreteCard.level-1];
		_experienceSlider.value = _concreteCard.experience / (float)BaseCard.experienceTable [_concreteCard.level - 1];
	}

	public void SetSell()
	{
		isSell = _sell.value;
	}

	public void Select()
	{
		_slotEdge.color = Color.white;
	}

	public void Deselect()
	{
		_slotEdge.color=Color.black;
	}
}
