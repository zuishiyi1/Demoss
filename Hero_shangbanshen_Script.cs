using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero_shangbanshen_Script : MonoBehaviour {

	public GameObject bullet;
	public Canvas diecanvas;
	public Image diebg;
	public Image dietext;
	public Button bt1;
	public Button bt2;
	public GameObject Maincamera;

	private bool flipx;
	private SpriteRenderer sr;
	private AudioSource audios;

	private Vector3 bullet_pos_left;
	private Vector3 bullet_pos_right;
	private Vector3 bullet_pos_up;    //rotation = -90  posx=0 posy=0.6
	private Vector3 bullet_pos_down;

	private GameObject firstbullet;

	public GameObject xiabanshen;

	public GameObject razereffect;

	//蹲下的时候 子弹y的位置在0.02

	private List<GameObject> bulletlist = new List<GameObject> ();


	[HideInInspector]
	public bool test;   //子弹爆炸状态

	[HideInInspector]public int holdwepon;


	// Use this for initialization
	void Start () {
		bullet_pos_left = new Vector3 (this.transform.parent.position.x - 0.3f, this.transform.parent.position.y + 0.1f, 0);
		bullet_pos_right = new Vector3 (this.transform.parent.position.x + 0.3f, this.transform.parent.position.y + 0.1f, 0);
		bullet_pos_up = new Vector3 (this.transform.parent.position.x, this.transform.parent.position.y + 0.6f, 0);
		sr = this.GetComponent<SpriteRenderer> ();

		firstbullet = Instantiate (bullet) as GameObject;
		firstbullet.GetComponent<SpriteRenderer> ().sprite = (Resources.Load ("bullet") as GameObject).GetComponent<SpriteRenderer> ().sprite;

		holdwepon = 0;

		audios = this.GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
		flipx = sr.flipX;

		foreach (GameObject go in bulletlist) {
			
			if (go) {
				if ((go.transform.position.x >= this.transform.parent.position.x + 10.0f) || (go.transform.position.x <= this.transform.parent.position.x - 10.0f) || (go.transform.position.y >= this.transform.parent.position.y + 10.0f)) {
					Destroy (go);
					continue;
				}

				if (go.GetComponent<Bullet> ().boom != true) {
					if (go.transform.tag == "up") {
						go.transform.position = new Vector3 (go.transform.position.x, go.transform.position.y + Time.deltaTime * 7.0f, 0);
					} else if (go.transform.tag == "Horizontal") {
						if (go.GetComponent<SpriteRenderer> ().flipX) {
							go.transform.position = new Vector3 (go.transform.position.x - Time.deltaTime * 7.0f,go.transform.position.y, 0);
						} else {
							go.transform.position = new Vector3 (go.transform.position.x + Time.deltaTime * 7.0f,go.transform.position.y, 0);
						}
					}
				}
			}
		}
			
	}


	void EndShot()
	{
		this.GetComponent<Animator> ().SetBool ("shot", false);
		this.GetComponent<Animator> ().SetBool ("sg_shot", false);
		this.GetComponent<Animator> ().SetBool ("sg_shot_shotgun", false);
		this.GetComponent<Animator> ().SetBool ("endshot", false);
	}

	void EndGrenade()
	{
		this.GetComponent<Animator> ().SetBool ("grenade", false);
	}

	void ShowBullet()
	{
		if (holdwepon == 0) {
			firstbullet.GetComponent<SpriteRenderer> ().sprite = (Resources.Load ("bullet") as GameObject).GetComponent<SpriteRenderer> ().sprite;
		}
		else if (holdwepon == 2) {
			firstbullet = Resources.Load ("razerbullet") as GameObject;
		}
		else if (holdwepon == 1) {
			firstbullet = Resources.Load ("shotgun_effect") as GameObject;
		}

		GameObject bullets = Instantiate(firstbullet) as GameObject;
		bullets.transform.parent = bullet.transform.parent;
		bullets.transform.localScale = new Vector3 (1.0f, 1.0f, 0);


		//抬头射击
		if (this.GetComponent<Animator> ().GetFloat ("Vertical") >= 0.5f) {
			bullets.transform.position = new Vector3 (this.transform.parent.position.x, this.transform.parent.position.y + 0.6f, 0);
			if (holdwepon == 2) {
				this.razereffect.transform.position = bullets.transform.position;
				this.razereffect.transform.eulerAngles = new Vector3 (0, 0, 90);
				this.razereffect.GetComponent<SpriteRenderer> ().enabled = true;
				if (this.razereffect.GetComponent<Animator> ().enabled) {
					razereffect.GetComponent<Animator> ().Play ("razereffect", 0, 0f);
				}
				this.razereffect.GetComponent<Animator> ().enabled = true;
			}
			bullets.tag = "up";
			bullet.tag = "up";
			Vector3 rotation = this.transform.localEulerAngles; 
			if (holdwepon == 1) {
				rotation.z = 90.0f; // 在这里修改坐标轴的值
			}
			else{
			rotation.z = -90.0f; // 在这里修改坐标轴的值
			}
			bullets.transform.localEulerAngles = rotation;
			bullet.transform.localEulerAngles = rotation;
		}
		//正常站立射击
		else {
			bullets.transform.tag = "Horizontal";

			Vector3 rotation = this.transform.localEulerAngles; 
			rotation.z = 0; // 在这里修改坐标轴的值
			bullets.transform.localEulerAngles = rotation;
			bullet.transform.localEulerAngles = rotation;

			if (flipx) {
				bullets.transform.position = new Vector3 (this.transform.parent.position.x - 0.5f, this.transform.parent.position.y + 0.1f, 0);
				if (holdwepon == 2) {
					this.razereffect.transform.position = bullets.transform.position;
					this.razereffect.transform.eulerAngles = new Vector3 (0, 0, 0);
					this.razereffect.GetComponent<SpriteRenderer> ().enabled = true;
					if (this.razereffect.GetComponent<Animator> ().enabled) {
						razereffect.GetComponent<Animator> ().Play ("razereffect", 0, 0f);
					}
					this.razereffect.GetComponent<Animator> ().enabled = true;
				}
				bullets.GetComponent<SpriteRenderer> ().flipX = flipx;

			
			} else {
				bullets.transform.position = new Vector3 (this.transform.parent.position.x + 0.5f, this.transform.parent.position.y + 0.1f, 0);
				if (holdwepon == 2) {
					this.razereffect.transform.position = bullets.transform.position;
					this.razereffect.transform.eulerAngles = new Vector3 (0, 0, 0);
					this.razereffect.GetComponent<SpriteRenderer> ().enabled = true;
					if (this.razereffect.GetComponent<Animator> ().enabled) {
						razereffect.GetComponent<Animator> ().Play ("razereffect", 0, 0f);
					}
					this.razereffect.GetComponent<Animator> ().enabled = true;
				}
				bullets.GetComponent<SpriteRenderer> ().flipX = flipx;
			}
		}

		//如果是散弹枪，则不加入列表，因为加入列表后就会移动
		if (holdwepon == 1) {
			return;
		} else {
			bulletlist.Add (bullets);
		}

	}

	public void ShowGrenade()
	{
		
	}

	void HideXiabanshen()
	{
		xiabanshen.GetComponent<SpriteRenderer> ().enabled = false;
	}

	void ShowDie()
	{
		diecanvas.GetComponent<AudioSource> ().enabled = true;
		diecanvas.enabled = true;
		GameObject.Find ("bgmusic").GetComponent<AudioSource> ().volume -= 0.1f;
		if (dietext.GetComponent<CanvasGroup> ().alpha >= 0.9f) {
			bt1.GetComponent<Image> ().enabled = true;
			bt2.GetComponent<Image> ().enabled = true;
			return;
		}
		if (diebg.GetComponent<CanvasGroup> ().alpha >= 0.9f) {
			dietext.GetComponent<CanvasGroup> ().alpha += 0.1f;
		}
		diebg.GetComponent<CanvasGroup> ().alpha += 0.1f;

		Invoke ("ShowDie", 0.1f);
	}

	public void ContinueOrExit(string s)
	{
		if (s == "c") {
			Maincamera.SendMessage ("Reset");
		}
		if (s == "e") {
			Application.Quit ();
		}
	}

	void PlayBulletVoice()
	{
		audios.clip = Resources.Load ("normalgunshoot") as AudioClip;
		audios.Play ();
	}

	void PlayDieVoice()
	{
		audios.clip = Resources.Load ("hero_die") as AudioClip;
		audios.Play ();
	}
		
		
}
