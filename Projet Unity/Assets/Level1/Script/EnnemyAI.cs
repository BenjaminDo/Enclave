//Created By Ludivine BARAST
//26/12/2013 00:37
//Version 1.0
//
//Source : http://answers.unity3d.com/questions/237571/ai-detecting-object-raycast.html
//
//IA des monstre permettant:
//-Le déplacement aléatoire dan sun petite zone
//-La poursuite du joueur si il entre dan sune zone de détection
//-Le retour a sa position d'origine au bout d'un certain temps
//
//Amelioration a faire :
//	-Ajouter une vie
//	-Permettre le retour au bout d'un certain temps et QUE s'il est encore en vie
//
//Other :
//Pour permettre un coutour des objets plus "propre, leur donner un colider legèrement plus large

using UnityEngine;
using System.Collections;

public class EnnemyAI : MonoBehaviour 
{
	//Caractéristiques
	public float EnemyVitality = 400;
	public int xpGave = 1;
	private float Speed;
	
	//Param for Player
	private GameObject Player;
	private CharacterCaracteristics PlayerCaract;
	public float PlayerLife;

	//Stats
	public bool Wander;
	public bool Hunt;
	public bool Atack;
	public bool Countdown;
	public bool ComeBack;
	public Vector3 InitialPos;

	private RaycastHit hit;

	//Parem for Wandering
	private Vector3 Wandir;

	//Param for Movement
	private Vector3 TargetDir;
	private Quaternion TargetRot;
	public float Vitesse;

	//Param for detection
	private float DetectDist;
	private float TargetDist;
	private float DetectRange;
	private float AttackRange;

	//Decompte
	private float decompte=5;
	private float HunTime;
	private float AttackDelay = 1;

	void Awake()
	{
		SetValues();
		InitialPos = transform.position;

	}

	// Use this for initialization
	void Start () 
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		PlayerCaract = Player.GetComponent("CharacterCaracteristics") as CharacterCaracteristics;
		Wandering();
		InvokeRepeating("Attack", 2, AttackDelay);

	}

	void SetValues()
	{
		Speed = 0.5f;
		DetectRange = 20.0f;
		AttackRange = 6.0f;
		HunTime = decompte;
		if(Vitesse==0)Vitesse=5;

		Wander = true;
		Hunt = false;
		Atack = false;
		Countdown = false;
		ComeBack = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(PlayerLife <= 0)
		{
			ComeBack = true;
			Atack = false;
			Countdown = false;
			Hunt = false;
		}

		if(EnemyVitality < 0)
		{
			Death();
		}
		
		if(Wander)
		{
			transform.position += transform.TransformDirection(Vector3.forward)* Speed *Time.deltaTime;
			if((transform.position - Wandir).magnitude < 2)
			{
				Wandering();
			}
		}

		if(Hunt)
		{

			TargetDir = (Player.transform.position - transform.position).normalized;
			TargetDist = Vector3.Distance(transform.position,Player.transform.position);
			if(TargetDist > AttackRange)
			{
				ComeBack = false;
				Hunting();
			}
			if(TargetDist < DetectDist)
				HunTime=decompte;
		}


		if(Countdown)
		{
			HunTime -= Time.deltaTime;
			//Debug.Log(HunTime);
			if(HunTime < 0)
			{
				Countdown = false;
				Hunt = false;
				ComeBack = true;
			}
		}

		if(ComeBack)
		{
			TargetDir = (InitialPos - transform.position).normalized;
			ComingHome();
			TargetDist = Vector3.Distance(transform.position,InitialPos);
		
			if(TargetDist < 1)
			{
				ComeBack = false;
				Wander = false;
				SetValues();
				Wandering();
			}
		}

		DetectDist = Vector3.Distance(transform.position, Player.transform.position);
		if(DetectDist < DetectRange)
		{
			PlayerLife = PlayerCaract.GetVitality();
			//Debug.Log("Player detected");
			if(PlayerLife > 0)
			{
				Wander = false;
				Hunt = true;
				Countdown = true;
			}
		}
		if(DetectDist < AttackRange)
		{
			Atack = true;
			Countdown = false;
			Hunt = true;

		}
		else
			Atack =false;
	}

	void Wandering()
	{
		Wandir =  InitialPos + Random.insideUnitSphere * 30;
		//Wandir.y = 30.5f;
		//transform.LookAt(Wandir);
	}

	void Hunting()
	{
		//Avancé et rotation
		transform.LookAt(Player.transform);
		transform.position += transform.forward * (Vitesse * Time.deltaTime);
	}

	void ComingHome()
	{
		// check for forward raycast
		if (Physics.Raycast(transform.position, transform.forward, out hit, 5))
		{
			if (hit.transform != this.transform)
			{

				
				TargetDir += hit.normal * 20;
			}
		}
		
		// Side raycasts   
		Vector3 leftRay = transform.position + new Vector3(-0.125f, 0f, 0f);
		Vector3 rightRay = transform.position + new Vector3(0.125f, 0f, 0f);
		
		// check for leftRay raycast
		if (Physics.Raycast(leftRay, transform.forward, out hit, 5)) // 20 is raycast distance
		{
			if (hit.transform != this.transform)
			{
			
				
				TargetDir += hit.normal * 20; // 20 is force to repel by
			}
		}
		
		// check for rightRay raycast
		if (Physics.Raycast(rightRay, transform.forward, out hit, 5)) // 20 is raycast distance
		{
			if (hit.transform != this.transform)
			{
			
				
				TargetDir += hit.normal * 20; // 20 is force to repel by
			}
		}
		
		// Deplacements
		
		// rotation
		TargetRot = Quaternion.LookRotation (TargetDir);
		
		//Avancé et rotation
		transform.rotation = Quaternion.Slerp (transform.rotation, TargetRot, Time.deltaTime);
		transform.position += transform.forward * (Vitesse * Time.deltaTime);

	}

	void Attack()
	{
		if(Atack)
		{
			PlayerCaract.SetDeltaVitality(-5);
			return;
		}
		else{
			return;
		}
	}

	void Death()
	{
		PlayerCaract.updateXp(xpGave, true);
		Destroy(gameObject);
	}

	public void SetVitality( float value )
	{
		EnemyVitality = value;
	}

	public float GetVitality()
	{
		return EnemyVitality;
	}

	public void UpdateVitality( float value )
	{
		EnemyVitality += value;
	}
}
