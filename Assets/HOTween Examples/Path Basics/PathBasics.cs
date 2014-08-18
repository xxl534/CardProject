using UnityEngine;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;

public class PathBasics : MonoBehaviour
{
	// VARS ///////////////////////////////////////////////////
	
	public		Transform			target1;
	public		Transform			target2;
	
	// ===================================================================================
	// UNITY METHODS ---------------------------------------------------------------------
	
	void Start()
	{
		// Create a path made of 6 points.
		// HOTween will create a smooth curved path that goes through these points,
		// and we will set it to automatically close the path, so that smooth cycling loops can be run.
		Vector3[] path = new Vector3[6];
		path[0] = new Vector3( 0,0,0 );
		path[1] = new Vector3( 5,0,0 );
		path[2] = new Vector3( 5,5,0 );
		path[3] = new Vector3( -2,8,0 );
		path[4] = new Vector3( -5,5,0 );
		path[5] = new Vector3( -5,0,0 );
		
		// Create the tween along the path, using PlugVector3Path.
		// We set the tween to infinite (-1) loops (chaining the Loop parameter to TweenParms),
		// and PlugVector3Path to automatically close the path,
		// orient the target to the movement direction, and keep a constant speed along all points of the curve
		// (chaining the ClosePath, OrientToPath, and ConstantSpeed parameters to PlugVector3Path).
		// 
		// ConstantSpeed serves the purpose of calculating the length of each curve segment correctly
		// (otherwise curve maths calculate each segment as if they had the same length),
		// but easing types can still be used.
		// In this case, we also set the easing to Linear, to have the target cycle along the path at a fixed speed.
		// 
		// Also note that setting PlugVector3Path to relative (new PlugVector3Path( path, true ))
		// will create the path relative to the target's position,
		// while having it absolute (like in this example) will add a connection line from the target's original position
		// to the start of the path (if they're not already the same).
		HOTween.To( target1, 8, new TweenParms().Prop( "position", new PlugVector3Path( path ).ClosePath().OrientToPath() ).Loops( -1 ).Ease( EaseType.Linear ).OnStepComplete( PathCycleComplete ) );
		
		// Target2 will move along the same path, but with a shorter duration, using relative mode
		// (thus no additional segments will be added to reach the starting point),
		// and without using ClosePath to close the path.
		// Also, it uses an easing, a Yoyo loop, and is set to always look at target1.
		HOTween.To( target2, 4, new TweenParms().Prop( "position", new PlugVector3Path( path, true ).LookAt( target1 ) ).Loops( -1, LoopType.Yoyo ).Ease( EaseType.EaseInOutQuad ) );
		
		// Here we set HOTween to show the path in use
		// (it is shown only if in Editor mode, and if the Gizmos button of the Game window is active).
		HOTween.showPathGizmos = true;
	}
	
	void PathCycleComplete()
	{
		Debug.Log( "target1 path cycle/step complete!" );
	}
}
