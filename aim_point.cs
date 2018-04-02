using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aim_point : MonoBehaviour {


	public GameObject missile;

	private Vector3 missileOrginPosititon;

	private List<GameObject> missiles = new List<GameObject>();

	private Animator _animator;
	private SpriteRenderer _sr;

	private GameObject linshiMissile;

	// Use this for initialization
	void Start () {

		linshiMissile = (GameObject)Instantiate (missile);
		linshiMissile.transform.position = missile.transform.position;

		_animator = this.GetComponent<Animator> ();
		_sr = this.GetComponent<SpriteRenderer> ();

		_animator.enabled = false;
		_sr.enabled = false;

		missileOrginPosititon = missile.transform.position;

		missiles.Add (missile);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void DestoryThis()
	{
		_sr.enabled = false;

	}

	void StartShow(string step)
	{
		
		foreach (GameObject go in missiles) {
			if (go.transform.position == missileOrginPosititon) {
				go.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
				go.tag = step;
				missiles.Remove (go);
				//Destroy (go);
				break;
			}
		}


		GameObject newmissile = (GameObject)Instantiate (linshiMissile);
		newmissile.transform.position = missileOrginPosititon;
		missiles.Add (newmissile);
		//missile.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;


		_sr.enabled = true;
		_animator.enabled = true;
		_animator.Play("aim_point",0,0);

	}
}


// 第一步要玩家向前走的前提条件是，后面有追赶物。强行让玩家要一直向前走，而不是走1步就可以停下来。 最后一步要玩家向右走 因为向右是向前
//先计算玩家能走的步数有多少，然后再加其步数上划上可以走的范围，以及哪一步应该发生什么事件
//让玩家按顺序面临单种或多种选择 让单种选择具有欺骗性 引导玩家以为单种选择是能过或者伪装成多种选择 ，迷宫规则
// 因为前面都是用惊吓的手段来，所以下导弹也应该继续惊吓玩家。联动热带丛林的环境，应该是更加阴险的
// 所以采用 (前3后2) （前2后1） 这时候玩家应该开始急躁 因为走了两次 回到了原地 （前20后02）
// 这时候分成了2条路线
// 路线1（走前面的）：(前2后0) 直接过关
// 路线2（走后面的）：（前101后3） （前2后3）