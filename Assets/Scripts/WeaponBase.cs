using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// TODO : 拓展出几个接口

public class WeaponBase : MonoBehaviour {

	/*
	[SerializeField]
	public UnityEngine.Events.UnityEvent onAttack;

	public delegate void TryShootDelegate();
	public delegate void DoShootDelegate();
	public delegate bool CanShootDelegate();
	
	public TryShootDelegate TryShootDele;
	public CanShootDelegate CanShootDele;
	public DoShootDelegate DoShootDele;
	
	public float shootFrequency;
	public float lastShootTime;

	public Sprite[] spriteList = new Sprite[1];
	public int[] bulletList = new int[1];

	private void Start () {
		TryShootDele = TryShoot;
		CanShootDele = CanShoot;
		DoShootDele = DoShoot;
	}
	
	void Update () {
		
	}
	
	public void TryShoot() {
		if (CanShootDele()) {
			DoShootDele();
		}
	}

	public bool CanShoot() {
		return true;
	}

	public void DoShoot() {
		Debug.Log("Shoot");
	}
	*/



	//public void AimByTarget(Transform target) {
	//	//AimByDirection(target.position - transform.position);
	//	// ？改用 LookAt方法 ...
	//	transform.LookAt(target);
	//	//aimTarget = target;
	//}
	//
	//public void AimByDirection(Vector2 dir) {
	//	transform.LookAt((Vector2)transform.position + dir);
	//	//aimDirection = dir;
	//	//aimTarget = null;
	//}

	public Transform aimTarget = null;
	//public Vector2 aimDirection;
	//public Vector2 shootDirection {
	//	get {
	//		return aimTarget != null
	//			? (Vector2)aimTarget.position
	//			: aimDirection;
	//	}
	//}
	public Vector2 aimDirection {
		get {
			return aimTarget != null
				? (Vector2)aimTarget.position
				: InputManager.lastAxisInput;
		}
	}

	public void UpdateAim() {
		//transform.LookAt((Vector2)transform.position + aimDirection);
		// ？LookAt是用在3D里的，会 ...
		//transform.rotation =
		//	Quaternion.FromToRotation(new Vector3(1, 0, 0), aimDirection);
		// ？...

		//Quaternion quat = Quaternion.FromToRotation(new Vector3(1, 0, 0), aimDirection);
		// ？Quaternion.FromToRotation(new Vector3(1, 0, 0), new Vector3(-1, 0, 0)).eulerAngles 是 (180,0,0) ...
		//if (90 <= quat.eulerAngles.z && quat.eulerAngles.z < 270) {
		//	Vector3 vec = quat.eulerAngles;
		//	vec = new Vector3(0, 180, 180 - vec.z);
		//	quat.eulerAngles = vec;
		//}
		//else {
		//	Vector3 vec = quat.eulerAngles;
		//	vec = new Vector3(0, 0, vec.z);
		//	quat.eulerAngles = vec;
		//}
		//transform.rotation = quat;

		//float angle = Vector2.Angle(Vector2.right, aimDirection);
		//Debug.Log(angle);
		// ？0 ~ 180
		
		// ？直接以左右区分，因为左右对称 ...
		float angle = Vector2.Angle(Vector2.up, aimDirection);
		Vector3 vec = new Vector3(0, aimDirection.x >= 0 ? 0 : 180, 90 - angle);
		transform.eulerAngles = vec;
	}


	public delegate void ShootDelegate();

	//public UnityEvent doShoot;

	public void DoShoot() {
		DoShootIfCan();
	}


	public BulletBase bulletPrefab;

	public float shootInterval = 0.5f;
	private float lastShootTime = -60f;

	public bool canShoot {
		get {
			return Time.time - lastShootTime > shootInterval;
		}
	}

	// 静态子弹测试用
	//public EmptyBullet bulletPrefab2;

	public void DoShootIfCan() {
		if (canShoot) DoShootWithoutIf();
	}

	public void DoShootWithoutIf() {
		lastShootTime = Time.time;
		InitBullet(CreateBullet(bulletPrefab));
	}

	public BulletBase CreateBullet(BulletBase prefab) {
		BulletBase bullet = Instantiate<BulletBase>(prefab);
		//bullet.transform.SetParent(null);
		return bullet;
	}

	public void InitBullet(BulletBase bullet) {
		//bullet.transform.position = transform.position;
		bullet.transform.position = muzzleTf.position;
		//bullet.direction = 
		//	lastMovement == Vector2.zero ? new Vector2(1, 0) : lastMovement;
		bullet.direction = aimDirection;
		bullet.transform.rotation =
			Quaternion.FromToRotation(new Vector3(1, 0, 0), bullet.direction);
	}


	// 子弹出现的位置
	public Transform _muzzleTf;
	public Transform muzzleTf {
		get {
			return _muzzleTf != null ? _muzzleTf : transform;
		}
	}
	
	private void Start() {
		Transform[] children = GetComponentsInChildren<Transform>();
		foreach (Transform tf in children) {
			if (tf.name == "muzzle") {
				_muzzleTf = tf;
				break;
			}
		}
	}

	private void Update() {
		UpdateAim(); // TODO : 交给Character处理
	}
}
