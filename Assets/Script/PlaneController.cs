using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//pos.x =1.5 第二段动画

public class PlaneController : MonoBehaviour {

	public GameObject bomb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if (GameObject.Find ("friend1").GetComponent<Friend1Controller> ().start) {
		
			this.transform.Translate (new Vector3 (-Time.deltaTime * 2.0f, 0, 0));
		}
		if (this.transform.position.x <= 4.0f) {
			this.GetComponent<Animator> ().SetBool ("showbomb", true);
		}

		if (this.transform.position.x <= 3.0f) {
			//show bomb
			bomb.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";

			this.GetComponent<Animator> ().SetBool ("showbomb", false);
		}

		if (bomb.GetComponent<SpriteRenderer> ().sortingLayerName == "Foreground") {
			//drop the bomb
			if (this.transform.position.x <= 2.5f) {
				

					
				//bomb.transform.rotation = Quaternion.AngleAxis (90.0f, new Vector3 (Time.deltaTime * 2.0f, -Time.deltaTime * 3.0f, Time.deltaTime * 30));
				bomb.transform.Translate (new Vector3 (Time.deltaTime * 2.0f, -Time.deltaTime * 3.0f, 0));
			}
		}

		if(this.transform.position.x <= 1.5f)
		{
			this.GetComponent<Animator> ().SetBool ("fly", true);
			this.transform.Translate (new Vector3 (0, Time.deltaTime * 2.0f, 0));
		}

		if (bomb.transform.localPosition.y <= -1.7f) {
			bomb.SetActive (false);
			GameObject.Find ("effect_boom").GetComponent<SpriteRenderer> ().sortingLayerName = "Foreground";
			GameObject.Find ("effect_boom").GetComponent<Animator> ().enabled = true;
		}
	}
}
