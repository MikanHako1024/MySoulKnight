using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : 在特定加载时机 统一创建和销毁 ...

// TODO : 所有池刷新的实现，针对长时间未使用的池 ...

// ？用 Component 的话 不支持通过 GameObject 创建 ...
// ？改 Component 为 GameObject ...

// TODO : Get 允许传入position等参数

// ？TODO : 是否应该更自由地接受预设体，允许以某一个或某一类配置创建池 ...

//public class PoolManager {
public class PoolManager : MonoBehaviour {
	// ？是否应该让管理器类继承MonoBehaviour类 ...

	/*
	
	//public static Transform allPoolTf = null;

	//public PoolManager() {
	// ？改用静态块 ...
	//static PoolManager() {
	//	initPoolData();
	//	Debug.Log("初始化池数据");
	//}
	private void Start() {
		initPoolData();
	}


	/*
	#region 池初始化

	[System.Serializable]
	public struct PoolData {
		public Component prefab;
		public int size;
		public Transform poolTf;
		public string name;
	}
	public static PoolData[] poolList;

	public void initPoolList() {
		foreach (PoolData poolData in poolList) {
			if (!poolDict.ContainsKey(poolData.name)) {
				poolDict[poolData.name] = CreatePool(poolData);
			}
		}
	}

	public ObjectPool<Component> CreatePool(PoolData pollData) {
		ObjectPool<Component> pool = new ObjectPool<Component>(pollData.prefab, pollData.size, pollData.poolTf, pollData.name);
		return pool;
	}

	#endregion
	* /
	// ？改 初始时根据数据创建 以后调用方法传入参数动态增加 ...
	// ？为 编辑所有需要用池的预设体及其对应名称等数据 通过名称找池数据创建 ...
	#region 池数据

	[System.Serializable]
	public struct PoolData {
		public string name;
		public Component prefab;
		//public GameObject prefab;
		public int size;
		public Transform poolTf;
	}
	public PoolData[] poolDataList = new PoolData[0];
	public static Dictionary<string, PoolData> poolDataDict = new Dictionary<string, PoolData>();
	
	// 增加 组件到数据名称的字典
	public static Dictionary<Component, string> componentDict = new Dictionary<Component, string>();
	//public static Dictionary<GameObject, string> componentDict = new Dictionary<GameObject, string>();
	// 改成 组件直接到池数据的字典
	//public static Dictionary<Component, PoolData> componentDict = new Dictionary<Component, PoolData>();

	public void initPoolData() {
		DestroyAllPool();
		foreach (PoolData poolData in poolDataList) {
			poolDataDict.Add(poolData.name, poolData);
			//componentDict.Add(poolData.prefab, poolData);
			componentDict.Add(poolData.prefab, poolData.name);
		}
	}

	#endregion

	
	#region 池创建和销毁
	
	public static Dictionary<string, ObjectPool<Component>> poolDict = new Dictionary<string, ObjectPool<Component>>();

	public static void CreatePool(PoolData poolData) {
		//ObjectPool<Component> pool = new ObjectPool<Component>(poolData.prefab, poolData.size, poolData.poolTf, poolData.name);
		ObjectPool<Component> pool = new ObjectPool<Component>(poolData.prefab, poolData.size, poolData.poolTf, poolData.name);
		poolDict.Add(poolData.name, pool);
	}
	
	public static void CreatePool(string name) {
		if (poolDataDict.ContainsKey(name)) {
			CreatePool(poolDataDict[name]);
		}
	}

	public static bool DestroyPool(string name) {
		if (poolDict.ContainsKey(name)) {
			//poolDict[name].RemoveAllContainer();
			poolDict.Remove(name);
			return true;
		}
		else {
			return false;
		}
	}

	public static void DestroyAllPool() {
		foreach (string name in poolDict.Keys) {
			DestroyPool(name);
		}
	}

	#endregion


	#region 池刷新

	public static float refreshTime = -1f;

	public static void RefreshPools() {
		if (refreshTime > 0f) {
			// ...
		}
	}

	#endregion


	#region 对象获取和释放

	//public static Dictionary<Component, string> objectDict = new Dictionary<Component, string>();
	public static Dictionary<GameObject, string> objectDict = new Dictionary<GameObject, string>();

	public static Component Get(string name) {
		if (poolDict.ContainsKey(name)) {
			Component comp = poolDict[name].Get();
			//objectDict.Add(comp, name);
			objectDict.Add(comp.gameObject, name);
			return comp;
		}
		else if (poolDataDict.ContainsKey(name)) {
			CreatePool(name);
			//return Get(name);
			Component comp = poolDict[name].Get();
			//objectDict.Add(comp, name);
			objectDict.Add(comp.gameObject, name);
			return comp;
		}
		else {
			return null;
		}
	}
	/*
	public static Component Get(Component comp) {
		//string msg1 = poolDataDict.ContainsKey("smoke1") ? poolDataDict["smoke1"].prefab.name : "null";
		//string msg2 = poolDataDict.ContainsKey("smoke1") ? (poolDataDict["smoke1"].prefab == comp ? "T" : "F") : "null";
		//Debug.Log(comp.name + ", " + poolDataDict.Count + ", " + msg1 + ", " + msg2);
		// ？拖入相同的预设体 但是预设体引用不相同 ...

		if (componentDict.ContainsKey(comp)) {
			return Get(componentDict[comp]);
		}
		else {
			//return null;
			return GameObject.Instantiate(comp);
		}
	}
	* /
	// ？方法泛型 ...
	/*
	public static T Get<T>(string name) where T : Component {
		if (poolDict.ContainsKey(name)) {
			T comp = poolDict[name].Get();
			objectDict.Add(comp, name);
			return comp;
		}
		else if (poolDataDict.ContainsKey(name)) {
			CreatePool(name);
			return Get<T>(name);
		}
		else {
			return null;
		}
	}
	
	public static T Get<T>(T comp) where T : Component {
		if (componentDict.ContainsKey(comp)) {
			return Get<T>(componentDict[comp]);
		}
		else {
			return null;
		}
	}
	* /
	
	public static Component Get(Component comp) {
		if (componentDict.ContainsKey(comp)) {
			return Get(componentDict[comp]);
		}
		else {
			return GameObject.Instantiate(comp);
		}
	}

	/*
	public static bool Release(Component item) {
	//public static bool Release(GameObject item) {
		if (objectDict.ContainsKey(item)) {
			poolDict[objectDict[item]].Release(item);
			objectDict.Remove(item);
			return true;
		}
		else {
			// 直接释放
			item.gameObject.SetActive(false);
			GameObject.Destroy(item); // ？...
			return false;
		}
	}
	* /
	public static bool Release(Component item) {
		GameObject go = item.gameObject;
		if (objectDict.ContainsKey(go)) {
			poolDict[objectDict[go]].Release(item);
			objectDict.Remove(go);
			return true;
		}
		else {
			go.SetActive(false);
			GameObject.Destroy(go); // ？...
			return false;
		}
	}

	// ？怎么直接获得 对应 脚本 ...

	// ？使用泛型方法，可以在调用的时候选择需要的类型 ...
	// ？而在储存时不考虑其准确类型 ...

	//public static T Get<T>(T prefab) where T : Component {
	//	if (poolDict.ContainsKey(prefab)) {
	//		...
	//}

	#endregion
	*/

