using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossXiabanshen : MonoBehaviour {

	public GameObject shangbanshen;
	public GameObject BossMain;

	public GameObject bullet1;
	public GameObject bullet2;
	private GameObject firstbullet;
	private List<GameObject> bulletlist = new List<GameObject> ();

	private Animator an;
	private SpriteRenderer sr;

	private bool bulletType;//true为biggun，false为smallgun

	private bool move;

	// Use this for initialization
	void Start () {
		this.an = this.GetComponent<Animator> ();
		this.sr = this.GetComponent<SpriteRenderer> ();

		bulletType = false;
		move = false;
	}
	
	// Update is called once per frame
	void Update () {
//		if (move) {
//			if (this.sr.flipX) {
//				BossMain.transform.Translate (Vector3.left * Time.deltaTime);
//			}
//			else {
//				BossMain.transform.Translate (Vector3.right * Time.deltaTime);
//			}
//		}
		foreach (GameObject go in bulletlist) {

			if (go) {
				if ((go.transform.position.x >= this.transform.parent.position.x + 10.0f) || (go.transform.position.x <= this.transform.parent.position.x - 10.0f) || (go.transform.position.y >= this.transform.parent.position.y + 10.0f)) {
					Destroy (go);
					continue;
				}

				if (go.GetComponent<Bullet> ().boom != true) {
					if (bulletType) {
						if (go.GetComponent<SpriteRenderer> ().flipX) {
							go.transform.position = new Vector3 (go.transform.position.x - Time.deltaTime * 7.0f, go.transform.position.y, 0);
						} else {
							go.transform.position = new Vector3 (go.transform.position.x + Time.deltaTime * 7.0f, go.transform.position.y, 0);
						}
					}
					else {
						if (go.GetComponent<SpriteRenderer> ().flipX) {
							go.transform.position = new Vector3 (go.transform.position.x - Time.deltaTime * 7.0f, go.transform.position.y, 0);
						} else {
							go.transform.position = new Vector3 (go.transform.position.x + Time.deltaTime * 7.0f, go.transform.position.y, 0);
						}
					}
				}
			}
		}
	}

	void SetShangbanshensrEnable()
	{
		shangbanshen.GetComponent<SpriteRenderer> ().enabled = true;
		shangbanshen.GetComponent<Animator> ().enabled = true;
		this.move = false;
		this.an.SetBool ("s_slideshoot", false);
		this.an.SetBool ("b_slideshoot", false);
		this.an.SetBool ("state2", false);
		BossMain.SendMessage ("SetEventEndFalse");
	}

	void State2()
	{
		shangbanshen.GetComponent<SpriteRenderer> ().enabled = true;
		shangbanshen.GetComponent<Animator> ().enabled = true;
		this.an.SetBool ("state2", false);
		BossMain.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
		BossMain.GetComponent<BoxCollider2D> ().enabled = true;
		shangbanshen.GetComponent<BoxCollider2D> ().enabled = true;
		BossMain.SendMessage ("SetEventEndFalse");
	}

	void SetShangbanshensrDisable()
	{
		shangbanshen.GetComponent<SpriteRenderer> ().enabled = false;
		shangbanshen.GetComponent<Animator> ().enabled = false;
	}

	void ShowbiggunBulletDunxia ()
	{
		bulletType = true;

		GameObject bullets = Instantiate (bullet2) as GameObject;
		bullets.GetComponent<SpriteRenderer> ().sprite = (Resources.Load ("BossBullet2") as GameObject).GetComponent<SpriteRenderer> ().sprite;

		bullets.transform.parent = bullet1.transform.parent;
		bullets.transform.localScale = new Vector3 (1.0f, 1.0f, 0);

		if (this.sr.flipX) {
			bullets.transform.position = new Vector3 (this.transform.parent.position.x - 0.5f, this.transform.parent.position.y - 0.1f, 0);
			bullets.GetComponent<SpriteRenderer> ().flipX = this.sr.flipX;


		} else {
			bullets.transform.position = new Vector3 (this.transform.parent.position.x + 0.5f, this.transform.parent.position.y - 0.1f, 0);
			bullets.GetComponent<SpriteRenderer> ().flipX = this.sr.flipX;
		}

		bullets.tag = "Horizontal";
		bullet1.tag = "Horizontal";
		bullets.layer = 20;
		bullet1.layer = 20;
		bulletlist.Add (bullets);
	}

	void ShowsmallgunBulletDunxia ()
	{
		bulletType = false;

		GameObject bullets = Instantiate (bullet1) as GameObject;
		bullets.GetComponent<SpriteRenderer> ().sprite = (Resources.Load ("BossBullet") as GameObject).GetComponent<SpriteRenderer> ().sprite;

		bullets.transform.parent = bullet1.transform.parent;
		bullets.transform.localScale = new Vector3 (1.0f, 1.0f, 0);

		if (this.sr.flipX) {
			bullets.transform.position = new Vector3 (this.transform.parent.position.x, this.transform.parent.position.y - 0.1f, 0);
			bullets.GetComponent<SpriteRenderer> ().flipX = this.sr.flipX;
		}
		else {
			bullets.transform.position = new Vector3 (this.transform.parent.position.x, this.transform.parent.position.y - 0.1f, 0);
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

	void SetStandJumpFalse()
	{
		BossMain.layer = 18;
		BossMain.SendMessage ("SetEventEndFalse");
		//BossMain.SendMessage ("SetEventEnd");
		//this.an.SetBool ("standjump", false);
		//this.an.SetBool ("jump", false);
	}

	void StartEvent()
	{
		BossMain.SendMessage ("SetEventEnd");
		//this.move = false;
	}

	void EndEvent()
	{
		BossMain.SendMessage ("SetEventEndFalse");
		//this.move = false;
	}

	void StartMove()
	{
		//this.move = true;
	}

	void PauseAnimator()
	{
//		this.an.enabled = false;
		shangbanshen.GetComponent<Animator> ().enabled = false;
		shangbanshen.GetComponent<SpriteRenderer> ().enabled = false;
	}
}
