using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scene1Script : MonoBehaviour {

	public Transform enmey1;   //stand_pos = new vec3(-0.753,-1.282,0) 跳下来的敌人
	public Transform enmey1_2; //从左边走出来的敌人
	public Transform Hero;


	private Animator enmey1_animator;   //stand_pos = new vec3(-0.753,-1.282,0)
	private Sequence enmey1_laihui;

	private bool isevent1_happen =false;


	// Use this for initialization
	void Start () {
		enmey1_animator = enmey1.GetComponent<Animator> ();
		//Enemy1_Showup ();


	}
	
	// Update is called once per frame
	void Update () {
		if (enmey1) {
			if (enmey1_animator.GetBool ("die")) {
				enmey1_laihui.Kill ();

			}
		}

		//当英雄走过某个地方时，触发改事件
		if (!isevent1_happen) {
			if (Hero.position.x >= -183.5f) {
				isevent1_happen = true;
				if (enmey1_2) {
					enmey1_2.SendMessage ("Run");
				}

			}
		}
		else {
			if (enmey1_2) {
				if (enmey1_2.GetComponent<Animator>().GetBool("run")) {
					enmey1_2.localPosition = new Vector3 (enmey1_2.localPosition.x + Time.deltaTime * 2.0f, enmey1_2.localPosition.y, 0);
				}
			}

		}
	}

	public void Enemy1_Showup()
	{
		if (GameObject.FindWithTag ("MainCamera").GetComponent<CameraController_mission1> ().whichscene == 1) {

			enmey1.GetComponent<Animator> ().enabled = true;
			enmey1.GetComponent<Animator> ().SetBool ("jumpattack", true);
			Sequence seq = DOTween.Sequence ();
			seq.Append (enmey1.DOLocalMove (new Vector3 (-0.753f, -1.282f, 0), 1.0f));
			seq.AppendCallback (delegate() {
				enmey1.GetComponent<Animator> ().SetBool ("slowmoving", true);
				enmey1_laihui = DOTween.Sequence ();
				enmey1_laihui.Append (enmey1.DOLocalMoveX (-0.853f, 0.5f));
				enmey1_laihui.Append (enmey1.DOLocalMoveX (-0.753f, 0.5f));
				enmey1_laihui.SetLoops (-1);
			});

			seq.SetLoops (1, LoopType.Yoyo);
		}
	}


}
