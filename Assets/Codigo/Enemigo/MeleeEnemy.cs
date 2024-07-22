using System.Collections;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public int damage = 10;
    public float detectionRange = 10f;
    public float moveSpeed = 2f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movementDirection;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            movementDirection = (player.position - transform.position).normalized;
            UpdateAnimation(movementDirection);
        }
        else
        {
            movementDirection = Vector2.zero;
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Speed", 0);
        }
    }

    private void FixedUpdate()
    {
        if (movementDirection != Vector2.zero)
        {
            rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<CharacterStats>().TakeDamage(damage);
        }
    }

    private void UpdateAnimation(Vector2 direction)
    {
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }
}
