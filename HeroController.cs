using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
	public bool test;
	public GameObject test1;

	[System.Serializable]
	public class AnimatorParameters
	{
		public string moving;
		public string horizotal;
		public string vertical;
	}

	private GameObject Boss;
	private bool bossdead;

	public GameObject bullet;
	public GameObject pick;
	public GameObject razereffect;

	public Animator shangbanshen;
	public Animator xiabanshen;
	public Animator target;
	public float speed = 2.5f;
	public AnimatorParameters parameters;

	public Transform groundCheckTransform;
	public  LayerMask groundCheckLayerMask;
	public  LayerMask platformCheckLayerMask;

	public GameObject mc;//完成任务文字

	[HideInInspector]
	public Dictionary<string,int> itemlist = new Dictionary<string, int> ();

	[HideInInspector]
	public int Hero_money;

	private bool grounded;
	private bool jump;
	private bool endjump;

	private AudioSource audios;

	public int holdWepon;
//0代表没拿着武器，1代表拿着散弹枪，2代表拿着镭射，3…………

	[HideInInspector]
	public bool intriggers;

	private int npcs;
	private bool win;
	private bool hitted;

	private Vector3 direction;
	private Coroutine cououtine;

	[HideInInspector] public bool die;
	[HideInInspector] public int scene;

	private List<GameObject> controlUIs = new List<GameObject> ();

	void Start ()
	{
		audios = this.GetComponent<AudioSource> ();

		jump = false;
		holdWepon = 0;
		win = false;
		scene = 1;
		intriggers = false;
		test = false;

		Boss = GameObject.Find ("Boss");
		bossdead = false;

		List<string> linshilist = new List<string> ();

		//var fileAddress = System.IO.Path.Combine (Application.streamingAssetsPath, "HeroState.txt");
		var fileAddress = System.IO.Path.Combine("jar:file://" + Application.dataPath + "!/assets/", "HeroState.txt");
        FileInfo fInfo0 = new FileInfo (fileAddress);  
		if (fInfo0.Exists) {  
			StreamReader r = new StreamReader (fileAddress);  
			string s = "";
			while (true) {
				s = r.ReadLine ();
				if (s != "end") {
					linshilist.Add (s);
				} else {
					break;
				}

			}
			r.Close ();
		}

		foreach (string s in linshilist) {
			string[] s1 = s.Split (':');
			if (s1 [0] == "money") {
				Hero_money = int.Parse (s1 [1]);
			}

			if (s1 [0] == "items") {
				string[] s2 = s1 [1].Split (',');

				foreach (string s3 in s2) {
					string[] s4 = s3.Split ('.');
					itemlist.Add (s4 [0], int.Parse (s4 [1]));
				}
			}
		}

//		Debug.Log (itemlist ["wepon_cold"].ToString ());
//		Debug.Log (itemlist ["shield_invincible"].ToString ());
//		Debug.Log (Hero_money.ToString ());
	}

	private IEnumerator Move ()
	{
		
		while (true) {
	
			//this.target.transform.position += this.direction * Time.deltaTime * this.speed;
			this.target.transform.position += new Vector3 (this.direction.x * Time.deltaTime * this.speed, 0, this.direction.z * Time.deltaTime * this.speed);
	
			this.shangbanshen.SetFloat (this.parameters.horizotal, this.direction.x);
			this.shangbanshen.SetFloat (this.parameters.vertical, this.direction.y);
			this.xiabanshen.SetFloat (this.parameters.horizotal, this.direction.x);
			this.xiabanshen.SetFloat (this.parameters.vertical, this.direction.y);
	
			yield return null;
		}
	}

	public void BeginMove ()
	{

		if (holdWepon == 0) {
			if (this.shangbanshen.GetBool ("idle")) {
				this.shangbanshen.SetBool (this.parameters.moving, true);
				this.xiabanshen.SetBool (this.parameters.moving, true);
				this.cououtine = StartCoroutine (this.Move ());
				return;
			}
		} else {
			//if (this.shangbanshen.GetBool ("sg_idle")) {
			this.shangbanshen.SetBool ("sg_moving", true);
			this.shangbanshen.SetBool ("sg_idle", false);
			this.xiabanshen.SetBool ("sg_moving", true);
			//this.xiabanshen.SetBool ("idle", false);
			this.cououtine = StartCoroutine (this.Move ());
			return;
			//}
		}
	}

	public void EndMove ()
	{

		if (holdWepon == 0) {
			this.shangbanshen.SetBool (this.parameters.moving, false);
			this.xiabanshen.SetBool (this.parameters.moving, false);
			if (this.cououtine != null) {
				this.shangbanshen.SetFloat (this.parameters.horizotal, this.direction.x);
				this.shangbanshen.SetFloat (this.parameters.vertical, 0);
				this.xiabanshen.SetFloat (this.parameters.horizotal, this.direction.x);
				this.xiabanshen.SetFloat (this.parameters.vertical, 0);
				StopCoroutine (this.cououtine);
			}
			return;
		} else {
			this.shangbanshen.SetBool ("sg_moving", false);
			this.shangbanshen.SetBool ("sg_idle", true);
			this.xiabanshen.SetBool ("sg_moving", false);
			if (this.cououtine != null) {
				this.shangbanshen.SetFloat (this.parameters.horizotal, this.direction.x);
				this.shangbanshen.SetFloat (this.parameters.vertical, 0);
				this.xiabanshen.SetFloat (this.parameters.horizotal, this.direction.x);
				this.xiabanshen.SetFloat (this.parameters.vertical, 0);
				StopCoroutine (this.cououtine);
			}
			return;
		}
	}

	public void UpdateDirection (Vector3 direction)
	{

		//Vector3 d = new Vector3 (direction.x, 0, direction.z);
		this.direction = direction;

		//this.direction = d;
	}

	void Update ()
	{
		if (test) {
			Test ();
			test = false;
		}

		if ((scene == 5) && (this.xiabanshen.GetBool ("roll"))) {
			this.GetComponent<BoxCollider2D> ().offset = new Vector2 (this.GetComponent<BoxCollider2D> ().offset.x, this.GetComponent<BoxCollider2D> ().offset.y - 0.1f);
			this.shangbanshen.GetComponentInChildren<SpriteRenderer> ().enabled = true;
			this.xiabanshen.GetComponentInChildren<SpriteRenderer> ().enabled = true;
			this.xiabanshen.SetBool ("roll", false);
			GameObject.Find ("Canvas").GetComponent<Canvas> ().enabled = true;
			grounded = true;

		}

		if (scene == 4) {
			if (!die) {
				if (this.xiabanshen.GetBool ("roll") == false) {
					this.shangbanshen.GetComponentInChildren<SpriteRenderer> ().enabled = false;
					this.xiabanshen.SetBool ("roll", true);
					GameObject.Find ("Canvas").GetComponent<Canvas> ().enabled = false;
					this.GetComponent<BoxCollider2D> ().offset = new Vector2 (this.GetComponent<BoxCollider2D> ().offset.x, this.GetComponent<BoxCollider2D> ().offset.y + 0.1f);
				}
				if (Input.GetMouseButtonDown (0)) {
					if (grounded) {
						this.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (40.0f, 290.0f));
					}
				}
			}
		} else {

			if(!win)
			{

			if (this.shangbanshen.GetFloat ("Horizontal") > 0) {
				//this.GetComponent<SpriteRenderer> ().flipX = true;
				this.shangbanshen.GetComponentInChildren<SpriteRenderer> ().flipX = false;
				this.xiabanshen.GetComponentInChildren<SpriteRenderer> ().flipX = false;
				if (bullet) {
					if (bullet.GetComponent<SpriteRenderer> ().sprite == null) {
						bullet.transform.localPosition = new Vector3 (0.3f, 0.1f, 0);
						bullet.GetComponent<SpriteRenderer> ().flipX = false;
					}

				}
			} else {
				//this.GetComponent<SpriteRenderer> ().flipX = false;
				this.shangbanshen.GetComponentInChildren<SpriteRenderer> ().flipX = true;
				this.xiabanshen.GetComponentInChildren<SpriteRenderer> ().flipX = true;
				if (bullet) {
					if (bullet.GetComponent<SpriteRenderer> ().sprite == null) {
						bullet.transform.localPosition = new Vector3 (-0.3f, 0.1f, 0);
						bullet.GetComponent<SpriteRenderer> ().flipX = true;
					}
				}
			}

			if (this.shangbanshen.GetFloat ("Vertical") < -0.5f && (this.xiabanshen.GetBool ("Moving") || this.xiabanshen.GetBool ("sg_moving"))) {
				//this.GetComponent<SpriteRenderer> ().flipX = true;
					if (grounded) {
						this.shangbanshen.GetComponentInChildren<SpriteRenderer> ().enabled = false;
					}
				//boss死了要摆出胜利姿势
			} else {
				this.shangbanshen.GetComponentInChildren<SpriteRenderer> ().enabled = true;
			}

			grounded = Physics2D.OverlapCircle (groundCheckTransform.position, 0.5f, groundCheckLayerMask);
			if (!grounded) {
				grounded = Physics2D.OverlapCircle (groundCheckTransform.position, 0.5f, platformCheckLayerMask);
			}

			if (grounded) {
				if (jump) {
					
				} else {
					endjump = true;
					if (holdWepon == 0) {
						this.shangbanshen.SetBool ("jump", false);
					} else {
						this.shangbanshen.SetBool ("sg_jump", false);
					}
					this.xiabanshen.SetBool ("jump", false);
				}
			}

//		if (grounded && endjump) {
//				if (holdWepon == 0) {
//					this.shangbanshen.SetBool ("jump", false);
//				}
//				else {
//					this.shangbanshen.SetBool ("sg_jump", false);
//				}
//			this.xiabanshen.SetBool ("jump", false);
//		}
			}
		}
	}


	public void HeroShot ()
	{
		if (!intriggers) {
			if (holdWepon == 0) {
				this.shangbanshen.SetBool ("shot", true);
			} else if (holdWepon == 2) {
				this.shangbanshen.SetBool ("sg_shot", true);
			} else if (holdWepon == 1) {
				this.shangbanshen.SetBool ("sg_shot_shotgun", true);
			}
		} else {
			//通知主摄像头，针对站在哪个npc处，执行显示的内容
			GameObject.Find ("Main Camera").GetComponent<CameraController> ().npcs = this.npcs;
			GameObject.Find ("dialogbox").GetComponent<SpriteRenderer> ().enabled = false;
			GameObject.Find ("dialogbox").GetComponent<Animator> ().enabled = false;
			GameObject.Find ("exclamatory_mark").GetComponent<SpriteRenderer> ().enabled = false;
		}
	}

	void BackState ()
	{
		this.target.SetBool ("idle", true);
		this.target.SetBool ("shot", false);
		this.target.SetBool ("sg_shot", false);
		this.target.SetBool ("sg_shot_shotgun", false);
		this.target.SetBool ("grenade", false);
	}

	public void HeroJump ()
	{
		if (this.xiabanshen.GetBool ("jump")) {
			return;
		}
		if (grounded) {

			jump = true;
			if(this.xiabanshen.GetFloat ("Vertical") > -0.5f)
			{
			this.gameObject.layer = 17;//17是herojump
			}

			if (holdWepon == 0) {
				if ((!this.shangbanshen.GetBool ("Moving")) && this.shangbanshen.GetBool ("idle") && (this.xiabanshen.GetFloat ("Vertical") > -0.5f)) {
					this.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 230.0f));
					endjump = false;
					this.shangbanshen.SetBool ("jump", true);
					this.xiabanshen.SetBool ("jump", true);
				} else if (this.shangbanshen.GetBool ("Moving") && (this.xiabanshen.GetFloat ("Vertical") > -0.5f)) {
					this.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (this.shangbanshen.GetFloat ("Horizontal") * 25.0f, 230.0f));
					endjump = false;
					this.shangbanshen.SetBool ("jump", true);
					this.xiabanshen.SetBool ("jump", true);
				}
			} else {
				if ((!this.shangbanshen.GetBool ("sg_moving")) && this.shangbanshen.GetBool ("sg_idle") && (this.xiabanshen.GetFloat ("Vertical") > -0.5f)) {
					this.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 230.0f));
					endjump = false;
					this.shangbanshen.SetBool ("sg_jump", true);
					this.xiabanshen.SetBool ("jump", true);
				} else if (this.shangbanshen.GetBool ("sg_moving") && (this.xiabanshen.GetFloat ("Vertical") > -0.5f)) {
					this.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (this.shangbanshen.GetFloat ("Horizontal") * 25.0f, 230.0f));
					endjump = false;
					this.shangbanshen.SetBool ("sg_jump", true);
					this.xiabanshen.SetBool ("jump", true);
				}
			}


		}
	}

	public void EndJump ()
	{
//		this.GetComponent<BoxCollider2D> ().enabled = true;
//		endjump = true;
		jump = false;
		this.gameObject.layer = 9;//跳跃结束后重新回归到hero层

	}

	void FixedUpdate ()
	{
		
	}

	void OnTriggerEnter2D (Collider2D col2d)
	{
		if (col2d.name == "water") {
			shangbanshen.SendMessage ("ShowDie");
			return;
		}

		if (col2d.name == "birds") {
			this.pick.GetComponent<SpriteRenderer> ().enabled = true;
			this.pick.GetComponent<Animator> ().enabled = true;
			Destroy (col2d.gameObject);
			return;
		}

		if (col2d.gameObject.layer == 19) {
			this.DieBySoward ();
		}

		if (col2d.gameObject.layer == 20) {
			this.DieByWepon ();
		}

		if (col2d.gameObject.tag == "wepon") {
			if (col2d.gameObject.name == "wepon_razer") {
				audios.clip = Resources.Load ("razergun") as AudioClip;
				audios.Play ();
				holdWepon = 2;
				this.shangbanshen.SetBool ("sg_idle", true);
				this.shangbanshen.GetComponent<Hero_shangbanshen_Script> ().holdwepon = 2;
				this.xiabanshen.GetComponent<Hero_xiabanshenScript> ().holdwepon = 2;
			}
			if (col2d.gameObject.name == "shotgun_item") {
				holdWepon = 1;
				audios.clip = Resources.Load ("shotgun") as AudioClip;
				audios.Play ();
				this.shangbanshen.SetBool ("sg_idle", true);
				this.shangbanshen.GetComponent<Hero_shangbanshen_Script> ().holdwepon = 1;
				this.xiabanshen.GetComponent<Hero_xiabanshenScript> ().holdwepon = 1;
			}

			col2d.GetComponent<SpriteRenderer> ().enabled = false;
			Destroy (col2d.gameObject);
			this.pick.GetComponent<SpriteRenderer> ().enabled = true;
			this.pick.GetComponent<Animator> ().enabled = true;
			return;
		}

		if (col2d.gameObject.tag == "npcs") {
			//this.target.SetBool ("idle", false);
			intriggers = true;
			switch (col2d.gameObject.name) {

			//当角色触碰其他npc时显示的提示，以后有新增的npc可以在这里添加
			case "soilders_phone":
				GameObject.Find ("dialogbox").GetComponent<SpriteRenderer> ().enabled = true;
				GameObject.Find ("dialogbox").GetComponent<Animator> ().enabled = true;
				npcs = 1;
				break;
			case "title":
				GameObject.Find ("exclamatory_mark").GetComponent<SpriteRenderer> ().enabled = true;
				npcs = 2;
				break;
			case "shop":
				GameObject.Find ("exclamatory_mark").GetComponent<SpriteRenderer> ().enabled = true;
				npcs = 3;
				break;
			default:
				break;
			}
		}

		if (col2d.gameObject.name == "FlowerTrap") {
			this.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
			DieByWepon ();
		}
	}

	void OnTriggerExit2D (Collider2D col2d)
	{

		intriggers = false;
		GameObject.Find ("dialogbox").GetComponent<SpriteRenderer> ().enabled = false;
		GameObject.Find ("dialogbox").GetComponent<Animator> ().enabled = false;
		GameObject.Find ("exclamatory_mark").GetComponent<SpriteRenderer> ().enabled = false;
		npcs = 0;
	}

	void OnCollisionEnter2D (Collision2D c2d)
	{
		if (scene == 4) {
			if (c2d.gameObject.tag == "ground") {
				grounded = true;
			}
		}
			


	}

	void OnCollisionExit2D (Collision2D c2d)
	{
		if (scene == 4) {
			if (c2d.gameObject.tag == "ground") {
				grounded = false;
			}
		}

	}

	void DieByWepon ()
	{
		die = true;
		if (Boss) {
			Boss.SendMessage ("HeroDead");
		}
		if (bossdead) {
			return;
		}
		this.shangbanshen.GetComponentInChildren<SpriteRenderer> ().enabled = true;
		this.xiabanshen.GetComponentInChildren<SpriteRenderer> ().enabled = false;
		this.shangbanshen.GetComponent<Animator> ().SetBool ("diebywepon", true);
		GameObject.Find ("Canvas").GetComponent<Canvas> ().enabled = false;
		this.GetComponent<BoxCollider2D> ().enabled = true;
		//this.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
		EndMove ();
	}

	void DieBySoward ()
	{
		die = true;
		if (Boss) {
			Boss.SendMessage ("HeroDead");
		}
		if (bossdead) {
			return;
		}
		this.shangbanshen.GetComponentInChildren<SpriteRenderer> ().enabled = true;
		this.xiabanshen.GetComponentInChildren<SpriteRenderer> ().enabled = false;
		this.shangbanshen.GetComponent<Animator> ().SetBool ("diebysoward", true);
		GameObject.Find ("Canvas").GetComponent<Canvas> ().enabled = false;
		this.GetComponent<BoxCollider2D> ().enabled = true;
		//this.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
		EndMove ();
	}

	void DisableSmallGunAction ()
	{
		this.shangbanshen.SetBool ("Moving", false);
		this.xiabanshen.SetBool ("Moving", false);
	}

	void Win()
	{
		win = true;
		bossdead = true;
		mc.SendMessage ("SetShowTrue");
		this.shangbanshen.GetComponent<SpriteRenderer> ().enabled = false;
		this.xiabanshen.GetComponent<SpriteRenderer> ().enabled = true;
		this.xiabanshen.GetComponent<Animator> ().SetBool ("win", true);
		GameObject.Find ("Canvas").GetComponent<Canvas> ().enabled = false;
		this.GetComponent<BoxCollider2D> ().enabled = true;
		EndMove ();
	}

	void Test()
	{
		test1.SendMessage ("Test");
	}
		
	void BossDead()
	{
		bossdead = true;
	}
		
		
		
}