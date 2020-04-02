using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour {
	
	public Vector2 direction;
	public float speed = 8f;
	public int limitTime = 50 * 6;

	public int damage;
	public int criticalChance;

	private Rigidbody2D rb;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	public void DestroyMe() {
		Debug.Log("destroy me");
	}

	private void FixedUpdate() {
		if (limitTime >= 0) {
			DoMove();
			limitTime--;
		}
		else {
			DestroyMe();
		}
	}

	public void DoMove() {
		Vector2 movement = direction * speed * Time.fixedDeltaTime;
		rb.MovePosition(rb.position + movement);
	}

	//private void OnCollisionEnter2D(Collision2D collision) {}

	public void OnCollideWall() {

	}

	public void OnCollideBox() {

	}

	public void OnCollideEnemy(CharacterBase ememy) {

	}

	//public void OnHurt() { }

}
