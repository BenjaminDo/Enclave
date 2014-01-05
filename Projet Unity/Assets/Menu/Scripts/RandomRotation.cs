// Script to link to a Gemstone of the Main Menu
//
//Role:
//		-Up and down movement
//		-Rotate with a random speed
//
// Created by Ludivine Barast
// Version 1.0

using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour 
{
	private float RotateSpeed;

	public string RotationAxe = "y";
	
	private Vector3 originalPosition;
	
	void Awake()
	{
		RotateSpeed = Random.Range(30,70);
	}

	// Use this for initialization
	void Start () 
	{
		originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(RotationAxe == "y")
		{
			//Rotatation
			transform.Rotate(0, -Time.deltaTime * RotateSpeed, 0, Space.World);
			
			Vector3 temp = transform.position;
			temp.y = originalPosition.y + Mathf.Sin(Time.time * 0.3f) * 0.2f;
			transform.position = temp;
		}
		else if(RotationAxe == "x")
			{
				//Rotatation
				transform.Rotate(-Time.deltaTime * RotateSpeed/2,0, 0, Space.World);
				
				Vector3 temp = transform.position;
				temp.x = originalPosition.x + Mathf.Sin(Time.time * 0.3f) * 2f;
				transform.position = temp;
			}
		
	}
}
