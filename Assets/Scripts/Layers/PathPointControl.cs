using UnityEngine;
using System.Collections;
using System;
using Holoville.HOTween;

public class PathPointControl : MonoBehaviour
{
		public PathPointControl _nextPathPoint;
		public LevelGateControl _nextLevelGate;
		public float _glowTime = 0.3f;
		bool _active;
		UISprite _sprite;
		// Use this for initialization
		void Awake ()
		{
				_sprite = GetComponent<UISprite> ();
				_active = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		/// <summary>
		///Activate by game progress.Animation can be inserted 
		/// </summary>
		public void Activate ()
		{
				if (_active == false) {
						_active = true;
						HOTween.To (_sprite, _glowTime, new TweenParms ().Prop ("alpha", 1f).Ease (EaseType.Linear).OnComplete (ActivateNext));
				}
		}

		void ActivateNext ()
		{
				if (_nextPathPoint != null)
						_nextPathPoint.Activate ();
				else if (_nextLevelGate != null)
						_nextLevelGate.Unlock ();
		}

		public 	void Init (bool show)
		{
				if (show) {
						gameObject.SetActive (true);
						_active = true;
						_sprite.alpha = 1f;
						if (_nextPathPoint != null) {
								_nextPathPoint.Init (show);
						}
				}
		}
}
