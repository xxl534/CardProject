using UnityEngine;
using System.Collections;


public  class PlayerStateBarControl : MonoBehaviour {

	UILabel _label_value;
	UISprite _valueBar;

	int _fullBarLength;
	int _value = 0;
	public void SetValue(int value)
	{
		_value = value;
		AlterBar ();
	}

	int _maxValue = 0;

	public  void SetMaxValue(int maxValue)
	{
		_maxValue = maxValue;
		AlterBar ();
	}


	void Awake()
	{
		_label_value = GetComponentInChildren<UILabel> ();
		foreach (Transform trans in GetComponentsInChildren<Transform>()) {
			if(trans.name.Equals("valueBar"))
				_valueBar=trans.GetComponent<UISprite>();
				}
		_fullBarLength = (int)_valueBar.transform.parent.GetComponent<UISprite> ().localSize.x;
	}



	void OnMouseEnter()
	{
		ShowValue ();
	}


	void OnMouseExit()
	{
		HideValue ();
	}

	void ShowValue()
	{
		_label_value.text = _value + "/" + _maxValue;
		_label_value.gameObject.SetActive (true);
	}

	void HideValue()
	{
		_label_value.gameObject.SetActive (false);
	}

	void AlterBar()
	{
		_valueBar.SetRect (_valueBar.localCorners [0].x, _valueBar.localCorners [0].y, _value / _maxValue * _fullBarLength, _valueBar.localSize.y);
	}
}
