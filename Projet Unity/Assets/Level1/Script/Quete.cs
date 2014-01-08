using UnityEngine;
using System.Collections;

public class Quete : MonoBehaviour {

	public string[] strDialogue;
	private bool made = false;
	private int id = 0;
	private int idCurrent = 0;

	private GameObject Text;

	void OnTriggerEnter(Collider other) {
		if(!made)
		{
			while(id < strDialogue.Length ){
				Invoke("parler", 2*id);
				id++;
			}
			made=true;
		}
	}

	void parler(){
		Text =  Instantiate(Resources.Load("Prefab/Text"),new Vector3(0.1f,0.5f,0f), Quaternion.identity) as GameObject;
		Text.guiText.text = strDialogue[idCurrent];
		idCurrent++;
	}
}