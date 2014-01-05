using UnityEngine;
using System.Collections;

public class CharacterCaracteristics : MonoBehaviour 
{
	private float PlayerForce;
	public float PlayerVitality;
	private float PlayerSpeed;

	// Use this for initialization
	void Start () 
	{
		PlayerForce = 100;
		PlayerVitality = 100;
		PlayerSpeed = 100;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(PlayerVitality < 0)
			PlayerVitality = 0;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Water")
		{
			Debug.Log("Noyade");
		}
	}

	public void SetForce( float value )
	{
		PlayerForce = value;
	}
	
	public float GetForce()
	{
		return PlayerForce;
	}

	public void SetVitality( float value )
	{
		PlayerVitality = value;
	}
	
	public float GetVitality()
	{
		return PlayerVitality;
	}

	public void SetSpeed( float value )
	{
		PlayerSpeed = value;
	}
	
	public float GetSpeed()
	{
		return PlayerSpeed;
	}
}
