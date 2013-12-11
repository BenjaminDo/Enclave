using UnityEngine;
using System.Collections;

/*	Script to link to a Gemstone of the Main Menu
 * 
 * 	Each Gemstone had the following componants : Light, Ellipsoid Particle Emiter, Particle Emiter,Particle Animator, Particle Renderer 
 * 
 * 	Must be present on the scene : GUI Text (already configurate)
 * 
 * 	Role :
 * 			-	Looping the glowing/pulsing effect of the object
 * 			-	Change the light color when hovered
 * 			-	Star the particle when hovered / Stop it when it's not
 * 			-	Display the text of the menu
 * 
 * Sources : - http://docs.unity3d.com/Documentation/ScriptReference/ParticleEmitter.html
 * 			 - http://docs.unity3d.com/Documentation/ScriptReference/ParticleAnimator.html
 * 			 - http://docs.unity3d.com/Documentation/ScriptReference/Light.html
 * 			 - http://answers.unity3d.com/questions/41931/how-to-randomly-change-the-intensity-of-a-point-li.html
 * 			 - http://docs.unity3d.com/Documentation/ScriptReference/GUIText.html
 * 
 * ToDo : 
 * 			- Trouver comment ajouter les Composant directement en scripts ; Done (May slow the starting??)
 * 			- Trouver comment charger les textures pour le ParticleRenderer
 * 
 * Manuellement : 
 * 			- Choisir Sparkle2 come material dans Particle Renderer
 * 			- Cocher "Draw Halo" dans Light
 * 
 * Created by Ludivine Barast
 * Version 1.4
 */

public class GeneralGemstone : MonoBehaviour 
{
	/*		MEMBERS			*/
	
	//Déclatation des composant des Particules
	private ParticleAnimator GemPartAnimation;
	private ParticleEmitter GemPartEmitter;
	//ParticleRenderer GemPartRenderer;
	private Color InitialColor;
	private Color OppositeColor;
	
	//Déclaration du tableau de Couleur des particules
	private Color[] GemPartStarkle;
	
	//Déclaration de la lumière
	private Light GemLight;																				
	
	//Membres pour l'effet lumineux
	private float minIntensity;
	private float maxIntensity;
	private float randomIntensity;
	
	private GameObject GemTitle;
	
	public ParticleSystem test;
	
	private ParticleSystem MyPart;
	
	private bool ChangeMode;
	
	
	
	/*			METHODES			*/
	
	//Constructor
	void Awake()
	{
		// A retenir : GemLight = gameObject.AddComponent("Light") as Light;
		
		//Lien avec les composants des particules
		GemPartEmitter = GetComponent<ParticleEmitter>();												//Lien avec l'Emitter des particules
		GemPartAnimation = GetComponent<ParticleAnimator>();											//Lien avec l'Animator des particules
		//GemPartRenderer = GetComponent<ParticleRenderer>();											//Lien avec le Renderer des particules
		InitialColor = Color.yellow;																	//Définition de la couleur initiale de la Gemme
		OppositeColor = Color.magenta;																	//Définition de la couleur opposé de la Gemme
		
		
		//Initialisation du tableau dynamiquement
		GemPartStarkle = new Color[5];																	//Initialisation des 5 couleurs comosant le Renderer
		
		//Lien avec la Lumière
		GemLight = GetComponent<Light>();
		
		GemTitle = GameObject.Find("GUI Text");															//Lien avec le GUIText de la scène
		
		
		//Initialisation des statuts
		GemPartEmitter.emit = false;																	//Dissimulation des particules
		GemTitle.guiText.enabled = false;																//Dissimulation du texte
	
		MyPart = (ParticleSystem) Instantiate(test);
		ChangeMode = false;
	}

	// Use this for initialization
	void Start () 
	{
		//Configuration de l'Emetteur
		GemPartEmitter.minSize = 0.2f;																	//Taille minimale de la particule émise
		GemPartEmitter.maxSize = 0.2f;																	//Taille maximale de la particule émise
		GemPartEmitter.minEmission = 15.0f;																//Nombre minimale de particules émisent
		GemPartEmitter.maxEmission = 25.0f;																//Nombre maximale de particules émisent
		GemPartEmitter.worldVelocity = new Vector3(0.0f,1.0f,0.0f);										//Vitesse d'émission des particules (x,y,z) ; "Distance de projection"
		GemPartEmitter.rndVelocity = new Vector3(2.0f,0.0f,0.0f);										//Angle d'émission des particules (x,y,z)
		
		
		//Configuration de l'Animateur
		GemPartAnimation.sizeGrow = 0.15f;																//Définition de la taille des particule au cours du temps ; "Assez petit, peut etre modifié"
		
		
		//Définition des 5 couleurs et application
		GemPartStarkle[0] = new Color(255,0,0,10);														//Rouge, couleur de la passion
		GemPartStarkle[1] = new Color(0,255,0,180);
		GemPartStarkle[2] = new Color(0,0,255,255);
		GemPartStarkle[3] = new Color(255,0,0,180);
		GemPartStarkle[4] = new Color(255,0,0,10);
		GemPartAnimation.colorAnimation = GemPartStarkle;												//Assignation du tableau a l'ensemble des couleurs
		
		
		//Configuration de la lumière
		GemLight.type = LightType.Spot;																	//Type de lumière
		GemLight.range = 3.0f;																			//Portée de la lumière
		GemLight.spotAngle = 20;																		//Angle de vue de la lumière
		GemLight.color = InitialColor;																	//Couleur de la lumière
		
		//Configuration des effets de lumière
		minIntensity = 0.1f;																			//Intensité minimale
		maxIntensity = 0.8f;																			//Intensité maximale
		randomIntensity = Random.Range(0.0f, 600.0f);													//Tirage aléatoire d'un float pour l'intensité
	
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(ChangeMode == false)
		{
			//Effet de lumière
			float noise = Mathf.PerlinNoise(randomIntensity, Time.time);
        	GemLight.intensity = Mathf.Lerp(minIntensity, maxIntensity,noise);								//La palpitation !!
		}
	}
	
	
	void OnMouseOver()
	{
		if(ChangeMode == false)
		{
			GemTitle.guiText.text = "Continuer";															//Initilaisation du titre de la Gemme
			GemLight.color = OppositeColor;																	//Changement de couleur
			GemPartEmitter.emit = true;																		//Lancement des particules
			GemTitle.guiText.enabled = true;																//Affichage du text
		}
		
		if(ChangeMode == true)
		{
			GemPartEmitter.emit = false;															
			GemPartEmitter.ClearParticles();
			GemLight.renderer.enabled = false;
			GemLight.intensity = 0;
		}
																						
	}
	
	void OnMouseExit()
	{
		if(ChangeMode == false)
		{
			GemLight.color = InitialColor;																	//Changement de couleur
			GemPartEmitter.emit = false;																	//Arret des particules
			GemPartEmitter.ClearParticles();																//Suppresion des particules restantes sur l'écran
			GemTitle.guiText.enabled = false;																//Dissimulation du text
		}
		
		if(ChangeMode == true)
		{
			GemPartEmitter.emit = false;																	//Arret des particules
			GemPartEmitter.ClearParticles();																//Suppresion des particules restantes sur l'écran
			GemLight.renderer.enabled = false;
		}
	}
	
	void OnMouseDown()
	{
		ChangeMode = true;
		MyPart.particleSystem.Play();
		gameObject.renderer.enabled = false;
	}
}
