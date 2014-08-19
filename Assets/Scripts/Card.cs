using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
//public abstract class Actor{
////	public abstract void Attack();
////	public abstract void UnderAttack();
//}
public class CardBuff
{
	string _field;
	int _deltaValue;
	int _round;
}

public abstract class BaseCard{
protected	internal int _magic,_health,_defense,_damage,_dropRate,_resilience,_Price;
protected 	internal	string _description;
protected	 internal Ability[] _abilities;
}

public delegate void Cast(Card from,Card to);
public abstract class Ability{
	public enum CastTarget{
		Self,Enemy,Friendly
	};
	public enum CastArea{
		Single,Area
	};
	protected string _name;
	protected string _description;
	protected	CastArea _releaseArea;
	protected	CastTarget _releaseTarget;
	protected Cast _cast;
	
}

public class InitCard:BaseCard
{
	int _id;
	public InitCard()
	{
		Type t = typeof(Card);
		Card c = new Card ();

		}
}
public class Card:BaseCard
{
	public enum Quality{
		Normal,Good,Elite,Legend
	};
	int _id,_experience;

	public Card(InitCard initCard)
	{

	}
}

