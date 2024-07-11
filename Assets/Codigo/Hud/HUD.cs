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
            return;
        }
        UpdateHearts();
    }

    public void UpdateHearts()
    {
        if (characterStats == null)
        {
            return;
        }


        int maxHealth = characterStats.currentStats.maxHealth;
        int currentHealth = characterStats.currentStats.currentHealth;
        int totalHearts = maxHealth / 2;


        for (int i = 0; i < listaCorazones.Count; i++)
        {
            Image heartImage = listaCorazones[i].GetComponent<Image>();

            if (i < totalHearts)
            {
                if (currentHealth >= (i + 1) * 2)
                {
                    heartImage.sprite = corazonActivado; // Corazón lleno
                }
                else if (currentHealth == (i * 2) + 1)
                {
                    heartImage.sprite = corazonMitad; // Medio corazón
                }
                else
                {
                    heartImage.sprite = corazonDesactivado; // Corazón vacío
                }
                listaCorazones[i].SetActive(true);
            }
            else
            {
                listaCorazones[i].SetActive(false);
            }
        }
    }

    public void RestaCorazones(int damage)
    {
        if (characterStats == null)
        {
            return;
        }

        characterStats.TakeDamage(damage);
        UpdateHearts();
    }
}
