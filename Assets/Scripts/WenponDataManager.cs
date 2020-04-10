using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WenponDataManager : MonoBehaviour {

	[SerializeField]
	public static WenponData[] wenponDatas;
	
	public WenponData[] aaa;
	public WenponData bbb;

	[System.Serializable]
	public class WenponData {
		public Sprite[] spriteList = new Sprite[1];
		public int[] bulletList = new int[1];
		
	}
	
	[System.Serializable]
	public delegate void AttackDele();
	
	[SerializeField]
	public void func1() { Debug.Log(1); }
	[SerializeField]
	public void func2() { Debug.Log(2); }
	[System.Runtime.Serialization.OnDeserializedAttribute]
	public void func3() { Debug.Log(3); }
	[System.Runtime.Serialization.OnDeserialized]
	public void func4() { Debug.Log(4); }
	
	public void func5() { Debug.Log(5); }
	public void func6(WenponData w) { Debug.Log(6); }
	public WenponData func7() { Debug.Log(7); return default(WenponData); }
	public WenponData func8(WenponData w) { Debug.Log(8); return default(WenponData); }

	private void Start () {
		
	}
}

// 序列化测试用
/*
public class WenponDataManager : MonoBehaviour {

	[SerializeField]
	public static WenponData[] wenponDatas;
	
	public WenponData[] aaa;
	public WenponData bbb;

	[System.Serializable]
	public class WenponData {
		public Sprite[] spriteList = new Sprite[1];
		public int[] bulletList = new int[1];
		
		[SerializeField]
		public AttackDele attackDele;
		
		[SerializeField]
		public System.Action<int> bbb;
		
		[SerializeField]
		public IEnumerator<AttackDele> ccc;

		[SerializeField]
		public enum ddd { a, b, c }
		
		[SerializeField]
		public UnityEngine.Events.UnityEvent onAttack;
		
		[SerializeField]
		public UnityEngine.Events.UnityAction action1;
		
		[SerializeField]
		public System.Delegate dele2;
		
		[SerializeField]
		public event AttackDele attackDele2;
		
		[SerializeField]
		public UnityEngine.Events.UnityEvent<AttackDele> onAttack2;

		[SerializeField]
		public UnityEngine.Events.UnityEvent<int> onAttack3;

		[SerializeField]
		public UnityEngine.Events.UnityEvent<Vector3> onAttack4;

		[SerializeField]
		public UnityEngine.Events.UnityEvent<WenponData> onAttack5;
		
		[SerializeField]
		public UnityEngine.Events.UnityAction<AttackDele> action2;
		
		[SerializeField]
		public WenponDataManager.AttackDele attackDele3;
		
		[System.Serializable]
		public delegate void Attack2Dele(WenponData w);
		
		[System.Serializable]
		public delegate WenponData Attack3Dele(WenponData w);

		[SerializeField]
		public AttackDele attack2Dele1;

		[SerializeField]
		public AttackDele attack3Dele1;

		[SerializeField]
		public UnityEngine.Events.UnityEvent<Attack2Dele> attack2Dele2;

		[SerializeField]
		public UnityEngine.Events.UnityEvent<Attack3Dele> attack3Dele2;

		public void test1() {
			//onAttack.AddListener(
			//action1.Target
		}
	}
	
	[System.Serializable]
	public delegate void AttackDele();
	
	[SerializeField]
	public void func1() { Debug.Log(1); }
	[SerializeField]
	public void func2() { Debug.Log(2); }
	[System.Runtime.Serialization.OnDeserializedAttribute]
	public void func3() { Debug.Log(3); }
	[System.Runtime.Serialization.OnDeserialized]
	public void func4() { Debug.Log(4); }
	
	public void func5() { Debug.Log(5); }
	public void func6(WenponData w) { Debug.Log(6); }
	public WenponData func7() { Debug.Log(7); return default(WenponData); }
	public WenponData func8(WenponData w) { Debug.Log(8); return default(WenponData); }

	private void Start () {
		
	}
}
*/
