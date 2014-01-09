using UnityEngine;
using System.Collections;

public class Achivement : MonoBehaviour {
	public string strAchivement;
	private bool made = false;

	private GameObject Text;

	private CharacterCaracteristics PlayerCaract;
	public int xpRecompence = 3;


	void OnTriggerEnter(Collider other) {
		if(!made)
		{
			Text =  Instantiate(Resources.Load("Prefab/Text"),new Vector3(0.4f,0.9f,0f), Quaternion.identity) as GameObject;
			
			Text.guiText.text = strAchivement;
			Text.guiText.color = new Color32(51,51,95,200);


			PlayerCaract = GameObject.FindGameObjectWithTag("Player").GetComponent("CharacterCaracteristics") as CharacterCaracteristics;
			PlayerCaract.updateXp(xpRecompence, false);
		}
	}
}
