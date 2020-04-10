using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour {
	
	[SerializeField]
	public UnityEngine.Events.UnityEvent onAttack;

	// ...


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
}
