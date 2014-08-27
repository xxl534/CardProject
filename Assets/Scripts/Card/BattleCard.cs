using UnityEngine;
using System.Collections;

public class BattleCard : ConcreteCard {
	protected int _health;
	/// <summary>
	/// If health less equal than zero ,this battle card will be dead and be moved from battle field.
	/// </summary>
	/// <value>The health.</value>
	public int health{
		get {return _health;}
		set{
			_health=value;
			if(_health<=0)
			{
				CardDead();
			}
		}
	}
	protected ConcreteCard _Card;

	public BattleCard(ConcreteCard concreteCard)
	{

	}


	void CardDead()
	{

	}
}
