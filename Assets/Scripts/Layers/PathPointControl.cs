using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;
using Holoville.HOTween;

public class PathPointControl : MonoBehaviour ,IComparable<PathPointControl>
{
		public PathPointControl _nextPathPoint;
		public LevelGateControl _nextLevelGate;
		public int _levelPostfix;
		public int _pointPostfix;
		public float _glowTime = 0.3f;
		bool _active;
		// Use this for initialization
		void Awake ()
		{
				MatchCollection matchCollection = Regex.Matches (name, "_(\\d+)");
				_levelPostfix = int.Parse (matchCollection [0].Groups [1].ToString ());
				_pointPostfix = int.Parse (matchCollection [1].Groups [1].ToString ());
				_active = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public int CompareTo (PathPointControl other)
		{
				if (transform.parent.name.Equals (other.transform.parent.name))
						return name.CompareTo (other.name);
				return transform.parent.name.CompareTo (other.transform.parent.name);
		}

		/// <summary>
		/// Set this path point  activated,determined by saved data .
		/// </summary>
		public void SetActivated ()
		{

		}

		/// <summary>
		///Activate by game progress.Animation can be inserted 
		/// </summary>
		public void Activate ()
		{
				if (_active == false) {
						Debug.Log (name + "activate");
						_active = true;
						
						HOTween.To (GetComponent<UISprite> (), _glowTime, new TweenParms ().Prop ("alpha", 1f).Ease (EaseType.Linear).OnComplete (ActivateNext));
				}
		}

		void ActivateNext ()
		{
				if (_nextPathPoint != null)
						_nextPathPoint.Activate ();
				else if (_nextLevelGate != null)
						_nextLevelGate.Unlock ();
		}
}
