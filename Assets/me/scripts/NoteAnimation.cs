using UnityEngine;
using System.Collections;

public class NoteAnimation : MonoBehaviour {

	public bool suena;

	// Update is called once per frame
	void FixedUpdate () {

		if(suena){
			if (gameObject.transform.position.y < 10) {
				float y = gameObject.transform.position.y + 0.04f;
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x,y, gameObject.transform.position.z); 
			} else {
				Destroy (gameObject,1f);
			}
		}
	}

	public void laToco(){
		Destroy (gameObject,0.1f);
	}

	public void pausa(bool pPausa){
		suena = pPausa;
	}
}
