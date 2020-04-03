﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class ObjectPool<T> {
public class ObjectPool<T> where T : Component {
	
	//private GameObject prefab { get; set; }
	private T prefab { get; set; }

	public int defaultSize = 0;
	public int currentSize = 0;
	
	private Stack<ObjectPoolContainer<T>> unUsedStack;
	private Dictionary<T, ObjectPoolContainer<T>> usedDict;

	public string poolName { get; set; }
	public Transform parentTf = null; // ？null表示在场景下 ...
	
	//public ObjectPool(GameObject prefab, int size = 10) {
	public ObjectPool(T prefab, int size = 10) {
		this.prefab = prefab;
		defaultSize = size;
		IncreContainer(defaultSize);
	}

	public void InitConfig(string poolName, Transform parentTf = null) {
		this.poolName = poolName;
		this.parentTf = parentTf;
	}

	private void IncreContainer(int size = 1) {
		for (int i = 0; i < size; i++) {
			unUsedStack.Push(CreateContainer());
			currentSize++;
		}
	}
	
	private ObjectPoolContainer<T> CreateContainer() {
		return new ObjectPoolContainer<T>(CreateInstantiate());
	}
	//private ObjectPoolContainer<GameObject> CreateContainer() {
	//	return new ObjectPoolContainer<GameObject>(CreateInstantiate());
	//}
	
	//private GameObject CreateInstantiate() {
	//	GameObject returnObj;
	//	returnObj = GameObject.Instantiate<GameObject>(prefab);
	//		// ？需要 where T : Component ...
	//	returnObj.transform.SetParent(parentTf);
	//	returnObj.SetActive(false);
	//	return returnObj;etActive(false);
	//	return returnObj;
	//}
	private T CreateInstantiate() {
		T returnObj;
		returnObj = GameObject.Instantiate<T>(prefab); 
		returnObj.transform.SetParent(parentTf);
		returnObj.gameObject.SetActive(false);
		return returnObj;
	}

	public T Get(Vector3 pos, Vector3 rot) {
		if (unUsedStack.Count <= 0) IncreContainer();
		ObjectPoolContainer<T> container = unUsedStack.Pop();
		T returnObj = container.Consume();
		usedDict.Add(returnObj, container);
		returnObj.transform.position = pos;
		returnObj.transform.eulerAngles = rot;
		return returnObj;
	}

	public bool Release(T item) {
		if (usedDict.ContainsKey(item)) {
			ObjectPoolContainer<T> container = usedDict[item];
			container.Release();
			usedDict.Remove(item);
			unUsedStack.Push(container);
			return true;
		}
		else {
			return false;
		}
	}
}