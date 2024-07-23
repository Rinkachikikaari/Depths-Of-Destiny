using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pause;
    public bool isOverlayActive = false;

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
        pause.SetActive(true);
        isOverlayActive = true;
        Time.timeScale = 0f; // Pausa el tiempo.
    }

    void UnloadOverlayScene()
    {
        pause.SetActive(false);
        isOverlayActive = false;
        Time.timeScale = 1f; // Reanuda el tiempo.
    }
}