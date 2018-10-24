using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightPicture : MonoBehaviour {

	public GameObject bird1;
	public GameObject bird2;
	public GameObject bird3;

	private bool fly;
	private Animator an;

	// Use this for initialization
	void Start () {
		fly = false;
		an = this.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (fly) {
			bird1.transform.position = new Vector3 (bird1.transform.position.x-Time.deltaTime*1.0f, bird1.transform.position.y + Time.deltaTime * 0.5f, 0);
			bird2.transform.position = new Vector3 (bird2.transform.position.x-Time.deltaTime*1.0f, bird2.transform.position.y + Time.deltaTime * 0.5f, 0);
			bird3.transform.position = new Vector3 (bird3.transform.position.x-Time.deltaTime*1.0f, bird3.transform.position.y + Time.deltaTime * 0.5f, 0);
		}
	}

	void DisableSr()
	{
		this.GetComponent<SpriteRenderer> ().enabled = false;
		this.GetComponent<BoxCollider2D> ().enabled = false;
		fly = true;
		bird1.GetComponent<SpriteRenderer> ().enabled = true;
		bird2.GetComponent<SpriteRenderer> ().enabled = true;
		bird3.GetComponent<SpriteRenderer> ().enabled = true;
	}

	void OnTriggerEnter2D (Collider2D c2d)
	{
		PlayAnimator ();
	}

	void StopAnimator()
	{
		an.enabled = false;
	}

	void PlayAnimator()
	{
		an.enabled = true;
	}
}