	// ？找不到引用 ...
	// ？因为 在转换组件类型的时候 变成了 transform ...
	
	// ？怎么直接获得 对应 脚本 ...
	// ？使用泛型方法，可以在调用的时候选择需要的类型 ...
	// ？而在储存时不考虑其准确类型 ...

	// ？TODO : 改成 传入任意预设体创建对应池 的方式 ...
	
	// ？TODO : 想个办法 省略transform ...
	

	/*
	private void Start() {
		initPoolData();
	}

	#region 池数据
	
	[System.Serializable]
	public struct PoolData {
		public string name;
		public Component prefab;
		//public GameObject prefab;
		public int size;
		public Transform poolTf;
	}
	public PoolData[] poolDataList = new PoolData[0];
	public static Dictionary<Component, PoolData> prefabPoolDataDict = new Dictionary<Component, PoolData>();
	
	public void initPoolData() {
		DestroyAllPool();
		foreach (PoolData poolData in poolDataList) {
			prefabPoolDataDict.Add(poolData.prefab, poolData);
		}
	}

	#endregion
	*/
	// ？使用Component类型接收预设体 得到的是预设体的Transform组件 ...
	// ？这样会丢失原来需要的组件类型 ...
	// ？所以不再配置每个预设体的池参数 ...

	// ？TODO : 是否可以配置一些预设体的类型，每种类型一组参数 ...

