using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ehb : MonoBehaviour {

	//0为未发射，1为左，2为右,3为下
	[HideInInspector]public int shoot;

	private Animator animator;
	private string currentstate;

	private Vector3 left;
	private Vector3 right;
	private Vector3 down;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();

		left = new Vector3 (-1, -1, 0);
		right = new Vector3 (1, 1, 0);
		down = new Vector3 (0, -1, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (this.tag == "clone") {
			if (currentstate != "die") {
				if (shoot == 1) {
					this.transform.Translate (left * Time.deltaTime * 1.0f);
				}
				if (shoot == 2) {
					this.transform.Translate (right * Time.deltaTime * 1.0f);
				}
				if (shoot == 3) {
					this.transform.Translate (down * Time.deltaTime * 1.0f);
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
			ehb.tag = "clone";
			ehb.GetComponent<ehb> ().shoot = this.shoot;
			ehb.GetComponent<CircleCollider2D> ().isTrigger = false;
			ehb.GetComponent<SpriteRenderer> ().enabled = true;
			ehb.GetComponent<Animator> ().enabled = true;
			shoot = 0;
		}
	}

	void OnCollisionEnter2D(Collision2D c2d)
	{
		//如果碰到的物体是主角的话
		if (c2d.gameObject.name == "Hero") {
			c2d.gameObject.GetComponent<HeroController> ().SendMessage ("DieByWepon");
			animator.SetBool ("die", true);
			currentstate = "die";
			this.GetComponent<CircleCollider2D> ().enabled = false;
			this.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
			Destroy (this.gameObject);
		}

	}
}
