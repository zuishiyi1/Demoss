using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savageryController : Enemy {


	private float collideroffset = -0.0119209f;  //属于第一种敌人的特别属性
	private bool run;//属于第一种敌人的特别属性

	private Rigidbody2D r2d;

	// Use this for initialization
	void Start () {
		InitializeData ();

		r2d = this.GetComponent<Rigidbody2D> ();
		if (r2d != null) {
			this.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
		}
	}
	
	// Update is called once per frame
	void Update () {
		BackState ();

		switch (currentState) {
		case"run_left":
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x - Time.deltaTime * 2.0f, this.transform.localPosition.y, 0);
			break;
		case"run_right":
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x + Time.deltaTime * 2.0f, this.transform.localPosition.y, 0);
			break;
		case "die":

			break;
		case "stop":

			break;
		case "idle":

			break;

		}
	}


	#region 
	void OnTriggerEnter2D(Collider2D c2d)
	{
		ShootByBullet (c2d);
	}

	void OnCollisionEnter2D(Collision2D c2d)
	{
		//如果碰到的物体是主角的话
		if (c2d.gameObject.name == "Hero") {
			hitHero = true;
			this.GetComponent<Animator> ().SetBool ("attack",true);
			this.GetComponent<Animator> ().SetBool ("run",false);
			Hero = c2d.gameObject;
		}

	}

	void DisableCollider()
	{
		this.GetComponent<BoxCollider2D> ().enabled = false;
	}
	#endregion


	void Run()
	{
		this.animator.enabled = true;
		this.sr.enabled = true;
		this.GetComponent<BoxCollider2D> ().enabled = true;
		if(r2d!=null)
		{
		this.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
		}
		animator.SetBool ("run", true);
		animator.SetBool ("idle", false);
		if (!sr.flipX) {
			currentState = "run_left";
		}
		else {
			currentState = "run_right";
		}
	}
}
