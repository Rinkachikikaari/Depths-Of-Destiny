using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    [SerializeField] private List<GameObject> listaCorazones;
    [SerializeField] private Sprite corazonDesactivado;
    [SerializeField] private Sprite corazonMitad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RestaCorazones(int indice)
    {
        // Verificar si el índice está dentro del rango válido
        if (indice >= 0 && indice < listaCorazones.Count)
        {
            Image imagenCorazon = listaCorazones[indice].GetComponent<Image>();

            // Verificar si el índice es par o impar
            if (indice % 2 == 0) // Si el índice es par
            {
                imagenCorazon.sprite = corazonDesactivado;
            }
            else // Si el índice es impar
            {
                imagenCorazon.sprite = corazonMitad;
            }
        }
        else
        {
            SceneManager.LoadScene(5);
        }
    }
}
