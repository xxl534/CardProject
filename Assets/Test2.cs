﻿using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;
public class Test2 : Test {
	public UILabel label;

	// Use this for initialization
//	void Start () {
//		Vector3[] path=new Vector3[3];
//		path [0] = transform.position;
//		path [1] = new Vector3 (0f, 10f, 0f);
//		path [2] = new Vector3 (10f, 0f, 0f);
//		HOTween.To (transform, 5, new TweenParms ().Prop ("position", new PlugVector3Path (path,EaseType.EaseOutCubic,PathType.Curved)).Loops(-1));
//		HOTween.showPathGizmos = true;
//	}
	void Start()
	{
//		HOTween .To (label)
	}

	void Update()
	{
//		renderer.material.SetFloat("coldDown",0.9f);
	}

	void OnClick()
	{
//		Debug.Log("Bgclick");
	}
}