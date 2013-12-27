using UnityEngine;
using System.Collections;

/* Script a attacher sur la camera
 * Il suit le personnage passé en paramètre dans Unity
 * 
 * Auteur Morgan Perre 08/11 à 17h52
 */

public class CameraView : MonoBehaviour {
	private Transform targetCamera;
	private Vector3 to;
	float distanceCO;
	
	// Use this for initialization
	void Start () {
		targetCamera = GameObject.Find("TargetCamera").transform;
		distanceCO = Vector3.Distance(targetCamera.position, transform.position);
	}
	 
	// Gestion de la camera
	void Update () {
		if(Input.GetMouseButton(0)){
	        float x = -Input.GetAxis("Mouse X");
	        float y = -Input.GetAxis("Mouse Y");

	        transform.Translate (x, y, 0);
		}
		
		//posCamera = target.position - transform.position;
		to = targetCamera.position - transform.position;
		
		float currentDistance = Vector3.Distance(targetCamera.position, transform.position);
		//Debug.Log("Test distance Object Camera :" + currentDistance);
		if((currentDistance - distanceCO) > -10 && (currentDistance - distanceCO) < 10)
			transform.Translate (0, 0, (currentDistance - distanceCO));
		
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(to), 1*Time.deltaTime);
	}
	
}
