using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using System.Collections.Generic;
public class Test3 : MonoBehaviour {
	public GameObject de;
	private Vector3 position;
	void Awake()
	{
		position =transform.position;
//		HOTweenComponent hotc=GetComponent<HOTweenComponent>();
//
//		Debug.Log (hotc == null);
//		Debug.Log(hotc.tweenDatas.Count);
//		Debug.Log(hotc.generatedTweeners.Count);
//
//		List<HOTweenManager.HOTweenData> hotweenDataList=hotc.tweenDatas;
//		hotweenDataList [0].propDatas [0].endValVector3 = de.transform.position;
//		hotweenDataList [0].propDatas [0].isActive = true;
//		foreach (var item in hotc.generatedTweeners) {
//			item.ResetAndChangeParms(TweenType.To,item.duration,new TweenParms());
//		}
////		hotc.generatedTweeners [0].Restart ();
//
//
//		Debug.Log((hotweenDataList[0].targetName)+"||"+hotweenDataList[0].propDatas[0].propName);

//		HOTween.To (transform, 5f, new TweenParms ().Prop ("position", de.transform.position,true).Loops(1, LoopType.Yoyo));
	}

//	void OnCollisionEnter()
//	{
//		Debug.Log("enter");
//	}
//
//	void OnTriggerEnter(Collider other)
//	{
//		Debug.Log("trigg");
//	}
//	void Update()
//	{
//		if(transform.position!=position)
//			transform.position=Vector3.Lerp(position,transform.position,0.3f);
//	}

	void OnHover()
	{
		Debug.Log("HOver");}
}
