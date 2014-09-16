using UnityEngine;
using System.Collections;

public static class RarityColorStatic {
	public static Color[] rarityColors;
	public static Color  normal ;
	public static Color rare;
	public static Color precious;
	public static Color legendary;
	public static Color epic ;

	static RarityColorStatic()
	{
		normal = new Color (0.53f,0.53f,0.53f);
		rare= new Color (0.177f,1f,0.345f);
		precious = new Color (0.169f,0.38f,1f);
		legendary = new Color (0.718f,0.247f,0.88f);
		epic = new Color (1f,0.518f,0f);
		rarityColors = new Color[]{normal,rare,precious,legendary,epic};
	}
}
