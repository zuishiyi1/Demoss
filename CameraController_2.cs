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

public class CameraController_2 : MonoBehaviour
{


	public GameObject npc2_1;
	public GameObject npc2_2;
	public GameObject npc2_3;




	// Use this for initialization
	void Start ()
	{
		Screen.SetResolution (1024, 600, true);
	}

	
	// Update is called once per frame
	void Update ()
	{




		if (SceneManager.GetActiveScene ().name == "TitleScene2") {
			if (this.transform.position.x <= -0.87f) {
				npc2_1.GetComponent<Animator> ().SetBool ("run", true);
				npc2_1.GetComponent<SpriteRenderer> ().flipX = false;
				npc2_1.transform.Translate (new Vector3 (Time.deltaTime * (-0.5f), 0, 0));
			}
			if (this.transform.position.x <= -2.629065f) {
				npc2_2.GetComponent<Animator> ().SetBool ("run", true);
				//npc2_2.GetComponent<SpriteRenderer> ().flipX = false;
				npc2_2.transform.Translate (new Vector3 (Time.deltaTime * (-0.5f), 0, 0));
			}
			if (this.transform.position.x <= -4.694457f) {
				npc2_3.GetComponent<Animator> ().SetBool ("run", true);
				//npc2_3.GetComponent<SpriteRenderer> ().flipX = false;
				npc2_3.transform.Translate (new Vector3 (Time.deltaTime * (-0.5f), 0, 0));
			}
		}
			

	}
		


}
