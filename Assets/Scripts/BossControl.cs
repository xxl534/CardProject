using UnityEngine;
using System.Collections;

public class BossControl : MonoBehaviour {
	int _health;
	BattleControl _battleController;


	float _showDetailTimer;
	float _timeToShowDetail = 2f;
	float _clickTimer;
	float _clickInterval=0.5f;

	bool _alive;
	// Use this for initialization
	void Awake()
	{
		_alive = true;
		_battleController=GameObject.FindGameObjectWithTag(Tags.battle).GetComponent<BattleControl>();
		_health=3;

		_showDetailTimer = 0f;
		_clickTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (_alive) {
						_clickTimer += Time.deltaTime;
						if (_health <= 0){
								_battleController.BattleComplete ();
				_alive=false;
			}
				}
	}


	void OnMouseOver()
	{
		_showDetailTimer += Time.deltaTime;
		if (_showDetailTimer > _timeToShowDetail)
						_battleController.ShowBossDetail (true);
	}

	void OnMouseExit()
	{
		_showDetailTimer = 0f;
		_battleController.ShowBossDetail (false);
		}
	public void MouseClick()
	{
		if (_clickTimer > _clickInterval) {
			_clickTimer=0f;
						_battleController.BossClick ();
				}
	}
	public void Showoff(){
		Debug.Log("I'm boss");
	}

	public void Injure(int value)
	{
		_health-=4;
	}


}
