using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiefriendsGrenade : MonoBehaviour {

	//0为未发射，1为左，2为右
	public int shoot;

	private Animator animator;
	private string currentstate;

	private Vector3 left;
	private Vector3 right;
	private Vector3 leftdown;
	private Vector3 rightdown;

	private bool down;

	private Vector3 orginPos;

	// Use this for initialization
	void Start () {
		left = new Vector3 (-1, -0.3f, 0);
		right = new Vector3 (-0.3f, 1.3f, 0);
		leftdown = new Vector3 (0.1f, -0.5f, 0);
		rightdown = new Vector3 (0.8f, 0.3f, 0);

		down = false;

		animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (this.tag == "clone") {
			if (currentstate != "die") {
				if (shoot == 1) {
					if (!down) {
						//如果距离到了 则开始下降
						if (orginPos.y - this.transform.position.y < -0.507f) {
							down = true;
						} else {
						
						}
					}

					if (down) {
						this.transform.Translate (leftdown * Time.deltaTime * 3.0f);

					}
					else {
						this.transform.Translate (left * Time.deltaTime * 1.5f);
						//Debug.Log (orginPos.y - this.transform.position.y);
					}
					this.animator.enabled = true;
				}
				if (shoot == 2) {
					if (!down) {
						//如果距离到了 则开始下降
						if (orginPos.y - this.transform.position.y < -0.507f) {
							down = true;
						} else {

						}
					}

					if (down) {
						this.transform.Translate (rightdown * Time.deltaTime * 2.0f);
					}
					else {
						this.transform.Translate (right * Time.deltaTime * 1.0f);
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
			ehb.GetComponent<ZombiefriendsGrenade> ().shoot = this.shoot;
			ehb.GetComponent<ZombiefriendsGrenade> ().orginPos = this.transform.transform.position;
			ehb.GetComponent<BoxCollider2D> ().isTrigger = true;
			ehb.GetComponent<SpriteRenderer> ().enabled = true;
			ehb.GetComponent<Animator> ().enabled = true;
			this.shoot = 0;
		}
	}

	void OnTriggerEnter2D(Collider2D c2d)
	{
		if (c2d.name == "Hero") {
			this.currentstate = "die";
			this.animator.SetBool ("bomb", true);
			c2d.gameObject.GetComponent<HeroController> ().SendMessage ("DieByWepon");
		}
	}

	void DestroyThis()
	{
		Destroy (this.gameObject);
	}
}
