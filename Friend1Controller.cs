using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Friend1Controller : MonoBehaviour {
	public GameObject f2;
	public GameObject f2_1;
	public GameObject npc4_hang;
	public bool start = false;

	public Canvas canvas;
	public Image image;
	public Text text1;
	public Text text2;
	public Text text3;

	private bool showtextend;
	private bool show;
	private bool fadein;

	private bool skip;
	public Text skipText;

	private float hideSkiptextTime;
	// Use this for initialization
	void Start () {
		if (SceneManager.GetActiveScene ().name == "TitleScene2") {
			ShowText ();
		}
		showtextend = false;
		show = false;
		skip = false;
		hideSkiptextTime = 0.1f;
		fadein = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (!skip) {
			//if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			if(Input.GetMouseButton(0)){
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
				//if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
				if(Input.GetMouseButton(0)){
					SceneManager.LoadScene ("MainTitleScene");
				}
			}
		}

		if (show) {
			this.transform.Translate (new Vector3 (-Time.deltaTime * 0.25f, 0, 0));
			f2_1.transform.Translate (new Vector3 (-Time.deltaTime * 1.5f, 0, 0));

			if (f2.transform.localPosition.x <= -3.3f) {
				f2.GetComponent<Animator> ().SetBool ("stop", true);
			} else {
				f2.transform.Translate (new Vector3 (-Time.deltaTime * 1.5f, 0, 0));
			}

			if ((f2_1.transform.position.x - f2.transform.position.x) < 1.5f) {
				f2.GetComponent<Animator> ().SetBool ("shot", true);
			}


			if (f2_1.transform.position.x < f2.transform.position.x) {

				if (npc4_hang.transform.position.y >= -1.169f) {
					npc4_hang.GetComponent<Animator> ().SetBool ("down", true);

					npc4_hang.transform.Translate (new Vector3 (0, -Time.deltaTime, 0));
				}
				
				if (GameObject.Find ("Main Camera").transform.position.x > -5.1f) {

					//镜头开始移动
					GameObject.Find ("Main Camera").transform.Translate (new Vector3 (-Time.deltaTime * 0.5f, 0, 0));
				}
				else {
					if (!fadein) {
						FadeIn ();
					}
				}
		
				start = true;
			}

			if (npc4_hang.transform.position.y <= -1.169f) {
				npc4_hang.GetComponent<SpriteRenderer> ().flipX = true;
				npc4_hang.GetComponent<Animator> ().SetBool ("down", false);
				npc4_hang.GetComponent<Animator> ().SetBool ("run", true);
				npc4_hang.transform.Translate (new Vector3 (Time.deltaTime * 1.5f, 0, 0));
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

	void FadeIn()
	{
		fadein = true;
		if (canvas.GetComponent<CanvasGroup> ().alpha >= 1.0f) {
			SceneManager.LoadScene ("MainTitleScene");
			return;

		}
		canvas.GetComponent<CanvasGroup> ().alpha += 0.03f;
		Invoke ("FadeIn", 0.1f);
	}

}
