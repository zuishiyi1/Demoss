using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUpdate : MonoBehaviour {

	private HeroController a;

	// Use this for initialization
	void Start () {
		a = GameObject.Find ("Hero").GetComponent<HeroController> ();
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Text> ().text = a.Hero_money.ToString ();
	}
}
