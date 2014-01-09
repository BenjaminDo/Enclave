using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]

public class CharacterCaracteristics : MonoBehaviour 
{
	private GameObject SfText;
	public GUIText textLevel;
	private float progress = 0.0f;

	private float PlayerForce;
	public int PlayerVitality;
	private float PlayerSpeed;
	private int maxLife = 100;

	private int xp;
	private int level;


	private float LifeDelay = 10;
	
	private bool parade = false;
	private GameObject Text;
	
	//Animation
	
	[System.NonSerialized]					
	public float lookWeight;					// the amount to transition when using head look
	
	[System.NonSerialized]
	public Transform enemy;						// a transform to Lerp the camera to during head look
	
	public float animSpeed = 1.5f;				// a public setting for overall animator animation speed
	public float lookSmoother = 3f;				// a smoothing setting for camera motion
	public bool useCurves;						// a setting for teaching purposes to show use of curves
	

	private Animator anim;							// a reference to the animator on the character
	private AnimatorStateInfo currentBaseState;			// a reference to the current state of the animator, used for base layer
	private AnimatorStateInfo layer2CurrentState;	// a reference to the current state of the animator, used for layer 2
	private CapsuleCollider col;					// a reference to the capsule collider of the character
	
	
	static int idleState = Animator.StringToHash("Base Layer.Idle");	
	static int locoState = Animator.StringToHash("Base Layer.Locomotion");			// these integers are references to our animator's states
	static int jumpState = Animator.StringToHash("Base Layer.Jump");				// and are used to check state for various actions to occur
	static int waveState = Animator.StringToHash("Layer2.Wave");
	
	// Use this for initialization
	void Start () 
	{
		PlayerForce = 100;
		PlayerVitality = 100;
		PlayerSpeed = 100;

		xp = 0;
		level = 1;

		InvokeRepeating("IncreaseLife", 2, LifeDelay);
		
		//Animation
		// initialising reference variables
		anim = GetComponent<Animator>();					  
		col = GetComponent<CapsuleCollider>();				
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(PlayerVitality < 0.5)
		{
			Death();
		}
		
		if(PlayerVitality > maxLife)
			PlayerVitality = maxLife;

		Text =  Instantiate(Resources.Load("Prefab/level"),new Vector3(0.97f,0.98f,0f), Quaternion.identity) as GameObject;
		Text.guiText.text = level.ToString();
	}
	
	//Animation
	void FixedUpdate ()
	{
		//float h = Input.GetAxis("Horizontal");				// setup h variable as our horizontal input axis
		float v = moveOnMouseClic.moveSpeed;				// setup v variables as our vertical input axis
		anim.SetFloat("Speed", v);							// set our animator's float parameter 'Speed' equal to the vertical input axis				
		//anim.SetFloat("Direction", h); 						// set our animator's float parameter 'Direction' equal to the horizontal input axis		
		anim.speed = animSpeed;								// set the speed of our animator to the public variable 'animSpeed'
		anim.SetLookAtWeight(lookWeight);					// set the Look At Weight - amount to use look at IK vs using the head's animation
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation
		
		if(anim.layerCount ==2)		
			layer2CurrentState = anim.GetCurrentAnimatorStateInfo(1);	// set our layer2CurrentState variable to the current state of the second Layer (1)
		
		// STANDARD JUMPING
		
		// if we are currently in a state called Locomotion (see line 25), then allow Jump input (Space) to set the Jump bool parameter in the Animator to true
		if (currentBaseState.nameHash == locoState)
		{
			if(Input.GetButtonDown("Jump"))
			{
				anim.SetBool("Jump", true);
			}
		}
		
		// if we are in the jumping state... 
		else if(currentBaseState.nameHash == jumpState)
		{
			//  ..and not still in transition..
			if(!anim.IsInTransition(0))
			{
				if(useCurves)
					// ..set the collider height to a float curve in the clip called ColliderHeight
					col.height = anim.GetFloat("ColliderHeight");
				
				// reset the Jump bool so we can jump again, and so that the state does not loop 
				anim.SetBool("Jump", false);
			}
			
			// Raycast down from the center of the character.. 
			Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
			RaycastHit hitInfo = new RaycastHit();
			
			if (Physics.Raycast(ray, out hitInfo))
			{
				// ..if distance to the ground is more than 1.75, use Match Target
				if (hitInfo.distance > 1.75f)
				{
					
					// MatchTarget allows us to take over animation and smoothly transition our character towards a location - the hit point from the ray.
					// Here we're telling the Root of the character to only be influenced on the Y axis (MatchTargetWeightMask) and only occur between 0.35 and 0.5
					// of the timeline of our animation clip
					anim.MatchTarget(hitInfo.point, Quaternion.identity, AvatarTarget.Root, new MatchTargetWeightMask(new Vector3(0, 1, 0), 0), 0.35f, 0.5f);
				}
			}
		}
	}
	
