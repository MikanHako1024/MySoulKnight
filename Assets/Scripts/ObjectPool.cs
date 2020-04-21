using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ？移除组件 和 设为静态 的效率 ...

// TODO : 池刷新的实现，针对长时间使用的对象，和长时间未使用的对象 ...

// ？TODO : 不需要泛型 直接GameObject ...

public class ObjectPool<T> where T : Component {
	
	public T prefab;
	//public GameObject prefab;

	public int defaultSize = 0;
	public int currentSize = 0;
	
	public Stack<ObjectPoolContainer<T>> unUsedStack = new Stack<ObjectPoolContainer<T>>();
	public Dictionary<T, ObjectPoolContainer<T>> usedDict = new Dictionary<T, ObjectPoolContainer<T>>();
	//private Dictionary<GameObject, ObjectPoolContainer<T>> usedDict = new Dictionary<GameObject, ObjectPoolContainer<T>>();

	public string name;
	public Transform parentTf = null;

	public ObjectPool(T prefab, int size = 10, Transform parentTf = null, string name = "") {
	//public ObjectPool(GameObject prefab, int size = 10, Transform parentTf = null, string name = "") {
		this.prefab = prefab;
		defaultSize = size;
		this.parentTf = parentTf;
		this.name = name;
		currentSize = 0;
		IncreContainer(defaultSize);
	}

	~ObjectPool() {
		RemoveAllContainer();
	}


	#region 容器创建和摧毁

	private void IncreContainer(int size = 1) {
		for (int i = 0; i < size; i++) {
			unUsedStack.Push(CreateContainer());
			currentSize++;
		}
	}
	
	private ObjectPoolContainer<T> CreateContainer() {
		return new ObjectPoolContainer<T>(CreateInstantiate());
	}

	private T CreateInstantiate() {
		T returnObj;
		returnObj = GameObject.Instantiate<T>(prefab); 
		returnObj.transform.SetParent(parentTf);
		returnObj.gameObject.SetActive(false);
		return returnObj;
	}

	
	public void RemoveContainer(int size = 1) {
		for (int i = 0; i < size; i++) {
			ObjectPoolContainer<T> container = unUsedStack.Pop();
			container.Release();
			GameObject.Destroy(container.item);
			currentSize--;
		}
	}

	public void RemoveAllContainer() {
		RemoveContainer(currentSize);
	}

	#endregion
	
	
	//public T temp1;
	//public T temp2;
	//public GameObject temp3;
	//public GameObject temp4;
	//public ObjectPoolContainer<T> temp5;


	#region 对象获取和释放

	public T Get(Vector3 pos = default(Vector3), Vector3 ang = default(Vector3)) {
		if (unUsedStack.Count <= 0) IncreContainer();
		ObjectPoolContainer<T> container = unUsedStack.Pop();
		T returnObj = container.Consume();
		usedDict.Add(returnObj, container);
		//usedDict.Add(returnObj.gameObject, container);
		//returnObj.transform.SetParent(parentTf, true);
		returnObj.transform.position = pos;
		returnObj.transform.eulerAngles = ang;
		
		//temp1 = returnObj;
		// ？GetType() : transform ...
		// ？GameObject.Instantiate<Component>(prefab) 得到的是 transform ...
		//temp3 = returnObj.gameObject;
		//temp5 = container;

		return returnObj;
	}

	// ===  break  202004210343  ===
	// ？统一以 transform 或 gameObject 做标识 ...

	
	public bool Release(T item) {
	//public bool Release(GameObject item) {
	
		//temp2 = item;
		//temp4 = item.gameObject;

		if (usedDict.ContainsKey(item)) {
			ObjectPoolContainer<T> container = usedDict[item];
			container.Release();
			usedDict.Remove(item);
			unUsedStack.Push(container);
			//Debug.Log(item.name + " pool T");
			return true;
		}
		else {
			//Debug.Log(item.name + " pool F");
			return false;
		}
	}

	#endregion


	// TODO
	#region 池刷新

	public static float refreshInterval = 60f;
	
	public void RefreshPool() {

	}

	public void RefreshUseSize() {

	}
	
	#endregion

}
