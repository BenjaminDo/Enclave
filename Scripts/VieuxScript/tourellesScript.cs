using UnityEngine;
using System.Collections;

public class tourellesScript : MonoBehaviour {

	public class Stuff
    {
        public int bullets;
        public int grenades;
        public int rockets;
        
        public Stuff(int bul, int gre, int roc)
        {
            bullets = bul;
            grenades = gre;
            rockets = roc;
        }
    }
    
    
    public Stuff myStuff = new Stuff(100, 7, 25);
    public float speedShoot;
    public float turnSpeed;
    public Rigidbody bulletPrefab;
    public Transform firePosition;
    public float bulletSpeed;
	public int distanceShoot;
    
	private GameObject[] zombies;
	private float exShoot;
    
    void Update ()
    {
        Movement();
    }
    
    
    void Movement ()
    {
		zombies = GameObject.FindGameObjectsWithTag("Zombie");
		Vector3 dirToTarget;
		
		 for(int i = 0; i < zombies.Length; i++)
        {
			dirToTarget = zombies[i].transform.position - transform.position;//Vector entre tourelle et Zombie
			
            if(dirToTarget.magnitude < distanceShoot)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation,  Quaternion.LookRotation(dirToTarget), turnSpeed * Global.deltaTime);
				Shoot();
				break;
			}
        }
		
        //float forwardMovement = Input.GetAxis("Vertical") * speed * Global.deltaTime;
        //float turnMovement = Input.GetAxis("Horizontal") * turnSpeed * Global.deltaTime;
        
        //transform.Translate(Vector3.forward * forwardMovement);
       
    }
    
    
    void Shoot ()
    {
		
        if(myStuff.bullets > 0 && (exShoot + speedShoot < Global.fixedTime) )
        {
            Rigidbody bulletInstance = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation) as Rigidbody;
            bulletInstance.AddForce(firePosition.forward * bulletSpeed);
            myStuff.bullets--;
			exShoot = Global.fixedTime;
        }
    }
}
