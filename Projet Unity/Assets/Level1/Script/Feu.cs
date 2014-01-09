using UnityEngine;
using System.Collections;

public class Feu : MonoBehaviour {

	private CharacterCaracteristics PlayerCaract;

	private GameObject Text;

	void OnTriggerStay(Collider other) {
		PlayerCaract = GameObject.FindGameObjectWithTag("Player").GetComponent("CharacterCaracteristics") as CharacterCaracteristics;
		PlayerCaract.SetDeltaVitality(-1);

		Text =  Instantiate(Resources.Load("Prefab/Text"),new Vector3(0.41f,0.2f,0f), Quaternion.identity) as GameObject;
		
		Text.guiText.text = "Brulure";
		Text.guiText.color = new Color32(255,12,56,200);
	}
}
