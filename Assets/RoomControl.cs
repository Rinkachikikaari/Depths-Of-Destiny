using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{
    public GameObject[] walls;
    public GameObject[] doors;
    public EnemySpawner enemySpawner;
    public bool isFirstRoom = false;
    public bool isUniqueRoom = false;

    private bool enemiesSpawned = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleWallsAndDoors(false);

            if (!enemiesSpawned && !isFirstRoom && !isUniqueRoom)
            {
                enemySpawner.SpawnEnemies();
                enemiesSpawned = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFirstRoom && !isUniqueRoom)
        {
            if (enemySpawner.AreAllEnemiesDead())
            {
                ToggleWallsAndDoors(true);
            }
        }
    }

    void ToggleWallsAndDoors(bool state)
    {
        foreach (GameObject wall in walls)
        {
            wall.SetActive(state);
        }
        foreach (GameObject door in doors)
        {
            door.SetActive(!state);
        }
    }

    public void PrepareRoom(bool L, bool U, bool D, bool R)
    {
        doors[0].SetActive(U); // Upper Door
        doors[1].SetActive(D); // Down Door
        doors[2].SetActive(R); // Right Door
        doors[3].SetActive(L); // Left Door
    }
}
