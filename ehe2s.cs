using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ehe2s : MonoBehaviour {

	private GameObject l;
	private GameObject m;
	private GameObject r;

	[HideInInspector]public bool shoot;
	[HideInInspector]public int shoottimes;


	private GameObject eh;

	// Use this for initialization
	void Start () {
		shoottimes = 0;

		foreach (Transform child in this.transform) {
			if (child.gameObject.name == "l") {
				l = child.gameObject;
			}
			if (child.gameObject.name == "m") {
				m = child.gameObject;
			}
			if (child.gameObject.name == "r") {
				r = child.gameObject;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (shoot == true) {
			
			shoot = false;
		}
	}

	public void ShootBullet(GameObject eh)
	{


		l.GetComponent<ehe> ().direction = "middle";
		l.GetComponent<ehe> ().Shoot (this.gameObject);
		//l.GetComponent<SpriteRenderer> ().enabled = true;
		//l.GetComponent<Animator> ().enabled = true;
		m.GetComponent<ehe> ().direction = "middle";
		m.GetComponent<ehe> ().Shoot (this.gameObject);
		//m.GetComponent<SpriteRenderer> ().enabled = true;
		//m.GetComponent<Animator> ().enabled = true;
		r.GetComponent<ehe> ().direction = "middle";
		r.GetComponent<ehe> ().Shoot (this.gameObject);
		//r.GetComponent<SpriteRenderer> ().enabled = true;
		//r.GetComponent<Animator> ().enabled = true;

		eh.GetComponent<Animator> ().enabled = false;

	}
}
