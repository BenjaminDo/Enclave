using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {
	
	public static float deltaTime;
	public static float fixedTime;
	public static float gravity;
	

	void Start () {
		gravity = 1;
	}

	void Update () {
		deltaTime = Time.deltaTime;
		fixedTime = Time.fixedTime;
	}
}
