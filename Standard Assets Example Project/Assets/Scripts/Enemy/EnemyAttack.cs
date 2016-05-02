using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{

    //all commenting is mine - KM


    public float timeBetweenAttacks = 0.5f;//bunny attack speed
    public int attackDamage = 10;//attack is fed to the damage taken method in the playerhealth script


    Animator anim;
    GameObject player;//getting the player reference
    PlayerHealth playerHealth;//getting the player health, and access the damagetaken function
    EnemyHealth enemyHealth;//reference to the enemy hp script
    bool playerInRange;
    float timer;


    void Awake ()
    {
        player = GameObject.Find("Player");//locating the player
        playerHealth = player.GetComponent <PlayerHealth> ();//getting the player health script based on the tag above
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)//calls a function when a trigger is triggered by colliding 
    {
        //with the player, and making the attack go out
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)//a call when another entity EXITS the detection range of the collision
    {
        //also checking if the player running away and letting the enemy not sit there swinging

        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        //the caller for the attack method that checks for timer && the enemy is actually alive/hp is more than 0
        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        //dance party
        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");//rip player - enemies win, setting idle state
        }
    }


    void Attack ()
    {
        timer = 0f;//swing timer is reset

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);//calling the other script function
        }
    }
}
