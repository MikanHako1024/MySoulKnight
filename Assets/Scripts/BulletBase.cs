using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletBase : MonoBehaviour {

	public Vector2 direction;
	public float speed = 16f;
	//public int limitTime = 50 * 6;

	public int damage;
	public int criticalChance;

	public bool isStop = false;

	private Rigidbody2D rb;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate() {
		//if (limitTime >= 0) {
		//	DoMove();
		//	limitTime--;
		//}
		//else {
		//	DestroyMe();
		//}
		if (canMove) {
			DoMove();
		}
		//limitTime--;
		//if (limitTime < 0) {
		//	DestroyMe();
		//}
	}

	public void DoMove() {
		Vector2 movement = direction.normalized * speed * Time.fixedDeltaTime;
		rb.MovePosition(rb.position + movement);
	}

	//public bool CanMove() {
	//	return ;
	//}
	public bool canMove { 
		//get { return !isStop && limitTime > 0; }
		get { return !isStop; }
	}

	//private void OnCollisionEnter2D(Collision2D collision) {
	private void OnTriggerEnter2D(Collider2D collider) {
		//Debug.Log(collider.transform.tag);
		switch (collider.transform.tag) {
			//case "wall":
			// 注意区分大小写
			case "Wall":
				OnTriggerWall(collider);
				break;
			case "Enemy":
				OnTriggerEnemy(collider.transform.GetComponent<CharacterBase>());
				break;
			case "Box":
				OnTriggerBox(collider);
				break;
			case "Player":
				break;
			default :
				break;
		};
	}

	private void OnTriggerWall(Collider2D collider) {
		isStop = true;
		//Debug.Log(11);
		//Destroy(this, 2);
		//StartCoroutine("DestroyCoroutine");
		//StartCoroutine("DestroyCoroutine", this);
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		InitSmoke(CreateSmoke());
		// 暂时这样
		//Debug.Log(22);
	}
	
	//IEnumerable DestroyCoroutine(GameObject go) {
	//IEnumerable DestroyCoroutine() {
	//	yield return 1;
	//	//Destroy(go);
	//	Destroy(this);
	//	Debug.Log(22);
	//}

	private void OnTriggerEnemy(CharacterBase enemy) {

	}

	private void OnTriggerBox(Collider2D collider) {

	}

	//public void OnHurt() { }
	
	public void DestroyMe() {
		isStop = true;
		gameObject.SetActive(false);
		GameObject.Destroy(this);
		Debug.Log("destroy me");
	}
	
	//public UnityEngine.Events.UnityEvent e1;
	//public UnityEngine.Events.UnityEvent<int> e2;
	
	public UnityEvent onCreate;
	public UnityEvent onUpdate;
	public UnityEvent onTrigWall;
	//public UnityEvent onTrigEnemy;
	//public UnityEvent onTrigBox;
	//public UnityEvent onTrigPlayer;
	public UnityEvent onDestory;

	public GameObject smokePrefab;
	
	public GameObject CreateSmoke() {
		GameObject smoke = Instantiate(smokePrefab);
		smoke.transform.SetParent(null);
		return smoke;
	}

	public void InitSmoke(GameObject smoke) {
		smoke.transform.position = transform.position;
		smoke.GetComponent<Animator>().SetTrigger("start");
	}
}
