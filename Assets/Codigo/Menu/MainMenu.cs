using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject pause;
    public Pause pauseScript;
    private void Start()
    {
        pause = GameObject.Find("Pause");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
     public void UnloadOverlayScene()
    {
        pause.SetActive(false);
        pauseScript.isOverlayActive = false;
        Time.timeScale = 1f; // Reanuda el tiempo.
    }
}
