using UnityEngine;
using System.Collections;

public class RealCard : BaseCard {
	protected CardRarity _cardRarity;
	protected int _experience;
	protected int _level;

	public RealCard(BaseCard baseCard):base(baseCard)
	{

		_cardRarity = CardRarity.Normal;
		_experience = 0;
		_level = 1;
	}

	public RealCard(BaseCard baseCard,int level,params int[] abilityLevel)
	{

	}

	/// <summary>
	/// Used by derived class to initial its base part.
	/// </summary>
	/// <param name="realCard">real card.</param>
	protected RealCard(RealCard realCard)
	{

	}

	public void ExperienceIncrease(int quantity)
	{

	}

	public void LevelUp()
	{}

}
