using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public Vector3 targetOffset;
	
	private Camera mainCamera;
	private Transform cameraTf { 
		get { 
			return mainCamera ? mainCamera.transform : null; 
		} 
	}
	
	// 暂时这样
	// ？镜头速度 = 与目标的距离 * 固定比例 ...
	// ？人物速度 / 固定比例 = 最大允许距离 ...
	//public float speedRate = 2f;
	
	// ？为了让镜头能平滑，即要让目标在镜头里的相对位置平滑 ...
	// ？需要让相对位置 ...

	// ？移动输入也会让镜头移动 ...
	// ？与角色位置共同决定移动方向和速度 ...
	// ？...
	public float speedRateDist = 2f;
	public float speedRateAxis = 6f;

	private void Start () {
		mainCamera = GetComponent<Camera>();
		//cameraTf = mainCamera.transfrom;
		// ？改 获取后存入 为 属性的get方法 ...
	}
	
	private void FixedUpdate () {
		if (CheckValid()) {
			cameraTf.position = cameraTf.position + CameraMovement() * Time.fixedDeltaTime;
		}
	}

	public bool CheckValid() {
		return target != null && mainCamera != null;
	}

	public Vector3 TargetPosition() {
		return target.position + targetOffset;
	}

	public Vector3 CameraMovement() {
		//return (TargetPosition() - cameraTf.position) * speedRate;
		//Vector3 dist = TargetPosition() - cameraTf.position;
		//return dist * speedRate;
		Vector3 movement = speedRateDist * (TargetPosition() - cameraTf.position);
		movement += targetOffset;
		//movement += speedRateAxis * InputManager.axisInput;
		movement += speedRateAxis * InputManager.axisInput.normalized;
		return movement;
	}
}
