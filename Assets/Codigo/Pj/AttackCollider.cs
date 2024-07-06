using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                CharacterStats playerStats = GetComponentInParent<CharacterStats>();
                enemy.TakeDamage(playerStats.attackPower);

            }
        }
    }
    }

