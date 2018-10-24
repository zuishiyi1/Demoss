using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//x=19,y=18.5

public class FishheadController : Enemy {

	private Rigidbody2D r2d;
	private CircleCollider2D cc2d;

	private int dieCounter;

	private float jumphigh=400.0f;

	private bool ground;

	private GameObject fishb;

	[HideInInspector]public bool jump;

	private int number;
	private  bool showup;

	// Use this for initialization
	void Start () {
		InitializeData ();

		jump = false;
		if (this.name.ToString ().IndexOf ("4") != -1) {
			this.number = 4;
		}

		this.r2d = this.GetComponent<Rigidbody2D> ();
		this.cc2d = this.GetComponent<CircleCollider2D> ();

		ground = false;
		showup = false;

		foreach (Transform child in this.transform) {
			if (child.gameObject.name == "fishb") {
				fishb = child.gameObject;
				break;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		BackState ();

		if (this.number != 4) {
			if (!seehero) {
			
				if (this.name.ToString ().IndexOf ("3") != -1) {
					if (jump) {
						this.sr.enabled = true;
						this.sr.flipX = false;
						//this.cc2d.enabled = true;
						this.animator.enabled = true;
						seehero = true;
						this.r2d.AddForce (new Vector2 (0, jumphigh));
					}
				} else {
					double dis = this.transform.position.x - Hero.transform.position.x;
					if ((dis < distanceofSeeHero + 1.5f) && (dis > 0)) {
						this.sr.enabled = true;
						this.sr.flipX = false;
						//this.cc2d.enabled = true;
						this.animator.enabled = true;
						seehero = true;
						this.r2d.AddForce (new Vector2 (0, jumphigh));
					}
				}
			} else if (seehero) {
				if (this.transform.position.x - Hero.transform.position.x > 0) {
					this.sr.flipX = false;
				} else {
					this.sr.flipX = true;
				}

			}
		}
		else {
			if (!showup) {
				if (this.isActiveAndEnabled) {
					this.sr.enabled = true;
					this.sr.flipX = false;
					this.animator.enabled = true;
					this.r2d.AddForce (new Vector2 (0, jumphigh-60.0f));
					this.cc2d.enabled = false;
					showup = true;
				}
			}
		}


		switch (currentState) {
		case"run_left":
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x - Time.deltaTime * 1.0f, this.transform.localPosition.y, 0);
			break;
		case"run_right":
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x + Time.deltaTime * 1.0f, this.transform.localPosition.y, 0);
			break;
		case "die":
			r2d.constraints = RigidbodyConstraints2D.FreezeAll;
			break;
		case "stop":

			break;
		case "idle":

			break;

		case "attack":

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

	void SetGroundTrue()
	{
		this.ground = true;
		this.animator.SetBool ("ground", true);

		//如果这是第二只的话，则在他落地之后让第二只跳出来
		if(this.name.ToString ().IndexOf ("2") != -1)
		{
			GameObject.Find ("fishhead3").GetComponent<FishheadController> ().jump = true;
		}

	}

	void AttackWithBullet()
	{
		if (fishb) {
			if (this.sr.flipX) {
				fishb.GetComponent<Fishb> ().shoot = 2;
			}
			else {
				fishb.GetComponent<Fishb> ().shoot = 1;
			}
		}
	}


	void BackToAttack()
	{
		this.currentState="attack";
		animator.SetBool ("attack", true);
		animator.SetBool ("idle", false);
	}

	void BackToIdle()
	{
		this.currentState="idle";
		animator.SetBool ("attack", false);
		animator.SetBool ("idle", true);
	}
}
