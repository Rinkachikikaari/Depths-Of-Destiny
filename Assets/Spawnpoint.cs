using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public GameObject[] enemies;

    public int countSpawns;
    public float timeSpawns = 0;
    private int randomTimes;

    public Transform spawnXL;
    public Transform spawnXR;
    public Transform spawnYU;
    public Transform spawnYD;


    public void RandomSpawn()
    { 
        int minTimes = 1; // Minimum number of times to activate the function
        int maxTimes = 5; // Maximum number of times to activate the function

        int randomTimes = Random.Range(minTimes, maxTimes + 1); // Generate a random number of times to activate the function

        for (int i = 0; i < randomTimes; i++)
        {
            Invoke("SpawnEnemies", timeSpawns);
        }
      
    }

    void Update()
    {
        
    }
    public void SpawnEnemies()
    {
        Vector3 spawnPosition = new Vector3(0, 0, 0);

        spawnPosition = new Vector3(Random.Range(spawnXL.position.x, spawnXR.position.x), Random.Range(spawnYD.position.y, spawnYU.position.y),0);

        GameObject enemie = Instantiate(enemies[Random.Range(0,enemies.Length)],spawnPosition,gameObject.transform.rotation);
    }
}
