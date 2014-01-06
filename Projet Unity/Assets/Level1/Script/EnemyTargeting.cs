using UnityEngine;
using System.Collections;

public class EnemyTargeting : MonoBehaviour 
{

	private CharacterCaracteristics PlayerCaract;
	private ActionBar barreAction;

	//Bruitage
	private AudioClip SalveSound;
	private AudioClip EstocadeSound;

	//Enemy 
	private Transform selectedTarget;

	//Enemy Life
	private Texture2D EmptyEnemyLife;
	private Texture2D EnemyLife;
	public float myEnemyLife;

	//Fight variables
	private float AttackRange;
	public float AttackDist;

	//Scripts
	private EnnemyAI myEnemyScript;
	

	// Use this for initialization
	void Start () 
	{
		//Textures
		EmptyEnemyLife = Resources.Load("GUI/ActionBar/EnemyLifeBar") as Texture2D;
		EnemyLife = Resources.Load("GUI/ActionBar/EnemyFullLife") as Texture2D;

		PlayerCaract = this.GetComponent("CharacterCaracteristics") as CharacterCaracteristics;
		barreAction = this.GetComponent("ActionBar") as ActionBar;

		SalveSound = Resources.Load("Sound/whoosh3") as AudioClip;
		EstocadeSound = Resources.Load("Sound/épée coupe") as AudioClip;

		AttackRange = 3;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100) && hit.transform.CompareTag("Enemy"))
			{
				Debug.Log("Enemy Selected");
				DeselectTarget();
				selectedTarget = hit.transform;
				SelectTarget();
			}
		}

		AttackDist = -1;

		if(selectedTarget)
			AttackDist = Vector3.Distance(transform.position,selectedTarget.position);

		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			if(selectedTarget)		
				if(AttackDist < AttackRange)
				{
					transform.rotation = Quaternion.LookRotation(selectedTarget.transform.position);
					Estocade();
				}

		}
		else if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			Salve();
		}
		else if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			Parade();
		}
		else if(Input.GetKeyDown(KeyCode.Alpha4))
		{
			if(AttackDist < AttackRange)
			{
				Pourfendeur();
			}
		}

		//Perte du target
		if(AttackDist > 90)						//Eloignement
		{
			DeselectTarget();
		}
		if(Input.GetMouseButtonDown(1))			//Deselect manuel
		{
			DeselectTarget();
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
		if(!barreAction.useSf(3))
			return;

		Debug.Log("Attack Estocade");

		audio.PlayOneShot(EstocadeSound);
		myEnemyLife -= (70 * PlayerCaract.GetForce())/100; // 70% de 100 de force = 70
		myEnemyScript.SetVitality(myEnemyLife);
	}

	void Salve()
	{
		if(!barreAction.useVap(20))
			return;

		Debug.Log("Attack Salve de couteaux"); // Il faut une classe Enemi avec la vitalité pour faire ça durant 6 secondes

	
		audio.PlayOneShot(SalveSound);

		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){
			if(Vector3.Angle(transform.forward, enemy.transform.position) < 120 ) // Si dans un cone
				if(Vector3.Distance(transform.position, enemy.transform.position) < 5.0f){
					Debug.Log ("Touché");
					enemy.GetComponent<EnnemyAI>().UpdateVitality(-((120 * PlayerCaract.GetForce())/100));
				}
		}

		Invoke("SalveHit", 1);
		Invoke("SalveHit", 2);
		Invoke("SalveHit", 3);
		Invoke("SalveHit", 4);
		Invoke("SalveHit", 5);

	}

	void SalveHit(){

		myEnemyLife -= (25 * PlayerCaract.GetForce())/100; 
		myEnemyScript.SetVitality(myEnemyLife);
	}

	void Parade()
	{
		
		if(!barreAction.useVap(10))
			return;

		Debug.Log("Parade");

		Instantiate(Resources.Load<GameObject>("Prefab/Sparkle Rising"), transform.position, transform.rotation);
		PlayerCaract.setParade(true);
		//myEnemyScript.SetVitality(myEnemyLife);
	}

	void Pourfendeur()
	{
		if(!barreAction.useVap(10))
			return;

		Instantiate(Resources.Load<GameObject>("Prefab/explosion"), transform.position, transform.rotation);


		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){
			if(Vector3.Distance(transform.position, enemy.transform.position) < 8.0f){
				Debug.Log ("Touché");
				Instantiate(Resources.Load<GameObject>("Prefab/explosion"), enemy.transform.position, enemy.transform.rotation);
				enemy.GetComponent<EnnemyAI>().UpdateVitality(-((120 * PlayerCaract.GetForce())/100));
			}
		}

			//Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation) as GameObject;
	}
}
