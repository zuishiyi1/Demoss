using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//每颗子弹的距离为0.15f
//子弹最远距离 -3.0f

public class Friend1_2Controller : MonoBehaviour {

	public GameObject bullet;
	public GameObject hited;

	private int maxValue = 0;
	private  GameObject[] objs = new GameObject[3];
	private Vector3 startPos;


	// Use this for initialization
	void Start () {
				
		startPos = bullet.transform.position;
		//this.InvokeRepeating ("ShotBullet", 0, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (maxValue < 3) {

			GameObject obj = GameObject.Instantiate (bullet) as GameObject;
			objs [maxValue] = obj;

			maxValue++;
		}

		ShotBullet ();


	}

	void ShotBullet()
	{
		if (objs [2] == null) {
			return;
		}
//		if (maxValue < 3) {
//			GameObject obj = GameObject.Instantiate (bullet) as GameObject;
//
//			Vector3 vec3 = Vector3.Lerp(obj.transform.position,new vec
//
//			obj.transform.Translate (new Vector3 (-3.0f, 0, 0));
//
//		}
//
//		maxValue++;

		if (objs [0]) {
			objs[0].GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
			objs[0].transform.Translate (new Vector3 (-Time.deltaTime * 1.0f, 0, 0));
		}

		if (objs [1] && (Mathf.Abs(objs[0].transform.position.x - objs[1].transform.position.x)>=0.15f)) {
			objs[1].GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
			objs[1].transform.Translate (new Vector3 (-Time.deltaTime * 1.0f, 0, 0));
		}

		if (objs [2] && (Mathf.Abs(objs[1].transform.position.x - objs[2].transform.position.x)>=0.15f)) {
			objs[1].GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
			objs[2].transform.Translate (new Vector3 (-Time.deltaTime * 1.0f, 0, 0));
		}

		if (Mathf.Abs(bullet.transform.position.x - objs[2].transform.position.x)>=0.15f) {
			bullet.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
			bullet.transform.Translate (new Vector3 (-Time.deltaTime * 1.0f, 0, 0));
		}

		if (objs [0].transform.position.x <= -2.0f) {

			hited.GetComponent<Animator> ().SetBool ("die", true);

			objs[0].GetComponent<SpriteRenderer>().sortingLayerName = "Default";
		}

		if (objs [1].transform.position.x <= -2.0f) {
			objs[1].GetComponent<SpriteRenderer>().sortingLayerName = "Default";
		}

		if (objs [2].transform.position.x <= -2.0f) {
			objs[2].GetComponent<SpriteRenderer>().sortingLayerName = "Default";
		}

		if (bullet.transform.position.x <= -2.0f) {
			Destroy (objs [0]);
			Destroy (objs [1]);
			Destroy (objs [2]);
			bullet.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
			bullet.transform.position = startPos;
			maxValue = 0;
		}
	}
}
