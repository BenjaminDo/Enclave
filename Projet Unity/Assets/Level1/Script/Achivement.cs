using UnityEngine;
using System.Collections;

public class Achivement : MonoBehaviour {
	public string strAchivement;
	private bool made = false;

	private GameObject Text;

	void OnTriggerEnter(Collider other) {
		if(!made)
		{
			Text =  Instantiate(Resources.Load("Prefab/Text"),new Vector3(0.4f,0.6f,0f), Quaternion.identity) as GameObject;
			
			Text.guiText.text = strAchivement;
		}
	}
}
