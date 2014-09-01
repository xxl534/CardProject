using UnityEngine;
using System.Collections;

public class StarFlyPath : MonoBehaviour {

	public LevelGateControl _levelControl;

	static Vector3[] relativePath_starOne;
	static Vector3[] relativePath_starTwo;
	static Vector3[] relativePath_starThree;
	


	public Vector3[] GetPathOne()
	{
		if (relativePath_starOne == null) {
			float dx=Mathf.Abs( _levelControl._starOne.transform.position.x-_levelControl._mainButton.transform.position.x);
			float dy=Mathf.Abs( _levelControl._starOne.transform.position.y-_levelControl._mainButton.transform.position.y);
			relativePath_starOne=new Vector3[5];
			relativePath_starOne[0]=Vector3.zero;
			relativePath_starOne[1]=new Vector3(3*-dx,0f,0f);
			relativePath_starOne[2]=new Vector3(3*-dx,dy,0f);
			relativePath_starOne[3]=new Vector3(2*-dx,2*dy,0f);
			relativePath_starOne[4]=new Vector3(-dx,dy,0f);
				}
		return relativePath_starOne;
	}

	public Vector3[] GetPathTwo()
	{
		if (relativePath_starTwo == null) {
			float dx=Mathf.Abs( _levelControl._starOne.transform.position.x-_levelControl._mainButton.transform.position.x);
			float dy=Mathf.Abs( _levelControl._starOne.transform.position.y-_levelControl._mainButton.transform.position.y);
			relativePath_starTwo=new Vector3[5];
			relativePath_starTwo[0]=Vector3.zero;
			relativePath_starTwo[1]=new Vector3(dx,3*dy,0f);
			relativePath_starTwo[2]=new Vector3(0.75f*dx,4*dy,0f);
			relativePath_starTwo[3]=new Vector3(0.5f*dx,3*dy,0f);
			relativePath_starTwo[4]=new Vector3(0,dy,0f);
		}
		return relativePath_starTwo;
	}

	public Vector3[] GetPathThree()
	{
		if (relativePath_starThree == null) {
			float dx=Mathf.Abs( _levelControl._starOne.transform.position.x-_levelControl._mainButton.transform.position.x);
			float dy=Mathf.Abs( _levelControl._starOne.transform.position.y-_levelControl._mainButton.transform.position.y);
			relativePath_starThree=new Vector3[5];
			relativePath_starThree[0]=Vector3.zero;
			relativePath_starThree[1]=new Vector3(3*dx,0f,0f);
			relativePath_starThree[2]=new Vector3(3*dx,dy,0f);
			relativePath_starThree[3]=new Vector3(2*dx,2*dy,0f);
			relativePath_starThree[4]=new Vector3(dx,dy,0f);
		}
		return relativePath_starThree;
	}



}
