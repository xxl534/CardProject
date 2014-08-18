using UnityEngine;
using System.Collections;

public delegate bool BoolMonitor ();

public delegate float FloatMonitor ();

public delegate void Operation ();

enum MonitorType
{
		None,
		Bool,
		FloatLarge,
		FloatLess
}
;

public class RunOnCondition : MonoBehaviour
{
		MonitorType _monitorType;
		BoolMonitor _boolMonitor;
		bool _condition;
		FloatMonitor _floatMonitor;
		float _floatThreshold;
		Operation _oper;
		float _timer;
		// Use this for initialization
		void  Awake ()
		{
				_monitorType = MonitorType.None;
				_timer = 0f;
		}
	
		// Update is called once per frame
		void Update ()
		{
				switch (_monitorType) {
				case MonitorType.Bool:
						if (_boolMonitor () == _condition) {
								_oper ();
								Destroy (this);
						}
						break;
				case MonitorType.FloatLarge:
						if (_floatMonitor () >= _floatThreshold) {
								_oper ();
								Destroy (this);
						}
						break;
				default:
						break;
				}
		}

		public void RunWhenBoolChange (BoolMonitor monitor, bool condition, Operation operation)
		{
				_monitorType = MonitorType.Bool;
				_boolMonitor = monitor;
				_oper = operation;
				_condition = condition;
		}

		public void RunWhenFloatLarger (FloatMonitor monitor, float floatThreshold, Operation operation)
		{
				_monitorType = MonitorType.FloatLarge;
				_floatMonitor = monitor;
				_oper = operation;
				_floatThreshold = floatThreshold;
		}

		public void RunDelay (float delayTime, Operation operation)
		{
				_monitorType = MonitorType.FloatLarge;
				_floatMonitor = delegate {
						_timer += Time.deltaTime;
						return _timer;
				};
				_oper = operation;
				_floatThreshold = delayTime;
		}
}