	#region 池创建和销毁

	public static Dictionary<Component, ObjectPool<Component>> poolDict = new Dictionary<Component, ObjectPool<Component>>();
	
	public static List<ObjectPool<Component>> poolList = new List<ObjectPool<Component>>();

	public static void CreatePool<T>(T prefab) where T : Component {
		if (!poolDict.ContainsKey(prefab)) {
			//ObjectPool<T> pool = new ObjectPool<T>(prefab);
			ObjectPool<Component> pool = new ObjectPool<Component>(prefab);
			poolDict.Add(prefab, pool);

			poolList.Add(pool);
		}
	}
	
	public static bool DestroyPool(Component prefab) {
		if (poolDict.ContainsKey(prefab)) {
			poolDict.Remove(prefab);
			// ？这样是否可以等待其被自动清理 ...
			return true;
		}
		else {
			return false;
		}
	}

	public static void DestroyAllPool() {
		poolDict.Clear();
		// ？这样是否可以等待其被自动清理 ...
	}

	#endregion


	#region 池获取

	/*
	public static ObjectPool<T> GetPool<T>(T prefab) where T : Component {
		CreatePool(prefab);
		//if (poolDict.ContainsKey(prefab)) {
		//	return poolDict[prefab] as ObjectPool<T>;
		//}
		if (poolDict.ContainsKey(prefab.transform)) {
			//return poolDict[prefab.transform] as ObjectPool<T>;
			return poolDict[prefab.transform];
		}
		else {
			return null;
		}
	}
	*/
	public static ObjectPool<Component> GetPool(Component prefab) {
		CreatePool(prefab);
		if (poolDict.ContainsKey(prefab)) {
			return poolDict[prefab];
		}
		else {
			return null;
		}
	}

	#endregion


	#region 对象获取和释放

	public static Dictionary<GameObject, ObjectPool<Component>> objectDict = new Dictionary<GameObject, ObjectPool<Component>>();
	// ？记录通过池获取的对象 ...
	// ？是否应该映射到对象对应的池，是否会出现池被摧毁但仍有映射的情况 ...
	
	public static T Get<T>(T prefab) where T : Component {
		//ObjectPool<T> pool = GetPool<T>(prefab);
		ObjectPool<Component> pool = GetPool(prefab);
		if (pool != null) {
			//T item = pool.Get(); 
			// ？缺少显式转换 ...
			// ？可以 item as T ...
			// ？TODO : 为何 ...
			Component item = pool.Get();
			//objectDict.Add(item.gameObject, prefab);
			//objectDict.Add(item.gameObject, pool);
			//objectDict.Add(item.gameObject, pool as ObjectPool<Component>);
			objectDict.Add(item.gameObject, pool);
			//return item;
			return item as T;
		}
		else {
			//return GameObject.Instantiate(prefab);
			return GameObject.Instantiate(prefab) as T;
		}
	}
	
	public static bool Release(Component item) {
		GameObject go = item.gameObject;
		if (objectDict.ContainsKey(go)) {
			ObjectPool<Component> pool = objectDict[go];
			pool.Release(item);
			objectDict.Remove(go);
			return true;
		}
		else {
			// 直接释放
			item.gameObject.SetActive(false);
			GameObject.Destroy(item); // ？...

			//foreach (ObjectPoolContainer<Component> c in poolList[0].unUsedStack.ToArray()) {
			//	if (c.item == item) {
			//		Debug.Log("Y");
			//	}
			//	else {
			//		Debug.Log("N");
			//	}
			//}

			return false;

			// ？TODO : 需要防止同一个对象被多次释放 ...
		}
	}

	#endregion

}
