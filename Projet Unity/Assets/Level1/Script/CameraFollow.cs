using UnityEngine;
using System.Collections;

/* Script a attacher sur la camera
 * Il suit le personnage passé en paramètre dans Unity
 * 
 * Auteur Morgan Perre 08/11 à 18h12
 */

public class CameraFollow : MonoBehaviour {
	
	public Transform _transformMainCharacter;
	private Vector3 _vec3lastMainCharacter;	
	
	private Vector3 _movement;
	
	// Use this for initialization
	void Start () {
		_vec3lastMainCharacter = _transformMainCharacter.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Calcul du déplacement du main Character (new - ex)
		_movement =  _transformMainCharacter.position - _vec3lastMainCharacter;
		//Mise a jour
		_vec3lastMainCharacter = _transformMainCharacter.position;
		
		transform.Translate(_movement, Space.World);
	}
}
