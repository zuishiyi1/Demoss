using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {

	public float speed = 0;

	public float stopPosX;

	public Animator animator;

	public bool carstop = false;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();

		this.GetComponent<AudioSource> ().enabled = true;

	}
	
	// Update is called once per frame
	void Update () {
		
		if (carstop) {
			//播放二段动画
			animator.SetBool ("stop", true);
			this.GetComponent<AudioSource> ().enabled = false;

		} else {
			//游戏开始
			transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
			animator.SetBool ("stop", false);
		}



	}

	void Release2()
	{
		this.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		animator.Play ("car_release2");

		this.transform.Find("car_shelter").gameObject.SetActive(true);
		this.transform.Find("car_shelter2").gameObject.SetActive(true);

		//this.enabled = true;
	}
	//车辆放下后架后，boss出来指挥放下主角
	void BossComeOut()
	{
		GameObject.Find ("NPC1").GetComponent<Npc1Controller> ().enabled = true;
			//this.enabled = true;
	}
}
