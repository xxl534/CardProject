using UnityEngine;
using System.Collections;

public class SceneFade : MonoBehaviour {
	float _fadeTime=1.5f;
	bool _toLight=false;
	UITexture fader;

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

	public void BeginFading()
	{
		_toLight = false;
		gameObject.SetActive (true);
		}
	 void FadeToDark()
	{
		fader.alpha = Mathf.Lerp (fader.alpha, 1f, _fadeTime * Time.deltaTime);
		if (fader.alpha > 0.95)
						fader.alpha = 1f;
	}

	public void ExitFading()
	{
		_toLight = true;
		}
	 void FadeToLight()
	{
		fader.alpha = Mathf.Lerp (fader.alpha, 0f, _fadeTime * Time.deltaTime);
		if (fader.alpha < 0.05) {
			fader.alpha=0;
			gameObject.SetActive(false);
				}
	}

	public bool IsOpeque()
	{
		return fader.alpha==1;
	}

}
