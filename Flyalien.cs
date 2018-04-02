using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyalien : Enemy {

	private GameObject fb;
	private Rigidbody2D r2d;
	private CircleCollider2D cc2d;

	private int shootTimes;


	// Use this for initialization
	void Start () {
		InitializeData ();

		foreach (Transform child in this.transform) {
			if (child.gameObject.name == "flyalienb") {
				fb = child.gameObject;
				break;
			}
		}

		this.r2d = this.GetComponent<Rigidbody2D> ();
		this.cc2d = this.GetComponent<CircleCollider2D> ();
		shootTimes = 0;
	}
	
	// Update is called once per frame
	void Update () {
		BackState ();

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
			//dieCounter++;
			//if (dieCounter >= 2) {
				ShootByBullet (c2d);
			//}
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
	#endregion

	void EnableColider()
	{
		//if(this.number!=4)
		cc2d.enabled = true;
	}

	void AttackWithBullet()
	{
		if (fb) {
			if (this.sr.flipX) {
				fb.GetComponent<flyalienb> ().shoot = 2;
			}
			else {
				fb.GetComponent<flyalienb> ().shoot = 1;
			}
		}
		shootTimes++;
		if (shootTimes >= 2) {
			animator.SetBool ("disappear", true);
			shootTimes = 0;
		}
	}

	void DestroyThis()
	{
		Destroy (this.gameObject);
	}

	void Disappear()
	{
		this.animator.SetBool ("disappear", false);
		this.animator.enabled = false;
		this.sr.enabled = false;
		this.cc2d.enabled = false;
		if (!this.sr.flipX) {
			this.transform.position = new Vector3 (this.transform.position.x - Random.Range (3, 5), this.transform.position.y, 0);

			//如果出了屏幕外面，就把他调回来
			Vector3 pos = Camera.main.WorldToViewportPoint (this.transform.position);
			bool isVisible = (Camera.main.orthographic || pos.z > 0f) && (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f);
			if (!isVisible) {
				this.transform.position = new Vector3 (this.transform.position.x + Random.Range (3, 5), this.transform.position.y, 0);
			}

			this.sr.flipX = true;
			fb.transform.localPosition = new Vector3 (fb.transform.localPosition.x + 0.388f, fb.transform.localPosition.y, 0);
		}
		else {
			this.transform.position = new Vector3 (this.transform.position.x + Random.Range (3, 5), this.transform.position.y, 0);

			//如果出了屏幕外面，就把他调回来
			Vector3 pos = Camera.main.WorldToViewportPoint (this.transform.position);
			bool isVisible = (Camera.main.orthographic || pos.z > 0f) && (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f);
			if (!isVisible) {
				this.transform.position = new Vector3 (this.transform.position.x - Random.Range (3, 5), this.transform.position.y, 0);
			}

			this.sr.flipX = false;
			fb.transform.localPosition = new Vector3 (fb.transform.localPosition.x - 0.388f, fb.transform.localPosition.y, 0);
		}
		StartCoroutine (ShowUp());
	}

	IEnumerator ShowUp()
	{
		yield return new WaitForSecondsRealtime (1.5f);
		this.animator.enabled = true;
		this.sr.enabled = true;
		this.cc2d.enabled = true;

	}

	public void firstShow()
	{
		this.animator.enabled = true;
		this.animator.SetBool ("shot", true);
		this.sr.enabled = true;
		this.cc2d.enabled = true;
	}
		
}
