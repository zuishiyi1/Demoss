using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitiveController : Enemy {

	private bool ground = false;
	private CapsuleCollider2D cc2d;

	private string type;

	private GameObject spear;
	private SpriteRenderer spear_sr;

	private bool throwspearend;

	private bool scene5;

	// Use this for initialization
	void Start () {
		InitializeData ();
		distanceofSeeHero = 3.0f;
		cc2d = this.GetComponent<CapsuleCollider2D> ();

		if (this.gameObject.name.ToString().IndexOf ("spear") != -1) {
			this.type = "spear";
		}
		else {
			this.type = "axe";
		}

		foreach (Transform child in this.transform) {
			if (child.gameObject.name == "spear") {
				spear = child.gameObject;
				spear_sr = spear.GetComponent<SpriteRenderer> ();
				break;
			}
		}

		if (this.transform.position.x > -110) {
			scene5 = true;
		}
		else {
			scene5 = false;
		}

		throwspearend = true;


	}

	// Update is called once per frame
	void Update () {
		BackState ();

		if (!scene5) {
			if (!seehero) {
				//将敌人和主角的位置相减，如果是正数，主角在左边，相反则在右边
				double dis = this.transform.position.x - Hero.transform.position.x;
				if ((dis < distanceofSeeHero) && (dis > 0)) {
					this.sr.flipX = false;
					this.animator.SetBool ("jumpdown", true);
					seehero = true;
				}
			}
		}
			

		switch (this.currentState) {
		case"run_left":
			this.sr.flipX = false;
			if (this.type == "spear") {
				double dis0 = this.transform.position.x - Hero.transform.position.x;
				if (dis0 < 1.2f) {
					this.currentState = "attackwithspear";
					animator.SetBool ("run", false);
					animator.SetBool ("attackwithspear", true);
				}

				if (this.throwspearend)
					this.transform.localPosition = new Vector3 (this.transform.localPosition.x - Time.deltaTime * 2.0f, this.transform.localPosition.y, 0);
			}
			else if (this.type == "axe") {
				this.transform.localPosition = new Vector3 (this.transform.localPosition.x - Time.deltaTime * 2.0f, this.transform.localPosition.y, 0);
			}
			break;
		case"run_right":
			this.sr.flipX = true;
			if (this.type == "spear") {
				double dis1 = this.transform.position.x - Hero.transform.position.x;
				if (dis1 > -1.2f) {
					this.currentState = "attackwithspear";
					animator.SetBool ("run", false);
					animator.SetBool ("attackwithspear", true);
				}

				if (this.throwspearend)
					this.transform.localPosition = new Vector3 (this.transform.localPosition.x + Time.deltaTime * 2.0f, this.transform.localPosition.y, 0);
			}
			else if (this.type == "axe") {
				this.transform.localPosition = new Vector3 (this.transform.localPosition.x + Time.deltaTime * 2.0f, this.transform.localPosition.y, 0);
			}
			break;
		case "die":

			break;
		case "stop":

			break;
		case "idle":

			break;

		case "attackwithspear":
			animator.SetBool ("attackwithspear", true);
			double dis2 = this.transform.position.x - Hero.transform.position.x;
			if (!this.sr.flipX) {
				if (dis2 > 1.2f) {
					this.currentState = "run_left";
					this.throwspearend = true;
					animator.SetBool ("run", true);
					animator.SetBool ("attackwithspear", false);
				}
			}
			else if (this.sr.flipX) {
				if (dis2 < -1.2f) {
					this.currentState = "run_right";
					this.throwspearend = true;
					animator.SetBool ("run", true);
					animator.SetBool ("attackwithspear", false);
				}
			}

			break;
		}

		if (!ground) {
			if (this.transform.position.y <= 0) {
				cc2d.enabled = true;
				//提前进入动画，显得更加流畅
				animator.SetBool ("ground", true);
				//this.ground = true;
			}
		}
			
//		if (this.currentState=="attackwithspear") {
//			double dis = this.transform.position.x - Hero.transform.position.x;
//			if (dis > distanceofSeeHero) {
//				if (dis > 0) {
//					this.currentState = "run_left";
//				} else if (dis < 0) {
//					this.currentState = "run_right";
//				}
//			}
//		}


//		if (this.type == "spear") {
//			if (this.transform.position.x - Hero.transform.position.x < 1.2f) {
//				this.currentState = "attackwithspear";
//			}
//		}
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
			animator.SetBool ("attack",true);
			animator.SetBool ("run",false);
			Hero = c2d.gameObject;
		}

	}

	void OnCollisionExit2D(Collision2D c2d)
	{
		
		if (c2d.gameObject.name == "scene3layer") {
		}
		//如果碰到的物体是主角的话
		if (c2d.gameObject.name == "Hero") {
			if (this.type == "axe") {
				//this.GetComponent<Animator> ().SetBool ("attack",true);
				//this.GetComponent<Animator> ().SetBool ("run",false);
				Hero = c2d.gameObject;	
			}
		}

	}

	void DisableCollider()
	{
		this.GetComponent<BoxCollider2D> ().enabled = false;
	}
	#endregion


	void CheckIsGround()
	{
//		if (ground) {
//			animator.SetBool("ground")
//		}
	}

	void test()
	{
		cc2d.enabled = false;
	}

	void SetGroundTrue()
	{
		this.ground = true;
		animator.SetBool ("run", true);

	}

	void SetRun()
	{
		if (this.sr.flipX) {
			this.currentState = "run_right";
		}
		else {
			this.currentState = "run_left";
		}

		//AnimationStart ();
	}

	void CheckHeroDie()
	{
		//this.Hero
	}

	void ShowSpear()
	{
		//同理与狙击手的子弹
		if (!this.sr.flipX) {
			spear.GetComponent<PrimitiveSpear> ().direction = 0;
		}
		else if (this.sr.flipX) {
			spear.GetComponent<PrimitiveSpear> ().direction = 1;
		}
		spear.GetComponent<PrimitiveSpear>().shoot=true;
		spear.GetComponent<CircleCollider2D> ().enabled = true;

	}
		
	void ThrowSpearEnd()
	{
		this.throwspearend = true;
	}

	void firstshow()
	{
		this.animator.enabled = true;
		this.sr.enabled = true;
		this.cc2d.enabled = true;
		animator.SetBool ("run", true);
		if (this.sr.flipX) {
			this.currentState = "run_right";
			this.throwspearend = true;
		}
		else {
			this.currentState = "run_left";
			this.throwspearend = true;
		}
	}

}
