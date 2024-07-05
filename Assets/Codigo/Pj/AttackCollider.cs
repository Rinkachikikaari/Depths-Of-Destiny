using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public CharacterStats playerStats;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(playerStats.attackPower);
            }
        }
    }
}
