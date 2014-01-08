using UnityEngine;
using System.Collections;

public class MobAd : PersoVivant
{

	// Use this for initialization
	void Start ()
	{
		strength = 100;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	//TODO equipements 
	
	//TODO donner des levels aux mobs pour definir ses caracs
	void setStats(){
		strength = strength + 5 * lvl;
		speedRate = speedRate + 5 * lvl;
		maxHealth = maxHealth + 5 * lvl;
		
	}
}

