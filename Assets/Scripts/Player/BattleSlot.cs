using UnityEngine;
using System.Collections;

public class BattleSlot : MonoBehaviour {
	public UISprite _Icon;
	public UISprite _Edge;
	private ConcreteCard _concreteCard;
	private BagManagement _bagManagement;

	private float _clickTiming=0.3f,_clickTimer=0,_hoverTiming=4f,_hoverTimer=float.NegativeInfinity;

	public ConcreteCard concreteCard
	{
		get{return _concreteCard;}
	}

	public UIWidget slotBody
	{
		get{return _Icon;}
	}

	public int Index {
		get;
		set;
	}

	void Awake(){
		_bagManagement = GetComponentInParent<BagManagement> ();
	}
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
	}
	public void LoadConcreteCard(ConcreteCard card)
	{
		_concreteCard = card;
		_Icon.spriteName = card.name;
		Deselect ();
		_Icon.alpha = 1;
	}

	public void Select()
	{
		_Edge.color = Color.white;
	}

	public void Deselect()
	{
		_Edge.color = Color.black;
	}

	void SlotClick()
	{
		_bagManagement.SlotClick (this);
	}

	void SlotHover()
	{
		_bagManagement.SlotShowDetail (this);
	}

	public  void Unload()
	{
		_concreteCard = null;
		_Icon.alpha = 0;
	}
}
