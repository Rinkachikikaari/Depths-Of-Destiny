using System.Collections;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public int health = 100;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isHit = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void TakeDamage(int damage)
    {
        if (!isHit)
        {
            health -= damage;
            StartCoroutine(FlashWhite());
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator FlashWhite()
    {
        isHit = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
        isHit = false;
    }

    private void Die()
    {
        // Implementar lógica de muerte del enemigo
        Destroy(gameObject);
    }
}
