using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGrenade : MonoBehaviour {


	//0为未发射，1为左，2为右
	public int shoot;

	private Animator animator;
	private SpriteRenderer sr;
	private Rigidbody2D r2d;

	private string currentstate;

	private Vector3 left;
	private Vector3 right;
	private Vector3 leftdown;
	private Vector3 rightdown;

	private bool down;
	private bool throws;

	private int grounded;

	private Vector3 orginPos;

	private AudioSource audios;

	// Use this for initialization
	void Start () {
		this.sr = this.GetComponent<SpriteRenderer> ();
		this.r2d = this.GetComponent<Rigidbody2D> ();
		throws = false;
		grounded = 0;

//		left = new Vector3 (-1, -0.3f, 0);
//		right = new Vector3 (-0.3f, 1.3f, 0);
//		leftdown = new Vector3 (0.1f, -0.5f, 0);
//		rightdown = new Vector3 (0.8f, 0.3f, 0);

		down = false;

		animator = this.GetComponent<Animator> ();
		audios = this.GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (this.tag == "clone") {
			if (this.currentstate != "die") {
				if (shoot == 1) {

					if (!throws) {
						this.r2d.constraints = RigidbodyConstraints2D.FreezeRotation;
						this.r2d.AddForce (new Vector2 (-150.0f, 100.0f));
						throws = true;
					}
						
					this.animator.enabled = true;
				}
				if (shoot == 2) {

					if (!throws) {
						this.r2d.constraints = RigidbodyConstraints2D.FreezeRotation;
						this.r2d.AddForce (new Vector2 (150.0f, 100.0f));
						throws = true;
					}

					this.animator.enabled = true;
				}
			}
			Vector3 pos = Camera.main.WorldToViewportPoint (this.transform.position);
			bool isVisible = (Camera.main.orthographic || pos.z > 0f) && (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f);
			if (!isVisible) {
				Destroy (this.gameObject);
			}
		}

		if ((shoot!=0) && (this.tag!="clone")) {
			GameObject ehb = (GameObject)Instantiate (this.gameObject);
			ehb.transform.position = this.gameObject.transform.position;
			ehb.transform.localScale = new Vector3 (1.5f, 1.5f, 0);
			ehb.tag = "clone";
			ehb.GetComponent<BossGrenade> ().shoot = this.shoot;
			ehb.GetComponent<BossGrenade> ().orginPos = this.transform.transform.position;
			ehb.GetComponent<CircleCollider2D> ().enabled = true;
			ehb.GetComponent<Rigidbody2D> ().sleepMode = RigidbodySleepMode2D.StartAwake;
			ehb.GetComponent<SpriteRenderer> ().enabled = true;
			ehb.GetComponent<Animator> ().enabled = true;
			this.shoot = 0;
		}

	}

	void Reverse()
	{
		if (this.sr.flipX) {
			this.sr.flipX = false;
			return;
		}
		else if (!this.sr.flipX) {
			this.sr.flipX = true;
			return;
		}
	}

	void DestroyThis()
	{
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D c2d)
	{
//		if (c2d.name == "Hero") {
//			this.currentstate = "die";
//			this.animator.SetBool ("bomb", true);
//			c2d.gameObject.GetComponent<HeroController> ().SendMessage ("DieByWepon");
//		}
//
//		if (c2d.tag == "ground") {
//			if (grounded >= 3) {
//				this.currentstate = "die";
//				this.animator.SetBool ("bomb", true);
//			}
//			else {
//				this.grounded++;
//			}
//		}
	}

	void OnCollisionEnter2D(Collision2D c2d)
	{
		if (c2d.gameObject.name == "Hero") {
			this.currentstate = "die";
			this.r2d.constraints = RigidbodyConstraints2D.FreezeAll;
			this.animator.SetBool ("bomb", true);
			c2d.gameObject.GetComponent<HeroController> ().SendMessage ("DieByWepon");
		}

		if (c2d.gameObject.tag == "ground") {
			if (grounded >= 1) {
				this.currentstate = "die";
				this.r2d.constraints = RigidbodyConstraints2D.FreezeAll;
				this.animator.SetBool ("bomb", true);
			}
			else {
				this.grounded++;
			}
		}
	}

	void PlayBombVoice()
	{
		audios.Play ();
	}
}
