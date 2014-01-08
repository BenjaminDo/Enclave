using UnityEngine;
using System.Collections;

public class DruideHero : PersoVivant {
	
	//sang froid
	public int Yin = 50;
	//vaporine
	public int Yang = 50;
	
	public int YinMax = 100;
	public int YangMax = 100;

	// Use this for initialization
	public override void Start () {
	
		intel = 100;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void addjustBar(bool YiorYa) {//yin or yang
		//On return un bool lors du cast du spell ? 
		//A voir avec les pélos
	}
	
	public void levelUp(){
		//+ 5 aux carac de cet enfoiré
		intel = intel + 5;
		maxHealth = maxHealth +5;
		speedRate = speedRate +5;
		
		lvl++;
		
	}
	
	//TODO equipements, comment les mettre en place? voir avec momo
}
