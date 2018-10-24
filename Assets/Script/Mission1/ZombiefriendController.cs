using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiefriendController : Enemy {

	private CapsuleCollider2D cc2d;
	private Rigidbody2D r2d;

	private int dieCounter;

	private int number;

	private float modifyDis;

	private GameObject grenade;


	// Use this for initialization
	void Start () {
		InitializeData ();

		cc2d = this.GetComponent<CapsuleCollider2D> ();
		r2d = this.GetComponent<Rigidbody2D> ();

		dieCounter = 0;


		if (this.gameObject.name.ToString ().IndexOf ("2") != -1) {
			this.number = 2;
			foreach (Transform child in this.transform) {
				if (child.gameObject.name == "grenade") {
					grenade = child.gameObject;
					break;
				}
			}
		}
		else {
			this.number = 1;
			modifyDis = 0.5f;
		}




	}
	
	// Update is called once per frame
	void Update () {
		BackState ();

		if (!seehero) {
			if (this.number == 1) {
				double dis = this.transform.position.x - Hero.transform.position.x;
				if ((dis < distanceofSeeHero + modifyDis) && (dis > 0)) {
					this.sr.enabled = true;
					this.sr.flipX = false;
					this.cc2d.enabled = true;
					this.animator.enabled = true;
					this.animator.SetBool ("run", true);
					seehero = true;
				}
			}
			else if (this.number == 2) {
				Vector3 pos = Camera.main.WorldToViewportPoint (this.transform.position);
				bool isVisible = (Camera.main.orthographic || pos.z > 0f) && (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f);
				if (isVisible) {
					this.sr.enabled = true;
					this.sr.flipX = false;
					this.cc2d.enabled = true;
					this.animator.enabled = true;
					this.animator.SetBool ("throw", true);
					seehero = true;
				}
			}
		}
		else if (seehero) {
			if (this.currentState != "runout") {
				if (this.transform.position.x - Hero.transform.position.x > 0) {
					this.sr.flipX = false;
				} else {
					this.sr.flipX = true;
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

		case "runout":
			if (this.sr.flipX) {
				this.transform.localPosition = new Vector3 (this.transform.localPosition.x + Time.deltaTime * 1.3f, this.transform.localPosition.y, 0);
			}
			else {
				this.transform.localPosition = new Vector3 (this.transform.localPosition.x - Time.deltaTime * 1.3f, this.transform.localPosition.y, 0);
			}
			break;

		}
	}

	#region 
	void OnTriggerEnter2D(Collider2D c2d)
	{
		if (c2d.name.ToString ().IndexOf ("bullet") != -1) {
			dieCounter++;
			if (dieCounter >= 4) {
				ShootByBullet (c2d);
			}
		}

	}

	void OnCollisionEnter2D(Collision2D c2d)
	{
		//如果碰到的物体是主角的话
		if ((c2d.gameObject.name == "Hero")) {
			hitHero = true;
			this.GetComponent<Animator> ().SetBool ("attack",true);
			this.GetComponent<Animator> ().SetBool ("run",false);
			this.currentState = "attack";
			Hero = c2d.gameObject;
		}

	}

	void DisableCollider()
	{
		this.GetComponent<BoxCollider2D> ().enabled = false;
	}
	#endregion


	void SetRunLeft()
	{
		this.currentState = "run_left";
	}

	void FreezeRotation()
	{
		r2d.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	void RunOut()
	{
		this.gameObject.layer = 11;
		this.currentState = "runout";
		this.animator.SetBool ("runout", true);
		this.animator.SetBool ("die", false);
	}

	void ThrowGrenade()
	{
		if (this.number == 1) {
			return;
		}
		//grenade.GetComponent<SpriteRenderer> ().enabled = true;
		if (!this.sr.flipX) {
			grenade.GetComponent<ZombiefriendsGrenade> ().shoot = 1;
		}
		else {
			grenade.GetComponent<ZombiefriendsGrenade> ().shoot = 2;
		}

	}

	void PlayeShowVoice()
	{
		audios.Play ();
	}


		
}
