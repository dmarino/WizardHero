using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public Text GestureInfo;

	
	public void UserDetected(uint userId, int userIndex)
	{
		// detect these user specific gestures
		Debug.Log ("aaaa");
		KinectManager manager = KinectManager.Instance;

		manager.DetectGesture(userId, KinectGestures.Gestures.lumus);

		if(GestureInfo != null)
		{
			GestureInfo.text = "Ready?";
		}
	}
	
	public void UserLost(uint userId, int userIndex)
	{
		if(GestureInfo != null)
		{
			GestureInfo.text = string.Empty;
		}
	}

	public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		// don't do anything here
	}

	public bool GestureCompleted (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{

		string sGestureText = gesture + " detected";
		Debug.Log (sGestureText);
		if(GestureInfo != null)
		{
			GestureInfo.text = sGestureText;
		}

		return true;
	}

	public bool GestureCancelled (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint)
	{
		if(GestureInfo != null)
		{
			GestureInfo.text = "BAAAAD";
		}
		return true;
	}
	
}
