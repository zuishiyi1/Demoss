using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperController : Enemy {

	public int turnDirectionTimes = 7;
	public bool needTurnDirection;


	private int LeftcycleCount = 0;
	private int rightcycleCount = 0;

	private GameObject bullet;
	private SpriteRenderer bullet_sr;

	private bool idleDirection = true; //左是true，右是false



	// Use this for initialization
	void Start () {
		InitializeData ();

		foreach (Transform child in this.transform) {
			if (child.gameObject.name == "sniperblet") {
				bullet = child.gameObject;
				bullet_sr = bullet.GetComponent<SpriteRenderer> ();
				break;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		BackState ();

		if (!seehero) {
			//将狙击手和主角的位置相减，如果是正数，主角在左边，相反则在右边
			double dis = this.transform.position.x - Hero.transform.position.x;
			if ((dis < distanceofSeeHero) && (dis > 0)) {
				this.sr.flipX = false;
				this.animator.SetBool ("seehero", true);
				seehero = true;
			}
		}
		else if (seehero) {
			if (this.transform.position.x - Hero.transform.position.x > 0) {
				this.sr.flipX = false;
			}
			else {
				this.sr.flipX = true;
			}
		}

		switch (currentState) {
			case "":
				break;	
		}


		if (!needTurnDirection) {
			return;
		} 
		else {
			if (LeftcycleCount >= turnDirectionTimes) {
				LeftcycleCount = 0;

				if (animator) {
					animator.SetBool ("turnright", true);
				}

				idleDirection = false;
			}
			if (rightcycleCount >= turnDirectionTimes) {
				rightcycleCount = 0;

				if (animator) {
					animator.SetBool ("turnright", true);
				}

				idleDirection = true;
			}
		}

		if (sr.flipX) {
			bullet.transform.localPosition = new Vector3 (0.5f, 0.1f, 0);
		}
		else {
			bullet.transform.localPosition = new Vector3 (-0.5f, 0.1f, 0);
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
			//this.GetComponent<Animator> ().SetBool ("attack",true);
			//this.GetComponent<Animator> ().SetBool ("run",false);
			Hero = c2d.gameObject;
		}

	}

	void DisableCollider()
	{
		this.GetComponent<BoxCollider2D> ().enabled = false;
	}
	#endregion

	void CountPlus()
	{
		//如果当前方向朝左的话
		if (idleDirection) {
			LeftcycleCount++;
		}
		else {
			rightcycleCount++;
		}
	}

	//过渡动画结束之后就进行
	void TurnRight()
	{
		animator.SetBool ("turnright", false);
		if (!sr.flipX) {
			sr.flipX = true;
		}
		else {
			sr.flipX = false;
		}
	}

	void SetSeeheroFalse()
	{
		animator.SetBool ("seehero", false);
	}

	void ShootBullet()
	{
		if (bullet) {
			//bullet_sr.enabled = true;
			bullet.GetComponent<SniperBullet>().shoot=true;
		}
	}

	void PlayShockVoice()
	{
		audios.clip = Resources.Load ("sniper_shock") as AudioClip;
		audios.Play ();
	}

	void PlayDieVoice()
	{
		audios.clip = Resources.Load ("enemydie2") as AudioClip;
		audios.Play ();
	}

}
