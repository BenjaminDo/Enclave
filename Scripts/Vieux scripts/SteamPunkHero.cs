using UnityEngine;
using System.Collections;

public class SteamPunkHero : PersoVivant {
	
	//sang froid
	public int coldBlood = 2;
	//vaporine
	public int vaporine = 2;
	
	public int coldBloodMax = 4;
	public int vaporineMax = 4;

	// Use this for initialization
	public override void Start () {
	
		strength = 100;	
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void addjustBar(bool VorCB) {//vaporine or coldBlood
		//On return un bool lors du cast du spell ? 
		//A voir avec les pélos
	}
	
	void levelUp(){
		//+ 5 aux carac de cet enfoiré
		strength = strength + 5;
		maxHealth = maxHealth +5;
		speedRate = speedRate +5;
		
		lvl++;
		
	}
	
	//TODO equipements, comment les mettre en place? voir avec momo
}
