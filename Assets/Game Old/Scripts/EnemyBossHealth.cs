using System.Security.Cryptography;
using UnityEngine;

public class EnemyBossHealth : MonoBehaviour
{
    Rigidbody2D rb;
    public int maxHealth = 20;
    private int currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Aquí puedes agregar cualquier lógica que desees cuando el enemigo muere
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Movimiento>().CausarHeridaBoss();
        }
    }
}

