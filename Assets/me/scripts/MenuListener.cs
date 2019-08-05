using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class MenuListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
		
	// private bool to track if progress message has been displayed
	private bool progressDisplayed;

	[Header("Cursor")]
	public GameObject cursor;
	private Vector3 cursorPos;

	[Header("Menus")]
	public GameObject mainMenu;
	public GameObject instructions;
	public GameObject songs;
	private int posMenu;

	[Header("Instrucciones")]
	public Sprite[] instrucciones;
	public GameObject instruccionActual;
	private int posInstruccion;

	[Header("Canciones")]
	public Sprite[] canciones;
	public GameObject cancionActual;
	private int posCancion;

	void Start(){
		posMenu = 1;
		posInstruccion = 0;
		posCancion = 0;
		cursorPos = cursor.transform.position;
	}
	
	
	public void UserDetected(uint userId, int userIndex)
	{
		//aqui podria haber una pista de que el usuario llego
	}
	
	public void UserLost(uint userId, int userIndex)
	{
		if (cursor != null) {
			cursor.transform.position = cursorPos;
		}
	}
	
	public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		//seria cool una barra de progreso aqui
	}
	
	public bool GestureCompleted (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		if (gesture == KinectGestures.Gestures.Click) {
			Debug.Log ("x: " + screenPos.x + " y: " + screenPos.y);
			if(posMenu==1){
				//if(screenPos.x>=0.35 && screenPos.x <=0.7 && screenPos.y >=0.35 && screenPos.y <=0.5){

				//}
				//else if(screenPos.x>=0.35 && screenPos.x <=0.7 && screenPos.y >=0 && screenPos.y <=0.3){
				//	mainMenu.SetActive( false);
				//	instructions.SetActive (true);
				//	posMenu = 3;
				//}
				mainMenu.SetActive( false);
				songs.SetActive(true);
				posMenu = 2;
			}
			else if(posMenu==2){
				StartCoroutine (destruirCamara());
			}
			else if(posMenu==3){
				instructions.SetActive (false);
				mainMenu.SetActive(true);
				posMenu=1;
			}

		}
		if (gesture == KinectGestures.Gestures.SwipeRight) {
			Debug.Log ("entra");
			if (posMenu == 2) {
				if (posCancion < canciones.Length) {
					posCancion++;
				} else {
					posCancion = 0;
				}
				cancionActual.GetComponent<SpriteRenderer> ().sprite = canciones [posCancion];
			}
			else if (posMenu == 3) {
				if (posInstruccion < instrucciones.Length) {
					posInstruccion++;
				} else {
					posInstruccion = 0;
				}
				instruccionActual.GetComponent<SpriteRenderer> ().sprite = instrucciones [posInstruccion];
			}

		}
		if (gesture == KinectGestures.Gestures.SwipeLeft) {
			Debug.Log ("entra");
			if (posMenu == 2) {
				if (posCancion > 0) {
					posCancion--;
				} else {
					posCancion = (canciones.Length-1);
				}
				cancionActual.GetComponent<SpriteRenderer> ().sprite = canciones [posCancion];
			}
			else if (posMenu == 3) {
				if (posInstruccion > 0) {
					posInstruccion--;
				} else {
					posInstruccion = (instrucciones.Length-1);
				}
				instruccionActual.GetComponent<SpriteRenderer> ().sprite = instrucciones [posInstruccion];
			}
		}
		
		progressDisplayed = false;
		
		return true;
	}
	
	public bool GestureCancelled (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint)
	{
		if(progressDisplayed)
		{	
			progressDisplayed = false;
		}
		
		return true;
	}

	public IEnumerator destruirCamara(){

		SceneManager.LoadScene (canciones[posCancion].name);
		yield return new WaitForSeconds(1.0f);
		Destroy (gameObject);
		
	}
	
}
