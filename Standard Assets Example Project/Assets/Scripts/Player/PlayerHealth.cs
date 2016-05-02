using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    //commenting below is mine - KM

    public int startingHealth = 100;
    public int currentHealth;//these two are self explanitory
    public Slider healthSlider;//the slider created in vid5
    public Image damageImage;//the image created in vid5 for damage taken
    public AudioClip deathClip;//oneshot sound that goes over the hurt sound when the player dies
    public float flashSpeed = 5f;//Quickly the damage image flashes on the screen
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake ()
    {
        //references
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();//use the name of the script to get the script
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        //the check for flashing - as update is called once per frame, checking if the player got hit
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            //fade back to transparent
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;//when we take damage this is set back at the end of the frame
    }

    //other scripts can call this function - IE the enemies call this, and reduce the player health, like normal methods
    //in C#
    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;//not defined so enemies can have different damage values

        healthSlider.value = currentHealth; //modifying the slider

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death (); // rip in pepperonies
        }
    }


    void Death ()
    {
        //if health is 0 on damage taken
        isDead = true;//end of game

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");//triggering the animation 'Die'

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene(0);
    }
}
