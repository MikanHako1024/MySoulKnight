using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour {
	
	public int maxHealth;
	public int curHealth;
	
	public float moveSpeed;
	
	[System.NonSerialized]
	public Vector3 moveDirection;
	
	protected Animator anim;

	[System.NonSerialized]
	public float currSpeed;

	[System.NonSerialized]
	public Vector3 currDirection;


	protected void Start() {
		anim = GetComponent<Animator>();
	}


	//protected void Update() {
	//	UpdateAnimation();
	//}
	
	protected void UpdateAnimation() {
		if (anim != null) {
			anim.SetFloat("speed", moveDirection.magnitude);

			if (moveDirection.x < 0) {
				anim.SetTrigger("leftSide");
			}
			else if (moveDirection.x > 0) {
				anim.SetTrigger("rightSide");
			}
		}
	}


	protected void FixedUpdate() {
		UpdateAnimation();
		FixedUpdateMove();
	}

	protected void FixedUpdateMove() {
		//Debug.Log(moveDirection + ", " + moveSpeed);
		//DoMove(moveDirection, moveSpeed);
		// ？分为基础值和当前值 需要修改的时候不必重写方法 只需要对当前值赋值 ...
		DoMove(currDirection, currSpeed);
	}

	public void DoMove(Vector3 move, float speed) {
		Rigidbody rb = GetComponent<Rigidbody>();
		if (rb != null && move != Vector3.zero) {
			//rb.MovePosition(move.normalized * speed * Time.fixedDeltaTime);
			// ？移动到 而不是 相对移动 ...
			rb.MovePosition(rb.position + move.normalized * speed * Time.fixedDeltaTime);
		}
	}
}
