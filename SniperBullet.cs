using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : MonoBehaviour {

	private SpriteRenderer _sr;
	private GameObject hero;
	private SniperBullet _sb;

	private bool isshoot = false;
	[HideInInspector]public Vector3 heropos = Vector3.zero;


	[HideInInspector]public bool shoot = false;

	// Use this for initialization
	void Start () {
		_sr = this.GetComponent<SpriteRenderer> ();
		hero = GameObject.Find ("Hero");
		_sb = this.GetComponent<SniperBullet> ();
		shoot = false;
	}

	// Update is called once per frame
	void Update () {
		
		if (this.tag == "clone") {
			this.transform.Translate (heropos * Time.deltaTime * 2.0f);

			Vector3 pos = Camera.main.WorldToViewportPoint (this.transform.position);
			bool isVisible = (Camera.main.orthographic || pos.z > 0f) && (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f);
			if (!isVisible) {
				Destroy (this.gameObject);
			}
		}
		if (shoot) {
			heropos = hero.transform.position - this.transform.position; 
			heropos = heropos.normalized; 
			GameObject bullet = (GameObject)Instantiate (this.gameObject);
			bullet.transform.position = this.gameObject.transform.position;
			bullet.tag = "clone";
			bullet.GetComponent<SpriteRenderer> ().enabled = true;
			bullet.GetComponent<SniperBullet> ().heropos = this.heropos;
			shoot = false;
		}
			

	}

	void OnTriggerEnter2D(Collider2D c2d)
	{
		if (c2d.gameObject.name == "Hero") {
			SendDieByWepon ();
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
