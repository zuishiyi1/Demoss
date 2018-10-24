using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_effect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void PauseAnime()
	{
		this.GetComponent<SpriteRenderer> ().enabled = false;
		this.GetComponent<Animator> ().enabled = false;
	}
}
