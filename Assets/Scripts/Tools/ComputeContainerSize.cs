using UnityEngine;
using System.Collections;

public class ComputeContainerSize : MonoBehaviour
{
		
		[ContextMenu("Compute Containei Size")]
		void ComputeSize ()
		{
				SetPositionAndSize posAndSize = GetComponent<SetPositionAndSize> ();
				int width = 0, height = 0;
				UIWidget[] widgets = GetComponentsInChildren<UIWidget> ();
				foreach (UIWidget widget in widgets) {
						if (widget.transform != transform) {
								width = (int)Mathf.Max (width, widget.localSize.x + widget.transform.localPosition.x);
								height = (int)Mathf.Max (height, -widget.transform.localPosition.y+widget.localSize.y);
						}
				}

				posAndSize.pixelWidth = width;
				posAndSize.pixelHeight = height;
		}
}
