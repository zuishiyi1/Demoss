using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class MissionComplete : MonoBehaviour {

	//y:3.43f
    public GameObject M;//-3.12
    public GameObject i;//-2.72
    public GameObject s;//-2.32
    public GameObject s2;//-1.92

    public GameObject i2;//-1.52
    public GameObject o;//-1.12
    public GameObject n;//-0.72
    public GameObject gantan;//-0.32


	//y:2.81f
	public GameObject C;//
	public GameObject o2;//
	public GameObject m2;//
	public GameObject p;//

	public GameObject l;//
	public GameObject e;//
	public GameObject t;//
	public GameObject e2;//

	public Image black;
	public Text text;


	private bool show;//第一次进屏幕
	private bool show2;//第二次变大并跳出屏幕

	private bool hide;

	private bool flash;
	private int flashTimes;


	// Use this for initialization
	void Start () {
		show = false;
		show2 = false;
		hide = false;
		flash = false;
		flashTimes = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (show) {
			M.transform.localPosition = Vector3.MoveTowards (M.transform.localPosition, new Vector3 (-3.12f, 3.43f, 0),Time.deltaTime*5.0f);
			i.transform.localPosition = Vector3.MoveTowards (i.transform.localPosition, new Vector3 (-2.72f, 3.43f, 0),Time.deltaTime*5.0f);
			s.transform.localPosition = Vector3.MoveTowards (s.transform.localPosition, new Vector3 (-2.32f, 3.43f, 0),Time.deltaTime*5.0f);
			s2.transform.localPosition = Vector3.MoveTowards (s2.transform.localPosition, new Vector3 (-1.92f, 3.43f, 0),Time.deltaTime*5.0f);
			i2.transform.localPosition = Vector3.MoveTowards (i2.transform.localPosition, new Vector3 (-1.52f, 3.43f, 0),Time.deltaTime*5.0f);
			o.transform.localPosition = Vector3.MoveTowards (o.transform.localPosition, new Vector3 (-1.12f, 3.43f, 0),Time.deltaTime*5.0f);
			n.transform.localPosition = Vector3.MoveTowards (n.transform.localPosition, new Vector3 (-0.72f, 3.43f, 0),Time.deltaTime*5.0f);
			gantan.transform.localPosition = Vector3.MoveTowards (gantan.transform.localPosition, new Vector3 (-0.32f, 3.43f, 0),Time.deltaTime*5.0f);
			C.transform.localPosition = Vector3.MoveTowards (C.transform.localPosition, new Vector3 (-3.12f, 2.81f, 0),Time.deltaTime*5.0f);
			o2.transform.localPosition = Vector3.MoveTowards (o2.transform.localPosition, new Vector3 (-2.72f, 2.81f, 0),Time.deltaTime*5.0f);
			m2.transform.localPosition = Vector3.MoveTowards (m2.transform.localPosition, new Vector3 (-2.32f, 2.81f, 0),Time.deltaTime*5.0f);
			p.transform.localPosition = Vector3.MoveTowards (p.transform.localPosition, new Vector3 (-1.92f, 2.81f, 0),Time.deltaTime*5.0f);
			l.transform.localPosition = Vector3.MoveTowards (l.transform.localPosition, new Vector3 (-1.52f, 2.81f, 0),Time.deltaTime*5.0f);
			e.transform.localPosition = Vector3.MoveTowards (e.transform.localPosition, new Vector3 (-1.12f, 2.81f, 0),Time.deltaTime*5.0f);
			t.transform.localPosition = Vector3.MoveTowards (t.transform.localPosition, new Vector3 (-0.72f, 2.81f, 0),Time.deltaTime*5.0f);
			e2.transform.localPosition = Vector3.MoveTowards (e2.transform.localPosition, new Vector3 (-0.32f, 2.81f, 0),Time.deltaTime*5.0f);
			if (flash) {
				Invoke ("FlashText", 1.5f);
				flash = false;
			}
			//show = false;
		}

		if (show2) {

			black.GetComponent<CanvasGroup> ().alpha += 0.01f;
			if (black.GetComponent<CanvasGroup> ().alpha >= 0.8f) {
				text.GetComponent<Text> ().enabled = true;
			}

			M.transform.localPosition = new Vector3 (M.transform.localPosition.x - Time.deltaTime * 1.5f, M.transform.localPosition.y + Time.deltaTime * 1.0f, 0);
			M.transform.localScale = new Vector3 (M.transform.localScale.x + Time.deltaTime * 1.0f, M.transform.localScale.y + Time.deltaTime * 1.0f, 0);

			i.transform.localPosition = new Vector3 (i.transform.localPosition.x - Time.deltaTime * 1.0f, i.transform.localPosition.y + Time.deltaTime * 1.3f, 0);
			i.transform.localScale = new Vector3 (i.transform.localScale.x + Time.deltaTime * 1.0f, i.transform.localScale.y + Time.deltaTime * 1.0f, 0);

			s.transform.localPosition = new Vector3 (s.transform.localPosition.x - Time.deltaTime * 0.5f, s.transform.localPosition.y + Time.deltaTime * 1.6f, 0);
			s.transform.localScale = new Vector3 (s.transform.localScale.x + Time.deltaTime * 1.0f, s.transform.localScale.y + Time.deltaTime * 1.0f, 0);

			s2.transform.localPosition = new Vector3 (s2.transform.localPosition.x - Time.deltaTime * 0.3f, s2.transform.localPosition.y + Time.deltaTime * 1.9f, 0);
			s2.transform.localScale = new Vector3 (s2.transform.localScale.x + Time.deltaTime * 1.0f, s2.transform.localScale.y + Time.deltaTime * 1.0f, 0);

			i2.transform.localPosition = new Vector3 (i2.transform.localPosition.x + Time.deltaTime * 0.3f, i2.transform.localPosition.y + Time.deltaTime * 1.9f, 0);
			i2.transform.localScale = new Vector3 (i2.transform.localScale.x + Time.deltaTime * 1.0f, i2.transform.localScale.y + Time.deltaTime * 1.0f, 0);

			o.transform.localPosition = new Vector3 (o.transform.localPosition.x + Time.deltaTime * 0.5f, o.transform.localPosition.y + Time.deltaTime * 1.6f, 0);
			o.transform.localScale = new Vector3 (o.transform.localScale.x + Time.deltaTime * 1.0f, o.transform.localScale.y + Time.deltaTime * 1.0f, 0);

			n.transform.localPosition = new Vector3 (n.transform.localPosition.x + Time.deltaTime * 1.0f, n.transform.localPosition.y + Time.deltaTime * 1.3f, 0);
			n.transform.localScale = new Vector3 (n.transform.localScale.x + Time.deltaTime * 1.0f, n.transform.localScale.y + Time.deltaTime * 1.0f, 0);

			gantan.transform.localPosition = new Vector3 (gantan.transform.localPosition.x + Time.deltaTime * 1.5f, gantan.transform.localPosition.y + Time.deltaTime * 1.0f, 0);
			gantan.transform.localScale = new Vector3 (gantan.transform.localScale.x + Time.deltaTime * 1.0f, gantan.transform.localScale.y + Time.deltaTime * 1.0f, 0);

			C.transform.localPosition = new Vector3 (C.transform.localPosition.x - Time.deltaTime * 1.5f, C.transform.localPosition.y - Time.deltaTime * 1.0f, 0);
			C.transform.localScale = new Vector3 (C.transform.localScale.x + Time.deltaTime * 1.0f, C.transform.localScale.y + Time.deltaTime * 1.0f, 0);

			o2.transform.localPosition = new Vector3 (o2.transform.localPosition.x - Time.deltaTime * 1.0f, o2.transform.localPosition.y - Time.deltaTime * 1.3f, 0);
			o2.transform.localScale = new Vector3 (o2.transform.localScale.x + Time.deltaTime * 1.0f, o2.transform.localScale.y + Time.deltaTime * 1.0f, 0);

			m2.transform.localPosition = new Vector3 (m2.transform.localPosition.x - Time.deltaTime * 0.5f, m2.transform.localPosition.y - Time.deltaTime * 1.6f, 0);
			m2.transform.localScale = new Vector3 (m2.transform.localScale.x + Time.deltaTime * 1.0f, m2.transform.localScale.y + Time.deltaTime * 1.0f, 0);

			p.transform.localPosition = new Vector3 (p.transform.localPosition.x - Time.deltaTime * 0.3f, p.transform.localPosition.y - Time.deltaTime * 1.9f, 0);
			p.transform.localScale = new Vector3 (p.transform.localScale.x + Time.deltaTime * 1.0f, p.transform.localScale.y + Time.deltaTime * 1.0f, 0);

			l.transform.localPosition = new Vector3 (l.transform.localPosition.x + Time.deltaTime * 0.3f, l.transform.localPosition.y - Time.deltaTime * 1.9f, 0);
			l.transform.localScale = new Vector3 (l.transform.localScale.x + Time.deltaTime * 1.0f, l.transform.localScale.y + Time.deltaTime * 1.0f, 0);

			e.transform.localPosition = new Vector3 (e.transform.localPosition.x + Time.deltaTime * 0.5f, e.transform.localPosition.y - Time.deltaTime * 1.6f, 0);
			e.transform.localScale = new Vector3 (e.transform.localScale.x + Time.deltaTime * 1.0f, e.transform.localScale.y + Time.deltaTime * 1.0f, 0);

			t.transform.localPosition = new Vector3 (t.transform.localPosition.x + Time.deltaTime * 1.0f, t.transform.localPosition.y - Time.deltaTime * 1.3f, 0);
			t.transform.localScale = new Vector3 (t.transform.localScale.x + Time.deltaTime * 1.0f, t.transform.localScale.y + Time.deltaTime * 1.0f, 0);

			e2.transform.localPosition = new Vector3 (e2.transform.localPosition.x + Time.deltaTime * 1.5f, e2.transform.localPosition.y - Time.deltaTime * 1.0f, 0);
			e2.transform.localScale = new Vector3 (e2.transform.localScale.x + Time.deltaTime * 1.0f, e2.transform.localScale.y + Time.deltaTime * 1.0f, 0);
		}
	}

	void SetShowTrue()
	{
		show = true;
		flash = true;
	}

	void SetShow2True()
	{
		show2 = true;
		black.GetComponent<Image> ().enabled = true;
	}

	void FlashText()
	{
		show = false;
		if (flashTimes > 5) {
			Invoke ("SetShow2True", 1.0f);
			return;
		}
		flashTimes++;
		if (hide) {
			M.GetComponent<SpriteRenderer> ().enabled = true;
			i.GetComponent<SpriteRenderer> ().enabled = true;
			s.GetComponent<SpriteRenderer> ().enabled = true;
			s2.GetComponent<SpriteRenderer> ().enabled = true;
			i2.GetComponent<SpriteRenderer> ().enabled = true;
			o.GetComponent<SpriteRenderer> ().enabled = true;
			n.GetComponent<SpriteRenderer> ().enabled = true;
			gantan.GetComponent<SpriteRenderer> ().enabled = true;
			C.GetComponent<SpriteRenderer> ().enabled = true;
			o2.GetComponent<SpriteRenderer> ().enabled = true;
			m2.GetComponent<SpriteRenderer> ().enabled = true;
			p.GetComponent<SpriteRenderer> ().enabled = true;
			l.GetComponent<SpriteRenderer> ().enabled = true;
			e.GetComponent<SpriteRenderer> ().enabled = true;
			t.GetComponent<SpriteRenderer> ().enabled = true;
			e2.GetComponent<SpriteRenderer> ().enabled = true;
			hide = false;
			Invoke ("FlashText", 0.5f);
			return;
		}
		else {
			M.GetComponent<SpriteRenderer> ().enabled = false;
			i.GetComponent<SpriteRenderer> ().enabled = false;
			s.GetComponent<SpriteRenderer> ().enabled = false;
			s2.GetComponent<SpriteRenderer> ().enabled = false;
			i2.GetComponent<SpriteRenderer> ().enabled = false;
			o.GetComponent<SpriteRenderer> ().enabled = false;
			n.GetComponent<SpriteRenderer> ().enabled = false;
			gantan.GetComponent<SpriteRenderer> ().enabled = false;
			C.GetComponent<SpriteRenderer> ().enabled = false;
			o2.GetComponent<SpriteRenderer> ().enabled = false;
			m2.GetComponent<SpriteRenderer> ().enabled = false;
			p.GetComponent<SpriteRenderer> ().enabled = false;
			l.GetComponent<SpriteRenderer> ().enabled = false;
			e.GetComponent<SpriteRenderer> ().enabled = false;
			t.GetComponent<SpriteRenderer> ().enabled = false;
			e2.GetComponent<SpriteRenderer> ().enabled = false;
			hide = true;
			Invoke ("FlashText", 0.5f);
			return;
		}
	}
}
