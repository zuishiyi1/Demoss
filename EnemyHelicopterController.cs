using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHelicopterController : Enemy {


	private GameObject ehe;
	private Animator ehe_animator;
	private ehe ehe_scrpit;

	private GameObject ehe2;//中间射击的
	private ehe2s ehe2_scrpit;

	private string direction;
	private string move_dir;
	private bool showup;

	private float sideattack_dis = 3.0f;

	private Vector3 hero_pos;

	[HideInInspector]public bool endshot;//当结束射击时，应该进行移动

	private bool firsttime_middleshot;

	//0是上一个状态，1是下一个状态
	private string[] stateAry = new string[2];

	private int dieCounter;

	public GameObject soilder2;



	// Use this for initialization
	void Start () {
		InitializeData ();

		direction = "left";
		move_dir = "";
		showup = false;
		stateAry[0] = "";
		endshot = false;
		firsttime_middleshot = true;
		dieCounter = 0;

		foreach (Transform child in this.transform) {
			if (child.gameObject.name == "ehe") {
				ehe = child.gameObject;
				ehe_animator = ehe.GetComponent<Animator> ();
				ehe_scrpit = ehe.GetComponent<ehe> ();
			}
			if (child.gameObject.name == "ehe2") {
				ehe2 = child.gameObject;
				ehe2_scrpit = ehe2.GetComponent<ehe2s> ();
			}
		}
			
			
	}
	
	// Update is called once per frame
	void Update () {
		BackState ();

		//被主角击毁之后
		if (this.currentState=="die") {
			this.animator.enabled = false;
			this.GetComponent<BoxCollider2D> ().enabled = false;
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x - Time.deltaTime * 1.0f, this.transform.localPosition.y - Time.deltaTime * 1.5f, 0);
			Vector3 pos = Camera.main.WorldToViewportPoint (this.transform.position);
			bool isVisible = (Camera.main.orthographic || pos.z > 0f) && (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f);
				if (!isVisible) {
				soilder2.GetComponent<SpriteRenderer> ().enabled = true;
				soilder2.GetComponent<Animator> ().enabled = true;
					Destroy (this.gameObject);
				}
			return;
		}

		if (!seehero) {
			double dis = this.transform.position.x - Hero.transform.position.x;
			if ((dis < distanceofSeeHero+2.0f) && (dis > 0)) {
				this.sr.flipX = false;
				this.currentState = "showup";
				seehero = true;
				this.audios.Play ();
			}
		}
		else if (seehero) {
			if (showup) {
				if ((ehe_scrpit.shoottimes == 3) || (ehe2_scrpit.shoottimes == 3)) {
					//出场之后，就要时刻判断跟主角的位置，如果小于0.5f，则向下射击，大于0.5f，就侧面射击，需要完成射击才能转动
					double dis = this.transform.position.x - Hero.transform.position.x;
					//在左边
					if (dis > sideattack_dis) {
						move_dir = "";
						direction = "left";
						stateAry [0] = this.currentState;
						this.currentState = "sideattack";
						stateAry [1] = "sideattack";
						hero_pos = new Vector3 (Hero.transform.position.x, this.transform.position.y, 0);
						ehe2_scrpit.shoottimes = 0;
						return;

					}
			//中间
				else if ((dis < sideattack_dis) && (dis > -sideattack_dis)) {
						if (dis > 0) {
							move_dir = "left";
							//this.sr.flipX = false;
						}
						if (dis < 0) {
							move_dir = "right";
							//this.sr.flipX = true;
						}
						direction = "middle";
						stateAry [0] = this.currentState;
						this.currentState = "middleattack";
						stateAry [1] = "middleattack";
						Vector3 new_heropos = hero_pos;
						hero_pos = new Vector3 (Hero.transform.position.x, this.transform.position.y, 0);
						if (new_heropos != hero_pos) {
							firsttime_middleshot = true;
						}
						ehe2_scrpit.shoottimes = 0;
						return;
					}
			//右边
				else if (dis < -sideattack_dis) {
						move_dir = "";
						direction = "right";
						stateAry [0] = this.currentState;
						this.currentState = "sideattack";
						stateAry [1] = "sideattack";
						hero_pos = new Vector3 (Hero.transform.position.x, this.transform.position.y, 0);
						ehe2_scrpit.shoottimes = 0;
						return;
					}
				}
			}
			
		}



		switch (currentState) {
		case "showup":
			if (this.transform.localPosition.y <= 1.11f && (seehero)) {
				this.currentState = "sideattack";
				animator.SetBool ("sideattack", true);
				animator.SetBool ("moving", false);
				animator.SetBool ("middleattack", false);
				stateAry[1] = "sideattack";
				showup = true;
				return;
			}
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y - Time.deltaTime * 1.0f, 0);
			break;

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
		case "sideattack":
			if (stateAry [0] != "sideattack") {
				this.animator.enabled = true;
			}
			animator.SetBool ("sideattack", true);
			animator.SetBool ("moving", false);
			animator.SetBool ("middleattack", false);
			stateAry [0] = stateAry [1];
			stateAry[1] = "sideattack";
			break;
		case "middleattack":
			if (Mathf.Abs (this.transform.position.x - hero_pos.x) > 0.2f) {

				if (move_dir == "left") {
					this.sr.flipX = false;
					this.transform.localPosition = new Vector3 (this.transform.localPosition.x - Time.deltaTime * 1.5f, this.transform.localPosition.y, 0);
				}
				if (move_dir == "right") {
					this.sr.flipX = true;
					this.transform.localPosition = new Vector3 (this.transform.localPosition.x + Time.deltaTime * 1.5f, this.transform.localPosition.y, 0);
				}
				this.animator.enabled = true;
			} 
			else {
				if (stateAry [0] != "middleattack") {
					this.animator.enabled = true;
				}
				//如果上一个是中间射击，这个也是，就要自己触发函数，因为动画停止了
				else if((stateAry [0] == "middleattack") && (this.animator.enabled == false)){
					firsttime_middleshot = false;
					MiddleAttackStop ();
				}

				animator.SetBool ("sideattack", false);
				animator.SetBool ("moving", false);
				animator.SetBool ("middleattack", true);
				stateAry [0] = stateAry [1];
				stateAry [1] = "middleattack";
			}
			break;

		}
	}
		

	void OnTriggerEnter2D(Collider2D c2d)
	{
		if (c2d.name.ToString ().IndexOf ("bullet") != -1) {
			dieCounter++;
			if (dieCounter >= 10) {
				ShootByBullet (c2d);
			}
		}

	}


	void SideAttack()
	{
			animator.enabled = false;

		//特效和子弹射出
		if (ehe) {
			ehe_scrpit.direction = this.direction;
			ehe_scrpit.Shoot (null);
		}
	}

	//停留在中部射击的那一帧
	void MiddleAttackStop()
	{
		//判断是否到达heropos，不是则不放
		//if()
		if (!firsttime_middleshot) {
			GameObject[] ehbList = GameObject.FindGameObjectsWithTag ("clone");
			//当有超过3次子弹的发射时，应该暂停发射
			if (ehbList.Length >= 3) {
				ehe2.GetComponent<ehe2s> ().shoottimes = 3;
				return;
			}
		}
		ehe2.GetComponent<ehe2s> ().ShootBullet (this.gameObject);
		//this.animator.enabled = false;
	}

	void Moving()
	{
		this.animator.enabled = false;
	}
		
}
