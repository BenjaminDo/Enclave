using UnityEngine;
using System.Collections;

abstract public class PersoVivant : MonoBehaviour {
	
	public int maxHealth = 100;//vitalité comme l'appelle Choub'
	public int curHealth = 100;
	
	public bool dead = false;//dead ou pas
	
	public int strength = 0;//force
	public int intel = 0;//intelligence
	public int speedRate = 100;//vitesse de deplacement
	
	public int lvl = 1; //lvl du perso
	
	
	// Use this for initialization
	public virtual void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			
	}
	
	//Ajout/suppression hp
	public void addjustCurrentHealt( int adj ) {
		//un adjust selon si heal ou damage
		curHealth += adj ;
		
		//si vie negative on la met a 0
		if (curHealth < 0)
		{
			curHealth = 0;
			dead = true;
		}
		
		//si vie actuelle superieure a vie max, on la met a max
		if (curHealth > maxHealth )
			curHealth = maxHealth ;
		
	}
	
	//todo GUI barHealt ?
}
