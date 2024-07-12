using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public GameObject HeartPrefab;
    public int minEnemyCount = 1;
    public int maxEnemyCount = 5;
    public List<GameObject> enemies = new List<GameObject>();
    public List<Transform> spawnPoints = new List<Transform>();
    public Transform spawnHeart;
    public List<Tp> doors = new List<Tp>();
    public bool noHayEnemigos = true;
    public bool spawnedOnce = false;
    public bool si = false;
    [SerializeField] UnityEvent onAllEnemiesDead;
    public SpriteRenderer Map;
    public Color originalColor;
    public Color Aqui;

    void Start()
    {
        // Actualiza el estado inicial de noHayEnemigos
        noHayEnemigos = AreAllEnemiesDead();
        Map.color = originalColor;

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CambiaColor();
            Invoke("SpawnEnemies", 1);
        }
        Map.color = originalColor;
    }

    public void SpawnEnemies()
    {
        if(spawnedOnce == true) 
        {
            return;
        }

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
            if(enemyCount > 0)
            {
                // Cerrar Puertas
                for (int i = 0; i < doors.Count; i++)
                {
                    doors[i].CloseDoor();
                }
            }
            spawnedOnce = true;
        }

        // Actualiza el estado de noHayEnemigos después de spawn
        bool lastNoHayEnemies = noHayEnemigos;
        
        noHayEnemigos = AreAllEnemiesDead();

        if (lastNoHayEnemies == false && noHayEnemigos == true)
        {
            onAllEnemiesDead.Invoke();
        }

    }

    public bool AreAllEnemiesDead()
    {
        enemies.RemoveAll(enemy => enemy == null);
        bool allDead = enemies.Count == 0;
        noHayEnemigos = allDead; // Actualiza el booleano aquí también
        // Abrir Puertas
        if (noHayEnemigos) { 
            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].OpenDoor();
            }
        }
        return allDead;
 
    }
    public void Item()
    {
        int Corazon = Random.Range(0, 2);
        if (Corazon == 1 && !si)
        {

            GameObject Heart = Instantiate(HeartPrefab, spawnHeart.position, Quaternion.identity);
            si = true;

        }
        if (Corazon == 2 && !si)
        {
            GameObject Heart = Instantiate(HeartPrefab, spawnHeart.position, Quaternion.identity);
            si = true;
        }
        Debug.Log("mala cuea");

    }

    void Update()
    {
        // Puedes llamar a AreAllEnemiesDead periódicamente en el Update si es necesario
        if (spawnedOnce && noHayEnemigos)
        {
            Item();
        }
        AreAllEnemiesDead();
    }
    void CambiaColor()
    {
        Map.color = Aqui;
    }
}
