using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene5Script : MonoBehaviour {

	public GameObject Hero;

	private bool eventHappen;
	private bool fa1show = false;
	private bool fa2show = false;
	private bool s1show = false;
	private bool s2show = false;
	private bool s3show = false;
	private bool ah1show = false;
	private bool ah2show = false;
	private bool ah3show = false;
	private bool ah4show = false;
	private bool p1show = false;
	private bool p2show = false;
	private bool p3show = false;


	public GameObject flyalien1;
	public GameObject flyalien2;
	public GameObject s1;
	public GameObject s2;
	public GameObject s3;
	public GameObject ah1;
	public GameObject ah2;
	public GameObject ah3;
	public GameObject ah4;
	public GameObject p1;
	public GameObject p2;
	public GameObject p3;

	public GameObject block7;
	public GameObject block8;

	[HideInInspector] public int diecounter;

	// Use this for initialization
	void Start () {
		eventHappen = false;
		diecounter = 0;

		//fa1show = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (diecounter >= 12) {
			block7.GetComponent<EdgeCollider2D> ().isTrigger = true;
			block8.GetComponent<EdgeCollider2D> ().isTrigger = true;
		}
		if (!eventHappen) {
			if (Hero.transform.position.x >= -97.32f) {
				eventHappen = true;
				block7.GetComponent<EdgeCollider2D> ().isTrigger = false;
				block8.GetComponent<EdgeCollider2D> ().isTrigger = false;
			}
		}
		//-93.04554f
		if (eventHappen) {
			if (!fa1show) {
				Invoke ("ShowFA1", 0);
				fa1show = true;
			}
			if (!fa2show) {
				Invoke ("ShowFA2", 15.0f);
				fa2show = true;
			}
			if (!s1show) {
				Invoke ("ShowS1", 2.5f);
				s1show = true;
			}
			if (!s2show) {
				Invoke ("ShowS2", 2.5f);
				s2show = true;
			}
			if (!s3show) {
				Invoke ("ShowS3", 5.0f);
				s3show = true;
			}
			if (!ah1show) {
				Invoke ("ShowAH1", 16.0f);
				ah1show = true;
			}
			if (!ah2show) {
				Invoke ("ShowAH2", 17.0f);
				ah2show = true;
			}
			if (!ah3show) {
				Invoke ("ShowAH3", 16.0f);
				ah3show = true;
			}
			if (!ah4show) {
				Invoke ("ShowAH4", 17.0f);
				ah4show = true;
			}
			if (!p1show) {
				Invoke ("ShowP1", 16.0f);
				p1show = true;
			}
			if (!p2show) {
				Invoke ("ShowP2", 16.0f);
				p2show = true;
			}
			if (!p3show) {
				Invoke ("ShowP3", 17.0f);
				p3show = true;
			}

		}
	}

	void ShowFA1()
	{
		flyalien1.SendMessage ("firstShow");
	}

	void ShowFA2()
	{
		flyalien2.SendMessage ("firstShow");
	}

	void ShowS1()
	{
		s1.SendMessage ("Run");
	}

	void ShowS2()
	{
		s2.SendMessage ("Run");
	}

	void ShowS3()
	{
		s3.SendMessage ("Run");
	}

	void ShowAH1()
	{
		ah1.SendMessage ("firstShow");
	}

	void ShowAH2()
	{
		ah2.SendMessage ("firstShow");
	}

	void ShowAH3()
	{
		ah3.SendMessage ("firstShow");
	}

	void ShowAH4()
	{
		ah4.SendMessage ("firstShow");
	}

	void ShowP1()
	{
		p1.SendMessage ("firstshow");
	}

	void ShowP2()
	{
		p2.SendMessage ("firstshow");
	}

	void ShowP3()
	{
		p3.SendMessage ("firstshow");
	}

	void Die()
	{
		diecounter++;
	}

}
