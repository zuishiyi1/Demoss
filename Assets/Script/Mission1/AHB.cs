﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AHB : MonoBehaviour {

	//0为未发射，1为左，2为右
	[HideInInspector]public int shoot;

	private Animator animator;
	private string currentstate;

	private GameObject hero;
	[HideInInspector]public Vector3 heropos = Vector3.zero;


	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();

		hero = GameObject.Find ("Hero");
	}
	
	// Update is called once per frame
	void Update () {
		if (this.tag == "clone") {
			if (currentstate != "die") {
				if (shoot == 1) {
					this.transform.Translate (heropos * Time.deltaTime * 1.0f);
				}
				if (shoot == 2) {
					this.transform.Translate (heropos * Time.deltaTime * 1.0f);
				}
			}
			Vector3 pos = Camera.main.WorldToViewportPoint (this.transform.position);
			bool isVisible = (Camera.main.orthographic || pos.z > 0f) && (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f);
			if (!isVisible) {
				Destroy (this.gameObject);
			}
		}

		if ((shoot!=0) && (this.tag!="clone")) {

			heropos = hero.transform.position - this.transform.position; 
			heropos = heropos.normalized; 


			GameObject ahb = (GameObject)Instantiate (this.gameObject);
			ahb.transform.position = this.gameObject.transform.position;
			ahb.tag = "clone";
			ahb.AddComponent<Rigidbody2D> ();
			ahb.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionY;
			ahb.GetComponent<AHB> ().shoot = this.shoot;
			ahb.GetComponent<CircleCollider2D> ().isTrigger = false;
			ahb.GetComponent<SpriteRenderer> ().enabled = true;
			ahb.GetComponent<Animator> ().enabled = true;
			ahb.GetComponent<AHB> ().heropos = this.heropos;
			shoot = 0;
		}
	}

	void OnCollisionEnter2D(Collision2D c2d)
	{
		//如果碰到的物体是主角的话
		if (c2d.gameObject.name == "Hero") {
			c2d.gameObject.GetComponent<HeroController> ().SendMessage ("DieByWepon");
			currentstate = "die";
			this.GetComponent<CircleCollider2D> ().enabled = false;
			this.GetComponent<SpriteRenderer> ().enabled = false;
			this.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
			DestroyThis ();
		}

	}

	void DestroyThis()
	{
		Destroy (this.gameObject);
	}
}
