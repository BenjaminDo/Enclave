using UnityEngine;
using System.Collections;

public class CubiSpawner : MonoBehaviour 
{
	//Prefab original
	private GameObject Enemy;

	private SpawnCoord myCoord;
	private int nbEnemy = 35;
	
	// Use this for initialization
	void Start () 
	{
		Enemy = Resources.Load("Prefab/Evil Cuby") as GameObject;
		myCoord = gameObject.GetComponent<SpawnCoord>();
		Spawn();
	}

	void Spawn()
	{
		for(int i=0; i<nbEnemy ;i++)
		{
			GameObject cubi = Instantiate(Enemy, myCoord.Get_Coord(i), new Quaternion(0,0,0,1)) as GameObject;
			cubi.transform.parent = transform;
		}
	}
}
