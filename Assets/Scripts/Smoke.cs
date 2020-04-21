using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour {
	
	//private Animator anim;
	public Animator anim;

	private void Start () {
		anim = GetComponent<Animator>();
		//Debug.Log(anim == null ? "null" : anim.name);
	}

	public void StartEffect() {
		//anim.SetTrigger("start");
		GetComponent<Animator>().SetTrigger("start");
	}

	public void EndEffect() {
		//PoolManager.Release(this);
		if (gameObject.activeSelf) PoolManager.Release(this);
	}

	public void Init(Vector2 position) {
		transform.position = position;
		StartEffect();
	}
}
