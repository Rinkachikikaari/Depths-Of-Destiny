using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public GameObject victoryPanel;
    public float delay = 2f; // Tiempo de espera antes de cambiar de escena.

    void Start()
    {
        StartCoroutine(HandleVictory());
    }

    private IEnumerator HandleVictory()
    {
        victoryPanel.SetActive(true); // Activa el panel de victoria.
        yield return new WaitForSeconds(delay); // Espera el tiempo especificado.
        SceneManager.LoadScene(0); // Cambia a la siguiente escena.
    }
}
