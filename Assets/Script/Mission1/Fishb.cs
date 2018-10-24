using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishb : MonoBehaviour {


	//0为未发射，1为左，2为右
	[HideInInspector]public int shoot;

	private Animator animator;
	private string currentstate;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();


	}
	
	// Update is called once per frame
	void Update () {


		if (this.tag == "clone") {
			if (currentstate != "die") {
				if (shoot == 1) {
					this.transform.Translate (Vector3.left * Time.deltaTime * 1.0f);
				}
				if (shoot == 2) {
					this.transform.Translate (Vector3.right * Time.deltaTime * 1.0f);
				}
			}
			Vector3 pos = Camera.main.WorldToViewportPoint (this.transform.position);
			bool isVisible = (Camera.main.orthographic || pos.z > 0f) && (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f);
			if (!isVisible) {
				Destroy (this.gameObject);
			}
		}

		if ((shoot!=0) && (this.tag!="clone")) {
			GameObject fishb = (GameObject)Instantiate (this.gameObject);
			fishb.transform.position = this.gameObject.transform.position;
			fishb.tag = "clone";
			fishb.AddComponent<Rigidbody2D> ();
			fishb.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionY;
			fishb.GetComponent<Fishb> ().shoot = this.shoot;
			fishb.GetComponent<CircleCollider2D> ().isTrigger = false;
			fishb.GetComponent<SpriteRenderer> ().enabled = true;
			fishb.GetComponent<Animator> ().enabled = true;
			shoot = 0;
		}
	}

	void OnTriggerEnter2D(Collider2D c2d)
	{
		if (this.tag == "clone") {
			if (c2d.name.ToString ().IndexOf ("bullet") != -1) {
				animator.SetBool ("die", true);
				currentstate = "die";
				this.GetComponent<CircleCollider2D> ().enabled = false;
				this.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;

			}
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
		}

	}

	void DestroyThis()
	{
		Destroy (this.gameObject);
	}
		
}
