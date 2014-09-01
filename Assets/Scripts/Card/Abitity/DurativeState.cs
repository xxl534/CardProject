using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class DurativeState:AbilityEntity {
	protected  int _duration,_interval,_roundCounter;
	public abstract void RoundStart();
	public abstract void RoundEnd();
}
