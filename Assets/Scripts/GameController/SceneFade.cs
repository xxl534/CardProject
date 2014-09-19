using UnityEngine;
using System.Collections;

public delegate void FadeEvent();

public class SceneFade : MonoBehaviour {
	float _fadeTime=1.5f;
	bool _toLight=false;
	UITexture fader;
	FadeEvent _fadeInEvent,_fadeOutEvent;
	// Use this for initialization
	void Awake(){
		fader = GetComponentInChildren<UITexture> ();
		fader.alpha = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (_toLight)
						FadeToLight ();
				else
						FadeToDark ();
	}


	public void BeginFading(FadeEvent fadeEvent=null)
	{
		_toLight = false;
		gameObject.SetActive (true);
		_fadeInEvent = fadeEvent;
		}


	 void FadeToDark()
	{
		fader.alpha = Mathf.Lerp (fader.alpha, 1f, _fadeTime * Time.deltaTime);
		if (fader.alpha > 0.98) {
			if(_fadeInEvent!=null){
			_fadeInEvent();
			_fadeInEvent=null;
			}
						fader.alpha = 1f;
				}
	}


	public void ExitFading(FadeEvent fadeEvent=null)
	{
		_toLight = true;
		_fadeOutEvent = fadeEvent;
		}
	 void FadeToLight()
	{
		fader.alpha = Mathf.Lerp (fader.alpha, 0f, _fadeTime * Time.deltaTime);
		if (fader.alpha < 0.02) {
			fader.alpha=0;
			if(_fadeOutEvent!=null){
				_fadeOutEvent();
			_fadeOutEvent=null;
			}
			gameObject.SetActive(false);
				}
	}

	public bool IsOpeque()
	{
		return fader.alpha==1;
	}



}
