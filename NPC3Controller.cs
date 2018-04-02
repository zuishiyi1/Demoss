using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//8.789


public class NPC3Controller : MonoBehaviour {

	private GameObject frined1;

	// Use this for initialization
	void Start () {
		frined1 = GameObject.FindGameObjectWithTag ("f1");
	}
	
	// Update is called once per frame
	void Update () {
		if ((frined1.transform.position.x <= 8.2f) && (this.name == "NPC3(0)")) {
			
			this.GetComponent<SpriteRenderer> ().flipX = false;
			this.GetComponent<Animator> ().SetBool ("start_run1", true);

			this.transform.Translate (new Vector3 (Time.deltaTime * 1.0f, 0, 0));




		} else if ((frined1.transform.position.x <= 7.2f) && (this.name == "NPC3 (2)")) {
			
			this.GetComponent<SpriteRenderer> ().flipX = true;
			this.GetComponent<Animator> ().SetBool ("start_run2", true);

			this.transform.Translate (new Vector3 (Time.deltaTime * 1.5f, 0, 0));



		} 
		else if((frined1.transform.position.x <= 8.2f) && (this.name == "NPC3 (1)")) {
			
			this.GetComponent<SpriteRenderer> ().flipX = false;
			this.GetComponent<Animator> ().SetBool ("start_run1", true);

			this.transform.Translate (new Vector3 (Time.deltaTime * 1.0f, 0, 0));
		}
	}
}
