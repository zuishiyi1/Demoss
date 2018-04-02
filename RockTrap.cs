using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTrap : MonoBehaviour {

	public int fallTimes;

	private Rigidbody2D r2d;
	private Animator an;

	// Use this for initialization
	void Start () {
		fallTimes = 0;

		r2d = this.GetComponent<Rigidbody2D> ();
		an = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D c2d)
	{
		if (c2d.gameObject.layer == 14) {
			fallTimes++;
			if (fallTimes == 1) {
				an.enabled = true;
			}
			if (fallTimes == 2) {
				an.enabled = true;
			}
			if (fallTimes == 3) {
				an.enabled = true;
			}
			if (fallTimes == 4) {
				an.enabled = true;
			}
			if (fallTimes == 5) {
				an.enabled = true;
			}
			if (fallTimes >= 6) {
				//开始掉落
				an.enabled = true;
				an.SetBool("fall",true);
				r2d.constraints = RigidbodyConstraints2D.FreezeRotation;
			}
		}
	}

	void OnCollisionEnter2D (Collision2D c2d)
	{
		if (c2d.gameObject.tag == "ground") {
			this.gameObject.layer = 1;
		}
	}

	void StopAnimator()
	{
		an.enabled = false;
	}

	void PlayAnimator()
	{
		an.enabled = true;
	}

	void Test()
	{
		an.enabled = true;
		an.SetBool("fall",true);
		r2d.constraints = RigidbodyConstraints2D.FreezeRotation;
	}
}
