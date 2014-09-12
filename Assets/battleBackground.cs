using UnityEngine;
using System.Collections;

public class battleBackground : MonoBehaviour
{
		float _clickTimer = 0;
		float _clickInterval = 0.3f;
		public BattleControl _battleController;

		void Update ()
		{
				_clickTimer += Time.deltaTime;
		}

		void OnClick ()
		{
				if (_clickTimer > _clickInterval) {
						_clickTimer = 0;
						MouseClick ();
				}
		}
	
		void MouseClick ()
		{
				_battleController.BackgroundClick ();
		}
}
