using UnityEngine;
using System.Collections;

public class EnemyTargeting : MonoBehaviour 
{
	//Enemy 
	private Transform selectedTarget;

	//Enemy Life
	private Texture2D EmptyEnemyLife;
	private Texture2D EnemyLife;
	public float myEnemyLife;

	//Fight variables
	private float AttackRange;
	private float AttackDist;

	//Scripts
	private EnnemyAI myEnemyScript;
	

	// Use this for initialization
	void Start () 
	{
		//Textures
		EmptyEnemyLife = Resources.Load("GUI/ActionBar/EnemyLifeBar") as Texture2D;
		EnemyLife = Resources.Load("GUI/ActionBar/EnemyFullLife") as Texture2D;

		AttackRange = 3;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Enemy"))
			{
				Debug.Log("Enemy Selected");
				DeselectTarget();
				selectedTarget = hit.transform;
				SelectTarget();
			}
		}

		if(selectedTarget)
		{
			AttackDist = Vector3.Distance(transform.position,selectedTarget.position);
			if(Input.GetKeyDown(KeyCode.E))
			{
				if(AttackDist < AttackRange)
				{
					Estocade();
				}
			}
		}
	}

	void SelectTarget()
	{
		selectedTarget.renderer.material.color = Color.red;
		myEnemyScript = selectedTarget.GetComponent<EnnemyAI>();
		myEnemyLife = myEnemyScript.GetVitality();
	}
	
	void DeselectTarget()
	{
		if (selectedTarget)
		{
			selectedTarget.renderer.material.color = Color.white;
			selectedTarget = null;
		}
	}

	void OnGUI()
	{
		if(selectedTarget)
		{
			//Background
			GUI.DrawTexture(new Rect(Screen.width / 2 - 150, 0 ,300,50), EmptyEnemyLife);

			//Foreground
			GUI.BeginGroup(new Rect(Screen.width / 2 - 150, 0 ,(myEnemyLife * 300 / 900),50));
			GUI.DrawTexture(new Rect(0,0,300,50),EnemyLife);
			GUI.EndGroup();
		}
	}

	void Estocade()
	{
		Debug.Log("Attack Estocade");

		myEnemyLife -= 70;
		myEnemyScript.SetVitality(myEnemyLife);
	}
}
