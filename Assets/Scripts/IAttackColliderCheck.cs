using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAttackColliderCheck {
	
	private Transform transform;
	private Collider2D collider;

	public IAttackColliderCheck(Transform tf) {
		transform = tf;
	}
	public void Start() {
		collider = transform.GetComponent<Collider2D>();
	}

	public void test1() {
		//RaycastHit2D[] hits;
		//collider.Cast(transform.eulerAngles, hits);
	}
}
