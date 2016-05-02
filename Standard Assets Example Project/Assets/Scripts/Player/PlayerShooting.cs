using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;//attack speed
    public float range = 100f;


    float timer;//attack speed
    Ray shootRay;//hit detections
    RaycastHit shootHit;//getting whatever was hit
    int shootableMask;//only hitting shootable objects
    ParticleSystem gunParticles;//the particles added 
    LineRenderer gunLine;//^^
    AudioSource gunAudio;//^^
    Light gunLight;//^^
    float effectsDisplayTime = 0.2f;//timer for the above effects


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");//the environment and enemies are all tagged with "Shootable"
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        //attack timer
        timer += Time.deltaTime;

        //fire1 is left ctrl OR lmb
		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        //turning off the effects after a shot
        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }

    //referenceable by another script
    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        //reset swing timer
        timer = 0f;

        gunAudio.Play ();//audio for the gun

        gunLight.enabled = true;//light for shot is on

        gunParticles.Stop ();//restart particles
        gunParticles.Play ();

        gunLine.enabled = true;//turn on visual element
        gunLine.SetPosition (0, transform.position);//start = 0, and start(gun barrel) is at transform.position

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;//move the ray forward from the gun barrel


        //get the hit object - if it exists
        //out variable is what object you hit
        //shootable is declared earlier by the tag "shootable"
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            //getting the enemy health if it hit an enemy
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)// a check if the enemy health is null IE hitting nothing
            {
                //take off enemy object health
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);//shootHit.point is the hit point for the fluff 
                                                                        //coming out of the model
               
            }
            gunLine.SetPosition (1, shootHit.point);//the line end point
        }
        else
        {
            //else we just shoot and the line is created and shown
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
