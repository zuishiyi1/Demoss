using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class picked : MonoBehaviour {

	public GameObject Hero;

	void DestroyThis()
	{
		this.GetComponent<SpriteRenderer> ().enabled = false;
		this.GetComponent<Animator> ().enabled = false;

		Hero.SendMessage ("DisableSmallGunAction");
	}


}
