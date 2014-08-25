using UnityEngine;
using System.Collections;

public class BattleCard : ConcreteCard {
	protected int _health;
	public int health{
		get {return _health;}
		set{_health=value;}
	}
	protected ConcreteCard _Card;
	public BattleCard(ConcreteCard concreteCard)
	{

	}
}
