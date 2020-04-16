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


	#region 瞄准方向

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

	#endregion


	#region 执行射击

	public float shootInterval = 0.5f;
	private float lastShootTime = -60f;

	public bool canShoot {
		get {
			return Time.time - lastShootTime > shootInterval;
		}
	}

	public delegate void ShootDelegate();

	//public UnityEvent doShoot;

	public void DoShoot() {
		DoShootIfCan();
	}

	public void DoShootIfCan() {
		if (canShoot) DoShootWithoutIf();
	}

	public void DoShootWithoutIf() {
		lastShootTime = Time.time;
		InitBullet(CreateBullet(bulletPrefab));
		//for (int i = 0; i <= 10; i++) InitBullet(CreateBullet(bulletPrefab));
	}

	#endregion


	#region 创建子弹

	public BulletBase bulletPrefab;

	// 静态子弹测试用
	//public EmptyBullet bulletPrefab2;

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
		//bullet.direction = aimDirection;
		//bullet.transform.rotation =
		//	Quaternion.FromToRotation(new Vector3(1, 0, 0), bullet.direction);
		
		//bullet.direction = shootAngle;
		float angle = shootAngle;
		//Debug.Log(angle);
		//bullet.direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		// ？Mathf.Cos, Mathf,Sin 传入的是弧度制角度 ...
		bullet.direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
		//bullet.transform.rotation =
		//	Quaternion.FromToRotation(new Vector3(1, 0, 0), bullet.direction);
		bullet.transform.eulerAngles = new Vector3(0, 0, angle);

		//bullet.speed *= Random.Range(0.8f, 1.2f);
	}

	#endregion


	#region 射击方向

	public float shootVariance = 1f;

	public float shootAngle {
		get {
			float angle = Vector2.Angle(Vector2.up, aimDirection);
			angle = aimDirection.x >= 0 ? 90 - angle : 90 + angle;
			return normalRandom(angle, shootVariance);
		}
	}

	/// <summary>
	/// 正态随机
	/// </summary>
	/// <param name="mean">均值</param>
	/// <param name="variance">方差</param>
	/// <returns>正态随机值</returns>
	public float normalRandom(float mean, float variance) {
		// Box Muller方法 正态随机数
		// u, v : random[0,1]
		// 标准正态随机 : 根号(-2ln(u)) * sin(2PI*v)
		float u = Random.Range(0f, 1f);
		float v = Random.Range(0f, 1f);
		float nRand = Mathf.Sqrt(-2 * Mathf.Log(u)) * Mathf.Sin(2 * Mathf.PI * v);
		//float nRand = Mathf.Sqrt(-2 * Mathf.Log10(u)) * Mathf.Sin(2 * Mathf.PI * v);
		//Debug.Log(u + ", " + v + ", " + nRand + ", " + (mean + nRand * variance));
		return mean + nRand * variance;
	}

	#endregion


	#region 枪口

	public Transform _muzzleTf; // 子弹出现的位置
	public Transform muzzleTf {
		get {
			return _muzzleTf != null ? _muzzleTf : transform;
		}
		set {
			_muzzleTf = value;
		}
	}

	private void InitMuzzle() {
		if (_muzzleTf == null) {
			Transform[] children = GetComponentsInChildren<Transform>();
			foreach (Transform tf in children) {
				if (tf.name == "muzzle") {
					_muzzleTf = tf;
					break;
				}
			}
		}
	}

	#endregion


	private void Start() {
		InitMuzzle();
	}

	private void Update() {
		UpdateAim(); // TODO : 交给Character处理
	}
}
