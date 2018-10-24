//if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
//实际运行时，getbutton请改成inputtouch，不然不能执行
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

//加载txt
using System.IO;
using System.Text;

///

//list
using System.Collections.Generic;

//任务1的场景1：左x=-184.38，右x=-178.71
//当主角的x >= -175，则跳到下个场景


//目前来看，第一关的节奏比较紧凑刺激，第二关应当放慢

public class CameraController_mission1 : MonoBehaviour
{

	public GameObject targetGameobject;
	private HeroController target_script;

	public GameObject block1;
	public GameObject block2;
	public GameObject block3;
	public GameObject block4;
	public GameObject block5;
	public GameObject block6;

	public static int scenenum = 1;




	//-3.75924   主角当前y轴


	public GameObject tishiobj;

	private float camera_y;

	[HideInInspector]
	public bool showtishi;

	[HideInInspector]
	public string showtishitext;

	[HideInInspector]
	public int whichscene;


	protected bool isziluolanDestory = true;

	public GameObject soilder2;

	private bool lockCamera;


	// Use this for initialization
	void Start ()
	{
		Screen.SetResolution (1024, 600, true);
		switch (scenenum) {
		case 1:
			break;
		case 2:
			targetGameobject.transform.position = new Vector3 (-174.0f, 0.71f, 0);
			break;
		case 3:
			targetGameobject.transform.position = new Vector3 (-160.3f,-0.005751047f, 0);
			this.transform.position = new Vector3 (-157.16f, 0.98f, -10);
			break;
		case 4:
			targetGameobject.transform.position = new Vector3 (-135.1258f,-0.2255612f, 0);
			this.transform.position = new Vector3 (-132.09f, 0.98f, -10);
			break;
		case 5:
			targetGameobject.transform.position = new Vector3 (-126.4367f,-4.628854f, 0);
			this.transform.position = new Vector3 (-123.26f, -4.12f, -10);
			break;
		}

		if (SceneManager.GetActiveScene ().name == "Mission1BossFight") {
			whichscene = 99;
			HeroIn ();
		}
		else {
			whichscene = 1;
		}
		showtishi = false;
		showtishitext = "";
		if (targetGameobject.transform.position.x < -190.0f) {
			HeroIn ();
		}
			

		target_script = targetGameobject.GetComponent<HeroController> ();

		lockCamera = false;

	}

	
	// Update is called once per frame
	void Update ()
	{
		if (showtishi && (showtishitext != "")) {
			tishiobj.SetActive (true);
			updatetishitext (showtishitext, 0.5f);
		}

		if (targetGameobject.transform.position.x >= -92.0f && (SceneManager.GetActiveScene ().name == "Mission1")) {
			scenenum = 1;
			SceneManager.LoadScene ("Mission1BossFight");
		}
			
		if (whichscene != 99) {
			if (targetGameobject.transform.position.x < -175.0f) {
			
				if ((this.transform.position.x >= -184.38f) && (this.transform.position.x <= -178.71f)) {

					if (targetGameobject.transform.position.x <= -184.38f) {
						this.transform.position = new Vector3 (-184.38f, -3.244019f, -10);
					} else if (targetGameobject.transform.position.x >= -178.71f) {
						this.transform.position = new Vector3 (-178.71f, targetGameobject.transform.position.y + 0.36f, -10);
					} else {
						this.transform.position = new Vector3 (targetGameobject.transform.position.x, targetGameobject.transform.position.y + 0.36f, -10);
					}
				}

			} 
		//大于等于-175的时候就进入了场景2
		else if ((targetGameobject.transform.position.x > -174.0f) && (targetGameobject.transform.position.x <= -161.5f)) {
				if (whichscene != 2) {
					this.transform.position = new Vector3 (-170.8f, 0.98f, -10);
					whichscene = 2;
					block2.GetComponent<EdgeCollider2D> ().isTrigger = false;
				}
				if ((this.transform.position.x >= -170.8f) && (this.transform.position.x <= -165.21f)) {

					if (targetGameobject.transform.position.x <= -170.8f) {
						this.transform.position = new Vector3 (-170.8f, 0.98f, -10);
					} else if (targetGameobject.transform.position.x >= -165.21f) {
						this.transform.position = new Vector3 (-165.21f, 0.98f, -10);
					} else {
						this.transform.position = new Vector3 (targetGameobject.transform.position.x, 0.98f, -10);
					}
				}
			}
		//场景3
		else if ((targetGameobject.transform.position.x > -160.3f) && (targetGameobject.transform.position.x <= -135.534f)) {
				if (whichscene != 3) {
					this.transform.position = new Vector3 (-157.16f, 0.98f, -10);
					//targetGameobject.transform.position.x = 160.3f;
					whichscene = 3;
					block3.GetComponent<EdgeCollider2D> ().isTrigger = false;

				}
				if ((this.transform.position.x >= -157.16f) && (this.transform.position.x <= -139.22f)) {

					if (targetGameobject.transform.position.x <= -157.16f) {
						this.transform.position = new Vector3 (-157.16f, 0.98f, -10);
					} else if (targetGameobject.transform.position.x >= -139.22f) {
						this.transform.position = new Vector3 (-139.22f, 0.98f, -10);
					} else {
						this.transform.position = new Vector3 (targetGameobject.transform.position.x, 0.98f, -10);
					}
				}
			}

			//场景4
		else if (targetGameobject.transform.position.x > -135.534f && (targetGameobject.transform.position.x <= -127.36f)) {
				if (whichscene != 4) {
					this.transform.position = new Vector3 (-132.09f, 0.98f, -10);
					//targetGameobject.transform.position.x = 160.3f;
					whichscene = 4;
					target_script.scene = 4;

				}
				if ((this.transform.position.x >= -132.09f) && (this.transform.position.x <= -130.6f)) {

					if (targetGameobject.transform.position.x <= -132.09f) {
						this.transform.position = new Vector3 (-132.09f, targetGameobject.transform.position.y + 0.36f, -10);
					} else if (targetGameobject.transform.position.x >= -127.349f) {
						this.transform.position = new Vector3 (-127.349f, targetGameobject.transform.position.y + 0.36f, -10);
					} else {
						this.transform.position = new Vector3 (targetGameobject.transform.position.x, targetGameobject.transform.position.y + 0.36f, -10);
					}
				}
			}

		//场景5
			else if (targetGameobject.transform.position.x > -126.4367f) {
				if (whichscene != 5) {
					this.transform.position = new Vector3 (-123.26f, -4.12f, -10);
					//targetGameobject.transform.position.x = 160.3f;
					whichscene = 5;
					target_script.scene = 5;
					block4.GetComponent<EdgeCollider2D> ().isTrigger = false;


				}
				if ((this.transform.position.x >= -123.26f) && (this.transform.position.x <= -95.89f)) {

					//测试时不启用
//					if (targetGameobject.transform.position.x >= -105.8856f) {
//						if (soilder2) {
//							//if (soilder2.GetComponent<SpriteRenderer> ().enabled)
//							block5.GetComponent<EdgeCollider2D> ().isTrigger = false;
//							block6.GetComponent<EdgeCollider2D> ().isTrigger = false;
//							lockCamera=true; 
//							return;
//							//}
//						} else {
//						lockCamera=false;
//							block5.GetComponent<EdgeCollider2D> ().isTrigger = true;
//							block6.GetComponent<EdgeCollider2D> ().isTrigger = true;
//						}
//
//					}
					///////////////////
					
					if (lockCamera) {
						return;
					}
					if (targetGameobject.transform.position.x <= -123.26f) {
						this.transform.position = new Vector3 (-123.26f, -4.12f, -10);
					} else if (targetGameobject.transform.position.x >= -92.706f) {
						this.transform.position = new Vector3 (-95.89f, -4.12f, -10);
					} else {
						this.transform.position = new Vector3 (targetGameobject.transform.position.x, -4.12f, -10);
					}
				}
			}
		}
		else {
			if (targetGameobject.transform.position.x <= -25.35761f) {
				this.transform.position = new Vector3 (-25.34f, -8.37f, -10);
			} 
			else if (targetGameobject.transform.position.x >= -14.12489f) {
				this.transform.position = new Vector3 (-14.12489f, -8.37f, -10);
			} 
			else {
				this.transform.position = new Vector3 (targetGameobject.transform.position.x, -8.37f, -10);
			}
			//if()
			//this.transform.position = new Vector3 (-25.34f, -8.37f, -10);
		}

	}

