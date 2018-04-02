using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_xiabanshenScript : MonoBehaviour {


	public GameObject mainbody;

	public GameObject bullet;

	private List<GameObject> bulletlist = new List<GameObject> ();

	private bool flipx;
	private SpriteRenderer sr;

	private Vector3 bullet_pos_left_dunxia;
	private Vector3 bullet_pos_right_dunxia;

	private GameObject firstbullet;

	//蹲下的时候 子弹y的位置在0.02

	[HideInInspector]public int holdwepon;

	public GameObject razereffect;

	private AudioSource audios;


	// Use this for initialization
	void Start () {
		bullet_pos_left_dunxia = new Vector3 (-0.5f, 0.02f, 0);
		bullet_pos_right_dunxia = new Vector3 (0.5f, 0.02f, 0);
		sr = this.GetComponent<SpriteRenderer> ();

		firstbullet = Instantiate (bullet) as GameObject;
		firstbullet.GetComponent<SpriteRenderer> ().sprite = (Resources.Load ("bullet") as GameObject).GetComponent<SpriteRenderer> ().sprite;

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
					if (go.GetComponent<SpriteRenderer> ().flipX) {
						go.transform.localPosition = new Vector3 (go.transform.position.x - Time.deltaTime * 7.0f, go.transform.position.y, 0);
					} else {
						go.transform.localPosition = new Vector3 (go.transform.position.x + Time.deltaTime * 7.0f, go.transform.position.y, 0);
					}
				}

			}
		}
			

//			if (bullet.GetComponent<SpriteRenderer> ().flipX) {
//				bullet.transform.localPosition = new Vector3 (bullet.transform.localPosition.x - Time.deltaTime * 7.0f, 0.02f, 0);
//			}
//			else {
//				bullet.transform.localPosition = new Vector3 (bullet.transform.localPosition.x + Time.deltaTime * 7.0f, 0.02f, 0);
//			}

	}

	void endjump()
	{
		mainbody.GetComponent<HeroController> ().EndJump ();
	}

	void EndShot()
	{
		this.GetComponent<Animator> ().SetBool ("shot", false);
		this.GetComponent<Animator> ().SetBool ("sg_shot", false);
		this.GetComponent<Animator> ().SetBool ("sg_shot_shotgun", false);
		this.GetComponent<Animator> ().SetBool ("endshot", false);
	}

	public void ShowBullet()
	{

		if (holdwepon == 0) {
			firstbullet.GetComponent<SpriteRenderer> ().sprite = (Resources.Load ("bullet") as GameObject).GetComponent<SpriteRenderer> ().sprite;
		}
		else if (holdwepon == 2) {
			firstbullet= Resources.Load ("razerbullet") as GameObject;
		}
		else if (holdwepon == 1) {
			firstbullet = Resources.Load ("shotgun_effect") as GameObject;
		}


		GameObject bullets = Instantiate(firstbullet) as GameObject;
		bullets.transform.parent = bullet.transform.parent;
		bullets.transform.localScale = new Vector3 (1.0f, 1.0f, 0);


		bullets.transform.tag = "Horizontal";
		Vector3 rotation = this.transform.localEulerAngles; 
		rotation.z = 0; // 在这里修改坐标轴的值
		bullets.transform.localEulerAngles = rotation;
		bullets.transform.localEulerAngles = rotation;

		if (this.GetComponent<Animator> ().GetFloat ("Vertical") < -0.5f) {
			if (flipx) {
				bullets.transform.localPosition = new Vector3 (this.transform.parent.position.x - 0.5f, this.transform.parent.position.y + 0.02f, 0);
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
				bullets.transform.localPosition = new Vector3 (this.transform.parent.position.x + 0.5f, this.transform.parent.position.y + 0.02f, 0);
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

	void PlayBulletVoice()
	{
		audios.clip = Resources.Load ("normalgunshoot") as AudioClip;
		audios.Play ();
	}

}
