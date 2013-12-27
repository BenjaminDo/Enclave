using UnityEngine;
using System.Collections;

/* Script a attacher sur le(s) main character
 * Controle les deplacements. Gestion XBox : 
 * Mouse X = 4th axis ; Horizontal = X axis ; Vertical = Y axis
 * 
 * Documentation : http://wiki.unity3d.com/index.php?title=Xbox360Controller
 * 
 * Auteur Morgan Perre 08/11 à 18h31 revu le 08/11
 * 
 * Note : Cette version est dissocié de la direction dans la quel regarde de personnage (Si changementregarder le Space)
 */

public class CharacterDeplacement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		float cubeRotationYbyX = Input.GetAxis("Mouse X");
		float cubeRotationYbyZ = -Input.GetAxis("Mouse Y");
		Quaternion rotationByJoys = Quaternion.LookRotation(new Vector3(cubeRotationYbyX,0.0f,cubeRotationYbyZ));
		Debug.Log ("Valeur Angle Rotation : " + rotationByJoys.eulerAngles);
		
		
		float cubeMoveX = Input.GetAxis("Horizontal");
		float cubeMoveZ = Input.GetAxis("Vertical");
		
		transform.Translate((new Vector3(cubeMoveX,0.0f,cubeMoveZ)) * 5.0f * Time.deltaTime, Space.World);
		
		if(cubeRotationYbyX != 0 || cubeRotationYbyZ != 0) //A remplacer par le test si Mouse X ou Y on une valeur
			transform.rotation = Quaternion.Slerp(transform.rotation, rotationByJoys, Time.deltaTime);
	}
	
}
