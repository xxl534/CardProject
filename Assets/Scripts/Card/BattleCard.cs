using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Holoville.HOTween;

/// <summary>
///This kind of card will only exist on battle field .
///The modification of attribute on BattleCard will not effect relative ConcreteCard 
/// </summary>
public class BattleCard : MonoBehaviour
{
		static float _evasionTime = 0.5f;
	#region Instance member

		protected  BattleCardShell _shell;
		protected ConcreteCard _concreteCard;
		protected CardEffected _cardEffect;
		protected Dictionary<int,DotAndHot> _DotAndHotTable;
		protected Dictionary<int,DebuffAndBuff> _debuffAndBuffTable;
		protected int _health,
				_mana, 
				_strength,
				_agility,
				_magic,
				_maxHealth,
				_maxMana,
				_physicalDefense,
				_magicalDefense,
				_physicalCriticalChance,
				_magicalCriticalChance,
				_physicalDamage,
				_magicalDamage,
				_healthResilience,
				_magicResilience,
				_evasion;
#endregion

	#region Properties

		/// <summary>
		/// If health less equal than zero ,this battle card will be dead and be moved from battle field.
		/// </summary>
		/// <value>The health.</value>
		DynamicTextAdmin dynamicText {
		get{ 
			return _shell.battleController.dynamicTextAdmin;}
		}

		public int health {
				get { return _health;}
				set {
						int valueDelta = value - _health;
						if (valueDelta > 0) {
								dynamicText.DynamicText (transform.position, "+" + valueDelta.ToString (), Color.green);
						} else if (valueDelta < 0) {
						_shell.GetHurt();
								dynamicText.DynamicText (transform.position, valueDelta.ToString (), Color.red);
						}
						
						if (value > _maxHealth) {
								_health = _maxHealth;
						} else {
								_health = value;
						}
			_shell._label_hp.text=_health.ToString();
						if (_health <= 0) {
				_shell.CardRoleDead();
						}
				}
		}

		public int mana {
				get{ return _mana;}
				set {
						int valueDelta = value - _mana;
						dynamicText.DynamicText (transform.position, "MP:" + valueDelta.ToString (), Color.blue);
						if (value > _maxMana) {
								_mana = _maxMana;
						} else {
								_mana = value;
						}
			_shell._label_mp.text=_mana.ToString();
				}
		}

		public int maxHealth {
				get{ return  _maxHealth;}
				set {
						int valueDelta = value - _maxHealth;
						dynamicText.DynamicText (transform.position, "HP-MAX:" + valueDelta.ToString (), Color.red);
						_maxHealth = value;
						if (_maxHealth > _health) {
								_health = _maxHealth;
						}
				}
		}

		public int maxMana {
				get{ return _maxMana;}
				set {
						int valueDelta = value - _maxMana;
						dynamicText.DynamicText (transform.position, "MP-MAX:" + valueDelta.ToString (), Color.blue);
						_maxMana = value;
						if (_maxMana > _mana) {
								_mana = _maxMana;
						}
				}
		}

		public int magicalDefense {
				get{ return _magicalDefense;}
				set { 
						int valueDelta = value - _magicalDefense;
						dynamicText.DynamicText (transform.position, "Spell Resistance:" + valueDelta.ToString ());
						_magicalDefense = value;
				}
		}

		public int magicalCriticalChance {
				get{ return _magicalCriticalChance;}
				set {
						int valueDelta = value - _magicalCriticalChance;
						dynamicText.DynamicText (transform.position, "Spell Crit:" + valueDelta.ToString ());
						_magicalCriticalChance = value;
				}
		}

		public int magicalDamage {
				get{ return _magicalDamage;}
				set {
						int valueDelta = value - _magicalDamage;
						dynamicText.DynamicText (transform.position, "Spell Damage:" + valueDelta.ToString ());
						_magicalDamage = value;
				}
		}

		public int healthResilience {
				get{ return _healthResilience;}
				set {
						int valueDelta = value - _healthResilience;
						dynamicText.DynamicText (transform.position, "HP Recovery:" + valueDelta.ToString ());
						_healthResilience = value;
				}
		}

		public int magicResilience {
				get{ return _magicResilience;}
				set { 
						int valueDelta = value - _magicResilience;
						dynamicText.DynamicText (transform.position, "MP Recovery:" + valueDelta.ToString ());
						_magicResilience = value;
				}
		}

		public int evasion {
				get{ return _evasion;}
				set { 
						int valueDelta = value - _evasion;
						dynamicText.DynamicText (transform.position, "Evasion:" + valueDelta.ToString ());
						_evasion = value;
				}
		}

		public int physicalDamage {
				get{ return _physicalDamage;}
				set { 
						int valueDelta = value - _physicalDamage;
						dynamicText.DynamicText (transform.position, "Physical Damage:" + valueDelta.ToString ());
						_physicalDamage = value;
				}
		}
	
		public int physicalCriticalChance {
				get{ return _physicalCriticalChance;}
				set {
						int valueDelta = value - _physicalCriticalChance;
						dynamicText.DynamicText (transform.position, "Physical Crit:" + valueDelta.ToString ());
						_physicalCriticalChance = value;
				}
		}
	
		public int physicalDefense {
				get{ return _physicalDefense;}
				set { 
						int valueDelta = value - _physicalDefense;
						dynamicText.DynamicText (transform.position, "Defense:" + valueDelta.ToString ());
						_physicalDefense = value;
				}
		}

		//The implement, by accumulation, of 3 properties below are different from ConcreteCard. 
		//The reason is for attribute value restoration  mechanism in battle

