using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour {


	private int shootCount;

	private Animator _animator;

	public GameObject door2;

	// Use this for initialization
	void Start () {
		_animator = this.GetComponent<Animator> ();

		shootCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D c2d)
	{
		if (c2d.name.ToString ().IndexOf ("bullet") != -1) {
			shootCount++;
			if (shootCount == 7) {
				_animator.SetBool ("step1", true);
			}

			if (shootCount == 14) {
				_animator.SetBool ("step2", true);
			}

			if (shootCount == 21) {
				_animator.SetBool ("step3", true);
			}

			if (shootCount == 28) {
				Destroy (this.gameObject);
				door2.GetComponent<SpriteRenderer> ().sortingOrder = 5;
				door2.GetComponent<AudioSource> ().Play ();
			}
		}
	}
}
