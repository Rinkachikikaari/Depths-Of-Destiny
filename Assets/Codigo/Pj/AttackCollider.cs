using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyLife enemy = collision.gameObject.GetComponent<EnemyLife>();
            if (enemy != null)
            {
                CharacterStats playerStats = GetComponentInParent<CharacterStats>();
                enemy.TakeDamage(playerStats.currentStats.attackPower);

            }
        }
    }
    }

