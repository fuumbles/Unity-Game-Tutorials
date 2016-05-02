using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    void Start ()
    {
        //spawner
        //call a spawn function, then use the spawn times to repeat every spawnTime seconds
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }

    //spawn function from above
    void Spawn ()
    {
        //so if a player dies and goes AFK there's no more spawns, and the game runs out of memory
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        //alt spawn points and picks a random one for spawning
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        //the enemy, where to spawn, the orientation of the enemy
        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
