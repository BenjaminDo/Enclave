using UnityEngine;
using System.Collections;

public class ActionBar : MonoBehaviour 
{
	//Caractéristiques
	private CharacterCaracteristics myCaract;

	//Skills
	private Texture2D Case;
	private Texture2D Estocad;
	private Texture2D Salve;
	private Texture2D Parade;
	private Texture2D Pourfendeur;
	private Texture2D Hachoir;

	//Skills ToolTips
	private Texture2D TipsEstocad;
	private Texture2D TipsSalve;
	private Texture2D TipsParade;
	private Texture2D TipsPourfendeur;
	private Texture2D TipsHachoir;

	//Life
	private Texture2D EmptyLife;
	private Texture2D myLife;

	//SangFroid
	private Texture2D EmptySF;
	private Texture2D mySF;
	private GameObject SfText;

	//Vaporine
	private Texture2D EmptyVap;
	private Texture2D myVap;
	private GameObject VapText;

	public float PlayerLife;
	private float max_NbSf = 4;
	private float max_NbVap = 4;
	private float NbSf = 4;
	private float NbVap = 0;

	private float VaporineDelay = 10;
	private float SangFroidDelay = 10;
	private float SfFactor;
	private float VapFactor;

	// Use this for initialization
	void Start () 
	{
		//Caractéristiques
		myCaract = gameObject.GetComponent<CharacterCaracteristics>();
		PlayerLife = myCaract.GetVitality();

		//Background
		EmptyLife =  Resources.Load("GUI/ActionBar/lifebar")as Texture2D;
		EmptySF = Resources.Load("GUI/ActionBar/sfvide")as Texture2D;
		EmptyVap = Resources.Load("GUI/ActionBar/vapovide")as Texture2D;
		Case = Resources.Load("GUI/ActionBar/case")as Texture2D;

		Estocad = Resources.Load("GUI/ActionBar/Estocad") as Texture2D;
		Salve = Resources.Load("GUI/ActionBar/Salve") as Texture2D;
		Parade = Resources.Load("GUI/ActionBar/Parade") as Texture2D;
		Pourfendeur = Resources.Load("GUI/ActionBar/Pourfendeur") as Texture2D;
		Hachoir = Resources.Load("GUI/ActionBar/Hachoir") as Texture2D;

		TipsEstocad = Resources.Load("GUI/ToolTipAttack/Estocade") as Texture2D;
		TipsSalve = Resources.Load("GUI/ToolTipAttack/Salve de couteaux") as Texture2D;
		TipsParade = Resources.Load("GUI/ToolTipAttack/Parade") as Texture2D;
		TipsPourfendeur = Resources.Load("GUI/ToolTipAttack/Pourfendeur") as Texture2D;
		TipsHachoir = Resources.Load("GUI/ToolTipAttack/Hachoir") as Texture2D;

		//Foreground
		myLife =  Resources.Load("GUI/ActionBar/FullLife")as Texture2D;
		mySF =  Resources.Load("GUI/ActionBar/SfFull") as Texture2D;
		myVap =  Resources.Load("GUI/ActionBar/RageFull") as Texture2D;

		InvokeRepeating("IncreaseVap", 2, VaporineDelay);
		InvokeRepeating("IncreaseSf", 2, SangFroidDelay);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(NbSf > max_NbSf)
			NbSf = max_NbSf;

		if(NbVap > max_NbVap)
			NbVap = max_NbVap;


		if(NbSf < 0)
			NbSf = 0;
		
		if(NbVap < 0)
			NbVap = 0;


		SfFactor = (4 - NbSf)/4*180;
		VapFactor = (4 - NbVap)/4*180;

		PlayerLife = myCaract.GetVitality();
	}

