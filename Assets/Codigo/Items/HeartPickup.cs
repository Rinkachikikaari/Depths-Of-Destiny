using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public Stats statsToAdd;
    private void Start()
    {
        statsToAdd.currentHealth = 2;

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        CharacterStats stats = other.GetComponent<CharacterStats>();
        if (stats != null)
        {
             stats.AddStats(statsToAdd);
             Destroy(gameObject); // Destruir el item después de recogerlo

        }
    }
}
