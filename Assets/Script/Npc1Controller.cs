using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Npc1Controller : MonoBehaviour {

	public Canvas canvas;

	private int shakeTimes;

	// Use this for initialization
	void Start () {
		shakeTimes = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x <= 3.7f) {
			this.GetComponent<Animator> ().SetBool ("stop", true);
		} else {
			transform.Translate (new Vector3 (-Time.deltaTime * 1.6f, 0, 0));
		}
	}

	void AddShake()
	{
		if (shakeTimes >= 0) {
			shakeTimes++;
		}
		if (shakeTimes >= 4) {
			FadeIn ();
			shakeTimes = -1;
		}
	}

	void FadeIn()
	{
		if (canvas.GetComponent<CanvasGroup> ().alpha >= 1.0f) {
			SceneManager.LoadScene ("TitleScene2");
			return;

		}
		canvas.GetComponent<CanvasGroup> ().alpha += 0.03f;
		Invoke ("FadeIn", 0.1f);
	}
}
