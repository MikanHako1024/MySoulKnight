using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : CharacterBase {
	
	public int maxMagic;
	public int curMagic;
	public int maxArmor;
	public int curArmor;
	
	private Vector2 movement {
		get { return InputManager.axisInput; }
	}

	private void Update() {
		if (needSkill) {
			DoSkill();
		}
		//if (needAttack && canAttack) {
		if (needAttack) {
			DoAttack();
		}
	}
	
	//private new void Update() {
	private new void FixedUpdate() {
		//Debug.Log(Time.fixedDeltaTime + ", " + Time.fixedTime + ", " + Time.deltaTime);
		// Time.timeScale
		
		//base.Update();
		//base.FixedUpdate();

		//UpdateInput();

		/*
		if (needSkill) {
			DoSkill();
		}
		if (needAttack && canAttack) {
			DoAttack();
		}
		*/

		//Debug.Log(isRolling + ", " + rollDirection + ", " + rollSpeed);
		if (movement != Vector2.zero) {
			lastMovement = movement;
		}

		if (isRolling) {
			currMovement = rollMovement;
			currSpeed = rollSpeed;
			//isRolling = false;
			// ？这样只有在进行动画更新的那一帧才会快速移动 ...
			// ？而两次动画更新之间的帧不会快速移动 ...
			// ？这样会显得有点卡顿 ...
			// ？应该是设置在一段时间内快速移动，而不是仅仅一帧 ...
			// 
			// ？改为在帧动画结束时的事件里手动停止isRolling ...
		}
		else {
			currMovement = movement;
			currSpeed = moveSpeed;
		}
		
		base.FixedUpdate();

		//Debug.Log(currMovement + ", " + rollMovement + ", " + movement);
	}

	//private void UpdateInput() {
		//InputManager inputManager = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>();
		//moveDirection = inputManager.axisInput;
		//needAttack = inputManager.keyDownAttack;
		//needSkill = inputManager.keyDownSkill;
		//movement = InputManager.axisInput;
		//needAttack = InputManager.keyDownAttack;
		//needSkill = InputManager.keyDownSkill;
		// ？改用 getter ...
		//Debug.Log(moveDirection + ", " + needAttack + ", " + needSkill);
	//}

	
	// --------------------------------
	// roll
	
	private Vector2 lastMovement;
	private Vector2 rollMovement;
	private float rollSpeed;
	private bool isRolling;
	
	private bool needSkill {
		get { return InputManager.keyDownSkill; }
	}

	// ？让帧事件调用，实现获取动画帧数，与动画交互 ...
	public void RollMove(int frame) {
		//rollSpeed = (rollMoveParamA * frame - 2 * rollMoveParamB) * frame;
		float[] list = new float[] { 1.4f, 1.6f, 1.9f, 2.0f, 1.6f, 1.2f };
		float speedRate = list[frame];
		rollSpeed = moveSpeed * speedRate; // 暂时用移动速度的倍率实现
		//Debug.Log(rollSpeed);
		//isRolling = (rollSpeed > 0.0);
		isRolling = true;
	}

	public void RollMoveEnd() {
		isRolling = false;
	}

	public void DoSkill() {
		//Debug.Log("skill");
		// ？xx技能.yy委托 ...

		// 使用当前移动方向作为滚动方向
		//rollDirection = moveDirection;
		// ？不是当前移动方向，而是上一次的移动方向 ...
		// ？而且是上一次有实际移动的移动方向 ...
		// ？需要在每帧更新 ...
		// ？而且正在滚动时 不更新上次输入方向到滚动方向 ...
		rollMovement = lastMovement;

		if (anim != null) {
			if (!isRolling) { // 暂时
				anim.SetTrigger("roll");
			}
			else {
				anim.ResetTrigger("roll");
				// ？...
			}
		}
	}


	// --------------------------------
	// attack
	/*
	public BulletBase bulletPrefab;
	
	public float attackInterval = 0.5f;
	private float lastAttackTime = -60f;
	
	private bool needAttack {
		get { return InputManager.keyDownAttack; }
	}
	
	// 静态子弹测试用
	//public EmptyBullet bulletPrefab2;

	public void DoAttack() {
		// ？xx对象.yy委托() ...
		//Debug.Log("attack");

		lastAttackTime = Time.time;
		// ？Time.timeSinceLevelLoad, Time ...

		InitBullet(CreateBullet());

		// 大量创建子弹测试用
		//for (int i = 0; i < 10; i++) {
		//	InitBullet(CreateBullet());
		//}

		// 大量创建静态子弹测试用
		//for (int i = 0; i < 100; i++) {
		//	EmptyBullet bullet = Instantiate<EmptyBullet>(bulletPrefab2);
		//	bullet.transform.SetParent(null);
		//	bullet.transform.position = transform.position;
		//}
	}

	public BulletBase CreateBullet() {
		BulletBase bullet = Instantiate<BulletBase>(bulletPrefab);
		bullet.transform.SetParent(null);
		return bullet;
	}

	public void InitBullet(BulletBase bullet) {
		bullet.transform.position = transform.position;
		//bullet.transform.eulerAngles = bullet.transform.eu
		//	lastMovement == Vector2.zero ? new Vector2(1, 0) : lastMovement;
		//bullet.direction = bullet.transform.eulerAngles;
		// ？rotation 是旋转角度 而不是方向向量 ...
		bullet.direction = 
			lastMovement == Vector2.zero ? new Vector2(1, 0) : lastMovement;
		//bullet.transform.eulerAngles = new Vector3(0, 0, ...);
		bullet.transform.rotation = 
			Quaternion.FromToRotation(new Vector3(1, 0, 0), bullet.direction);
		//Debug.Log(bullet.transform.rotation + ", " + bullet.transform.eulerAngles);
		
	}

	public bool canAttack {
		get {
			return Time.time - lastAttackTime > attackInterval;
		}
	}
	*/
	// 移至武器的类里

	private bool needAttack {
		get { return InputManager.keyDownAttack; }
	}

	public void DoAttack() {
		shootDele();
	}

	public WeaponBase.ShootDelegate shootDele;
	public WeaponBase weaponUse;

	private new void Start() {
		base.Start();
		shootDele = weaponUse.DoShoot;
	}

	//private new void FixedUpdate() {
	//		// ？new ...
	//	base.FixedUpdate();
	//}

	//public new void FixedUpdateMove() {
	//	Debug.Log(isRolling);
	//	if (isRolling) {
	//		Debug.Log(rollDirection + ", " + rollSpeed);
	//		DoMove(rollDirection, rollSpeed);
	//		isRolling = false;
	//	}
	//	else {
	//		base.FixedUpdateMove();
	//	}
	//}
}
