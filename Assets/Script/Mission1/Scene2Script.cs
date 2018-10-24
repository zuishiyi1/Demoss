

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Script : MonoBehaviour {

	public Transform enemy1;
	public Transform enemy2;  //当1冲出去的时候2和3同时冲出去
	public Transform enemy3;
	public Transform Hero;
	public CameraController_mission1 cm1;

	private Animator enemy1_animator;
	private Animator Hero_animator;
	private bool isevent1_happen = false;
	private bool happenonce = false;

	private Transform HeroMain;

	private AudioSource audios;

	private Transform[] aim_points = new Transform[10];
	private bool step1 = false;
	[HideInInspector]public bool step1over = false;
	private bool step2 = false;
	[HideInInspector]public bool step2over = false;
	private bool step3 = false;
	[HideInInspector]public bool step3over = false;
	private bool step4_1 = false;
	[HideInInspector]public bool step4_1over = false;
	private bool step4_2 = false;
	[HideInInspector]public bool step4_2over = false;
	private bool step5 = false;
	[HideInInspector]public bool step5over = false;

	//[HideInInspector] public bool laststepisover = false;
	// Use this for initialization
	void Start () {
		audios = this.GetComponent<AudioSource> ();
		enemy1_animator = enemy1.GetComponent<Animator> ();
		Hero_animator = Hero.GetComponent<Animator> ();

		HeroMain = Hero.parent;

		int i = 0;
		foreach (Transform tf in this.transform) {
			if (tf.name.IndexOf ("point") != -1) {
				aim_points [i] = tf;
				i++;
				//Debug.Log (tf.name);
			}
		}
			

	}
	
	// Update is called once per frame
	void Update () {
		if (enemy1) {
			//if ((enemy1_animator.GetBool ("run") == false) && (Hero_animator.GetBool ("diebysoward") == false)) {
				if (Vector3.Distance (new Vector3 (Hero.position.x, 0, 0), new Vector3 (enemy1.position.x, 0, 0)) < 4.5f) {
					if (!happenonce) {
						isevent1_happen = true;
					}
				}
			//}

//			if (enemy1_animator.GetBool ("run")) {
//				if(enemy1_animator.GetBool("die") == false)
//				enemy1.localPosition = new Vector3 (enemy1.localPosition.x - Time.deltaTime * 2.0f, enemy1.localPosition.y, 0);
//			}
		}

		if (isevent1_happen) {
			SendEnemy2and3Run ();
		}

		if ((Vector3.Distance (new Vector3 (HeroMain.position.x, 0, 0), new Vector3 (aim_points [4].position.x, 0, 0)) < 0.4f) && (step1 == false)) {


			aim_points[2].GetComponent<aim_point>().SendMessage("StartShow","step1");
			aim_points[3].GetComponent<aim_point>().SendMessage("StartShow","step1");
			aim_points[4].GetComponent<aim_point>().SendMessage("StartShow","step1");
			aim_points[5].GetComponent<aim_point>().SendMessage("StartShow","step1");
			aim_points[6].GetComponent<aim_point>().SendMessage("StartShow","step1");
			aim_points[7].GetComponent<aim_point>().SendMessage("StartShow","step1");
			step1 = true;
			audios.Play ();
		}

		if ((Vector3.Distance (new Vector3 (HeroMain.position.x, 0, 0), new Vector3 (aim_points [1].position.x, 0, 0)) < 0.4f) && (step1over == true) && (step2 == false)) {

			//if (laststepisover) {
			aim_points [0].GetComponent<aim_point> ().SendMessage ("StartShow","step2");
			aim_points [1].GetComponent<aim_point> ().SendMessage ("StartShow","step2");
			aim_points [2].GetComponent<aim_point> ().SendMessage ("StartShow","step2");
			aim_points [3].GetComponent<aim_point> ().SendMessage ("StartShow","step2");
				step2 = true;
			audios.Play ();
				//laststepisover = false;
			//}
				

		}

		if ((Vector3.Distance (new Vector3 (HeroMain.position.x, 0, 0), new Vector3 (aim_points [4].position.x, 0, 0)) < 0.4f) && (step1over == true) && (step2over == true)&& (step3 == false)) {

			//if (laststepisover) {
			aim_points [1].GetComponent<aim_point> ().SendMessage ("StartShow","step3");
			aim_points [2].GetComponent<aim_point> ().SendMessage ("StartShow","step3");
			aim_points [4].GetComponent<aim_point> ().SendMessage ("StartShow","step3");
			aim_points [5].GetComponent<aim_point> ().SendMessage ("StartShow","step3");
			aim_points [6].GetComponent<aim_point> ().SendMessage ("StartShow","step3");
				step3 = true;
			audios.Play ();
				//laststepisover = false;
			//}

		}

		if ((Vector3.Distance (new Vector3 (HeroMain.position.x, 0, 0), new Vector3 (aim_points [7].position.x, 0, 0)) < 0.4f) && (step1over == true) && (step2over == true) && (step3over == true) && (step4_1over == false) && (step4_2 == false)) {

			//if (laststepisover) {
			aim_points [1].GetComponent<aim_point> ().SendMessage ("StartShow","step4_1");
			aim_points [2].GetComponent<aim_point> ().SendMessage ("StartShow","step4_1");
			aim_points [4].GetComponent<aim_point> ().SendMessage ("StartShow","step4_1");
			aim_points [5].GetComponent<aim_point> ().SendMessage ("StartShow","step4_1");
			aim_points [6].GetComponent<aim_point> ().SendMessage ("StartShow","step4_1");
			aim_points [7].GetComponent<aim_point> ().SendMessage ("StartShow","step4_1");
			aim_points [8].GetComponent<aim_point> ().SendMessage ("StartShow","step4_1");
			aim_points [9].GetComponent<aim_point> ().SendMessage ("StartShow","step4_1");
				step4_1 = true;
			audios.Play ();
				//laststepisover = false;
			//}
		}

		if ((Vector3.Distance (new Vector3 (HeroMain.position.x, 0, 0), new Vector3 (aim_points [3].position.x, 0, 0)) < 0.4f) && (step1over == true) && (step2over == true) && (step3over == true) && (step4_2 == false)) {

			//if (laststepisover) {
			aim_points [0].GetComponent<aim_point> ().SendMessage ("StartShow","step4_2");
			aim_points [1].GetComponent<aim_point> ().SendMessage ("StartShow","step4_2");
			aim_points [2].GetComponent<aim_point> ().SendMessage ("StartShow","step4_2");
			aim_points [3].GetComponent<aim_point> ().SendMessage ("StartShow","step4_2");
			aim_points [4].GetComponent<aim_point> ().SendMessage ("StartShow","step4_2");
			aim_points [6].GetComponent<aim_point> ().SendMessage ("StartShow","step4_2");
				step4_2 = true;
			audios.Play ();
				//laststepisover = false;
			//}
		}

		if ((Vector3.Distance (new Vector3 (HeroMain.position.x, 0, 0), new Vector3 (aim_points [5].position.x, 0, 0)) < 0.4f) && (step1over == true) && (step2over == true) && (step3over == true) && (step4_1over == false) && (step4_2over == true) && (step5 == false)) {

			//if (laststepisover) {
			aim_points [0].GetComponent<aim_point> ().SendMessage ("StartShow","step5");
			aim_points [1].GetComponent<aim_point> ().SendMessage ("StartShow","step5");
			aim_points [2].GetComponent<aim_point> ().SendMessage ("StartShow","step5");
			aim_points [3].GetComponent<aim_point> ().SendMessage ("StartShow","step5");
			aim_points [4].GetComponent<aim_point> ().SendMessage ("StartShow","step5");
			aim_points [5].GetComponent<aim_point> ().SendMessage ("StartShow","step5");
			aim_points [6].GetComponent<aim_point> ().SendMessage ("StartShow","step5");
			aim_points [7].GetComponent<aim_point> ().SendMessage ("StartShow","step5");
			step5 = true;
			audios.Play ();
				//laststepisover = false;
			//}
		}
	}


	void SendEnemy2and3Run()
	{
		happenonce = true;
		if (enemy1) {
			enemy1.SendMessage ("Run");
		}
		if (enemy2) {
			//进入冲刺状态
			enemy2.SendMessage ("Run");
			//enemy2.transform.localPosition = new Vector3 (enemy2.localPosition.x - Time.deltaTime * 2.0f, enemy2.localPosition.y, 0);
		}
		if (enemy3) {
			enemy3.SendMessage ("Run");
			//enemy3.transform.localPosition = new Vector3 (enemy3.localPosition.x - Time.deltaTime * 2.0f, enemy3.localPosition.y, 0);
		}
		//执行一次之后可以不再执行了
		isevent1_happen = false;
	}
}
