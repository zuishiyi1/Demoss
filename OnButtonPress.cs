using UnityEngine;  
using UnityEngine.EventSystems;  
using System.Collections; 

public class OnButtonPress : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerExitHandler {


	public GameObject shangbanshen;
	public GameObject xiabanshen;
	public GameObject HeroMain;

	private HeroController hero_c;


	// Use this for initialization
	void Start () {
		if (HeroMain) {
			hero_c = HeroMain.GetComponent<HeroController> ();
		}


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerUp (PointerEventData eventData)  
	{  
		if (this.name == "shot") {
				shangbanshen.GetComponent<Animator> ().SetBool ("endshot", true);
			if (xiabanshen.GetComponent<Animator> ().GetFloat ("Vertical") <= -0.5f) {
				xiabanshen.GetComponent<Animator> ().SetBool ("endshot", true);
			}
		}

		if (this.name == "grenade") {
			shangbanshen.GetComponent<Animator> ().SetBool ("grenade", false);
			shangbanshen.GetComponent<Animator> ().SetBool ("endgrenade", true);
		}

	}  

	public void OnPointerDown (PointerEventData eventData)  
	{  
		if (this.name == "shot") {
			if (shangbanshen.GetComponent<SpriteRenderer> ().enabled) {
				if (HeroMain.GetComponent<HeroController> ().intriggers == false) {
					if (hero_c.holdWepon == 0) {
						shangbanshen.GetComponent<Animator> ().SetBool ("shot", true);
					} 
					else if (hero_c.holdWepon == 2) {
						shangbanshen.GetComponent<Animator> ().SetBool ("sg_shot", true);
					}
					else if (hero_c.holdWepon == 1) {
						shangbanshen.GetComponent<Animator> ().SetBool ("sg_shot_shotgun", true);
					}
				}
			} 
			else {
				if (hero_c.holdWepon == 0) {
					xiabanshen.GetComponent<Animator> ().SetBool ("shot", true);
				} 
				else if (hero_c.holdWepon == 2) {
					xiabanshen.GetComponent<Animator> ().SetBool ("sg_shot", true);
				}
				else if (hero_c.holdWepon == 1) {
					xiabanshen.GetComponent<Animator> ().SetBool ("sg_shot_shotgun", true);
				}
			}
		}

		if(this.name == "grenade")
			shangbanshen.GetComponent<Animator>().SetBool ("grenade", true);
	} 

	public void OnPointerExit (PointerEventData eventData)  
	{  
		
	}  
}
