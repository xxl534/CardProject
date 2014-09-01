using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityFactory
{
	private static AbilityFactory _instance;
		private Dictionary <int,AbilityBase> _BaseAbilityTable;
		private Dictionary<string ,int> _BaseAbilityIdTable;

		private AbilityFactory ()
		{
				_BaseAbilityTable = new Dictionary<int, AbilityBase> ();
		_BaseAbilityIdTable=new Dictionary<string, int>();
		LoadAbilities();
		}
		
	public static AbilityFactory GetAbilityFactory()
	{
		if(_instance==null)
			_instance=new AbilityFactory();
		return _instance;
	}
		void LoadAbilities ()
		{
				TextAsset[] textAssets = Resources.LoadAll<TextAsset> (ResourcesFolderPath.json_ability);
				foreach (TextAsset tAsset in textAssets) {
						string jsonString = tAsset.text;
						var dict = MiniJSON.Json.Deserialize (jsonString) as Dictionary<string,object>;
						Dictionary<string,object> abilitiesInfo = dict ["ability"] as Dictionary<string,object>;
						foreach (string abilityName in abilitiesInfo.Keys) {
								//Detect existence of cardName
								if (_BaseAbilityIdTable.ContainsKey (abilityName)) {
										Debug.Log (string.Format ("Ability name has existed -name:{0} in folder: 'Resources/Json/Ability/{1}'", abilityName, tAsset.name));
										throw new System.Exception (string.Format ("Ability name has existed -name:{0} in folder: 'Resources/Json/Card/{1}'", abilityName, tAsset.name));
								}
								KeyValuePair<string,Dictionary<string,object>> abilityInfo
					= new KeyValuePair<string, Dictionary<string, object>> (abilityName, abilitiesInfo [abilityName]as Dictionary<string,object>);
				
								AbilityBase abilityBase;
								try {
										abilityBase = AbilityBase.GeneAbilityBase (abilityInfo);
								} catch (System.Exception e) {
										Debug.Log (string.Format ("Fail to Generate AbilityBase via ability data that named:{0}", abilityName));
										throw e;
								}
								int id = abilityBase.id;
								//Detect existence of baseCard's id
								if (_BaseAbilityTable.ContainsKey (id)) {
										Debug.Log (string.Format ("Ability id has existed -name:{0} in folder: 'Resources/Json/Ability/{1}'", abilityName, tAsset.name));
										throw new System.Exception (string.Format ("Ability id has existed -name:{0} in folder: 'Resources/Json/Ability/{1}'", abilityName, tAsset.name));
								}
								//Check the validity of ability
								if (!CheckAbilityValidity (abilityBase)) {
										Debug.Log (string.Format ("Failure to pass ability validity test about ability named :{0} in folder: 'Resources/Json/Ability/{1}'", abilityName, tAsset.name));
										throw new System.Exception (string.Format ("Failure to pass ability validity test about ability named :{0} in folder: 'Resources/Json/Ability/{1}'", abilityName, tAsset.name));
								}
								_BaseAbilityIdTable.Add (abilityName, id);
								_BaseAbilityTable.Add (id, abilityBase);
						}
				}
		}

		bool CheckAbilityValidity (AbilityBase abilityBase)
		{
				bool bPass = true;
				bPass &= AbilityCastStatic.HasAbilityCast (abilityBase.abilityCast);
				bPass &= EffectCardStatic.HasEffectCard (abilityBase.effectCard);
				foreach (string variableName in abilityBase.variables) {
						bPass &= AbilityVariable.HasVariableString (variableName);
				}
				return bPass;
		}

		public Ability GeneAbility (int abilityId, int level=1)
		{
		return GeneAbility(_BaseAbilityTable[abilityId],level);
		}
		
	public Ability GeneAbility(string abilityName,int level=1)
	{
		return GeneAbility(_BaseAbilityIdTable[abilityName],level);
	}
	Ability GeneAbility(AbilityBase abilityBase,int level=1)
	{
		if(level>abilityBase.maxLevel)
		{
			Debug.Log(string.Format("Level of ability '{0}' should not be larger than maxLevel",abilityBase.name));
			throw new System.ArgumentException(string.Format("Level of ability '{0}' should not be larger than maxLevel",abilityBase.name));
		}
		AbilityCast abilityCast=AbilityCastStatic.GetAbilityCast( abilityBase.abilityCast);
		EffectCard effectCard=EffectCardStatic.GetEffectCard(abilityBase.effectCard);
		Dictionary<string,int > variables=new Dictionary<string, int>();
		List<string> variableName=abilityBase.variables;
		List<int> variableValue=abilityBase.variableValueTable[level-1];
		for (int i = 0; i < variableName.Count; i++) {
			variables.Add(variableName[i],variableValue[i]);
				}
		return new Ability(abilityBase,level,abilityCast,effectCard,variables);
	}
}
