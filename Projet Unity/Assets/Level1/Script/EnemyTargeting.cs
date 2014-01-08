using UnityEngine;
using System.Collections;

public class EnemyTargeting : MonoBehaviour 
{

	private CharacterCaracteristics PlayerCaract;
	private ActionBar barreAction;
	private GameObject SfText;

	//Bruitage
	private AudioClip SalveSound;
	private AudioClip EstocadeSound;

	//Enemy 
	private Transform selectedTarget;
	private Transform selectIcon;

	//Enemy Life
	private Texture2D EmptyEnemyLife;
	private Texture2D EnemyLife;
	public float myEnemyLife;

	//Fight variables
	private float AttackRange;
	public float AttackDist;

	//CoolDown
	private float lastTimeEstocade;
	private float lastTimeSalve;
	private float lastTimePourfendeur;
	private float lastTimeParade;
	private float lastTime;

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


		AttackRange = 6;

		lastTimeEstocade = 0f;
		lastTimeSalve = 0f;
		lastTimePourfendeur = 0f;
		lastTimeParade = 0f;
		lastTime = 0f;
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
		//selectedTarget.renderer.material.color = Color.red;
		selectIcon = selectedTarget.FindChild("Quad");
		selectIcon.renderer.enabled = true;
		myEnemyScript = selectedTarget.GetComponent<EnnemyAI>();
		myEnemyLife = myEnemyScript.GetVitality();
	}
	
	void DeselectTarget()
	{
		if (selectedTarget)
		{
			//selectedTarget.renderer.material.color = Color.white;
			selectIcon = selectedTarget.FindChild("Quad");
			selectIcon.transform.renderer.enabled = false;
			selectedTarget = null;
			selectIcon = null;
		}
	}

	void OnGUI()
	{
		if(selectedTarget)
		{
			//Background
			GUI.DrawTexture(new Rect(Screen.width / 2 - 150, 0 ,300,50), EmptyEnemyLife);

			//Foreground
			GUI.BeginGroup(new Rect(Screen.width / 2 - 150, 0 ,(myEnemyLife * 300 / 400),50));
			GUI.DrawTexture(new Rect(0,0,300,50),EnemyLife);
			GUI.EndGroup();
		}
	}

	void Estocade()
	{
		if(lastTimeEstocade > Time.realtimeSinceStartup - 0.5f){
			//Ajouter Ce sort n'est pas encore disponible
			SfText =  Instantiate(Resources.Load("Prefab/SfUse"),new Vector3(0.415f,0.20f,0f), Quaternion.identity) as GameObject;
			SfText.guiText.color = Color.red;
			SfText.guiText.text = "Ce sort n'est pas encore disponible";
			return;
		}

		if(!barreAction.useSf(1))
			return;

		lastTimeEstocade = Time.realtimeSinceStartup;

		Plane playerPlane = new Plane(Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float hitdist = 0.0f;
		Vector3 destinationPosition;
		
		if (playerPlane.Raycast(ray, out hitdist)) {
			Vector3 targetPoint = ray.GetPoint(hitdist);
			destinationPosition = ray.GetPoint(hitdist);
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			transform.rotation = targetRotation;
		}

		Debug.Log("Attack Estocade");

		audio.PlayOneShot(EstocadeSound);
		myEnemyLife -= (70 * PlayerCaract.GetForce())/100; // 70% de 100 de force = 70
		myEnemyScript.SetVitality(myEnemyLife);
	}

	void Salve()
	{
		if(lastTimeSalve > Time.realtimeSinceStartup - 5.0f)
		{
			//Ajouter Ce sort n'est pas encore disponible
			SfText =  Instantiate(Resources.Load("Prefab/SfUse"),new Vector3(0.4f,0.20f,0f), Quaternion.identity) as GameObject;
			SfText.guiText.color = Color.red;
			SfText.guiText.text = "Ce sort n'est pas encore disponible";
			return;
		}

		if(!barreAction.useVap(2))
			return;

		lastTimeSalve=Time.realtimeSinceStartup;

		Debug.Log("Attack Salve de couteaux"); // Il faut une classe Enemi avec la vitalité pour faire ça durant 6 secondes

		Plane playerPlane = new Plane(Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float hitdist = 0.0f;
		Vector3 destinationPosition;
		
		if (playerPlane.Raycast(ray, out hitdist)) {
			Vector3 targetPoint = ray.GetPoint(hitdist);
			destinationPosition = ray.GetPoint(hitdist);
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			transform.rotation = targetRotation;
		}
	
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
		if(lastTimeParade > Time.realtimeSinceStartup - 4.0f)
		{
			//Ajouter Ce sort n'est pas encore disponible
			SfText =  Instantiate(Resources.Load("Prefab/SfUse"),new Vector3(0.4f,0.20f,0f), Quaternion.identity) as GameObject;
			SfText.guiText.color = Color.red;
			SfText.guiText.text = "Ce sort n'est pas encore disponible";
			return;
		}

		if(!barreAction.useVap(1))
			return;

		lastTimeParade=Time.realtimeSinceStartup;

		Debug.Log("Parade");

		Instantiate(Resources.Load<GameObject>("Prefab/Sparkle Rising"), transform.position, transform.rotation);
		PlayerCaract.setParade(true);
		//myEnemyScript.SetVitality(myEnemyLife);
	}

	void Pourfendeur()
	{
		if(lastTimePourfendeur > Time.realtimeSinceStartup - 10.0f)
		{
			//Ajouter Ce sort n'est pas encore disponible
			SfText =  Instantiate(Resources.Load("Prefab/SfUse"),new Vector3(0.4f,0.20f,0f), Quaternion.identity) as GameObject;
			SfText.guiText.color = Color.red;
			SfText.guiText.text = "Ce sort n'est pas encore disponible";
			return;
		}

		if(!barreAction.useSf(1))
			return;
		
		lastTimePourfendeur=Time.realtimeSinceStartup;

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
