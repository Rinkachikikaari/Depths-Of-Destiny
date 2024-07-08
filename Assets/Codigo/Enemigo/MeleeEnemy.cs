using System.Collections;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 2f;
    public float detectionRange = 10f;
    public float moveSpeed = 2f;
    public float attackCooldown = 1f;

    private Transform player;
    private Rigidbody2D rb;
    private bool isAttacking = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        player.GetComponent<CharacterStats>().TakeDamage(damage);
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}
