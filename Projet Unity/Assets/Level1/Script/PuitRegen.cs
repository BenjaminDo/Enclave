using UnityEngine;
using System.Collections;

public class PuitRegen : MonoBehaviour {

	private bool use = false;
	private CharacterCaracteristics PlayerCaract;

	void OnTriggerEnter(Collider other) {
		if(!use)
			PlayerCaract.SetVitality(100);
		use = true;
	}
	
	void Start(){
		PlayerCaract = GameObject.FindGameObjectWithTag("Player").GetComponent("CharacterCaracteristics") as CharacterCaracteristics;
	}


}
