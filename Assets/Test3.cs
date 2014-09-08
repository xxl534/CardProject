using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using System.Collections.Generic;
public class Test3 : MonoBehaviour {
	void Awake()
	{
//		HOTweenComponent hotc=GetComponent<HOTweenComponent>();
//		List<HOTweenManager.HOTweenData> hotweenDataList=hotc.tweenDatas;
//		hotweenDataList[0].propDatas[0].endValVector3=new Vector3 (1f,1f,0);
//		Debug.Log((hotweenDataList[0].targetName)+"||"+hotweenDataList[0].propDatas[0].propName);
	}

	void OnCollisionEnter()
	{
		Debug.Log("enter");
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("trigg");
	}
}
