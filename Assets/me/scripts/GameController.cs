using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameController : MonoBehaviour {

	[Header("Canvas Juego Normal")]
	public Text info;
	public bool final;
	public Text ScoreText;

	[Header("Sonido y cancion")]

	public GameObject soundManager;
	public bool jugar;
	private SongModel cancion;
	private int pos;

	[Header("prefabs de notas y hechizos")]
	public GameObject prefab;
	public ParticleSystem luzHechizo;

	[Header("bombillos de lanes")]
	public GameObject rojo;
	public GameObject azul;
	public GameObject verde;
	public GameObject amarillo;

	[Header("Menus")]
	public GameObject JuegoNormal;
	public GameObject MenuPausa;
	public GameObject MenuScore;

	[Header("Menu score")]
	public Text finalScore;
	private int score;
	private ScoreModel scores;
	public Text name;

	[Header("poder")]
	public Text poderText;
	private int poder;
	private bool puedeUsarPoder;


	void Awake(){
		loadSong ();
		loadScores ();
		luzHechizo.Stop ();
		inicio ();
	}
		
	//Funcionamiento del juego

	public void inicio(){
		jugar = true;
		score = 0;
		pos = 0;
		final = false;

		poder = 0;
		puedeUsarPoder = false;

		StartCoroutine(cuentaRegresiva());
		soundManager.GetComponent<SoundManager> ().playBaquetas ();

	}

	public IEnumerator cuentaRegresiva(){
		info.text = "Ready?";
		yield return new WaitForSeconds(1.5f);
		info.text = "3";
		yield return new WaitForSeconds(0.5f);
		info.text = "2";
		yield return new WaitForSeconds(0.5f);
		info.text = "1";
		yield return new WaitForSeconds(0.7f);
		info.text = "";
		juego ();

	}

	public void juego(){
		soundManager.GetComponent<SoundManager> ().playCancion ();
		StartCoroutine(tocarNotas());

	}

	public IEnumerator tocarNotas(){
		while (pos < cancion.notas.Length && jugar) {
			Debug.Log ("nota: "+ cancion.notas[pos].tipo +"-" + pos);
			if(cancion.notas[pos].tipo ==1){
				Vector3 posNota = new Vector3 (-5.89f,-1.26f,-1.51f);
				Instantiate (prefab, posNota, Quaternion.identity);
			}
			if(cancion.notas[pos].tipo ==2){
				Vector3 posNota = new Vector3 (-4.32f,-1.26f,-1.51f);
				Instantiate (prefab, posNota, Quaternion.identity);
			}
			if(cancion.notas[pos].tipo ==3){
				Vector3 posNota = new Vector3 (-2.69f,-1.26f,-1.51f);
				Instantiate (prefab, posNota, Quaternion.identity);
			}
			if(cancion.notas[pos].tipo ==4){
				Vector3 posNota = new Vector3 (-1.09f,-1.26f,-1.51f);
				Instantiate (prefab, posNota, Quaternion.identity);
			}

			pos++;
			yield return new WaitForSeconds(1f);
		}
		if (pos == (cancion.notas.Length)) {
			yield return new WaitForSeconds(5f);
			terminarJuego ();
		}
	}

	//Manejar Menus

	public void terminarJuego(){
		soundManager.GetComponent<SoundManager> ().pararCancion ();

		final = true;
		string s = "Score: " + score;
		finalScore.text = s;


		JuegoNormal.SetActive (false);
		MenuScore.SetActive (true);
	}

	public void pausa(){
		jugar = false;

		NoteAnimation[] notas = FindObjectsOfType<NoteAnimation> ();
		for (int i = 0; i < notas.Length; i++) {
			notas [i].pausa (false);
		}

		soundManager.GetComponent<SoundManager> ().pausaCancion ();
		JuegoNormal.SetActive (false);
		MenuPausa.SetActive (true);
	}

	public void reanudar(){
		jugar = true;
		NoteAnimation[] notas = FindObjectsOfType<NoteAnimation> ();
		for (int i = 0; i < notas.Length; i++) {
			notas [i].pausa (true);
		}

		soundManager.GetComponent<SoundManager> ().playCancion ();
		MenuPausa.SetActive (false);
		JuegoNormal.SetActive (true);
		StartCoroutine(tocarNotas());
	}

	//Hechizos

	public void jugarNota(int tipo){
		StartCoroutine(prenderBombillo (tipo));
		NoteAnimation[] notas = FindObjectsOfType<NoteAnimation> ();
		for (int i = 0; i < notas.Length; i++) {
			Vector3 posNota = notas [i].transform.position;
			bool si = false;
			if (posNota.y > 6f && posNota.y < 8f){
				if (tipo == 1 && posNota.x == -5.89f) {
					si = true;
				} else if (tipo == 2 && posNota.x == -4.32f) {
					si = true;
				} else if (tipo == 3 && posNota.x == -2.69f) {
					si = true;
				} else if (tipo == 4 && posNota.x == -1.09f) {
					si = true;
				}
			}

			if (si) {
				poder++;
				notas [i].laToco ();
				score = score + 100;
				ScoreText.text = score + "";
			}
			if (poder == 0) {
				poderText.text = "";
			}
			if (poder == 1) {
				poderText.text = "X2";
			}
			if (poder == 2) {
				poderText.text = "X4";
			}
			if (poder == 3) {
				poderText.text = "X8";
			}
			if (poder ==4) {
				poderText.text = "Hechizo Cargado";
				puedeUsarPoder = true;
			}
		}

	}
		
		
	public void lumus(){
		if (puedeUsarPoder) {
			poder = 0;
			poderText.text = "";
			puedeUsarPoder = false;
			score = score + 400;
			ScoreText.text = score + "";
			StartCoroutine(prenderHechizo (1));

		}
	}

	public void wingardiumLeviosa(){
		if (puedeUsarPoder) {
			poder = 0;
			poderText.text = "";
			puedeUsarPoder = false;
			score = score + 400;
			ScoreText.text = score + "";
			StartCoroutine(prenderHechizo (3));
		}
	}

	public void aquamenti(){
		if (puedeUsarPoder) {
			poder = 0;
			poderText.text = "";
			puedeUsarPoder = false;
			score = score + 400;
			ScoreText.text = score + "";
			StartCoroutine(prenderHechizo (2));
		}
	}

	//notas y bombillos
	public IEnumerator prenderHechizo(int tipo){
		if (tipo == 1) {
			info.text = "LUMUS!";
			luzHechizo.Play ();
			yield return new WaitForSeconds(3f);
			info.text = "";
			yield return new WaitForSeconds(1f);
			luzHechizo.Stop ();

		}
		else if (tipo == 2) {
			info.text = "AQUAMENTI!";
			luzHechizo.Play ();
			yield return new WaitForSeconds(3f);
			info.text = "";
			yield return new WaitForSeconds(1f);
			luzHechizo.Stop ();
		}
		else if (tipo == 3) {
			info.text = "WINGARDIUM LEVIOSA!";
			luzHechizo.Play ();
			yield return new WaitForSeconds(3f);
			info.text = "";
			yield return new WaitForSeconds(1f);
			luzHechizo.Stop ();
		}
	}
		
	public IEnumerator prenderBombillo(int tipo){
		if (tipo == 1) {
			verde.SetActive (true);
			yield return new WaitForSeconds(0.5f);
			verde.SetActive (false);
		}
		else if (tipo == 2) {
			azul.SetActive (true);
			yield return new WaitForSeconds(0.5f);
			azul.SetActive (false);
		}
		else if (tipo == 3) {
			amarillo.SetActive (true);
			yield return new WaitForSeconds(0.5f);
			amarillo.SetActive (false);
		}
		else if (tipo == 4) {
			rojo.SetActive (true);
			yield return new WaitForSeconds(0.5f);
			rojo.SetActive (false);
		}
	}
		
	//Cargar cosas

	public void loadSong(){
		string filePath = "Songs/" + SceneManager.GetActiveScene().name;
		TextAsset targetFile = Resources.Load<TextAsset>(filePath);
		cancion = JsonUtility.FromJson<SongModel>(targetFile.text);
	}

	public void loadScores(){
		string filePath = "Scores/" + SceneManager.GetActiveScene().name;
		TextAsset targetFile = Resources.Load<TextAsset>(filePath);
		scores = JsonUtility.FromJson<ScoreModel>(targetFile.text);
	}

	// Guardar score

	public void save(){

		ScorePlayer m = new ScorePlayer ();
		m.name = name.text;
		m.cancion = cancion.name;
		m.score = score;

		scores.scores.Add (m);

		string guardar = JsonUtility.ToJson(scores);
		string filePath = "Assets/Resources/Scores/" + SceneManager.GetActiveScene().name +".json";

		File.WriteAllText (filePath,guardar);

		SceneManager.LoadScene("MainMenu");
	}
}