		/// <summary>
		/// Max health,physical damage,physical defense and health resilience are changed along with strength
		/// </summary>
		/// <value>The strength.</value>
		public int strength {
				get{ return _strength;}
				set {
						int valueDelta = value - _strength;
						dynamicText.DynamicText (transform.position, "Strength:" + valueDelta.ToString ());
						_strength = value;
						_maxHealth += (int)(valueDelta * BaseCard.rate_strength_maxHealth);
						_physicalDamage += (int)(valueDelta * BaseCard.rate_strength_physicalDamage);
						_physicalDefense += (int)(valueDelta * BaseCard.rate_strength_physicalDefense);
						_healthResilience += (int)(valueDelta * BaseCard.rate_strength_healthResilience);
				}
		}
	
		/// <summary>
		/// Physical defense ,physical critical chance and evasion are changed along with agility
		/// </summary>
		/// <value>The agility.</value>
		public int agility {
				get{ return _agility;}
				set {
						int valueDelta = value - _agility;
						dynamicText.DynamicText (transform.position, "Agility:" + valueDelta.ToString ());
						_agility = value;
						_physicalDefense += (int)(valueDelta * BaseCard.rate_agility_physicalDefense);
						_physicalCriticalChance += (int)(valueDelta * BaseCard.rate_agility_physicalCriticalChance);
						_evasion += (int)(valueDelta * BaseCard.rate_agility_evasion);
				}
		}
	
		/// <summary>
		/// Max mana,magical defense,magical critical chance,magical damage and magic resiliense are changed along with magic
		/// </summary>
		/// <value>The magic.</value>
		public int magic {
				get{ return _magic;}
				set {
						int valueDelta = value - _magic;
						dynamicText.DynamicText (transform.position, "Magic:" + valueDelta.ToString ());
						_magic = value;
						_maxMana += (int)(valueDelta * BaseCard.rate_magic_maxMana);
						_magicalDefense += (int)(valueDelta * BaseCard.rate_magic_magicalDefense);
						_magicalCriticalChance += (int)(valueDelta * BaseCard.rate_magic_magicalCriticalChance);
						_magicalDamage += (int)(valueDelta * BaseCard.rate_magic_magicalDamage);
						_magicResilience += (int)(valueDelta * BaseCard.rate_magic_magicResilience);
				}
		}

		public ConcreteCard concreteCard {
				get{ return _concreteCard;}
		}
		
	public BattleCardShell shell
	{
		get{return _shell;}
	}
		public CardEffected cardEffected {
				get{ return _cardEffect;}
		}

		public List<Ability> abilities {
				get{ return _concreteCard.abilities;}
		}
#endregion

		public void LoadConcreteCard (ConcreteCard concreteCard)
		{
				_concreteCard = concreteCard;
				_strength = concreteCard.strength;
				_agility = concreteCard.agility;
				_magic = concreteCard.magic;
				_maxHealth = concreteCard.maxHealth;
				_maxMana = concreteCard.maxMana;
				_health = _maxHealth;
				_mana = 0;
				_physicalDefense = concreteCard.physicalDefense;
				_magicalDefense = concreteCard.magicalDefense;
				_physicalCriticalChance = concreteCard.physicalCriticalChance;
				_magicalCriticalChance = concreteCard.magicalCriticalChance;
				_physicalDamage = concreteCard.physicalDamage;
				_magicalDamage = concreteCard.magicalDamage;
				_healthResilience = concreteCard.healthResilience;
				_magicResilience = concreteCard.magicResilience;
				_evasion = concreteCard.evasion;
		}

		void Awake ()
		{
				_DotAndHotTable = new Dictionary<int, DotAndHot> ();
				_debuffAndBuffTable = new Dictionary<int, DebuffAndBuff> ();
		_shell = GetComponent<BattleCardShell> ();
				_cardEffect = CardEffectedStatic.CardEffected_Normal;
		}

		public void Clear ()
		{
				FieldInfo[] fields = typeof(BattleCard).GetFields (BindingFlags.NonPublic | BindingFlags.Instance);
				foreach (var item in fields) {
						if (item.FieldType == typeof(int)) {
								item.SetValue (this, 0);
						}
				}
				_debuffAndBuffTable.Clear ();
				_DotAndHotTable.Clear ();

		}

		public void AddDotOrHot (DotAndHot dotOrHot)
		{
				_DotAndHotTable [dotOrHot.id] = dotOrHot;
		}

		public void AddDebuffOrBuff (DebuffAndBuff debuffOrBuff)
		{
				_debuffAndBuffTable [debuffOrBuff.id] = debuffOrBuff;
		}

		public void Evade ()
		{
				dynamicText.DynamicText (transform.position, "Evade!!!");
				_shell.battleController.shieldPanel.Activate ();
				HOTween.To (transform, _evasionTime / 2, new TweenParms ().Prop ("localPotion", transform.localPosition + new Vector3 (1f, 0, 0.5f)).Ease (EaseType.Linear).OnComplete (delegate() {
						HOTween.To (transform, _evasionTime / 2, new TweenParms ().Prop ("localPosition", _shell.originalLocalPosition).Ease (EaseType.Linear).OnComplete (delegate() {
								_shell.battleController.shieldPanel.Deactivate ();
						}));
				}));
				HOTween.To (transform, _evasionTime / 2, new TweenParms ().Prop ("localRotation", Quaternion.Euler (new Vector3 (0, 0, 20f))).Ease (EaseType.Linear).OnComplete (delegate() {
						HOTween.To (transform, _evasionTime / 2, new TweenParms ().Prop ("localRotation", Quaternion.identity).Ease (EaseType.Linear));
				}));
		}
}
