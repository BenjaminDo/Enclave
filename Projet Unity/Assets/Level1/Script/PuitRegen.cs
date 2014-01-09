using UnityEngine;
using System.Collections;

public class PuitRegen : MonoBehaviour {

	private GameObject SfText;

	private bool use = false;
	private CharacterCaracteristics PlayerCaract;

	void OnCollisionEnter(Collision collision) {
		if( !use && (PlayerCaract.GetVitality()<PlayerCaract.getMaxLife()) ){
			PlayerCaract.SetDeltaVitality(PlayerCaract.getMaxLife());
			SfText =  Instantiate(Resources.Load("Prefab/SfUse"),new Vector3(0.41f,0.6f,0f), Quaternion.identity) as GameObject;
			SfText.guiText.fontSize = 60;
			SfText.guiText.color = new Color32(3,91,9,160);
			SfText.guiText.text = "Régénération";
			use = true;
		}

	}
	
	void Start(){
		PlayerCaract = GameObject.FindGameObjectWithTag("Player").GetComponent("CharacterCaracteristics") as CharacterCaracteristics;
	}
}
