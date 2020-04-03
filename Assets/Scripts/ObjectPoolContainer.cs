using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolContainer<T> : MonoBehaviour {

	public T item { get; set; }
	private bool isUsed = false;

	public ObjectPoolContainer(T item) {
		this.item = item;
		isUsed = false;
	}
	
	//public void Consume() {
	//	isUsed = true;
	//}
	public T Consume() {
		isUsed = true;
		return item;
	}

	public void Release() {
		isUsed = false;
		// ...
	}
}