	void OnGUI ()
	{
		//GUILayout.Label ("asdasdasd");
	}

	public void updatetishitext (string showtext, float speed)
	{
		tishiobj.GetComponentInChildren<Text> ().text = showtext;
		tishiobj.GetComponent<CanvasGroup> ().alpha += (Time.deltaTime * speed);
		if (tishiobj.GetComponent<CanvasGroup> ().alpha >= 1.0f) {
			tishiobj.GetComponent<CanvasGroup> ().alpha = 0;
			showtishitext = "";
			showtishi = false;
			tishiobj.SetActive (false);
		}
	}


	void HeroIn ()
	{
		GameObject.Find ("Canvas").GetComponent<Canvas> ().enabled = false;
		GameObject sbs = GameObject.Find ("shangbanshen");
		GameObject xbs = GameObject.Find ("xiabanshen");

		sbs.GetComponent<Animator> ().SetBool ("Moving", true);
		sbs.GetComponent<Animator> ().SetFloat ("Horizontal", 1.0f);
		xbs.GetComponent<Animator> ().SetBool ("Moving", true);
		xbs.GetComponent<Animator> ().SetFloat ("Horizontal", 1.0f);

		Sequence seq = DOTween.Sequence ();


		if(whichscene!=99)
		seq.Append (targetGameobject.transform.DOMoveX (-186.51f, 3.5f));
		else
		seq.Append (targetGameobject.transform.DOMoveX (-27.06f, 3.5f));
		
		seq.AppendCallback (delegate() {
			sbs.GetComponent<Animator> ().SetBool ("Moving", false);
			xbs.GetComponent<Animator> ().SetBool ("Moving", false);
			GameObject.Find ("Canvas").GetComponent<Canvas> ().enabled = true;
			if (SceneManager.GetActiveScene ().name == "Mission1")
			block1.GetComponent<EdgeCollider2D>().isTrigger=false;
			if (SceneManager.GetActiveScene ().name == "Mission1BossFight")
			{
				block1.GetComponent<EdgeCollider2D>().isTrigger=false;
				block2.GetComponent<EdgeCollider2D>().isTrigger=false;
			}
			if(whichscene!=99)
			//出现敌人（暂时用来测试敌人1的状态）
				GameObject.Find ("Scene1").SendMessage("Enemy1_Showup");
			else
				GameObject.Find ("Boss").SendMessage("FirstJumpAttack");
		});
	}
		

	void Reset()
	{
		switch (whichscene) {
		case 1:
			SceneManager.LoadScene ("Mission1");
			break;
		case 2:
			SceneManager.LoadScene ("Mission1");
			scenenum = 2;
			break;
		case 3:
			SceneManager.LoadScene ("Mission1");
			scenenum = 3;
			break;
		case 4:
			SceneManager.LoadScene ("Mission1");
			scenenum = 4;
			break;
		case 5:
			SceneManager.LoadScene ("Mission1");
			scenenum = 5;
			break;
		case 99:
			SceneManager.LoadScene ("Mission1BossFight");
			scenenum = 99;
			break;
		}
	}
}
