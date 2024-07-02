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
            Debug.Log("Player took damage!"); // Agrega esta l�nea para verificar si se llama a TakeDamage()
            // Aqu� puedes a�adir m�s l�gica de da�o si es necesario
        }
    }
}
