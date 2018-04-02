using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ehe : MonoBehaviour {

	private GameObject ehb;
	private SpriteRenderer sr;
	private Animator animator;

	[HideInInspector]public int shoottimes;

	[HideInInspector]public string direction;

	private GameObject eh;

	// Use this for initialization
	void Start () {
		shoottimes = 0;

		this.sr = this.GetComponent<SpriteRenderer> ();
		this.animator = this.GetComponent<Animator> ();

		foreach (Transform child in this.transform) {
			if (child.gameObject.name == "ehb") {
				ehb = child.gameObject;
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void Shoot(GameObject eh)
	{
		if (ehb) {
			if (shoottimes == 3) {
				this.sr.enabled = false;
				this.animator.enabled = false;
				shoottimes = 0;
				if (eh != null) {
					if (eh.name == "ehe2") {
						eh.GetComponent<ehe2s> ().shoottimes = 3;
					}
				}
			} 
			else {
				//第一次的时候才开启
				if (shoottimes == 0) {
					this.sr.enabled = true;
					this.animator.enabled = true;
				}

			
				if (direction=="right") {
					ehb.GetComponent<ehb> ().shoot = 2;
					shoottimes++;
				} 
				else if(direction=="left"){
					ehb.GetComponent<ehb> ().shoot = 1;
					shoottimes++;
					}
				else if(direction=="middle"){
					ehb.GetComponent<ehb> ().shoot = 3;
					shoottimes++;
				}
				}
		}
	}
}
