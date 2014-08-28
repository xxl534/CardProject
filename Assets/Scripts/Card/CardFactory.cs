using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public static class CardFactory {
	private static Dictionary<int,BaseCard> _baseCardList;

	public static ConcreteCard GeneConcreteCart(BaseCard baseCard,Rarity rarity,int level,int[] abilitiesLevel=null)
	{
		List<Ability> abilityList=new List<Ability>();
		//That's for not to exam whether the array is null every for-loop
		if (abilitiesLevel == null)
						abilitiesLevel = new int[0];
		for(int i=0;i!= baseCard.abilitiesId.Length;i++)
		{
			int abilityLevel=1;
			if(i<abilitiesLevel.Length)
			{
				abilityLevel=abilitiesLevel[i];
			}
			if(abilityLevel<1)
			{
				Debug.Log("Level of ability should be positive.");
				throw new System.ArgumentException("abilitiesLevel");
			}

			Ability ability= AbilityFactory.GeneAbility(baseCard.abilitiesId[i],abilityLevel);
			abilityList.Add (ability);
		}
		return new ConcreteCard(baseCard,rarity,level,abilityList);
	}
}
