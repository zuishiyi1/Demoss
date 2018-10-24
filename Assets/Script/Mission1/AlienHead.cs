using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienHead : Enemy {

	private Rigidbody2D r2d;
	private CircleCollider2D cc2d;

	private int dieCounter;

	private GameObject ahb;

	private float leftDis;
	private float rightDis;

	private float firstleft;//第一次出现时要比平常移动的范围大，left指从左边出现
	private float firstright;


	// Use this for initialization
	void Start () {
		InitializeData ();

		this.r2d = this.GetComponent<Rigidbody2D> ();
		this.cc2d = this.GetComponent<CircleCollider2D> ();

		foreach (Transform child in this.transform) {
			if (child.gameObject.name == "alienheadb") {
				ahb = child.gameObject;
				break;
			}
		}
			
		firstleft = this.transform.localPosition.x - 7.0f;
		firstright = this.transform.localPosition.x + 7.0f;

	}
	
	// Update is called once per frame
	void Update () {
		BackState ();

		switch (currentState) {
		case "first_left":
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x - Time.deltaTime * 1.0f, this.transform.localPosition.y, 0);
			if (this.transform.localPosition.x - firstleft <= 0.01f) {
				leftDis = 0;
				rightDis = 0;
				this.currentState="l2r";
				this.animator.SetBool ("moving", false);
			}
			break;
		case "first_right":
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x + Time.deltaTime * 1.0f, this.transform.localPosition.y, 0);
			if (this.transform.localPosition.x + firstright >= 27.82f) {
				leftDis = 0;
				rightDis = 0;
				this.currentState="r2l";
				this.animator.SetBool ("moving", false);
			}
			break;
		case"run_left":
			if (leftDis == 0) {
				leftDis = this.transform.localPosition.x - Random.Range (1, 3);
			}
			else {
				this.transform.localPosition = new Vector3 (this.transform.localPosition.x - Time.deltaTime * 1.0f, this.transform.localPosition.y, 0);
				if (this.transform.localPosition.x - leftDis <= 0.01f) {
					leftDis = 0;
					rightDis = 0;
					this.currentState="l2r";
					this.animator.SetBool ("moving", false);
				}
			}
			break;
		case"run_right":
			if (rightDis == 0) {
				rightDis = this.transform.localPosition.x + Random.Range (1, 3);
			} else {
				this.transform.localPosition = new Vector3 (this.transform.localPosition.x + Time.deltaTime * 1.0f, this.transform.localPosition.y, 0);
				if (this.transform.localPosition.x - rightDis >= 0.01f) {
					leftDis = 0;
					rightDis = 0;
					this.currentState="r2l";
					this.animator.SetBool ("moving", false);
				}
			}
			break;
		case "die":
			r2d.constraints = RigidbodyConstraints2D.FreezeAll;
			break;
		case "stop":

			break;
		case "idleR":

			break;

		case "idleL":

			break;

		case "attack":

			break;
		case "l2r":
			ahb.transform.localPosition = new Vector3 (0.102f, ahb.transform.localPosition.y, 0);
			this.animator.SetBool ("l2r", true);
			this.currentState = "idleR";
			break;
		case "r2l":
			ahb.transform.localPosition = new Vector3 (-0.102f, ahb.transform.localPosition.y, 0);
			this.animator.SetBool ("r2l", true);
			this.currentState = "idleL";
			break;

		}
	}

	#region 
	void OnTriggerEnter2D(Collider2D c2d)
	{
		if (c2d.name.ToString ().IndexOf ("bullet") != -1) {
			dieCounter++;
			if (dieCounter >= 2) {
				ShootByBullet (c2d);
			}
		}

	}

	void OnCollisionEnter2D(Collision2D c2d)
	{
		//如果碰到的物体是主角的话
		if (c2d.gameObject.name == "Hero") {
			hitHero = true;
			this.GetComponent<Animator> ().SetBool ("attack",true);
			this.GetComponent<Animator> ().SetBool ("run",false);
			this.currentState = "attack";
			Hero = c2d.gameObject;
		}



	}

	void DisableCollider()
	{
		cc2d.enabled = false;
	}

	void EnableColider()
	{
		//if(this.number!=4)
		cc2d.enabled = true;
	}
	#endregion


	void AttackWithBullet()
	{
		if (ahb) {
			if (this.sr.flipX) {
				ahb.GetComponent<AHB> ().shoot = 2;
				//this.currentState="run_right";
				this.animator.SetBool ("moving", true);
			}
			else {
				ahb.GetComponent<AHB> ().shoot = 1;
				//this.currentState="run_left";
				this.animator.SetBool ("moving", true);
			}
		}
	}

	public void firstShow()
	{
		this.animator.enabled = true;
		this.sr.enabled = true;
		this.cc2d.enabled = true;
		if (this.sr.flipX) {
			this.currentState = "first_right";
		}
		else {
			this.currentState = "first_left";
		}
	}

	void TurnDirection()
	{
		if (this.currentState == "idleL") {
			this.sr.flipX = false;
			this.animator.SetBool ("r2l", false);
		}
		else if (this.currentState == "idleR") {
			this.sr.flipX = true;
			this.animator.SetBool ("l2r", false);
		}
	}

	void Moving()
	{
			if (this.sr.flipX) {
			if(this.currentState!="first_right")
				this.currentState = "run_right";
			} else if (!this.sr.flipX) {
			if(this.currentState!="first_left")
				this.currentState = "run_left";
			}
	}
}
