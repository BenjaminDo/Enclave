using UnityEngine;
using System.Collections;

public class Quete : MonoBehaviour {

	public string[] strDialogue;
	private bool made = false;
	private int id = 0;
	private int idCurrent = 0;

	private CharacterCaracteristics PlayerCaract;
	public int xpRecompence = 1;

	private GameObject Text;

	void OnTriggerEnter(Collider other) {
		if(!made)
		{

			PlayerCaract = GameObject.FindGameObjectWithTag("Player").GetComponent("CharacterCaracteristics") as CharacterCaracteristics;
			PlayerCaract.updateXp(xpRecompence, false);

			while(id < strDialogue.Length ){
				Invoke("parler", 2*id);
				id++;
			}
			made=true;
		}
	}

	void parler(){
		float dist = 0.02f;

		if(strDialogue[idCurrent].Length > 25 &&  strDialogue[idCurrent].Length < 40)
			dist = 0.22f;
		else if(strDialogue[idCurrent].Length < 25)
			dist = 0.32f;
		else
			dist = 0.12f;

		if(idCurrent%2==0)
			Text =  Instantiate(Resources.Load("Prefab/Text"),new Vector3(dist,0.8f,0f), Quaternion.identity) as GameObject;
		else
			Text =  Instantiate(Resources.Load("Prefab/Text2"),new Vector3(dist + 0.08f,0.78f,0f), Quaternion.identity) as GameObject;

		Text.guiText.text = strDialogue[idCurrent];
		idCurrent++;
	}
}