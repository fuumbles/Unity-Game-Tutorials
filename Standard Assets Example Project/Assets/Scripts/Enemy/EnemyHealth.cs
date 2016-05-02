using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //all of this is the same as the player info
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;//sink through the floor speed
    public int scoreValue = 10;//high scores!
    public AudioClip deathClip;//sound


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {

        if(isSinking)
        {
            //if the enemy is sinking, actually sink through the floor, and despawn
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    //called by the player attack script - it's public
    public void TakeDamage (int amount, Vector3 hitPoint)//hit point is for the stuffing hit location
    {
        if(isDead)//break out if dead
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();//the fluff

        if(currentHealth == 0)//ded
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true; // no longer hittable

        anim.SetTrigger ("Dead");//trigger the animation

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
        StartSinking();
    }


    public void StartSinking ()
    {

        GetComponent <NavMeshAgent> ().enabled = false; // not a physical body anymore
        GetComponent <Rigidbody> ().isKinematic = true; // other enemies ignore this one now
        isSinking = true;
        ScoreManager.score += scoreValue;//adding to the score
        Destroy (gameObject, 2f);//when the time sinking is so long, destroy the dead enemy - cleanup
    }
}
