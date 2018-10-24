using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{

	public GameObject shangbanshen;
	public GameObject xiabanshen;
	public GameObject bullet;

	public GameObject Hero;
	public Slider HP;

	public GameObject platform1;
	public GameObject sky;
	public GameObject background;

	public Transform groundCheckTransform;
	public  LayerMask groundCheckLayerMask;
	public LayerMask platformCheckLayerMask;

	private int blood;
	private bool die;

	private bool herodead;

	private Rigidbody2D r2d;

	private Animator shangbanshen_an;
	private Animator xiabanshen_an;
	private SpriteRenderer shangbanshen_sr;
	private SpriteRenderer xiabanshen_sr;

	private HeroController herocontroller;

	private int currentstate;
	private bool secondState = false;//第二阶段

	private bool grounded;
	private bool stock;
	private bool state2;

	private bool firstshow = false;
	[HideInInspector] bool eventend = false;

	private bool jump = false;
	private bool flipx = true;

	private bool hitted;
	private int hittedTimes;

	[HideInInspector]public int direction;//0-left,1-right

	enum groundActions : int {slideShoot=2,throwGrenade,sleep,standJump,moveJump,upShoot,shoot,knife,downshoot,drink,callairsuport};
	enum platformActions : int {sleep=13,downShoot,jumpdownShoot,throwGrenade,moveJump,shoot,knife,drink,callairsuport};
	enum normalActions : int {run_left=0,run_right,winpose=99};


	public bool test;

	// Use this for initialization
	void Start ()
	{
		herocontroller = Hero.GetComponent<HeroController> ();
		r2d = this.GetComponent<Rigidbody2D> ();

		shangbanshen_an = shangbanshen.GetComponent<Animator> ();
		xiabanshen_an = xiabanshen.GetComponent<Animator> ();
		shangbanshen_sr = shangbanshen.GetComponent<SpriteRenderer> ();
		xiabanshen_sr = xiabanshen.GetComponent<SpriteRenderer> ();

		currentstate = (int)normalActions.winpose;
		direction = 0;
		blood = 100;


		test = false;
		die = false;
		jump = false;
		state2 = false;
		hitted = false;
		hittedTimes = 0;
		herodead = false;


	}
	
	// Update is called once per frame
	void Update ()
	{
		BackState ();
		grounded = Physics2D.OverlapCircle (groundCheckTransform.position, 0.5f, groundCheckLayerMask);
		if (!grounded) {
			grounded = Physics2D.OverlapCircle (groundCheckTransform.position, 0.5f, platformCheckLayerMask);
		}
		FaceHero();

		if (test) {
			StandJump ();
			test = false;
		}

		switch (this.currentstate) {
		case (int)normalActions.run_left:
			if (this.die) {
				this.currentstate = -2;
				return;
			}
			this.transform.position -= new Vector3 (1.5f * Time.deltaTime, 0, 0);
			break;
		case (int)normalActions.run_right:
			if (this.die) {
				this.currentstate = -2;
				return;
			}
			this.transform.position += new Vector3 (1.5f * Time.deltaTime, 0, 0);
			break;
		case (int)groundActions.slideShoot:
			if (!state2) {
				xiabanshen_an.SetBool ("s_slideshoot", true);
			}
			else {
				xiabanshen_an.SetBool ("b_slideshoot", true);
			}
			this.currentstate = -2;
			break;
		case (int)groundActions.throwGrenade:
			shangbanshen_an.SetBool ("s_throwgrenade", true);
			this.currentstate = -2;
			break;
		case (int)groundActions.drink:
			if (!state2) {
				shangbanshen_an.SetBool ("drink", true);
				xiabanshen_an.SetBool ("moving", false);
				xiabanshen_an.SetBool ("idle", true);
			}
			else {
				eventend = true;
			}
			this.currentstate = -2;
			break;
		case (int)groundActions.sleep:
			shangbanshen_an.SetBool ("sleep", true);
			xiabanshen_an.SetBool ("moving", false);
			xiabanshen_an.SetBool ("idle", true);
			this.currentstate = -2;
			break;
		case (int)groundActions.standJump:
			this.gameObject.layer = 21;
			this.jump = true;
			shangbanshen_an.SetBool ("standjump", true);
			xiabanshen_an.SetBool ("standjump", true);
			this.r2d.AddForce (new Vector2 (0, 220f));
			this.currentstate = -2;
			break;
		case (int)groundActions.moveJump:
			if (this.stock) {
				this.stock = false;
			}
			this.jump = true;
			this.gameObject.layer = 21;
			shangbanshen_an.SetBool ("jump", true);
			shangbanshen_an.SetBool ("moving", true);
			xiabanshen_an.SetBool ("moving", true);
			xiabanshen_an.SetBool ("jump", true);
			if (flipx) {
				this.r2d.AddForce (new Vector2 (-150f, 220f));
			}
			else {
				this.r2d.AddForce (new Vector2 (150f, 220f));
			}
			this.currentstate = -2;
			break;
		case (int)groundActions.upShoot:
			shangbanshen_an.SetBool ("upshoot", true);
			this.currentstate = -2;
			break;
		case (int)groundActions.shoot:
			shangbanshen_an.SetBool ("s_shoot", true);
			//Debug.Log ("headShoot");
			this.currentstate = -2;
			break;
		case (int)groundActions.downshoot:
			shangbanshen_an.SetBool ("s_downshoot", true);
			this.currentstate = -2;
			break;
		case (int)groundActions.knife:
			shangbanshen_an.SetBool ("s_knife", true);
			this.currentstate = -2;
			break;
		case (int)groundActions.callairsuport:
			if (state2) {
				shangbanshen_an.SetBool ("call", true);
				xiabanshen_an.SetBool ("moving", false);
				xiabanshen_an.SetBool ("idle", true);
			}
			else {
				eventend = true;
			}
			this.currentstate = -2;
			break;



//		case (int)platformActions.airKnife:
//			this.jump = true;
//			this.shangbanshen_an.SetBool ("jump", true);
//			this.shangbanshen_an.SetBool ("airknife", true);
//			this.xiabanshen_an.SetBool ("jump", true);
//			this.r2d.AddForce (new Vector2 (-100f, 150f));
//			this.currentstate = -2;
//			break;
		case (int)platformActions.sleep:
			shangbanshen_an.SetBool ("sleep", true);
			xiabanshen_an.SetBool ("moving", false);
			xiabanshen_an.SetBool ("idle", true);
			this.currentstate = -2;
			break;
		case (int)platformActions.drink:
			if (!state2) {
				shangbanshen_an.SetBool ("drink", true);
				xiabanshen_an.SetBool ("moving", false);
				xiabanshen_an.SetBool ("idle", true);
			}
			else {
				eventend = true;
			}
			this.currentstate = -2;
			break;
		case (int)platformActions.downShoot:
			shangbanshen_an.SetBool ("s_downshoot", true);
			this.currentstate = -2;
			break;
		case (int)platformActions.jumpdownShoot:
			if (this.stock) {
				this.stock = false;
			}
			this.jump = true;
			this.gameObject.layer = 21;
			shangbanshen_an.SetBool ("s_downshoot", true);
			xiabanshen_an.SetBool ("moving", true);
			xiabanshen_an.SetBool ("jump", true);
			if (flipx) {
				this.r2d.AddForce (new Vector2 (-150f, 220f));
			}
			else {
				this.r2d.AddForce (new Vector2 (150f, 220f));
			}
			this.currentstate = -2;
			break;
		case (int)platformActions.callairsuport:
			if (state2) {
				shangbanshen_an.SetBool ("call", true);
				xiabanshen_an.SetBool ("moving", false);
				xiabanshen_an.SetBool ("idle", true);
			}
			else {
				eventend = true;
			}
			this.currentstate = -2;
			break;
		case (int)platformActions.throwGrenade:
			shangbanshen_an.SetBool ("s_throwgrenade", true);
			//Debug.Log ("throwGrenade");
			this.currentstate = -2;
			break;
		case (int)platformActions.moveJump:
			if (this.stock) {
				this.stock = false;
			}
			this.jump = true;
			this.gameObject.layer = 21;
			shangbanshen_an.SetBool ("jump", true);
			shangbanshen_an.SetBool ("moving", true);
			xiabanshen_an.SetBool ("moving", true);
			xiabanshen_an.SetBool ("jump", true);
			if (flipx) {
				this.r2d.AddForce (new Vector2 (-150f, 220f));
			}
			else {
				this.r2d.AddForce (new Vector2 (150f, 220f));
			}
			this.currentstate = -2;
			break;
		case (int)platformActions.shoot:
			shangbanshen_an.SetBool ("s_shoot", true);
			this.currentstate = -2;
			break;
		case (int)platformActions.knife:
			shangbanshen_an.SetBool ("s_knife", true);
			this.currentstate = -2;
			break;
		}

	}

	//先进入跑步状态再进入跳跃小刀状态
	void FirstJumpAttack ()
	{
		jump = true;
		xiabanshen.GetComponent<SpriteRenderer> ().enabled = true;
		xiabanshen.GetComponent<Animator> ().enabled = true;
		shangbanshen_an.SetBool ("moving", true);
		xiabanshen_an.SetBool ("moving", true);
		xiabanshen_an.SetBool ("idle", false);
		this.currentstate = (int)normalActions.run_left;
		Invoke ("ShowPlatform1", 3.0f);
	}

	void OnTriggerEnter2D (Collider2D c2d)
	{
		if (c2d.tag == "edagecheck") {
			if (!firstshow) {
				if (this.currentstate == (int)normalActions.run_left) {
					firstshow = true;
					this.jump = true;
					this.shangbanshen_an.SetBool ("jump", true);
					this.shangbanshen_an.SetBool ("airknife", true);
					this.xiabanshen_an.SetBool ("jump", true);
					this.r2d.AddForce (new Vector2 (-100f, 150f));
					this.currentstate = -2;
				}
			}
		}

		if (c2d.gameObject.layer==14) {
			HP.value -= 0.02f;
			//到0血之后死亡
			if (!state2) {
				if (HP.value <= 0.5f) {
					state2 = true;
					TurnState2 ();
				}
			}
			if (HP.value <= 0) {
				this.die = true;
				BossDie ();
			}
		}
	}

	void OnTriggerExit2D (Collider2D c2d)
	{
		if (c2d.tag == "edagecheck") {
			
		}

		if (c2d.gameObject.layer==14) {
			//this.DieByWepon ();
		}
	}

	void OnCollisionEnter2D (Collision2D c2d)
	{
		if (firstshow) {
			if (c2d.gameObject.tag == "ground") {
				this.shangbanshen_an.SetBool ("airknife", false);
				this.shangbanshen_an.SetBool ("jump", false);
				this.xiabanshen_an.SetBool ("jump", false);
				shangbanshen_an.SetBool ("moving", true);
				//jump = false;
				//this.currentstate = (int)normalActions.run_left;
			}
		}

		if (c2d.gameObject.tag == "ground") {
			if(jump)
			{
				jump = false;
				shangbanshen_an.SetBool ("standjump", false);
				shangbanshen_an.SetBool ("jump", false);
				xiabanshen_an.SetBool ("standjump", false);
				xiabanshen_an.SetBool ("jump", false);
				this.gameObject.layer = 18;
//				if (die) {
//					xiabanshen_an.enabled = true;
//				}
			}
		}

		//陷阱
		if (c2d.gameObject.layer == 23) {
			if (c2d.gameObject.GetComponent<Rigidbody2D> ().constraints == RigidbodyConstraints2D.FreezeAll) {
				return;
			}
			HP.value -= 0.2f;
			c2d.gameObject.layer = 1;
			hitted = true;
			Hitted ();
		}
	}

	void ShowPlatform1 ()
	{
		platform1.SetActive (true);
	}

	//当主角死后，应该变成原始状态
	void BackState ()
	{
		if (!die) {
			if (herocontroller.die) {
				if (state2) {
					shangbanshen_an.SetBool ("b_winpose", true);
					shangbanshen_an.SetBool ("winpose", false);
				} else {
					shangbanshen_an.SetBool ("winpose", true);
					shangbanshen_an.SetBool ("b_winpose", false);
				}
				shangbanshen_an.SetBool ("moving", false);
				xiabanshen.GetComponent<SpriteRenderer> ().enabled = false;
				this.currentstate = (int)normalActions.winpose;
			}
		}
		
	}

	void FaceHero()
	{
		if (this.stock) {
			return;
		}
		if (this.die) {
			return;
		}
		//yield return new WaitForSeconds (1.0f);
		if (!herocontroller.die) {
			if (grounded && eventend) {
				float dis = this.transform.position.x - Hero.transform.position.x;
				float dis2 = this.transform.position.y - Hero.transform.position.y;
				//Debug.Log (dis2);
				if (dis < 0) {
					this.shangbanshen_sr.flipX = false;
					this.xiabanshen_sr.flipX = false;
					this.flipx = false;
				} else {
					this.shangbanshen_sr.flipX = true;
					this.xiabanshen_sr.flipX = true;
					this.flipx = true;
				}
				if (dis < -1.5f) {
					this.currentstate = (int)normalActions.run_right;
				} else if (dis < 0 && dis >= -1.5f) {
					eventend = false;
					if (dis >= -0.5f) {
						if (dis2 <= -1.0f) {
							this.currentstate = (int)groundActions.upShoot;
						}
						else {
							this.currentstate = (int)groundActions.knife;
						}
					}
					else {
						if (dis2 >= 1.5f) {
							this.currentstate = Random.Range ((int)platformActions.sleep, (int)platformActions.callairsuport+1);
						} else{
							this.currentstate = Random.Range ((int)groundActions.slideShoot, (int)groundActions.callairsuport+1);
						}
					}
				}
				if (dis > 1.5f) {
					this.currentstate = (int)normalActions.run_left;
				} else if (dis > 0 && dis <= 1.5f) {
					eventend = false;
					if (dis <= 0.5f) {
						if (dis2 <= -1.0f) {
							this.currentstate = (int)groundActions.upShoot;
						} else {
							this.currentstate = (int)groundActions.knife;
						}
					}
					else {
						if (dis2 >= 1.5f) {
							this.currentstate = Random.Range ((int)platformActions.sleep, (int)platformActions.callairsuport+1);
						} else{
							this.currentstate = Random.Range ((int)groundActions.slideShoot, (int)groundActions.callairsuport+1);
						}
					}
				}

			}
		}
	}

	public void SetEventEnd()
	{
		eventend = true;
	}
	public void SetEventEndFalse()
	{
		eventend = false;
	}

	void StandJump()
	{
		//shangbanshen_an.SetTrigger ("test");
		//xiabanshen_an.SetTrigger ("test");
		//this.r2d.AddForce (new Vector2 (0, 150f));
		//this.currentstate=(int)groundActions.upShoot;
//		this.die = true;
		BossDie ();
	}

	public void MoveJump()
	{
		this.currentstate = (int)groundActions.moveJump;
		this.stock = true;
	}

	void TurnState2()
	{
		state2=true;
		eventend = false;
		background.GetComponent<Animator> ().enabled = true;
		sky.GetComponent<Animator> ().enabled = true;
		this.r2d.constraints = RigidbodyConstraints2D.FreezeAll;
		this.GetComponent<BoxCollider2D> ().enabled = false;
		shangbanshen.GetComponent<BoxCollider2D> ().enabled = false;
		xiabanshen_an.SetBool ("state2", true);
		shangbanshen_an.SetTrigger ("biggun");
		this.currentstate = -2;
	}

	void BossDie()
	{
		if (herodead) {
			return;
		}

		this.die = true;
		if (this.jump) {
			xiabanshen_an.SetTrigger ("die_air");
		}
		else {
			xiabanshen_an.SetTrigger ("die");
		}

		if (this.flipx) {
			this.r2d.AddForce (new Vector2 (80.0f, 0));
		}
		else {
			this.r2d.AddForce (new Vector2 (-80.0f, 0));
		}

		Hero.SendMessage ("Win");
	}

	void Hitted()
	{
		if (hittedTimes >= 20) {
			return;
		}
		if (hitted) {
			shangbanshen_sr.enabled = false;
			xiabanshen_sr.enabled = false;
			hitted = false;
		}
		else {
			shangbanshen_sr.enabled = true;
			xiabanshen_sr.enabled = true;
			hitted = true;
		}
		Invoke ("Hitted", 0.1f);
		hittedTimes++;
	}

	void HeroDead()
	{
		herodead = true;
	}

}
