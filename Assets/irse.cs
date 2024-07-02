using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class irse : MonoBehaviour
{

    [SerializeField] private Collider2D col;
    bool bossvivo = true;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !bossvivo)
        {
            int indiceEscenaActual = SceneManager.GetActiveScene().buildIndex;
            int indiceSiguienteEscena = (indiceEscenaActual + 1) % SceneManager.sceneCountInBuildSettings;
            Debug.Log(indiceSiguienteEscena);
            Debug.Log(indiceEscenaActual);
            SceneManager.LoadScene(indiceSiguienteEscena);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            bossvivo = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
