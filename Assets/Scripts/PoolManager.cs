using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager {

	public float refreshTime = -1f;

	public Transform allPoolTf = null;
	
	//public Dictionary<string, Component>
	public Dictionary<string, ObjectPool<Component>> poolDict;

	// ？使用 ObjectPoolContainer<T> 的方式 无法从对象获取容器、池等信息 ...
	// ？所以需要有 对象到池的字典 ...

	public void RefreshPools() {
		
	}

	public Component Get(string name) {
		return poolDict[name].Get();
	}

	public bool Release(Component item) {
		// if 在池内 
		//    池释放
		// else
		//     直接销毁

		return true;
	}
}
