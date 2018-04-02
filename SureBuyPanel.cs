using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SureBuyPanel : MonoBehaviour {

	public GameObject image;
	public GameObject objprice;

	private int price;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}

	public void ShowSurePanel_item1(Sprite _image)
	{
		GameObject.Find ("hint2").GetComponent<RectTransform> ().localScale = new Vector3 (0, 0, 0);
		this.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
		image.GetComponent<Image> ().sprite = _image;
		price = 1000;
		objprice.GetComponent<Text> ().text = "¥" + price.ToString ();
	}

	public void ShowSurePanel_item2(Sprite _image)
	{
		GameObject.Find ("hint2").GetComponent<RectTransform> ().localScale = new Vector3 (0, 0, 0);
		this.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
		image.GetComponent<Image> ().sprite = _image;
		price = 120;
		objprice.GetComponent<Text> ().text = "¥" + price.ToString ();
	}

	public void AddItems()
	{
		HeroController a = GameObject.Find ("Hero").GetComponent<HeroController> ();
		CameraController b = GameObject.Find ("Main Camera").GetComponent<CameraController> ();
		//获得主角和商店，更新前的物品数量，方便之后的添加或减少
		int itemsnum_before = a.itemlist [image.GetComponent<Image> ().sprite.name];
		int itemsnum_before2=0;
		int heromoney_before = a.Hero_money;
		string itemname = "";

		switch (image.GetComponent<Image> ().sprite.name) {
		case "wepon_cold":
			itemname = "冷冻武器";
			break;
		case "shield_invincible":
			itemname = "无敌";
			break;
		}

		List<string> linshilist = b.shopitemlist;
		foreach (string s in linshilist) {
			string[] ss = s.Split (':');
			if (ss [0] == image.GetComponent<Image> ().sprite.name) {
				itemsnum_before2 = int.Parse (ss [1]);
				break;
			}
		}

		//如果主角的金钱低于价格
		if (a.Hero_money < price) {
			GameObject.Find ("hint2").GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
		} 
		else {
			a.Hero_money -= price;
			//如果这个道具已经存在至少一个的话
			if (a.itemlist.ContainsKey (image.GetComponent<Image> ().sprite.name)) {
				a.itemlist [image.GetComponent<Image> ().sprite.name]++;
				//发送信息显示提示
				b.showtishi=true;
				b.showtishitext = "购买了：" + itemname;
			} else {
				a.itemlist.Add (image.GetComponent<Image> ().sprite.name, 1);
				//发送信息显示提示
				b.showtishi=true;
				b.showtishitext = "购买了：" + itemname;
			}

			//更新主角状态
			var fileAddress = System.IO.Path.Combine (Application.streamingAssetsPath, "HeroState.txt");  
			string s = File.ReadAllText (fileAddress);
			string ss=
			s.Replace (image.GetComponent<Image> ().sprite.name + "." + itemsnum_before.ToString (), image.GetComponent<Image> ().sprite.name + "." + (itemsnum_before + 1).ToString ());
			string sss=
				ss.Replace ("money:" + heromoney_before.ToString(), "money:" + a.Hero_money.ToString());
			File.WriteAllText (fileAddress, sss);

			//更新商店状态
			var fileAddress2 = System.IO.Path.Combine (Application.streamingAssetsPath, "shop_items_list.txt");  
			string s1 = File.ReadAllText (fileAddress2);
			string ss1=
				s1.Replace (image.GetComponent<Image> ().sprite.name + ":" + itemsnum_before2.ToString (), image.GetComponent<Image> ().sprite.name + ":" + (itemsnum_before2 - 1).ToString ());
			File.WriteAllText (fileAddress2, ss1);

			b.shopitemlist.Clear ();

			this.GetComponent<RectTransform> ().localScale = new Vector3 (0, 0, 0);
		}
	}

	public void ExitPanel()
	{
		
		this.GetComponent<RectTransform> ().localScale = new Vector3 (0, 0, 0);
	}
}
