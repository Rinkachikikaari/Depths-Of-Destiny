using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    public GameObject Next;
    public GameObject PowerUp;

    void Update()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        // Puedes llamar a AreAllEnemiesDead periódicamente en el Update si es necesario
        if (enemySpawner.spawnedOnce && enemySpawner.noHayEnemigos)
        {
            Item();
        }
    }
    void Item()
    {
        Next.SetActive(true);
        PowerUp.SetActive(true);
    }
}
