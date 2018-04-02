using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//flipX为false的时候是向左，为true的时候是向右

public class Enemy : MonoBehaviour {


	protected Animator animator;
	protected SpriteRenderer sr;
	protected GameObject Hero;
	protected HeroController herocontroller;
	protected string currentState;
	protected string enemy_tag;
	protected bool seehero;
	protected float distanceofSeeHero;

	protected bool hitHero;

	protected AudioSource audios;


	// Use this for initialization
	void Start () {
		InitializeData ();


	}

	//初始化贴图，动画等组件
	protected void InitializeData()
	{
		animator = this.GetComponent<Animator> ();
		sr = this.GetComponent<SpriteRenderer> ();
		Hero = GameObject.Find ("Hero");
		herocontroller = Hero.GetComponent<HeroController> ();
		currentState = "";
		enemy_tag = this.tag;
		seehero = false;
		distanceofSeeHero = 1.5f;
		hitHero = false;


		if (this.GetComponent<AudioSource> () == null) {
			audios = this.gameObject.AddComponent<AudioSource> ();
		}
		else {
			audios = this.gameObject.GetComponent<AudioSource> ();
		}
		audios.playOnAwake = false;
		if (this.gameObject.name.IndexOf ("enemy") != -1) {
			audios.clip = Resources.Load ("enemydie1") as AudioClip;
		}
		if (this.gameObject.name.IndexOf ("primitive") != -1) {
			audios.clip = Resources.Load ("enemydie1") as AudioClip;
		}
		if (this.gameObject.name.IndexOf ("sniper") != -1) {
			audios.clip = Resources.Load ("enemydie2") as AudioClip;
		}

	}
	
	// Update is called once per frame
//	void Update () {
//	}


		

	//当死亡时销毁这个对象
	protected void DestoryThis()
	{
		Destroy (this.gameObject);
	}



	//结束攻击
	protected void EndAttack()
	{
		animator.SetBool ("run", false);
		animator.SetBool ("idle", true);
		animator.SetBool ("attack", false);
		currentState = "stop";
	}



	//发送死亡通知给主角
	protected void SendDieBySoward()
	{
		if (hitHero) {
			currentState = "stop";
			animator.SetBool ("run", false);
			animator.SetBool ("idle", true);
			animator.SetBool ("attack", false);
			if (Hero) {
				Hero.GetComponent<HeroController> ().SendMessage ("DieBySoward");
			}
		}
	}

	//发送死亡通知给主角
	protected void SendDieByWepon()
	{
		if (hitHero) {
			animator.SetBool ("run", false);
			animator.SetBool ("attack", false);
			animator.SetBool ("idle", true);
			currentState = "stop";
			if (Hero) {
				Hero.GetComponent<HeroController> ().SendMessage ("DieByWepon");
			}
		}
	}


	//当主角死后，应该变成原始状态
	protected void BackState()
	{
		if (animator) {
			if (herocontroller.die) {
				animator.SetBool ("run", false);
				animator.SetBool ("idle", true);
				animator.SetBool ("attack", false);
				currentState = "idle";
			}
		}
	}


	//被子弹射到
	protected void ShootByBullet(Collider2D c2d)
	{
		if (c2d.name.ToString ().IndexOf ("bullet") != -1) {

			if (audios.clip) {
				audios.Play ();
			}

			if (this.transform.parent.name == "Scene5") {
				GameObject.Find ("Scene5").SendMessage ("Die");
			}

			animator.SetBool ("die", true);
			animator.SetBool ("run", false);
			animator.SetBool ("idle", false);
			animator.SetBool ("attack", false);
			currentState = "die";
			if (this.GetComponent<Rigidbody2D>() != null) {
				this.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
			}

		}
	}


	//被近战攻击杀死
	protected void KillByMeleeAttack()
	{
		animator.SetBool ("diebymeleeattack", true);
		currentState = "die";
		if (this.GetComponent<Rigidbody2D>() != null) {
			this.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
		}
		if (this.GetComponent<CapsuleCollider2D> ()) {
			this.GetComponent<CapsuleCollider2D> ().enabled = false;
		}
	}
		
}
