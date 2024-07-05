using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public int minEnemyCount = 1;
    public int maxEnemyCount = 5;
    private List<GameObject> enemies = new List<GameObject>();
    public List<Transform> spawnPoints = new List<Transform>();

    void Start()
    {
        // Encontrar todos los puntos de spawn en la sala
        foreach (Transform child in transform)
        {
            if (child.CompareTag("SpawnPoint"))
            {
                spawnPoints.Add(child);
            }
        }
    }

    public void SpawnEnemies()
    {
        if (enemies.Count == 0 && spawnPoints.Count > 0)
        {
            int enemyCount = Random.Range(minEnemyCount, maxEnemyCount + 1);
            for (int i = 0; i < enemyCount; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                enemies.Add(enemy);
            }
        }
    }

    public bool AreAllEnemiesDead()
    {
        enemies.RemoveAll(enemy => enemy == null);
        return enemies.Count == 0;
    }
}
