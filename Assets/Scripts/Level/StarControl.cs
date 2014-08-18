using UnityEngine;
using System.Collections;
using Holoville.HOTween.Plugins;
using Holoville.HOTween;

public class StarControl : MonoBehaviour
{
		StarSlotControl _starSlot;
		Vector3[] _flyPath = null;
		static float _flyTime = 0.5f;
		 static float _flyToCounterTime = 0.3f;
		public FlyStar _flystar;
		StarCounterLayerControl _starCounter;
		// Use this for initialization
		void Awake ()
		{
				_starSlot = GetComponentInParent<StarSlotControl> ();
				_starCounter = GameObject.FindGameObjectWithTag (Tags.starCounter).GetComponent<StarCounterLayerControl> ();
			
		}
	
		// Update is called once per frame
		void Update ()
		{
		}

		public void Activate ()
		{

				if (_flyPath == null || _flyPath.Length == 0) {
						_flyPath = _starSlot.GetFlyPath ();
				}
				_flystar = (Instantiate (Resources.Load<GameObject> ("Prefabs/flyStar"))as GameObject).GetComponent<FlyStar>();
				_flystar.transform.parent = _starSlot.transform;

				_flystar.transform.position = _flyPath [0];
				_flystar.transform.localScale = Vector3.one * 1.5f;



				HOTween.To (_flystar.transform, _flyTime, new TweenParms ().Prop ("position", new PlugVector3Path (_flyPath)).Ease (EaseType.Linear).OnComplete (ActivateComplete));
				HOTween.To (_flystar.transform, _flyTime, new TweenParms ().Prop ("localScale", Vector3.one).Ease (EaseType.Linear));
				

		}

		void ActivateComplete ()
		{
		_flystar.TurnOffTail ();
				if (!gameObject.activeInHierarchy) {
						gameObject.SetActive (true);
						HOTween.To (_flystar.transform, _flyToCounterTime, new TweenParms ().Prop ("position", _starCounter._bigStar.transform.position).Ease (EaseType.Linear).OnComplete (ActivateNewStar));
						HOTween.To (_flystar.transform, _flyToCounterTime, new TweenParms ().Prop ("localScale", Vector3.one * 2.3f).Ease (EaseType.Linear));
				} else {
						Destroy (_flystar);
						_flystar = null;
				}

		}

		void ActivateNewStar ()
		{
		_flystar.TurnOffTail ();
		_flystar.Blow ();
				
				Destroy (_flystar.gameObject,0.2f);
				_starCounter.GainStar ();

		}
}
