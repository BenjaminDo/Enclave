using UnityEngine;
using System.Collections;

abstract public class Heroes : PersoVivant {
	
	public int maxMana1 = 100;
	public int curMana1 = 100;
	
	public int maxMana2 = 100;
	public int curMana2 = 100;

	// Use this for initialization
	public override void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void addjustManaBar1(int adj) {
		curMana1 += adj ;

		if (curMana1  > maxMana1  )
			curMana1  = maxMana1;	
	}
}
