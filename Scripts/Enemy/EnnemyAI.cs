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
	private float Speed;

	private bool Wander;
	private bool Hunt;
	private bool Countdown;
	private bool ComeBack;
	private Vector3 InitialPos;

	private RaycastHit hit;

	//Parem for Wandering
	private Vector3 Wandir;

	//Param for Hunting
	private GameObject Player;

	private Vector3 TargetDir;
	private Quaternion TargetRot;

	private float DetectDist;
	private float TargetDist;
	private float DetectRange;
	private float AttackRange;

	private float HunTime;

	void Awake()
	{
		SetValues();

		Wander = true;
		Hunt = false;
		Countdown = false;
		ComeBack = false;

		InitialPos = transform.position;
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	// Use this for initialization
	void Start () 
	{
		Wandering();
	}

	void SetValues()
	{
		Speed = 0.5f;
		DetectRange = 8.0f;
		AttackRange = 2.0f;
		HunTime = 30;

	}
	
	// Update is called once per frame
	void Update () 
	{
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
			HunTime -= Time.deltaTime;
			TargetDir = (Player.transform.position - transform.position).normalized;
			TargetDist = Vector3.Distance(transform.position,Player.transform.position);
			if(TargetDist > AttackRange)
			{
				Hunting();
			}
		}

		if(Countdown)
		{
			HunTime -= Time.deltaTime;
			Debug.Log(HunTime);
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
			Debug.Log(TargetDist);
			if(TargetDist < 1)
			{
				ComeBack = false;
				Wander = true;
				SetValues();
				Wandering();
			}
		}

		DetectDist = Vector3.Distance(transform.position, Player.transform.position);
		if(DetectDist < DetectRange)
		{
			Debug.Log("Player detected");
			Wander = false;
			Hunt = true;
			Countdown = true;
		}
	}

	void Wandering()
	{
		Wandir =  InitialPos + Random.insideUnitSphere * 10;
		Wandir.y = 0.5f;
		transform.LookAt(Wandir);
	}

	void Hunting()
	{
		// check for forward raycast
		if (Physics.Raycast(transform.position, transform.forward, out hit, 5))
		{
			if (hit.transform != this.transform)
			{
				Debug.DrawLine (transform.position, hit.point, Color.white);
				
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
				Debug.DrawLine (leftRay, hit.point, Color.red);
				
				TargetDir += hit.normal * 20; // 20 is force to repel by
			}
		}
		
		// check for rightRay raycast
		if (Physics.Raycast(rightRay, transform.forward, out hit, 5)) // 20 is raycast distance
		{
			if (hit.transform != this.transform)
			{
				Debug.DrawLine (rightRay, hit.point, Color.green);
				
				TargetDir += hit.normal * 20; // 20 is force to repel by
			}
		}

		// Deplacements
		
		// rotation
		TargetRot = Quaternion.LookRotation (TargetDir);
		
		//Avancé et rotation
		transform.rotation = Quaternion.Slerp (transform.rotation, TargetRot, Time.deltaTime);
		transform.position += transform.forward * (2 * Time.deltaTime);
	}

	void ComingHome()
	{
		// check for forward raycast
		if (Physics.Raycast(transform.position, transform.forward, out hit, 5))
		{
			if (hit.transform != this.transform)
			{
				Debug.DrawLine (transform.position, hit.point, Color.white);
				
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
				Debug.DrawLine (leftRay, hit.point, Color.red);
				
				TargetDir += hit.normal * 20; // 20 is force to repel by
			}
		}
		
		// check for rightRay raycast
		if (Physics.Raycast(rightRay, transform.forward, out hit, 5)) // 20 is raycast distance
		{
			if (hit.transform != this.transform)
			{
				Debug.DrawLine (rightRay, hit.point, Color.green);
				
				TargetDir += hit.normal * 20; // 20 is force to repel by
			}
		}
		
		// Deplacements
		
		// rotation
		TargetRot = Quaternion.LookRotation (TargetDir);
		
		//Avancé et rotation
		transform.rotation = Quaternion.Slerp (transform.rotation, TargetRot, Time.deltaTime);
		transform.position += transform.forward * (2 * Time.deltaTime);

	}
}
