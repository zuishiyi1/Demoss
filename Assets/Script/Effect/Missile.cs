using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

	public GameObject aim_point;

	private SpriteRenderer _sr;
	private bool shouldDestory = false;


	public GameObject effect_boom;
	public GameObject bomb;


	private SpriteRenderer boom_sr;
	private Animator boom_animator;
	private AudioSource boom_audios;

	private Scene2Script scene2;
	private HeroController Hero;
	// Use this for initialization
	void Start () {
		_sr = this.GetComponent<SpriteRenderer> ();

		boom_sr = effect_boom.GetComponent<SpriteRenderer>();
		boom_animator = effect_boom.GetComponent<Animator> ();
		boom_audios = effect_boom.GetComponent<AudioSource> ();

		scene2 = GameObject.Find ("Scene2").GetComponent<Scene2Script>();
		Hero = GameObject.Find ("Hero").GetComponent<HeroController>();


	}
	
	// Update is called once per frame
	void Update () {
		if ((Vector3.Distance (new Vector3 (0, this.transform.position.y, 0), new Vector3 (0, aim_point.transform.position.y, 0)) <= 0.5f) && bomb.activeInHierarchy) {
			//当炸弹到地面并引发爆炸时
			_sr.enabled = false;
			effect_boom.transform.position = this.transform.position;
			boom_sr.enabled = true;
			boom_sr.sortingLayerName = "Foreground";
			boom_animator.enabled = true;
			boom_audios.Play ();
			//Destroy (this.gameObject);
			//scene2.laststepisover = true;
			switch (this.tag) {
			case "step1":
				scene2.step1over = true;
				break;
			case "step2":
				scene2.step2over = true;
				break;
			case "step3":
				scene2.step3over = true;
				break;
			case "step4_1":
				scene2.step4_1over = true;
				break;
			case "step4_2":
				scene2.step4_2over = true;
				break;
			case "step5":
				scene2.step5over = true;
				break;

			}
			if(Vector3.Distance (new Vector3 (Hero.transform.position.x, 0, 0), new Vector3 (bomb.transform.position.x, 0, 0)) < 0.4f)
			{
				Hero.SendMessage ("DieBySoward");
			}

			this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
			bomb.SetActive(false);

		}	
	}
}
