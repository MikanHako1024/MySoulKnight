using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ？TODO : 是否要 在对象容器中保存 所在池的名称或引用 ...

public class ObjectPoolContainer<T> where T : Component {
		// ？Component 还是 MonoBehaviour ...

	public T item { get; set; }
	private bool isUsed = false;

	public ObjectPoolContainer(T item) {
		this.item = item;
		isUsed = false;
	}
	
	public T Consume() {
		isUsed = true;
		item.gameObject.SetActive(true);
		return item;
	}

	public void Release() {
		isUsed = false;
		item.gameObject.SetActive(false);
	}
}
