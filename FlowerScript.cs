using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour {

	public GameObject Hero;

	private float distanceofSeeHero;

	private Animator _animator;

	private AudioSource audios;


	// Use this for initialization
	void Start () {
		distanceofSeeHero = 1.5f;
		_animator = this.GetComponent<Animator> ();
		audios = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		double dis = this.transform.position.x - Hero.transform.position.x;
		if ((dis < distanceofSeeHero) && (dis > 0)) {
			_animator.enabled = true;
		}
	}

	void PlayVoice()
	{
		audios.Play ();
	}
}
