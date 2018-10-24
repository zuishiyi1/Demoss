//if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
//实际运行时，getbutton请改成inputtouch，不然不能执行
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//加载txt
using System.IO;
using System.Text;
///

//list
using System.Collections.Generic;

//标题场景里，最左是-5.35，最右是5.35

public class CameraController_1 : MonoBehaviour {

	public GameObject targetGameobject; 

	public Canvas canvas;
	public Image image;
	public Text text1;
	public Text text2;
	public Text text3;
	public Text skipText;

	private bool showtextend;

	private bool show;

	private bool skip;

	private float hideSkiptextTime;


	// Use this for initialization
	void Start ()
	{
		Screen.SetResolution (1024, 600, true);

		if (SceneManager.GetActiveScene ().name == "TitleScene1") {
			ShowText ();
		}
		showtextend = false;
		show = false;
		skip = false;
		hideSkiptextTime = 0.1f;
	}

	void Awake()
	{
//		if (PlayerPrefs.GetString ("pos") == "0") {
//			SceneManager.LoadScene ("MainTitleScene");
//		}
	}

	
	// Update is called once per frame
	void Update () {


		if (SceneManager.GetActiveScene ().name == "TitleScene1") {
			GameObject.Find ("NPC2_1").GetComponent<Animator> ().SetBool ("run", false);
			GameObject.Find ("NPC2").GetComponent<Animator> ().SetBool ("run", false);
		}

		if (!skip) {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
				skip = true;
			}
		}
		else {
			if (hideSkiptextTime <= 1) {
				skipText.GetComponent<CanvasGroup> ().alpha += Time.deltaTime;
			}
			if (hideSkiptextTime >= 1.0f) {
				skipText.GetComponent<CanvasGroup> ().alpha -= Time.deltaTime;
			}
			if (hideSkiptextTime >= 1f && skipText.GetComponent<CanvasGroup> ().alpha == 0) {
				skip = false;
				hideSkiptextTime = 0.1f;
			}
			if (skipText.GetComponent<CanvasGroup> ().alpha >= 0.5f) {
				if (skipText.GetComponent<CanvasGroup> ().alpha == 1) {
					hideSkiptextTime += Time.deltaTime;
				}
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
					SceneManager.LoadScene ("TitleScene2");
				}
			}
		}

		if (show) {
			targetGameobject.GetComponent<CarController> ().enabled = true;
			if ((this.transform.position.x - targetGameobject.transform.position.x) < 1) {

				//如果车到达停止地点并且车还没到达x=2.0，那就继续移动镜头
				if ((targetGameobject.transform.position.x >= 1.2f) && (targetGameobject.transform.position.x <= 2.0f)) {
				
					targetGameobject.transform.Translate (new Vector3 (0.8f * Time.deltaTime, 0, 0));

				} else if (targetGameobject.transform.position.x <= 1.2f) {
				
					this.transform.position = new Vector3 (targetGameobject.transform.position.x + 1, 0, -10);

				} else {
					targetGameobject.GetComponent<CarController> ().carstop = true;
				}
			

			}
		}


			

	}

	void ShowText()
	{
		if (text3.GetComponent<CanvasGroup> ().alpha >= 1.0f) {
			show = true;
			canvas.GetComponentInParent<CanvasGroup> ().alpha = 0;
			text1.text = "";
			text2.text = "";
			text3.text = "";
			return;
		}

		if (text2.GetComponent<CanvasGroup> ().alpha >= 1.0f) {
			text3.GetComponent<CanvasGroup> ().alpha += 0.03f;
		}
		else if (text1.GetComponent<CanvasGroup> ().alpha >= 1.0f) {
			text2.GetComponent<CanvasGroup> ().alpha += 0.03f;
		}
		else {
			text1.GetComponent<CanvasGroup> ().alpha += 0.03f;
		}
		Invoke ("ShowText", 0.1f);
	}
		
		

}
