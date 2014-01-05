using UnityEngine;
using System.Collections;

public class CharacterCaracteristics : MonoBehaviour 
{
	private float PlayerForce;
	public float PlayerVitality;
	private float PlayerSpeed;

	private float LifeDelay = 10;

	private bool parade = false;

	// Use this for initialization
	void Start () 
	{
		PlayerForce = 100;
		PlayerVitality = 100;
		PlayerSpeed = 100;

		InvokeRepeating("IncreaseLife", 2, LifeDelay);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(PlayerVitality < 0)
		{
			Death();
		}

		if(PlayerVitality > 100)
			PlayerVitality = 100;
	}

	void IncreaseLife()
	{
		PlayerVitality += 1;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Water")
		{
			Death();
		}
	}

	void Death()
	{
		Application.LoadLevel(0);
	}

	public void SetForce( float value )
	{
		PlayerForce = value;
	}
	
	public float GetForce()
	{
		return PlayerForce;
	}

	/*** Avec value le chagment ***/
	public void SetDeltaVitality( float value )
	{
		if(parade){
			parade = false;
			return;
		}

		PlayerVitality += value;
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

	public void setParade(bool bParade)
	{
		parade = bParade;
	}
}