	void OnGUI()
	{

		GUI.DrawTexture(new Rect(Screen.width / 2 - 183, Screen.height - 148 ,300,50), EmptyLife);

		//Life
		GUI.BeginGroup(new Rect(Screen.width / 2 - 183, Screen.height - 148 ,(PlayerLife * 300 / myCaract.getMaxLife()),50));
		GUI.DrawTexture(new Rect(0, 0,300,50), myLife);
		GUI.EndGroup();

		GUI.DrawTexture(new Rect(Screen.width / 2 - 180, Screen.height - 100,60,60), Estocad);
		GUI.DrawTexture(new Rect(Screen.width / 2 - 120, Screen.height - 100,60,60), Salve);
		GUI.DrawTexture(new Rect(Screen.width / 2 - 60, Screen.height - 100,60,60), Parade);
		GUI.DrawTexture(new Rect(Screen.width / 2, Screen.height - 100,60,60), Pourfendeur);
		GUI.DrawTexture(new Rect(Screen.width / 2 + 60, Screen.height - 100,60,60), Hachoir);
		//GUI.DrawTexture(new Rect(Screen.width / 2 + 60, Screen.height - 100,60,60), Case);

		//Sang-Froid
		GUI.DrawTexture(new Rect(Screen.width / 2 - 275, Screen.height - 180 ,100,180), EmptySF);
		GUI.BeginGroup(new Rect(Screen.width / 2 - 275, (Screen.height - 180)+ SfFactor ,100,180));
		GUI.DrawTexture(new Rect(0,-SfFactor,100,180), mySF);
		GUI.EndGroup();

		//Vaporine
		GUI.DrawTexture(new Rect(Screen.width / 2 + 110, Screen.height - 180 ,70,180), EmptyVap);
		GUI.BeginGroup(new Rect(Screen.width / 2 + 110, (Screen.height - 180)+ VapFactor ,70,180));
		GUI.DrawTexture(new Rect(0,-VapFactor,70,180), myVap);
		GUI.EndGroup();


		//TollTips Attack
		if(new Rect(Screen.width / 2 - 180, Screen.height - 100 ,60,60).Contains(Event.current.mousePosition))
		{
			GUI.DrawTexture(new Rect(0, Screen.height - 200 ,368,200), TipsEstocad);
		}
		if(new Rect(Screen.width / 2 - 120, Screen.height - 100,60,60).Contains(Event.current.mousePosition))
		{
			GUI.DrawTexture(new Rect(0, Screen.height - 200 ,368,200), TipsSalve);
		}
		if(new Rect(Screen.width / 2 - 60, Screen.height - 100 ,60,60).Contains(Event.current.mousePosition))
		{
			GUI.DrawTexture(new Rect(0, Screen.height - 200 ,368,200), TipsParade);
		}
		if(new Rect(Screen.width / 2, Screen.height - 100 ,60,60).Contains(Event.current.mousePosition))
		{
			GUI.DrawTexture(new Rect(0, Screen.height - 200 ,368,200), TipsPourfendeur);
		}
		if(new Rect(Screen.width / 2 + 60, Screen.height - 100 ,60,60).Contains(Event.current.mousePosition))
		{
			GUI.DrawTexture(new Rect(0, Screen.height - 200 ,368,200), TipsHachoir);
		}

	}

	void IncreaseVap()
	{
		NbVap -= 1;
	}

	void IncreaseSf()
	{
		NbSf += 1;
	}

	public bool useVap(int nbUse)
	{
		if(NbVap < nbUse){
			SfText =  Instantiate(Resources.Load("Prefab/SfUse"),new Vector3(0.4f,0.6f,0f), Quaternion.identity) as GameObject;
			SfText.guiText.color = Color.red;
			SfText.guiText.text = "Pas assez de Vaporine";
			return false;
		}

		NbVap -= nbUse;
		NbSf += nbUse; // Pour l'équilibre
		return true;
	}

	public bool useSf(int nbUse)
	{
		if(NbSf < nbUse){
			SfText =  Instantiate(Resources.Load("Prefab/SfUse"),new Vector3(0.4f,0.6f,0f), Quaternion.identity) as GameObject;
			SfText.guiText.text = "Pas assez de Sang Froid";
			return false;
		}


		SfText =  Instantiate(Resources.Load("Prefab/SfUse"),new Vector3(0.37f,0.22f,0f), Quaternion.identity) as GameObject;
		SfText.guiText.text = "- " + nbUse.ToString();

		VapText =  Instantiate(Resources.Load("Prefab/SfUse"),new Vector3(0.60f,0.22f,0f), Quaternion.identity) as GameObject;
		VapText.guiText.color = Color.red;
		VapText.guiText.text = "+ " + nbUse.ToString();

		NbSf -= nbUse;
		NbVap += nbUse; // Pour l'équilibre
		return true;
	}
}
