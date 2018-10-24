using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//小孩比其他人矮0.226


public class KidController : MonoBehaviour {

	private float talloffset = 0.226f; 


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ((this.transform.localPosition.x >= -1.1f) && (this.transform.localPosition.x <= -0.7f)) {
			this.transform.Translate (new Vector3 (-Time.deltaTime, 0, 0));
		} else if ((this.transform.localPosition.y >= (0.2f-talloffset)) && (this.transform.localPosition.x <=1.1f)) {
			this.transform.Translate (new Vector3 (-Time.deltaTime, -Time.deltaTime, 0));
		} else {
			this.transform.Translate (new Vector3 (-Time.deltaTime, 0, 0));
		}
	}
}
