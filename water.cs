using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour {

	public GameObject effect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col2d)
	{
		if (col2d.name == "Hero") {
			effect.transform.position = new Vector3 (col2d.transform.position.x, this.transform.position.y, 0);
			effect.GetComponent<SpriteRenderer> ().enabled = true;
			effect.GetComponent<Animator> ().enabled = true;
		}
	}
}
