using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour{

	public AudioSource baquetas;
	public AudioSource cancion;
	public AudioSource buzzer;


	public void playBaquetas(){

		baquetas.Play ();
	}

	public void playCancion(){
		
		cancion.Play ();
	}

	public void pausaCancion(){

		cancion.Pause();
	}

	public void pararCancion(){
		cancion.Stop ();
	}

	public void playBuzzer(){
		
		buzzer.Play ();
	}
}
