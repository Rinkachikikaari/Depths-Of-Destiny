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
        // Verificar si el �ndice est� dentro del rango v�lido
        if (indice >= 0 && indice < listaCorazones.Count)
        {
            Image imagenCorazon = listaCorazones[indice].GetComponent<Image>();

            // Verificar si el �ndice es par o impar
            if (indice % 2 == 0) // Si el �ndice es par
            {
                imagenCorazon.sprite = corazonDesactivado;
            }
            else // Si el �ndice es impar
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
