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

public class CameraController : MonoBehaviour {

	public GameObject targetGameobject; 

	public GameObject npc2_stand;

	public GameObject chicken;

	public Canvas entergame;
	public Image entergame_image;

	public int npcs; 

	public GameObject _nametext;
	public GameObject _nametext2;
	public GameObject _storytext;
	public GameObject herotalk;
	public GameObject talkobj;
	public float textspeed = 1;

	public GameObject item1_num;
	public GameObject item2_num;

	public GameObject tishiobj;

	private float camera_offset = 1.5f;

	private int totalpagesfor_firsttime;
	private int totalpagesfor_secondtime;

	private bool goNextpage;
	private bool endwrite;

	private Vector3 chicken_pos;
	private Vector3 chicken_firstpos;
	private int currenttimes;
	private int currentpage;
	private List<string> converstation_soilders_phone = new List<string> ();
	private string nametext="";
	private string storytext = "";
	private bool writing;
	private string sheltertext = "<color=#FFFFFF00>";
	private float testfloat = 0;
	private int storytextlength=0;
	private int currentplaceholder=1;    //当前占位符在哪个位置，因为第一次插入就需要在1的位置
	private float nametextPosX;
	private bool endConversation;

	[HideInInspector]
	public bool showtishi;

	[HideInInspector]
	public string showtishitext;

	[HideInInspector]
	public List<string> shopitemlist = new List<string> ();

	[HideInInspector]
	public bool needtorefresh;

	protected bool isziluolanDestory=true; 

	// Use this for initialization
	void Start ()
	{
		Screen.SetResolution (1024, 600, true);

		chicken_pos = new Vector3 (chicken.transform.position.x - 1.0f, chicken.transform.position.y, chicken.transform.position.z);
		chicken_firstpos = chicken.transform.position;
		npcs = 0;
		currenttimes = 1;
		currentpage = 1;
		writing = false;
		goNextpage = false;
		endwrite = false;
		endConversation = false;
		needtorefresh = false;
		showtishi = false;
		showtishitext = "";

		nametextPosX = _nametext.transform.position.x;

		StartCoroutine (LoadText ());

//		var fileAddress = System.IO.Path.Combine (Application.streamingAssetsPath, "converstation_soilders_phone.txt");  
//		FileInfo fInfo0 = new FileInfo (fileAddress);  
//		if (fInfo0.Exists) {  
//			StreamReader sr = new StreamReader (fileAddress);  
//			string s="";
//			while (true){
//				s = sr.ReadLine ();
//				if (s != "end") {
//					converstation_soilders_phone.Add (s);
//				}
//				else {
//					break;
//				}
//
//			}
//			sr.Close ();
//		}
		//test.text = converstation_soilders_phone.Count.ToString ();
	}

	
	// Update is called once per frame
	void Update () {


		///物体往返代码
		if ((chicken.transform.position.x > chicken_pos.x) && (chicken.GetComponent<SpriteRenderer> ().flipX == false)) {
				chicken.transform.Translate (new Vector3 (-Time.deltaTime, 0));
		}
		else {
			chicken.GetComponent<SpriteRenderer> ().flipX = true;
			chicken.transform.Translate (new Vector3 (Time.deltaTime, 0));

			if (chicken.transform.position.x >= chicken_firstpos.x) {
				chicken.GetComponent<SpriteRenderer> ().flipX = false;
				chicken.transform.Translate (new Vector3 (-Time.deltaTime, 0));
			}
		}
		////////////////







			if (showtishi && (showtishitext != "")) {
				tishiobj.SetActive (true);
				updatetishitext (showtishitext,0.5f);
			}


			if ((this.transform.position.x >= -5.35f) && (this.transform.position.x <= 5.35f)) {

				if (targetGameobject.transform.position.x <= -5.35f) {
					this.transform.position = new Vector3 (-5.35f, 0, -10);
				}
				else if (targetGameobject.transform.position.x >= 5.35f) {
					this.transform.position = new Vector3 (5.35f, 0, -10);
				}
				else
				{
					this.transform.position = new Vector3 (targetGameobject.transform.position.x, 0, -10);
				}
			}

			switch (npcs) {
			case 0:
				//不触发任何事件
				break;
			case 1:
				//跟聊电话的士兵互动
				if (talkobj.GetComponent<Image> ().sprite == null) {
					talkobj.GetComponent<Image> ().sprite = GameObject.Find ("soilders_phone").GetComponent<SpriteRenderer> ().sprite;
				}

				if (endConversation) {
					//在对话第一次结束的时候，应该要显示已接到任务
					if (currenttimes == 1) {
						showtishi = true;
						showtishitext = "接到任务：捕获鸟蛋";
					}
					GameObject.Find ("Canvas").GetComponent<Canvas> ().enabled = true;
					GameObject.Find ("SayDialog").GetComponent<Canvas> ().enabled = false;
					GameObject.Find ("soilders_phone").GetComponent<Animator> ().enabled = true;
					currenttimes++;
					currentpage = 1;
					npcs = 0;

				} 
				else {
					SpeakWithSoilderPhone ();
				}

				break;

		case 2:
				//进入故事模式
			EnterGame ("");
			npcs = 0;
				break;

			case 3:
				//商店交易
				ShowShopMenu();
				break;
			}
				

			

	}

