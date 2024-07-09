using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    [SerializeField] private List<GameObject> listaCorazones;
    [SerializeField] private Sprite corazonActivado;
    [SerializeField] private Sprite corazonDesactivado;
    [SerializeField] private Sprite corazonMitad;

    private CharacterStats characterStats;

    void Start()
    {
        characterStats = FindObjectOfType<CharacterStats>();
        if (characterStats == null)
        {
            Debug.LogError("CharacterStats no encontrado en la escena.");
            return;
        }
        Debug.Log("CharacterStats encontrado: " + characterStats.name);
        UpdateHearts();
    }

    public void UpdateHearts()
    {
        if (characterStats == null)
        {
            Debug.LogError("CharacterStats no está asignado.");
            return;
        }

        Debug.Log("Actualizando corazones...");
        int maxHealth = characterStats.currentStats.maxHealth;
        int currentHealth = characterStats.currentStats.currentHealth;
        int totalHearts = maxHealth / 2;

        Debug.Log("Max Health: " + maxHealth);
        Debug.Log("Current Health: " + currentHealth);
        Debug.Log("Total Hearts: " + totalHearts);

        for (int i = 0; i < listaCorazones.Count; i++)
        {
            Image heartImage = listaCorazones[i].GetComponent<Image>();

            if (i < totalHearts)
            {
                Debug.Log("Heart " + i + ": processing...");
                if (currentHealth >= (i + 1) * 2)
                {
                    Debug.Log("Heart " + i + ": Full heart");
                    heartImage.sprite = corazonActivado; // Corazón lleno
                }
                else if (currentHealth == (i * 2) + 1)
                {
                    Debug.Log("Heart " + i + ": Half heart");
                    heartImage.sprite = corazonMitad; // Medio corazón
                }
                else
                {
                    Debug.Log("Heart " + i + ": Empty heart");
                    heartImage.sprite = corazonDesactivado; // Corazón vacío
                }
                listaCorazones[i].SetActive(true);
            }
            else
            {
                listaCorazones[i].SetActive(false);
                Debug.Log("Heart " + i + ": Disabled");
            }
        }
    }

    public void RestaCorazones(int damage)
    {
        if (characterStats == null)
        {
            Debug.LogError("CharacterStats no está asignado.");
            return;
        }

        characterStats.TakeDamage(damage);
        UpdateHearts();
    }
}
