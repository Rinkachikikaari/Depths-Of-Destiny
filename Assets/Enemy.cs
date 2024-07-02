using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage();
            Debug.Log("Player took damage!"); // Agrega esta línea para verificar si se llama a TakeDamage()
            // Aquí puedes añadir más lógica de daño si es necesario
        }
    }
}
