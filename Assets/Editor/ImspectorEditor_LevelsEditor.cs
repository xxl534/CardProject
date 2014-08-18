using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelsEditor))]
public class ImspectorEditor_LevelsEditor : Editor
{

		int selectValue = 0;

		public override void OnInspectorGUI ()
	{
		LevelsEditor myLeverlEditor = (LevelsEditor)target ;
		if (GUILayout.Button ("Create a map layer")) {
			myLeverlEditor.GeneNewMapLayer();
				}
			int mapLayerCount = myLeverlEditor.mapLayers.Count;
			string[] displayOptions = new string[mapLayerCount];
				int[] mapLayerIndex = new int[mapLayerCount];
				for (int i=0; i!=mapLayerCount; i++) {
						displayOptions [i] = myLeverlEditor.mapLayers [i].name;
						mapLayerIndex [i] = i;
				}
				selectValue = EditorGUILayout.IntPopup (selectValue, displayOptions, mapLayerIndex);
		EditorGUILayout.LabelField("haha","zhenshide");
		                                     
		}


}