	void IncreaseLife()
	{
		PlayerVitality += 1;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Water")
		{
			Death();
		}
	}
	
	void Death()
	{
		AudioClip Sound = Resources.Load("Sound/mort_hero") as AudioClip;
		audio.PlayOneShot(Sound);

		Application.LoadLevel(0);
	}
	
	public void SetForce( float value )
	{
		PlayerForce = value;
	}
	
	public float GetForce()
	{
		return PlayerForce;
	}
	
	/*** Avec value le chagment ***/
	public void SetDeltaVitality( int value )
	{
		if(value < 0 && parade){
			parade = false;
			SfText =  Instantiate(Resources.Load("Prefab/SfUse"),new Vector3(0.43f,0.32f,0f), Quaternion.identity) as GameObject;
			SfText.guiText.fontSize = 50;
			SfText.guiText.color = new Color32(13,157,20,255);
			SfText.guiText.text = "PAAARAAAAAADE !!!";


			return;
		}

		PlayerVitality += value;
	}
	
	public void SetVitality( int value )
	{
		PlayerVitality = value;
	}
	
	public int GetVitality()
	{
		return PlayerVitality;
	}
	
	public void SetSpeed( float value )
	{
		PlayerSpeed = value;
	}
	
	public float GetSpeed()
	{
		return PlayerSpeed;
	}
	
	public void setParade(bool bParade)
	{
		parade = bParade;
	}

	public int getXp(){
		return xp;
	}

	public void updateXp(int a, bool b){
		xp += a;

		SfText =  Instantiate(Resources.Load("Prefab/SfUse"),new Vector3(0.445f,0.6f,0f), Quaternion.identity) as GameObject;
		SfText.guiText.fontSize = 60;
		if(b){
			SfText.guiText.color = new Color32(91,3,24,160);
			SfText.guiText.text = "Kill !";
		}else{
			SfText.guiText.color = new Color32(11,230,34,160);
			SfText.guiText.text = "+XP";
		}

		if(xp > level*10 ){ // Linéaire levelUp tout les 10 xp{
			PlayerForce += 30;
			maxLife += 10;

			level++;
			xp=0;
		}

		progress = (float)xp/(level*10);
	}

	public int getLevel(){
		return level;
	}

	public int getMaxLife(){
		return maxLife;
	}

	void OnGUI () {
		DrawRectangle( new Rect(Screen.width-100, 0, 100, 20), new Color(0f ,0.0f ,0.1f ,0.65f));
		DrawRectangle( new Rect(Screen.width-100, 0, progress*100, 20), new Color(0f ,0.9f ,1f ,1f)); 
		/*// Constrain all drawing to be within a pixel area .
		GUI.BeginGroup (new Rect (Screen.width-100, 0, progress*100, 20));
		
		// Define progress bar texture within customStyle under Normal > Background
		GUI.Box (new Rect (0,0, 100, 200), "Expérience");
		
		// Always match BeginGroup calls with an EndGroup call
		GUI.EndGroup ();*/
	}

	void DrawRectangle (Rect position, Color color)
	{    
		// We shouldn't draw until we are told to do so.
		if (Event.current.type != EventType.Repaint)
			return;
		
		// Optimization hint: 
		// Consider Graphics.DrawMeshNow
		GL.Color (color);
		GL.Begin (GL.QUADS);
		GL.Vertex3 (position.x, position.y, 0);
		GL.Vertex3 (position.x + position.width, position.y, 0);
		GL.Vertex3 (position.x + position.width, position.y + position.height, 0);
		GL.Vertex3 (position.x, position.y + position.height, 0);
		GL.End ();
	}
	
	void DrawQuad(Rect position, Color color) {
		Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel(0,0,color);
		texture.Apply();
		GUI.skin.box.normal.background = texture;
		GUI.Box(position, GUIContent.none);
	}
}