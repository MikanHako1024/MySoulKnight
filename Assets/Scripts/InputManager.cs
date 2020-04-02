using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	
	//public KeyCode keyUp = KeyCode.UpArrow;
	//public KeyCode keyLeft = KeyCode.LeftArrow;
	//public KeyCode keyDown = KeyCode.DownArrow;
	//public KeyCode keyRight = KeyCode.RightArrow;

	public KeyCode keyAttack = KeyCode.Z;
	public KeyCode keySkill = KeyCode.C;

	[System.NonSerialized]
	//public Vector3 axisInput;
	//public bool keyDownAttack;
	//public bool keyDownSkill;
	// 不用 getter ...

	// ？改用静态成员 ...
	// ？可以在实例里 直接用成员名 访问到静态成员 ...
	// ？用实例为静态成员更新 ...
		// TODO : 笔记
	//public static Vector3 axisInput;
	public static Vector2 axisInput;
	public static bool keyDownAttack;
	public static bool keyDownSkill;

	
	//private static void Update () {
		// ？不能自动执行 ...
	/*
	private void Update () {
		axisInput.x = Input.GetAxisRaw("Horizontal");
		axisInput.z = Input.GetAxisRaw("Vertical");
		axisInput.y = 0;
		keyDownAttack = Input.GetKey(keyAttack);
		keyDownSkill = Input.GetKey(keySkill);
		//Debug.Log(axisInput + ", " + keyDownAttack + ", " + keyDownSkill);
	}
	*/
	
	private void FixedUpdate () {
		axisInput.x = Input.GetAxisRaw("Horizontal");
		//axisInput.z = Input.GetAxisRaw("Vertical");
		//axisInput.y = 0;
		axisInput.y = Input.GetAxisRaw("Vertical");
		
		axisInput = axisInput.normalized * Mathf.Min(axisInput.magnitude, 1f);

		keyDownAttack = Input.GetKey(keyAttack);
		keyDownSkill = Input.GetKey(keySkill);
		//Debug.Log(axisInput + ", " + keyDownAttack + ", " + keyDownSkill);
	}
}