	void OnGUI()
	{
		//GUILayout.Label ("asdasdasd");
	}
		

	void SpeakWithSoilderPhone()
	{

		if ((currenttimes == 3) && (!isziluolanDestory)) {
			currenttimes = 2;
		}

		//电脑端
		if (Input.GetMouseButtonDown(0)) {
		//手机端
		//if (Input.GetTouch(0).phase==TouchPhase.Began) {
			if (endwrite) {
				goNextpage = true;
				currentpage++;
			} 
			else if(!endwrite && writing){
				currentplaceholder = (storytextlength - 7);
				_storytext.GetComponent<Text> ().text = _storytext.GetComponent<Text> ().text.Replace (sheltertext, "");
				_storytext.GetComponent<Text> ().text = _storytext.GetComponent<Text> ().text.Replace ("</color>", "");
				endwrite = true;
				writing = false;
			}
		}

		if (!endwrite) {

			goNextpage = false;
		} else{
		}

		if (goNextpage) {
			//到下一页就要先清除显示名字和内容，然后状态切换为进入了下一页，开始判断是第几页第几次对话
			_storytext.GetComponent<Text> ().text = "";
			_nametext.GetComponent<Text> ().text = "";
			nametext = "";
			storytext = "";
			currentplaceholder = 1;
			goNextpage = false;
			endwrite = false;
		}

		if(!goNextpage){
		//if(false){
			//如果下一页的bool条件未达成，则继续显示文字
			//但首先也要判断目前是显示的是哪一页，第几次对话
			//如果当前次数为1的话，既为第一次对话
			//按设定好的剧本进行配置
			if (!writing && !endwrite) {
				foreach (string s in converstation_soilders_phone) {
					//当名字和内容已经确定的时候，不应该再循环这个列表了，先显示完这堆文字，等玩家再次触摸，才进行下一页的搜索
					if ((nametext != "") && (storytext != "")) {
						writing = true;
						endwrite = false;

						if (nametext == "士兵") {
							_nametext2.GetComponent<Text> ().text = nametext;
							_nametext.GetComponent<Text> ().text = "";
							_nametext.transform.position = new Vector3 (nametextPosX, _nametext.transform.position.y, _nametext.transform.position.z);
							talkobj.GetComponent<Image> ().enabled = true;
							herotalk.GetComponent<Image> ().enabled = false;
						}
						if (nametext == "克拉克") {
							_nametext.GetComponent<Text> ().text = nametext;
							_nametext2.GetComponent<Text> ().text = "";
							_nametext.transform.position = new Vector3 (nametextPosX, _nametext.transform.position.y, _nametext.transform.position.z);
							talkobj.GetComponent<Image> ().enabled = false;
							herotalk.GetComponent<Image> ().enabled = true;
						}
						break;
					}


					if ((nametext != "") && (s.Substring(0,1) == "c")) {
						storytext = s.Substring (1);
						continue;
					}

					if (currenttimes.ToString () == s.Substring (0, 1)) {
						if (currentpage.ToString () == s.Substring (1, 1)) {
							nametext = s.Substring (2);
							continue;
						}
						if (s.Substring (1) == "end") {
							endConversation = true;
							GameObject.Find ("dialogbox").GetComponent<SpriteRenderer> ().enabled = true;
							GameObject.Find ("dialogbox").GetComponent<Animator> ().enabled = true;
							break;
						}
					}

				}
			}
			//如果名字和内容都确定了，就可以开始写了(writring = true)
			 if(writing){
				ShowText ();
			}


			////////
		}

		//ChangePage (goNextpage, 10, 5, "soilders_phone");

		GameObject.Find ("ControllerUI").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("SayDialog").GetComponent<Canvas> ().enabled = true;
		GameObject.Find ("soilders_phone").GetComponent<Animator>().enabled =false;
	}

