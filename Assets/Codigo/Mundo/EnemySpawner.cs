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
       
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invoke("SpawnEnemies",1);
        }
    }

    public void SpawnEnemies()
    {
        List<int> posibilities = new List<int>();
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            int j = i;
            posibilities.Add(j);
        }
        if (enemies.Count == 0 && spawnPoints.Count > 0)
        {
            int enemyCount = Random.Range(minEnemyCount, maxEnemyCount + 1);
            for (int i = 0; i < enemyCount; i++)
            {
                int pointID = Random.Range(0, posibilities.Count);
                int point = posibilities[pointID];
                posibilities.RemoveAt(pointID);

                Transform spawnPoint = spawnPoints[point];
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                enemy.transform.parent = this.transform;
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
