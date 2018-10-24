using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soilder2 : MonoBehaviour {

	private bool move;

	private float shock_pos_x;
	public GameObject fishhead;
	private Animator animator;

	// Use this for initialization
	void Start () {
		move = false;
		this.animator = this.GetComponent<Animator> ();

		shock_pos_x = 3.8f;
	}
	
	// Update is called once per frame
	void Update () {
		if (move) {
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x + Time.deltaTime * 1.0f, this.transform.localPosition.y, 0);
		}

		if (this.transform.localPosition.x >= shock_pos_x) {
			//鱼跳出来
			fishhead.SetActive(true);
		}

		if (fishhead.transform.localPosition.y >= this.transform.localPosition.y) {
			this.animator.SetBool ("die", true);
			move = false;
		}
	}

	void Move()
	{
		move = true;
	}

	void DestoryThis()
	{
		Destroy (this.gameObject);
	}
}
