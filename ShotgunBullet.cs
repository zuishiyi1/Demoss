using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour {


	private Animator _animator; 

	[HideInInspector]
	public bool boom;

	// Use this for initialization
	void Start () {
		_animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D c2d)
	{

		//子弹开始分裂
//		_animator.SetBool ("boom", true);
//		boom = true;
	}

	void DestoryObj()
	{
		if (this.name == "bullet") {
		} else {
			Destroy (this.gameObject);
		}
	}

	void DisableCollider()
	{
		this.GetComponent<BoxCollider2D> ().enabled = false;
	}


}
