// Uses community FPS method (http://www.unifycommunity.com/wiki/index.php?title=FramesPerSecond)

using UnityEngine;
using System.Collections;

/// <summary>
/// Shows an FPS counter on the top right of the screen.
/// </summary>
public class FPSCounter : MonoBehaviour
{
	static public	bool	isEnabled = true;
	
	public	float		updateDelay = 0.5f;
	
	private	GUIStyle	fpsStyle = new GUIStyle();
	private	float		accum = 0;
	private	int			frames = 0;
	private	float		timeleft;
	private	string		fps = "";
	
	private void Awake()
	{
		fpsStyle.alignment = TextAnchor.UpperRight;
		fpsStyle.normal.textColor = Color.white;
		
		timeleft = updateDelay;
	}
	
	private void Update()
	{
		if ( !isEnabled )		return;
		
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		++frames;
		
		if ( timeleft <= 0 ) {
			fps = ( accum / frames ).ToString( "f2" );
			timeleft = updateDelay;
			accum = 0;
			frames = 0;
		}
	}
	
	private void OnGUI ()
	{
		if ( !isEnabled )		return;
		
		GUI.Label( new Rect( Screen.width - 160, 10, 150, 50 ), "FPS: " + fps, fpsStyle );
		
	}
}
