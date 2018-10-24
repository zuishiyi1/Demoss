using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//0.2f groundY
//1.1 npcs godown

public class linshiScript : MonoBehaviour {

	private GameObject npc1;

	private  bool oldman=false;


	// Use this for initialization
	void Start () {
		npc1 = GameObject.Find ("NPC1");
		if (SceneManager.GetActiveScene ().name == "TitleScene1") {
			GameObject.Find ("shangbanshen").GetComponent<Animator> ().SetBool ("scene1", true);
			GameObject.Find ("xiabanshen").GetComponent<Animator> ().SetBool ("scene1", true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (npc1.GetComponent<Animator>().GetBool("stop")) {
			if ((this.transform.localPosition.x >= -1.1f) && (this.transform.localPosition.x <= -0.7f)) {
				this.transform.Translate (new Vector3 (-Time.deltaTime, 0, 0));
			} else if ((this.transform.localPosition.y >= 0.2f) && (this.transform.localPosition.x <=1.1f)) {
				this.transform.Translate (new Vector3 (-Time.deltaTime, -Time.deltaTime, 0));
			} else {
				this.transform.Translate (new Vector3 (-Time.deltaTime, 0, 0));
				if (!oldman) {
					GameObject.Find ("oldman").GetComponent<OldmanController> ().enabled = true;
					oldman = true;
				}
			}
		}
	}
}