	void ShowText()
	{
		if ((_nametext.GetComponent<Text> ().text == "") || (_storytext.GetComponent<Text> ().text == "")) {
			//_nametext.GetComponent<Text> ().text = nametext;
			_nametext.GetComponent<Text> ().color = Color.blue;
			_nametext2.GetComponent<Text> ().color = Color.blue;
			_storytext.GetComponent<Text> ().text = storytext;
			storytextlength = storytext.Length - sheltertext.Length;
		}

		testfloat += 0.1f;
		if (testfloat >= 1.0f) {
			testfloat = 0;

			if (currentplaceholder < (storytextlength-7)) {
				_storytext.GetComponent<Text> ().text = _storytext.GetComponent<Text> ().text.Replace (sheltertext, "");
				_storytext.GetComponent<Text> ().text = _storytext.GetComponent<Text> ().text.Insert (currentplaceholder, sheltertext);
				currentplaceholder++;
			} 
		}
		if (currentplaceholder == (storytextlength-7)){
			///结束第一页对话
			_storytext.GetComponent<Text> ().text = _storytext.GetComponent<Text> ().text.Replace (sheltertext, "");
			_storytext.GetComponent<Text> ().text = _storytext.GetComponent<Text> ().text.Replace ("</color>", "");
			endwrite = true;
			writing = false;
			return;
		}

		Invoke ("ShowText", 0.1f);
	}

	void ShowShopMenu()
	{

		if (shopitemlist.Count < 1) {
			var fileAddress = System.IO.Path.Combine (Application.streamingAssetsPath, "shop_items_list.txt");  
			FileInfo fInfo0 = new FileInfo (fileAddress);  
			if (fInfo0.Exists) {  
				StreamReader r = new StreamReader (fileAddress);  
				string s = "";
				while (true) {
					s = r.ReadLine ();
					if (s != "end") {
						shopitemlist.Add (s);
					} else {
						break;
					}
				}
				r.Close ();
			}
			needtorefresh = true;
		}
		else {
			if (needtorefresh) {
				foreach (string s in shopitemlist) {

					string[] ss = s.Split (':');

					if (ss [0] == "wepon_cold") {
						if (ss [1] == "0") {
							GameObject.Find ("wepon_cold").GetComponent<Image> ().enabled = false;
							item1_num.GetComponent<Text> ().enabled = false;
						} 
						else {
							GameObject.Find ("wepon_cold").GetComponent<Image> ().enabled = true;
							item1_num.GetComponent<Text> ().enabled = true;
							item1_num.GetComponent<Text> ().text = ss [1];
						}
					}

					if (ss [0] == "shield_invincible") {
						if (ss [1] == "0") {
							GameObject.Find ("shield_invincible").GetComponent<Image> ().enabled = false;
							item2_num.GetComponent<Text> ().enabled = false;
						}
						else {
							GameObject.Find ("shield_invincible").GetComponent<Image> ().enabled = true;
							item2_num.GetComponent<Text> ().enabled = true;
							item2_num.GetComponent<Text> ().text = ss [1];
						}
					}


				}
				needtorefresh = false;
			}



		}

		GameObject.Find ("ControllerUI").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("Shop").GetComponent<Canvas> ().enabled = true;


			
	}

	public void ExitShopMenu()
	{
		GameObject.Find ("ControllerUI").GetComponent<Canvas> ().enabled = true;
		GameObject.Find ("Shop").GetComponent<Canvas> ().enabled = false;
		npcs = 0;
	}

	public void updatetishitext(string showtext,float speed)
	{
		tishiobj.GetComponentInChildren<Text> ().text = showtext;
		tishiobj.GetComponent<CanvasGroup> ().alpha += (Time.deltaTime * speed);
		if (tishiobj.GetComponent<CanvasGroup> ().alpha >= 1.0f) {
			tishiobj.GetComponent<CanvasGroup> ().alpha = 0;
			showtishitext = "";
			showtishi = false;
			tishiobj.SetActive (false);
		}
	}

	public void EnterGame(string s)
	{
		entergame.enabled = true;
		if (s == "y") {
			entergame_image.enabled = true;
			EnterGameImage ();
			//SceneManager.LoadScene ("Mission1");
		}
		if (s == "n") {
			entergame.enabled = false;
		}
	}

	void EnterGameImage()
	{
		if (entergame_image.GetComponent<CanvasGroup> ().alpha >= 0.9f) {
			SceneManager.LoadScene ("Mission1");
			return;
		}
		entergame_image.GetComponent<CanvasGroup> ().alpha += 0.1f;
		Invoke ("EnterGameImage", 0.1f);
	}

	IEnumerator LoadText()
	{
		WWW ww = new WWW ("jar:file://" + Application.dataPath + "!/assets/" + "converstation_soilders_phone.txt");
		yield return ww;
		if (ww.text!="") {  
			StringReader sr = new StringReader (ww.text);
			string s="";
			while (true){
				s = sr.ReadLine ();
				if (s != "end") {
					converstation_soilders_phone.Add (s);
				}
				else {
					break;
				}

			}
			sr.Close ();
		}
	}
		
}
