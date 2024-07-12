using System.Collections;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo que el Boss invocará
    public Transform[] spawnPoints; // Puntos alrededor del Boss donde los enemigos serán invocados
    private EnemySpawner enemySpawner;
    public float Cooldown = 4;

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>(); // Encuentra el EnemySpawner en la escena
        StartCoroutine(SpawnEnemiesPeriodically());
    }

    IEnumerator SpawnEnemiesPeriodically()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(Cooldown); // Invoca un enemigo cada 2 segundos
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length > 0)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemySpawner.enemies.Add(enemy); // Agrega el enemigo a la lista en EnemySpawner
            Debug.Log("Enemy spawned at " + spawnPoint.position); // Log de verificación
        }
    }

}
