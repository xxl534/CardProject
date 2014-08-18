using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelsEditor : MonoBehaviour {
	public List<UIWidget> _levelButtons=new List<UIWidget>();

	 UIWidget _levelButtonPrefab;
	public List<UIWidget> _mapLayers = new List<UIWidget> (); 

	public UIWidget _selectedMapLayer;

	public List<UIWidget> mapLayers{
		get{
			_mapLayers.Remove(null);
			return _mapLayers;
		}
	}
	public string _levelName;
	public int _left;
	public int _top;

	UISprite _pathPointPrefab;
	public Vector2[] _pathPointsPosition;

	public List<UISprite> _pathPoints=new List<UISprite>();

	void Awake()
	{
		_levelButtonPrefab = Resources.Load <UIWidget>(PrefabPath.btn_levelEntrance);
		_pathPointPrefab = Resources.Load<UISprite> (PrefabPath.path_point);
	}
	public void GeneNewMapLayer()
	{
		//Create an empty container as new map layer
		GameObject mapLayerGO = new GameObject ();
		mapLayerGO.name = "gameLayer_map00" + _mapLayers.Count.ToString();
		mapLayerGO.layer=Layers.NGUI;
		mapLayerGO.transform.parent=GameObject.FindGameObjectWithTag (Tags.UIRoot).transform;
		UIWidget mapLayer=mapLayerGO.AddComponent<UIWidget> ();
		_mapLayers.Add (mapLayer);

		//add map background picture
		GameObject background = new GameObject ();
		background.name="Backgound_Map";
		background.layer = Layers.NGUI;
		background.transform.parent = mapLayerGO.transform;
		UISprite sprite = background.AddComponent<UISprite> ();
	}

	[ContextMenu("Generate new level")]
	public void GeneNewLevel()
	{
		_levelButtons.Add (GeneNewLevel (_levelName,_left, _top,_selectedMapLayer.gameObject));
		//		GeneNewLevel (newLevel, left, top);
	}

	[ContextMenu("Generate path")]
	public void GenePathPoints()
	{
		for (int i = 0; i < _pathPointsPosition.Length; i++) {
			_pathPoints.Add(GeneNewPathPoint((int)_pathPointsPosition[i].x,
			                                (int)_pathPointsPosition[i].y,i+1));
				}
	}

	UISprite GeneNewPathPoint(int left,int top,int index)
	{
		UISprite newPathPoint = GameObject.Instantiate (_pathPointPrefab) as UISprite;
		newPathPoint.name = "sprite_pathPoint_" + index;

		Locate (newPathPoint, left, top,_selectedMapLayer);
		return newPathPoint;
	}


	void Locate(UIWidget widget,int left,int top,UIWidget parent)
	{
		SetPositionAndSize posAndSize= widget.GetComponent<SetPositionAndSize> ();
		if (posAndSize == null)
						posAndSize = widget.gameObject.AddComponent<SetPositionAndSize> ();
			posAndSize.transform.parent = parent.transform;
		
		SetPositionAndSize setPosAndSize = posAndSize.GetComponent<SetPositionAndSize> ();
		setPosAndSize.localLeft = left;
		setPosAndSize.localTop = top;
		setPosAndSize.LocateAndResize ();
		posAndSize.transform.localScale = Vector3.one;

	}

	UIWidget GeneNewLevel(string levelName,int left,int top,GameObject mapLayer)
	{
		UIWidget newLevelBtn=GameObject.Instantiate (_levelButtonPrefab) as UIWidget;
		LevelInfo level=newLevelBtn.gameObject.AddComponent<LevelInfo>();
		level.Init (levelName);
		newLevelBtn.name = "btn_levelEntrance_" + (_levelButtons.Count+1);
		UILabel[] labels=newLevelBtn.GetComponentsInChildren<UILabel>();
		foreach (UILabel label in labels) {
			if(label.name=="label_levelName")
			{
				label.text=level.levelName;
			}
		}

		UIWidget parent = GameObject.FindGameObjectWithTag (Tags.UIRoot).GetComponent<UIWidget> ();
		Locate (newLevelBtn,left,top,parent);
		return newLevelBtn;
	}
}
