using UnityEngine;
using System.Collections;


public class SetPositionAndSize : MonoBehaviour {

	public int localLeft=0;
	public int localTop=0;
	public int pixelWidth=0;
	public int pixelHeight=0;
	public bool center = false;

	[ContextMenu("LocateAndResize")]
	public void LocateAndResize()
	{
		UIWidget parentUI = transform.parent.GetComponentInParent<UIWidget> ();
		UIWidget thisUI = GetComponent<UIWidget> ();

		if (pixelWidth == 0 || pixelHeight == 0) {
			pixelWidth=System.Convert.ToInt32(thisUI.localSize.x);
				pixelHeight=System.Convert.ToInt32(thisUI.localSize.y);
				}
		int width = pixelWidth, height = pixelHeight;
		//thisUI.pivot = UIWidget.Pivot.TopLeft;
		if (!center){

			Debug.Log((parentUI==null)+"dd");
			thisUI.SetRect (parentUI.localCorners[1].x+localLeft,parentUI.localCorners[1].y -localTop - pixelHeight, width, height);			//UIWidget.SetRect params:left,bottom,width,height

		}
		else {
			thisUI.pivot= UIWidget.Pivot.Center;
//			Debug.Log(parentUI.localCenter.x+"//"+parentUI.localCenter.y);
//			Debug.Log(parentUI.name);
			thisUI.SetRect (parentUI.localCenter.x-thisUI.localSize.x/2+localLeft
			                , parentUI.localCenter.y-thisUI.localSize.y/2-localTop, width, height);
				}
	}


}
