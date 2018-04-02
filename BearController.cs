using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour {


	private Animator _animator;
	private SpriteRenderer _sp;

	private Vector3 move_pos;
	private Vector3 first_pos;

	private bool attack;

	private GameObject _enemy;

	// Use this for initialization
	void Start () {

		attack = false;

		_animator = this.GetComponent<Animator> ();
		_sp = this.GetComponent<SpriteRenderer> ();

		move_pos = new Vector3 (this.transform.position.x - 1.0f, this.transform.position.y, this.transform.position.z);
		first_pos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if(!attack)
		{
		///物体往返代码
		if ((this.transform.position.x > move_pos.x) && (_sp.flipX == true)) {
			this.transform.Translate (new Vector3 (-Time.deltaTime, 0));
		}
		else {
			_sp.flipX = false;
			this.transform.Translate (new Vector3 (Time.deltaTime, 0));

			if (this.transform.position.x >= first_pos.x) {
				_sp.flipX = true;
				this.transform.Translate (new Vector3 (-Time.deltaTime, 0));
			}
		}
		////////////////
		}

		//如果敌人跟熊碰撞了，熊就消灭敌人
	}

	void EndAttack()
	{
		_animator.SetBool ("attack", false);
		this.attack = true;
	}

	void SetAttackFalse()
	{
		attack = false;
	}

	void OnTriggerEnter2D(Collider2D c2d)
	{
		if (c2d.name.ToString ().IndexOf ("bullet") != -1) {
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D c2d)
	{
		//如果碰到的物体是主角的话
		if (c2d.gameObject.tag=="enemy") {
			_enemy = c2d.gameObject;
//			if (this.transform.position.x - c2d.gameObject.transform.position.x >0) {
//				this._sp.flipX = true;
//			}
//			else {
				this._sp.flipX = false;
//			}
			this.attack = true;
			_animator.SetBool("attack",true);

		}

	}

	void EnemyDie()
	{
		if (_enemy) {
			_enemy.SendMessage ("KillByMeleeAttack");
		}
	}
}
