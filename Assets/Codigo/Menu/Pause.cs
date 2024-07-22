using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public string overlaySceneName = "Pause"; // Nombre de la escena que quieres sobreponer
    private bool isOverlayActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOverlayActive)
            {
                UnloadOverlayScene();
            }
            else
            {
                LoadOverlayScene();
            }
        }
    }

    void LoadOverlayScene()
    {
        SceneManager.LoadScene(overlaySceneName, LoadSceneMode.Additive);
        isOverlayActive = true;
        Time.timeScale = 0f; // Pausa el tiempo.
    }

    void UnloadOverlayScene()
    {
        SceneManager.UnloadSceneAsync(overlaySceneName);
        isOverlayActive = false;
        Time.timeScale = 1f; // Reanuda el tiempo.
    }
}