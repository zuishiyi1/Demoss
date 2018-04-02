using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShangbanshen : MonoBehaviour
{

	public GameObject BossMain;
	public GameObject xiabanshen;

	public GameObject Grenade;

	public GameObject bullet1;
	public GameObject bullet2;
	private GameObject firstbullet;
	private List<GameObject> bulletlist = new List<GameObject> ();

	private BossController Boss_Script;
	private Animator an;
	private SpriteRenderer sr;
	private BoxCollider2D box2d;

	private int runtimes;

	private AudioSource audios;

	[HideInInspector]public bool state2;
//测试为true

	private bool move;

	// Use this for initialization
	void Start ()
	{
		state2 = true;
		this.an = this.GetComponent<Animator> ();
		box2d = this.GetComponent<BoxCollider2D> ();
		this.sr = this.GetComponent<SpriteRenderer> ();

		Boss_Script = BossMain.GetComponent<BossController> ();

		audios = this.GetComponent<AudioSource> ();

		move = false;

	}
	
	// Update is called once per frame
	void Update ()
	{
//		if (move) {
//			if (this.sr.flipX) {
//				BossMain.transform.Translate (Vector3.left * Time.deltaTime);
//			}
//			else {
//				BossMain.transform.Translate (Vector3.right * Time.deltaTime);
//			}
//		}
		if (runtimes >= 4) {
			Boss_Script.MoveJump ();
			runtimes = 0;
		}
		foreach (GameObject go in bulletlist) {

			if (go) {
//				if ((go.transform.position.x >= this.transform.parent.position.x + 10.0f) || (go.transform.position.x <= this.transform.parent.position.x - 10.0f) || (go.transform.position.y >= this.transform.parent.position.y + 10.0f)) {
//					Destroy (go);
//					continue;
//				}
				Vector3 pos = Camera.main.WorldToViewportPoint (go.transform.position);
				bool isVisible = (Camera.main.orthographic || pos.z > 0f) && (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f);
				if (!isVisible) {
					Destroy (go);
					continue;
				}

				if (go.GetComponent<Bullet> ().boom != true) {
					if (go.transform.tag == "up") {
						go.transform.position = new Vector3 (go.transform.position.x, go.transform.position.y + Time.deltaTime * 7.0f, 0);
					} else if (go.transform.tag == "Horizontal") {
						if (go.GetComponent<SpriteRenderer> ().flipX) {
							go.transform.position = new Vector3 (go.transform.position.x - Time.deltaTime * 7.0f, go.transform.position.y, 0);
						} else {
							go.transform.position = new Vector3 (go.transform.position.x + Time.deltaTime * 7.0f, go.transform.position.y, 0);
						}
					} else if (go.transform.tag == "down") {
						go.transform.position = new Vector3 (go.transform.position.x, go.transform.position.y - Time.deltaTime * 7.0f, 0);
					} else if (go.transform.tag == "up30") {
						if (go.GetComponent<SpriteRenderer> ().flipX) {
							go.transform.position = new Vector3 (go.transform.position.x - Time.deltaTime * 7.0f, go.transform.position.y + Time.deltaTime * 3.0f, 0);
						}
						else {
							go.transform.position = new Vector3 (go.transform.position.x + Time.deltaTime * 7.0f, go.transform.position.y + Time.deltaTime * 3.0f, 0);
						}
					}
					else if (go.transform.tag == "up60") {
						if (go.GetComponent<SpriteRenderer> ().flipX) {
							go.transform.position = new Vector3 (go.transform.position.x - Time.deltaTime * 7.0f, go.transform.position.y + Time.deltaTime * 7.0f, 0);
						}
						else {
							go.transform.position = new Vector3 (go.transform.position.x + Time.deltaTime * 7.0f, go.transform.position.y + Time.deltaTime * 7.0f, 0);
						}
					}
					else if (go.transform.tag == "down30") {
						if (go.GetComponent<SpriteRenderer> ().flipX) {
							go.transform.position = new Vector3 (go.transform.position.x - Time.deltaTime * 5.0f, go.transform.position.y - Time.deltaTime * 7.0f, 0);
						}
						else {
							go.transform.position = new Vector3 (go.transform.position.x + Time.deltaTime * 5.0f, go.transform.position.y - Time.deltaTime * 7.0f, 0);
						}
					}
					else if (go.transform.tag == "down60") {
						if (go.GetComponent<SpriteRenderer> ().flipX) {
							go.transform.position = new Vector3 (go.transform.position.x - Time.deltaTime * 2.0f, go.transform.position.y - Time.deltaTime * 7.0f, 0);
						}
						else {
							go.transform.position = new Vector3 (go.transform.position.x + Time.deltaTime * 2.0f, go.transform.position.y - Time.deltaTime * 7.0f, 0);
						}
					}
				}
			}
		}
	}

	void PauseshangbanshenAnimator ()
	{
		an.enabled = false;
		Invoke ("PlayshangbanshenAnimator", 0.7f);
	}

	void PlayshangbanshenAnimator ()
	{
		an.enabled = true;
		this.gameObject.layer = 19;
		runtimes = 0;
	}

	void BackToNormalLayer ()
	{
		this.gameObject.layer = 18;
		Boss_Script.SetEventEnd ();
	}

	void ShowSmallgunBullet ()
	{
		runtimes = 0;
		GameObject bullets = Instantiate (bullet1) as GameObject;
		bullets.GetComponent<SpriteRenderer> ().sprite = (Resources.Load ("BossBullet") as GameObject).GetComponent<SpriteRenderer> ().sprite;

		bullets.transform.parent = bullet1.transform.parent;
		bullets.transform.localScale = new Vector3 (1.0f, 1.0f, 0);

		if (this.sr.flipX) {
			bullets.transform.position = new Vector3 (this.transform.parent.position.x - 0.5f, this.transform.parent.position.y + 0.1f, 0);
			bullets.GetComponent<SpriteRenderer> ().flipX = this.sr.flipX;


		} else {
			bullets.transform.position = new Vector3 (this.transform.parent.position.x + 0.5f, this.transform.parent.position.y + 0.1f, 0);
			bullets.GetComponent<SpriteRenderer> ().flipX = this.sr.flipX;
		}

		bullets.tag = "Horizontal";
		bullet1.tag = "Horizontal";
		bullets.layer = 20;
		bullet1.layer = 20;
		Vector3 rotation = this.transform.localEulerAngles; 
		rotation.z = 0;
		bullets.transform.localEulerAngles = rotation;
		bullet1.transform.localEulerAngles = rotation;
		bulletlist.Add (bullets);
	}

	void EndShot ()
	{
		this.GetComponent<Animator> ().SetBool ("shoot", false);
		this.GetComponent<Animator> ().SetBool ("s_shoot", false);
	}

	void ShowSmallgunBulletUp ()
	{
		runtimes = 0;
		GameObject bullets = Instantiate (bullet1) as GameObject;
		bullets.GetComponent<SpriteRenderer> ().sprite = (Resources.Load ("BossBullet") as GameObject).GetComponent<SpriteRenderer> ().sprite;

		bullets.transform.parent = bullet1.transform.parent;
		bullets.transform.localScale = new Vector3 (1.0f, 1.0f, 0);

		bullets.transform.position = new Vector3 (this.transform.parent.position.x, this.transform.parent.position.y + 0.6f, 0);

		bullets.tag = "up";
		bullet1.tag = "up";
		bullets.layer = 20;
		bullet1.layer = 20;
		Vector3 rotation = this.transform.localEulerAngles; 
		rotation.z = 90.0f;
		bullets.transform.localEulerAngles = rotation;
		bullet1.transform.localEulerAngles = rotation;

		bulletlist.Add (bullets);
	}

	void ShowSmallgunBulletDown ()
	{
		runtimes = 0;
		GameObject bullets = Instantiate (bullet1) as GameObject;
		bullets.GetComponent<SpriteRenderer> ().sprite = (Resources.Load ("BossBullet") as GameObject).GetComponent<SpriteRenderer> ().sprite;

		bullets.transform.parent = bullet1.transform.parent;
		bullets.transform.localScale = new Vector3 (1.0f, 1.0f, 0);

		bullets.transform.position = new Vector3 (this.transform.parent.position.x, this.transform.parent.position.y - 0.6f, 0);

		bullets.tag = "down";
		bullet1.tag = "down";
		bullets.layer = 20;
		bullet1.layer = 20;
		Vector3 rotation = this.transform.localEulerAngles; 
		rotation.z = -90.0f;
		bullets.transform.localEulerAngles = rotation;
		bullet1.transform.localEulerAngles = rotation;

		bulletlist.Add (bullets);
	}

	void ShowBiggunBullet ()
	{
		runtimes = 0;
		GameObject bullets = Instantiate (bullet2) as GameObject;
		bullets.GetComponent<SpriteRenderer> ().sprite = (Resources.Load ("BossBullet2") as GameObject).GetComponent<SpriteRenderer> ().sprite;

		bullets.transform.parent = bullet1.transform.parent;
		bullets.transform.localScale = new Vector3 (1.0f, 1.0f, 0);

		if (this.sr.flipX) {
			bullets.transform.position = new Vector3 (this.transform.parent.position.x - 0.5f, this.transform.parent.position.y + 0.1f, 0);
			bullets.GetComponent<SpriteRenderer> ().flipX = this.sr.flipX;


		} else {
			bullets.transform.position = new Vector3 (this.transform.parent.position.x + 0.5f, this.transform.parent.position.y + 0.1f, 0);
			bullets.GetComponent<SpriteRenderer> ().flipX = this.sr.flipX;
		}

		bullets.layer = 20;
		bulletlist.Add (bullets);
	}

	void ShowbiggunBulletUp30 ()
	{
		runtimes = 0;
		GameObject bullets = Instantiate (bullet2) as GameObject;
		bullets.GetComponent<SpriteRenderer> ().sprite = (Resources.Load ("BossBullet2") as GameObject).GetComponent<SpriteRenderer> ().sprite;

		bullets.transform.parent = bullet1.transform.parent;
		bullets.transform.localScale = new Vector3 (1.0f, 1.0f, 0);

		Vector3 rotation = this.transform.localEulerAngles; 


		if (this.sr.flipX) {
			bullets.GetComponent<SpriteRenderer> ().flipX = true;
			bullets.transform.position = new Vector3 (this.transform.parent.position.x-0.5f, this.transform.parent.position.y + 0.1f, 0);
			rotation.z = -30.0f;
		}
		else {
			bullets.GetComponent<SpriteRenderer> ().flipX = false;
			bullets.transform.position = new Vector3 (this.transform.parent.position.x+0.5f, this.transform.parent.position.y + 0.2f, 0);
			rotation.z = 30.0f;
		}

		bullets.tag = "up30";
		bullet1.tag = "up30";
		bullets.layer = 20;
		bullet1.layer = 20;

		bullets.transform.localEulerAngles = rotation;
		bullet1.transform.localEulerAngles = rotation;

		bulletlist.Add (bullets);
	}

	void ShowbiggunBulletUp60 ()
	{
		GameObject bullets = Instantiate (bullet2) as GameObject;
		bullets.GetComponent<SpriteRenderer> ().sprite = (Resources.Load ("BossBullet2") as GameObject).GetComponent<SpriteRenderer> ().sprite;

		bullets.transform.parent = bullet1.transform.parent;
		bullets.transform.localScale = new Vector3 (1.0f, 1.0f, 0);

		Vector3 rotation = this.transform.localEulerAngles; 


		if (this.sr.flipX) {
			bullets.GetComponent<SpriteRenderer> ().flipX = true;
			bullets.transform.position = new Vector3 (this.transform.parent.position.x - 0.5f, this.transform.parent.position.y + 0.4f, 0);
			rotation.z = -60.0f;
		}
		else {
			bullets.GetComponent<SpriteRenderer> ().flipX = false;
			bullets.transform.position = new Vector3 (this.transform.parent.position.x + 0.5f, this.transform.parent.position.y + 0.4f, 0);
			rotation.z = 60.0f;
		}

		bullets.tag = "up60";
		bullet1.tag = "up60";
		bullets.layer = 20;
		bullet1.layer = 20;
		bullets.transform.localEulerAngles = rotation;
		bullet1.transform.localEulerAngles = rotation;

		bulletlist.Add (bullets);
	}

	void ShowbiggunBulletUp90 ()
	{
		GameObject bullets = Instantiate (bullet2) as GameObject;
		bullets.GetComponent<SpriteRenderer> ().sprite = (Resources.Load ("BossBullet2") as GameObject).GetComponent<SpriteRenderer> ().sprite;

		bullets.transform.parent = bullet1.transform.parent;
		bullets.transform.localScale = new Vector3 (1.0f, 1.0f, 0);

		bullets.transform.position = new Vector3 (this.transform.parent.position.x, this.transform.parent.position.y + 0.6f, 0);

		bullets.tag = "up";
		bullet1.tag = "up";
		bullets.layer = 20;
		bullet1.layer = 20;
		Vector3 rotation = this.transform.localEulerAngles; 
		rotation.z = 90.0f;
		bullets.transform.localEulerAngles = rotation;
		bullet1.transform.localEulerAngles = rotation;

		bulletlist.Add (bullets);
	}


	/// <summary>
	/// Showbigguns the bullet down
	/// </summary>
	void ShowbiggunBulletDown30 ()
	{
		runtimes = 0;
		GameObject bullets = Instantiate (bullet2) as GameObject;
		bullets.GetComponent<SpriteRenderer> ().sprite = (Resources.Load ("BossBullet2") as GameObject).GetComponent<SpriteRenderer> ().sprite;

		bullets.transform.parent = bullet1.transform.parent;
		bullets.transform.localScale = new Vector3 (1.0f, 1.0f, 0);

		Vector3 rotation = this.transform.localEulerAngles; 


		if (this.sr.flipX) {
			bullets.GetComponent<SpriteRenderer> ().flipX = true;
			bullets.transform.position = new Vector3 (this.transform.parent.position.x-0.5f, this.transform.parent.position.y - 0.2f, 0);
			rotation.z = 30.0f;
		}
		else {
			bullets.GetComponent<SpriteRenderer> ().flipX = false;
			bullets.transform.position = new Vector3 (this.transform.parent.position.x+0.5f, this.transform.parent.position.y - 0.2f, 0);
			rotation.z = -30.0f;
		}

		bullets.tag = "down30";
		bullet1.tag = "down30";
		bullets.layer = 20;
		bullet1.layer = 20;

		bullets.transform.localEulerAngles = rotation;
		bullet1.transform.localEulerAngles = rotation;

		bulletlist.Add (bullets);
	}

	void ShowbiggunBulletDown60 ()
	{
		GameObject bullets = Instantiate (bullet2) as GameObject;
		bullets.GetComponent<SpriteRenderer> ().sprite = (Resources.Load ("BossBullet2") as GameObject).GetComponent<SpriteRenderer> ().sprite;

		bullets.transform.parent = bullet1.transform.parent;
		bullets.transform.localScale = new Vector3 (1.0f, 1.0f, 0);

		Vector3 rotation = this.transform.localEulerAngles; 


		if (this.sr.flipX) {
			bullets.GetComponent<SpriteRenderer> ().flipX = true;
			bullets.transform.position = new Vector3 (this.transform.parent.position.x - 0.3f, this.transform.parent.position.y - 0.5f, 0);
			rotation.z = 60.0f;
		}
		else {
			bullets.GetComponent<SpriteRenderer> ().flipX = false;
			bullets.transform.position = new Vector3 (this.transform.parent.position.x + 0.3f, this.transform.parent.position.y - 0.5f, 0);
			rotation.z = -60.0f;
		}

		bullets.tag = "down60";
		bullet1.tag = "down60";
		bullets.layer = 20;
		bullet1.layer = 20;
		bullets.transform.localEulerAngles = rotation;
		bullet1.transform.localEulerAngles = rotation;

		bulletlist.Add (bullets);
	}

	void ShowbiggunBulletDown90 ()
	{
		GameObject bullets = Instantiate (bullet2) as GameObject;
		bullets.GetComponent<SpriteRenderer> ().sprite = (Resources.Load ("BossBullet2") as GameObject).GetComponent<SpriteRenderer> ().sprite;

		bullets.transform.parent = bullet1.transform.parent;
		bullets.transform.localScale = new Vector3 (1.0f, 1.0f, 0);

		bullets.transform.position = new Vector3 (this.transform.parent.position.x, this.transform.parent.position.y - 0.6f, 0);

		bullets.tag = "down";
		bullet1.tag = "down";
		bullets.layer = 20;
		bullet1.layer = 20;
		Vector3 rotation = this.transform.localEulerAngles; 
		rotation.z = -90.0f;
		bullets.transform.localEulerAngles = rotation;
		bullet1.transform.localEulerAngles = rotation;

		bulletlist.Add (bullets);
	}

	void ThrowGrenade()
	{
		runtimes = 0;
		this.move = true;
		if (this.sr.flipX) {
			Grenade.GetComponent<BossGrenade> ().shoot = 1;
		}
		else {
			Grenade.GetComponent<BossGrenade> ().shoot = 2;
		}
			
	}

	void GrenadeToMoving()
	{
		this.move = false;
		this.an.SetBool ("s_throwgrenade", false);
		this.an.SetBool ("throwgrenade", false);
		BossMain.SendMessage ("SetEventEndFalse");
		//BossMain.SendMessage ("SetEventEnd");
	}

	void DrinkToMoving()
	{
		runtimes = 0;
		this.an.SetBool ("drink", false);
		this.an.SetBool ("sleep", false);
		xiabanshen.GetComponent<Animator> ().SetBool ("idle", false);
		xiabanshen.GetComponent<Animator> ().SetBool ("moving", true);
		BossMain.SendMessage ("SetEventEndFalse");
	}

	void SetStandJumpFalse()
	{
		runtimes = 0;
		BossMain.layer = 18;
		BossMain.SendMessage ("SetEventEndFalse");
	}

	void UpshootStart()
	{
		this.move = true;
	}

	void UpshootEnd()
	{
		runtimes = 0;
		this.move = false;
		this.an.SetBool ("upshoot", false);
	}

	void StartEvent()
	{
		runtimes++;
		BossMain.SendMessage ("SetEventEnd");
		this.move = false;
	}

	void EndEvent()
	{
		BossMain.SendMessage ("SetEventEndFalse");
	}

	void StartMove()
	{
	}

	void EndMove()
	{
		this.move = false;
	}

	void KnifeEnd()
	{
		this.an.SetBool ("s_knife", false);
		BossMain.SendMessage ("SetEventEndFalse");
		this.gameObject.layer = 18;
	}

	void ShootEnd()
	{
		this.an.SetBool ("s_shoot", false);
		BossMain.SendMessage ("SetEventEndFalse");
	}

	void DownShootEnd()
	{
		this.an.SetBool ("s_downshoot", false);
		BossMain.SendMessage ("SetEventEndFalse");
	}

	void CallToMoving()
	{
		runtimes = 0;
		this.an.SetBool ("call", false);
		xiabanshen.GetComponent<Animator> ().SetBool ("idle", false);
		xiabanshen.GetComponent<Animator> ().SetBool ("moving", true);
		BossMain.SendMessage ("SetEventEndFalse");
	}

	void PlayKnifeVoice()
	{
		audios.clip = Resources.Load ("boss_knife") as AudioClip;
		audios.Play ();
	}

	void PlayNormalgunVoice()
	{
		audios.clip = Resources.Load ("bossNormalgunshoot") as AudioClip;
		audios.Play ();
	}
		
}
