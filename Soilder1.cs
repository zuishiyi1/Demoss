using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soilder1 : MonoBehaviour {

	private SpriteRenderer sr;
	private Animator animator;

	// Use this for initialization
	void Start () {
		sr = this.GetComponent<SpriteRenderer> ();
		animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D c2d)
	{
		if (c2d.gameObject.name.ToString ().IndexOf ("ehb") != -1) {
			animator.SetBool ("die", true);
			this.GetComponent<BoxCollider2D> ().enabled = false;
			Destroy (c2d.gameObject);
		}

	}

	void DestroyThis()
	{
		Destroy (this.gameObject);
	}

	void TrunSide()
	{
			this.sr.flipX = false;
	}
}
