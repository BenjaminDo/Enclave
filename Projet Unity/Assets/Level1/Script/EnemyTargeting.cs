using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	//Fight variables
	private float AttackRange;
	public float AttackDist;

	//CoolDown
	private float lastTimeEstocade;
	private float lastTimeSalve;
	private float lastTimePourfendeur;
	private float lastTimeParade;
	private float lastTimeHachoir;

	private int potion=20;

	//Scripts
	private EnnemyAI myEnemyScript;

	private List<EnnemyAI> ennemysSalve;
	

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

		lastTimeEstocade = -5f;
		lastTimeSalve = -50f;
		lastTimePourfendeur = -50f;
		lastTimeParade = -50f;
		lastTimeHachoir = -50f;

		ennemysSalve = new List<EnnemyAI>();
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
				Pourfendeur();
		}
		else if(Input.GetKeyDown(KeyCode.Alpha5))
		{
			Hachoir ();
		}else if(Input.GetKeyDown(KeyCode.F1)){
			Debug.Log ("Sisi");
			if(potion>0){
				potion--;
				PlayerCaract.SetDeltaVitality(30 + PlayerCaract.GetVitality()); // Pour ne pas etre parade
				SfText =  Instantiate(Resources.Load("Prefab/SfUse"),new Vector3(0.465f,0.20f,0f), Quaternion.identity) as GameObject;
				SfText.guiText.color = Color.green;
				SfText.guiText.text = "+30 Vie";


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

		SfText =  Instantiate(Resources.Load("Prefab/MAbite"),new Vector3(0.92f,0.96f,0f), Quaternion.identity) as GameObject;
		SfText.guiText.color = Color.green;
		SfText.guiText.text = "Potion : " + potion;
	}

	void SelectTarget()
	{
		//selectedTarget.renderer.material.color = Color.red;
		selectIcon = selectedTarget.FindChild("Quad");
		selectIcon.renderer.enabled = true;
		myEnemyScript = selectedTarget.GetComponent<EnnemyAI>();
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
			GUI.BeginGroup(new Rect(Screen.width / 2 - 150, 0 ,(myEnemyScript.GetVitality() * 300 / myEnemyScript.GetMaxVitality()),50));
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

		myEnemyScript.UpdateVitality(-(int)(70 * PlayerCaract.GetForce())/100); // 70% de 100 de force = 70
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
	
		Instantiate(Resources.Load<GameObject>("Prefab/Salve"), transform.position, transform.rotation);
		audio.PlayOneShot(SalveSound);

		ennemysSalve.Clear();

		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){


			if(Vector3.Distance(transform.position, enemy.transform.position) < 8.0f){
				if(Vector3.Angle(transform.position, enemy.transform.position) < 60 ){ // Si dans un cone 120 °
						Debug.Log("Ok");
						if(enemy.GetComponent<EnnemyAI>()){
							enemy.GetComponent<EnnemyAI>().UpdateVitality(-(int)((120 * PlayerCaract.GetForce())/100));
							ennemysSalve.Add(enemy.GetComponent<EnnemyAI>());
						}
					}
			}
		}

		Invoke("SalveHit", 1);
		Invoke("SalveHit", 2);
		Invoke("SalveHit", 3);
		Invoke("SalveHit", 4);
		Invoke("SalveHit", 5);

	}

	void SalveHit(){
		foreach(EnnemyAI enemy in ennemysSalve){
			enemy.UpdateVitality(-25 * (int)PlayerCaract.GetForce()/100); 
		}
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
					Instantiate(Resources.Load<GameObject>("Prefab/explosion"), enemy.transform.position, enemy.transform.rotation);
					if(enemy.GetComponent<EnnemyAI>())
						enemy.GetComponent<EnnemyAI>().UpdateVitality(-((120 * (int)PlayerCaract.GetForce())/100));
				}
		}

			//Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation) as GameObject;
	}


	void Hachoir()
	{
		if(lastTimeHachoir > Time.realtimeSinceStartup - 10.0f)
		{
			//Ajouter Ce sort n'est pas encore disponible
			SfText =  Instantiate(Resources.Load("Prefab/SfUse"),new Vector3(0.4f,0.20f,0f), Quaternion.identity) as GameObject;
			SfText.guiText.color = Color.red;
			SfText.guiText.text = "Ce sort n'est pas encore disponible";
			return;
		}
		
		if(!barreAction.useSf(2))
			return;
		
		lastTimeHachoir=Time.realtimeSinceStartup;
		
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
		
		Instantiate(Resources.Load<GameObject>("Prefab/Hachoir"), transform.position, transform.rotation);
		audio.PlayOneShot(SalveSound);
		
		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){
			if(Vector3.Distance(transform.position, enemy.transform.position) < 12.0f) // Si dans un rayon de 180°
				if(Vector3.Angle(transform.position, enemy.transform.position) < 90 ){
						if(enemy.GetComponent<EnnemyAI>())	
							enemy.GetComponent<EnnemyAI>().UpdateVitality(-((110 * (int)PlayerCaract.GetForce())/100));
					}
		}
	}
}
