using UnityEngine;
using System.Collections;

public class CubiSpawner : MonoBehaviour 
{
	//Prefab original
	private GameObject Aigle;
	private GameObject Loup;

	private SpawnCoord myCoord;
	private int nbEnemy = 35;
	
	// Use this for initialization
	void Start () 
	{
		Aigle = Resources.Load("Prefab/Aigle") as GameObject;
		Loup = Resources.Load("Prefab/Loup") as GameObject;
		myCoord = gameObject.GetComponent<SpawnCoord>();
		Spawn();
	}

	void Spawn()
	{
		for(int i=0; i<nbEnemy -1 ;i++)
		{
		GameObject Enemy1 = Instantiate(Aigle, myCoord.Get_Coord(i), new Quaternion(0,0,0,1)) as GameObject;
			Enemy1.transform.parent = transform;

		GameObject Enemy2 = Instantiate(Loup, myCoord.Get_Coord(i+1), new Quaternion(0,0,0,1)) as GameObject;
			Enemy2.transform.parent = transform;
			i++;
		}
	}
}
