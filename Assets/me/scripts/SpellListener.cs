using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SpellListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public Text GestureInfo;
	public bool primero;

	void Awake(){
		primero = true;
	}


	public void UserDetected(uint userId, int userIndex)
	{	
		if (primero==true) {
			Debug.Log ("primero");
			gameObject.GetComponent<GameController> ().inicio ();
			primero = false;
		} else {
			Debug.Log ("reanudar");
			gameObject.GetComponent<GameController> ().reanudar ();
		}

	}
	
	public void UserLost(uint userId, int userIndex)
	{
		if(gameObject.GetComponent<GameController> ().final==false){
		gameObject.GetComponent<GameController> ().pausa ();
		}
		//aqui algo que diga que ya no estas
	}
	
	public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		// algo para dar feedback?
	}
	
	public bool GestureCompleted (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{

		if (gesture == KinectGestures.Gestures.Stop) {
			if (gameObject.GetComponent<GameController> ().jugar) {
				gameObject.GetComponent<GameController> ().pausa ();
			} else {
				gameObject.GetComponent<GameController> ().reanudar ();
			}

		}
		else if (gesture == KinectGestures.Gestures.Jump) {
			gameObject.GetComponent<GameController> ().jugarNota(1);
		}
		else if (gesture == KinectGestures.Gestures.Squat) {
			gameObject.GetComponent<GameController> ().jugarNota(3);
		}
		else if (gesture == KinectGestures.Gestures.SwipeRight) {
			gameObject.GetComponent<GameController> ().jugarNota(2);
		}
		else if (gesture == KinectGestures.Gestures.SwipeLeft) {
			gameObject.GetComponent<GameController> ().jugarNota(4);
		}
		else if (gesture == KinectGestures.Gestures.lumus) {
			gameObject.GetComponent<GameController> ().lumus();
		}	
		else if (gesture == KinectGestures.Gestures.aquamenti) {
			gameObject.GetComponent<GameController> ().aquamenti();
		}	
		else if (gesture == KinectGestures.Gestures.wingardium) {
			gameObject.GetComponent<GameController> ().wingardiumLeviosa();
		}	
		return true;
	}
	
	public bool GestureCancelled (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint)
	{
		//sonido cuando te equivocas (?
		return true;
	}
	
}