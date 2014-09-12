using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class AbilityDetailDisplayer : MonoBehaviour
{
	
	public UITexture _texture_abilityIcon;
	public UILabel _label_level,_label_comsume,_label_cooldown,_label_description,_label_name;
	public float _fadeTime=0.5f;
	private UIPanel _panel;
	void Awake()
	{
		_panel=GetComponent<UIPanel>();
		_panel.alpha=0;
	}
	void Start()
	{
		gameObject.SetActive(false);
	}
	public void DisplayerAbilityDetail(Ability abililty,Vector3 position)
	{
		DisplayerAbilityDetail(abililty,position,-1);
	}
	public void DisplayerAbilityDetail(Ability abililty,Vector3 position,int cdRound)
	{
		_texture_abilityIcon.mainTexture=abililty.icon;
		_label_name.text=abililty.name;
		_label_level.text="Lv."+abililty.level.ToString();
		string txt_cd;
		if(cdRound>=0)
		{
			txt_cd=string.Format("CD:{0}/{1}",cdRound%(abililty.cooldown+1),abililty.cooldown);
		}
		else
		{
			txt_cd="CD:"+abililty.cooldown.ToString();
		}
		_label_cooldown.text=txt_cd;
		_label_comsume.text=abililty.mana.ToString();
		_label_description.text=abililty.GetDescription();

		transform.position=new Vector3(transform.position.x,position.y,transform.position.z);
		gameObject.SetActive(true);
		HOTween.To(_panel,_fadeTime,new TweenParms().Prop("alpha",1f).Ease( EaseType.Linear));		
	}

	public void HideAbilityDetail()
	{
		HOTween.To(_panel,_fadeTime,new TweenParms().Prop("alpha",0f).Ease( EaseType.Linear));		
		gameObject.SetActive(false);
	}
}

