using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject[] enemigos; // Lista de enemigos a activar

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivarEnemigos();
        }
    }

    void ActivarEnemigos()
    {
        foreach (GameObject enemigo in enemigos)
        {
            enemigo.SetActive(true); // Activa cada enemigo
        }
        // Puedes agregar más lógica aquí, como reproducir sonidos o mostrar efectos visuales
    }
}

