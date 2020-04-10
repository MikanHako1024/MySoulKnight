using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBattleBodyBase {

	public string tag = "base";

	public int curHealth = 1;
	public int maxHealth = 1;

	public IBattleBodyBase() {
	}

	//public void Init() {
	//}

	public void RefreshHealth() {
		curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
	}
	public void RefreshAll() {
		RefreshHealth();
	}

	public void GainHealth(int value) {
		curHealth += value;
		RefreshHealth();
	}

	public void Hurt(int value) {
		if (value > 0) {
			GainHealth(-value);
			if (CheckDead()) Dead();
		}
	}

	public bool CheckDead() {
		return curHealth <= 0;
	}
	public void Dead() {
	}
}

public class IBattleBodyBox : IBattleBodyBase {

	public new string tag = "box";
	
	public IBattleBodyBox() : base() {
	}
	
	public new void Dead() {
	}
}

public class IBattleBodyEnemy : IBattleBodyBase {

	public new string tag = "enemy";
	
	public IBattleBodyEnemy() : base() {
	}
	
	public new void Dead() {
	}
}

public class IBattleBodyPlayer : IBattleBodyBase {

	public new string tag = "player";
	
	public int curMana = 0;
	public int maxMana = 0;
	public int curArmor = 0;
	public int maxArmor = 0;

	public float curArmorColdDown = 8f;
	public float maxArmorColdDown = 8f;
	public float curArmorRestore = 2f;
	public float maxArmorRestore = 2f;

	public void FixedUpdate() {
		FixedUpdateArmorRestore();
	}

	public void RefreshMana() {
		curMana = Mathf.Clamp(curMana, 0, maxMana);
	}
	public void RefreshArmor() {
		curArmor = Mathf.Clamp(curArmor, 0, maxArmor);
	}
	public new void RefreshAll() {
		RefreshHealth();
		RefreshMana();
		RefreshArmor();
	}

	public void GainMana(int value) {
		curMana += value;
		RefreshMana();
	}
	public void GainArmor(int value) {
		curArmor += value;
		RefreshArmor();
	}

	public new void Hurt(int value) {
		if (value <= 0) return;
		int remain = value - curArmor;
		if (curArmor > 0) {
			GainArmor(-value);
		}
		if (remain > 0) {
			GainHealth(-remain);
		}
		if (value > 0) ClearArmorCount();
	}

	public bool UseMana(int value) {
		if (CheckMana(value)) {
			GainMana(-value);
			return true;
		}
		else {
			return false;
		}
	}
	public bool CheckMana(int value) {
		return curMana >= value;
	}

	public void ActiveUseMana(int mana) {
		if (UseMana(mana)) {
			ActiveSuccess();
		}
		else {
			ActiveFail();
		}
	}
	public void ActiveSuccess() {
	}
	public void ActiveFail() {
	}

	public void FixedUpdateArmorRestore() {
		if (curArmor >= maxArmor) return;
		float time = Time.fixedDeltaTime;
		if (curArmorColdDown < maxArmorColdDown) {
			curArmorColdDown += time;
		}
		else {
			curArmorRestore += time;
			if (curArmorRestore >= maxArmorRestore) {
				GainArmor(1);
				curArmorRestore = 0;
			}
		}
	}
	public void ClearArmorCount() {
		curArmorColdDown = 0;
		curArmorRestore = 0;
	}
	
	public new void Dead() {
	}
}
