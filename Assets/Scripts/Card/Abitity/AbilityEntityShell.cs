using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityEntityShell : MonoBehaviour
{
		AbilityEntity _abilityEntity;
		Animation[] _animationArray;
		HOTweenComponent[] _hotweenComArray;


	public AbilityEntity abilityEntity
	{
		get{return _abilityEntity;}
	}
		void Awake ()
		{
		}

		public void Init (AbilityEntity abilityEntity)
		{
				_abilityEntity = abilityEntity;
				_animationArray = GetComponentsInChildren<Animation> ();
				_hotweenComArray = GetComponentsInChildren<HOTweenComponent> ();
				Vector3 destination = abilityEntity.targetCard.transform.position;

				//Set animations playing position
				foreach (var item in _animationArray) {
						item.transform.position = destination;
				}

				//Set destination of hotweens which tween transform.position.
				foreach (HOTweenComponent hotweenCom in _hotweenComArray) {
						foreach (HOTweenManager.HOTweenData hotweenData in hotweenCom.tweenDatas) {
								if (hotweenData.targetName == "transform") {
										foreach (HOTweenManager.HOPropData propData in  hotweenData.propDatas) {
												if (propData.propName == "position") {
														propData.endValVector3 = destination;
												}
										}
								}
						}
				}
		}

	public void AttachTheTarget()
	{}
}
