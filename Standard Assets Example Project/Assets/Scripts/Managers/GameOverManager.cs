using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float restartDelay = 5;

    Animator anim;
    float restartTimer;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        //the check for the player being alive
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");//triger the animation just made
            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                SceneManager.LoadScene("Level 01");
                //Application.LoadLevel(Application.loadedLevel);//restart
            }
        }
    }
}
