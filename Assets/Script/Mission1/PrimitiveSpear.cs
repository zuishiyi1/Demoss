using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitiveSpear : MonoBehaviour {


	private SpriteRenderer _sr;
	[HideInInspector]public Animator _animator;
	private GameObject hero;

	private Vector3 firstpos;
	private Vector3 highestpos;
	private Vector3 lowestpos;


	[HideInInspector]public bool step1;
	[HideInInspector]public bool step2;



	[HideInInspector]public bool shoot = false;

	[HideInInspector]public int direction;


	// Use this for initialization
	void Start () {
		_sr = this.GetComponent<SpriteRenderer> ();
		_animator = this.GetComponent<Animator> ();

		firstpos = this.transform.position;
		highestpos = new Vector3 (-0.607f, 0.203f, 0);
		lowestpos = new Vector3 (-1.009f, -0.133f, 0);

		highestpos = highestpos - firstpos;
		highestpos = highestpos.normalized;

		this.shoot = false;

		hero = GameObject.Find ("Hero");



	}
	
	// Update is called once per frame
	void Update () {
		//计算原始矛与运动矛的距离，大于多少则进入二阶段（开始下降）
		if (this.tag == "clone") {
			this._sr.enabled = true;
			if (direction == 0) {
				this._sr.flipX = false;
				if (!this.step2) {
					this.transform.Translate (new Vector3 (-1.5f, 0.5f, 0) * Time.deltaTime * 1.2f);
				} else {
					this.transform.Translate (new Vector3 (-1.5f, -1.7f, 0) * Time.deltaTime * 1.0f);
				}
				//Debug.Log (this.transform.position.x - firstpos.x);

				if ((this.transform.position.x - firstpos.x) < -1.2315f) {
					_animator.SetBool ("step2", true);
					this.step2 = true;
				}
			}
			else if (direction == 1) {
				this._sr.flipX = true;
				if (!this.step2) {
					this.transform.Translate (new Vector3 (1.5f, 0.5f, 0) * Time.deltaTime * 1.2f);
				} else {
					this.transform.Translate (new Vector3 (1.5f, -1.7f, 0) * Time.deltaTime * 1.0f);
				}
				//Debug.Log (this.transform.position.x - firstpos.x);

				if ((this.transform.position.x - firstpos.x) > 1.2315f) {
					_animator.SetBool ("step2", true);
					this.step2 = true;
				}
			}
			

			Vector3 pos = Camera.main.WorldToViewportPoint (this.transform.position);
			bool isVisible = (Camera.main.orthographic || pos.z > 0f) && (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f);
			if (!isVisible) {
				Destroy (this.gameObject);
			}
		}

		if (shoot) {
			GameObject spear = (GameObject)Instantiate (this.gameObject);
			spear.transform.position = this.gameObject.transform.position;
			spear.tag = "clone";
			spear.GetComponent<PrimitiveSpear> ().enabled = true;
			spear.GetComponent<PrimitiveSpear> ().step1 = true;
			spear.GetComponent<PrimitiveSpear> ().direction = this.direction;
			spear.GetComponent<PrimitiveSpear> ()._animator.SetBool ("step1", true);
			shoot = false;
		}

		if (step1) {
			
		}

		//Time.timeScale = 0.1f;
	}


	void OnTriggerEnter2D(Collider2D c2d)
	{
		if (c2d.gameObject.name == "Hero") {
			SendDieByWepon ();
			Destroy (this.gameObject);
		}
		else if(step2){
			Destroy (this.gameObject);
		}
	}

	void SendDieByWepon()
	{
		if (hero) {
			hero.GetComponent<HeroController> ().SendMessage ("DieByWepon");
		}
	}
}
