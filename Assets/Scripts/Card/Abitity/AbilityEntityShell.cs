using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;
public class AbilityEntityShell : MonoBehaviour
{
		AbilityEntity _abilityEntity;
//		public  HOTweenComponent[] _hotweenComArray;
	public float _flyTime;
	public GameObject[] _moveToTarget;
	public GameObject[] _atTargetPosition;
	public GameObject[] _hitEffect;
	public AbilityEntity abilityEntity
	{
		get{return _abilityEntity;}
	}

	void Update()
	{
//		Debug.Log (Time.time);
	}
	public void Init (AbilityEntity abilityEntity)
	{Debug.Log(gameObject.activeSelf);
		Debug.Log (gameObject.activeInHierarchy);
		Debug.Log (gameObject.transform.parent);
		Debug.Log (name);
				_abilityEntity = abilityEntity;
		transform.position = _abilityEntity.castCard.transform.position;
				
				Vector3 destination = abilityEntity.targetCard.transform.position;
				
				//Set animations playing position
				foreach (var item in _atTargetPosition) {

			Debug.Log(8);
						item.transform.position = destination;
				}
				
		foreach (var item in _hitEffect) {
			item.SetActive(false);
			Debug.Log(7);
				}

		foreach (var item in _moveToTarget) {
			item.transform.up=(_abilityEntity.targetCard.transform.position-item.transform.position).normalized;
			HOTween.To(item.transform,_flyTime,new TweenParms().Prop("position",_abilityEntity.targetCard.transform.position));
				}
				//Set destination of hotweens which tween transform.position.
//				foreach (HOTweenComponent hotweenCom in _hotweenComArray) {
//			Debug.Log(hotweenCom.name);
//			Debug.Log(hotweenCom.tweenDatas==null);
//			hotweenCom.generatedTweeners;
//						foreach (HOTweenManager.HOTweenData hotweenData in hotweenCom.tweenDatas) {
//								if (hotweenData.targetName == "transform") {
//										foreach (HOTweenManager.HOPropData propData in  hotweenData.propDatas) {
//												if (propData.propName == "position") {
//														propData.endValVector3 = destination;
//												}
//										}
//								}
//						}
//				}
		}

	public void Hit()
	{
		_abilityEntity.effectCard (_abilityEntity);
		foreach (var item in _moveToTarget) {
			item.SetActive(false);
				}
		_abilityEntity.targetCard.shell.GetHurt ();
		foreach (var item in _hitEffect) {
			item.SetActive(true);
				}
		Destroy (gameObject, 0.5f);
	}

	public void Abort()
	{

	}

}
