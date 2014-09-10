using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class DynamicTextAdmin : MonoBehaviour {
	public UILabel _textPrefab;
	public float _movementDuration=1f;
	public Vector3 _movementDirection;
	public Color _textColor=Color.blue;

	//Range(0,1).
	[Range(0f,1f)]
	public float _fadeOutTiming=0.8f;
	private Transform _activeSet,_deactiveSet;
	void Awake()
	{
		_activeSet=transform.GetChild(0);
		_deactiveSet=transform.GetChild(1);
	}


	public void DynamicText(Vector3 startPosition,string text)
	{
		DynamicText(startPosition,text,_movementDuration,_movementDirection,_textPrefab,_textColor);
	}
	public void DynamicText(Vector3 startPosition,string text,Color color)
	{
		DynamicText(startPosition,text,_movementDuration,_movementDirection,_textPrefab,color);
	}
	public void DynamicText(Vector3 startPosition,string text,float duration,UILabel sample)
	{
		DynamicText(startPosition,text,duration,_movementDirection,sample,_textColor);
	}
	public void DynamicText(Vector3 startPosition,string text,float duration,
	                        Vector3 direction,UILabel sample)
	{
		DynamicText(startPosition,text,duration,direction,sample,_textColor);
	}
	public void DynamicText(Vector3 startPosition,string text,float duration,
	                        Vector3 direction,UILabel sample,Color color)
	{
		if(text==null||text=="")
		{
			Debug.Log("Please set text's content");
			throw new System.ArgumentNullException("Please set text's content");
		}
		if(duration==0)
		{
			duration=_movementDuration;
		}
		if(sample==null)
		{
			sample=_textPrefab;
		}
		duration=Random.Range(duration-0.25f,duration+0.25f);
		startPosition=startPosition+Random.insideUnitSphere*0.5f;
		ActivateText(startPosition,text,duration,color,direction,sample);
	}

	void ActivateText(Vector3 startPosition,string text,float duration,
	                  Color color,Vector3 direction,UILabel sample)
	{
		GameObject textGO;
		if(_deactiveSet.childCount>0)
		{
			textGO=_deactiveSet.GetChild(0).gameObject;
		}
		else
		{
			textGO=(Instantiate(sample)as UILabel).gameObject;

		}

		textGO.transform.parent=_activeSet;
		UILabel label=textGO.GetComponent<UILabel>();

		textGO.transform.position=startPosition;
		label.text=text;
		label.gradientTop=color;
		label.alpha=1;
//		label.color=color;
		textGO.SetActive(true);

		HOTween.To(textGO.transform,duration,new TweenParms().Prop("position",textGO.transform.position+direction)
		           .Ease(EaseType.EaseOutQuad).OnComplete(delegate() {
			textGO.SetActive(false);
			textGO.transform.parent=_deactiveSet;
		}));
		StartCoroutine(TextFadeOut(duration*_fadeOutTiming,duration*(1-_fadeOutTiming),label));

	}

	IEnumerator TextFadeOut(float startTime,float duration,UILabel label)
	{
		yield return new WaitForSeconds(startTime);
		HOTween.To(label,duration,new TweenParms().Prop("alpha",0).Ease(EaseType.Linear));
	}
}
