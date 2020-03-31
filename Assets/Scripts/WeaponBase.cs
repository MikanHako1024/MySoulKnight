using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour {
	
	public delegate void TryShootDelegate();
	public delegate void DoShootDelegate();
	public delegate bool CanShootDelegate();
	
	public TryShootDelegate TryShootDele;
	public CanShootDelegate CanShootDele;
	public DoShootDelegate DoShootDele;
	
	public float shootFrequency;
	public float lastShootTime;

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
}
