using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class razer_effect : MonoBehaviour {

	public SpriteRenderer Hero;
	private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!sr.enabled) {
			if (!Hero.flipX) {
				this.sr.flipX = false;
			} else {
				this.sr.flipX = true;
			}
		}
	}

	void DestroyThis()
	{
		this.GetComponent<SpriteRenderer> ().enabled = false;
		this.GetComponent<Animator> ().enabled = false;
	}
}
