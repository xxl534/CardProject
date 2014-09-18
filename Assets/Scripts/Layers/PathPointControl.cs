using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;
using Holoville.HOTween;

public class PathPointControl : MonoBehaviour 
{
		public PathPointControl _nextPathPoint;
		public LevelGateControl _nextLevelGate;
//		public int _levelPostfix;
//		public int _pointPostfix;
		public float _glowTime = 0.3f;
		bool _active;
	UISprite _sprite;
		// Use this for initialization
		void Awake ()
		{
//				MatchCollection matchCollection = Regex.Matches (name, "_(\\d+)");
//				_levelPostfix = int.Parse (matchCollection [0].Groups [1].ToString ());
//				_pointPostfix = int.Parse (matchCollection [1].Groups [1].ToString ());
		_sprite = GetComponent<UISprite> ();
				_active = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

//		public int CompareTo (PathPointControl other)
//		{
//				if (transform.parent.name.Equals (other.transform.parent.name))
//						return name.CompareTo (other.name);
//				return transform.parent.name.CompareTo (other.transform.parent.name);
//		}

		/// <summary>
		/// Set this path point  activated,determined by saved data .
		/// </summary>
//		public void SetActivated ()
//		{
//
//		}

		/// <summary>
		///Activate by game progress.Animation can be inserted 
		/// </summary>
		public void Activate ()
		{
				if (_active == false) {
						Debug.Log (name + "activate");
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

 public 	void Init(bool show)
	{
		if (show) {
			gameObject.SetActive(true);
			_active=true;
			_sprite.alpha=1f;
			if(_nextPathPoint!=null)
			{
				_nextPathPoint.Init(show);
			}
				}
	}
}
