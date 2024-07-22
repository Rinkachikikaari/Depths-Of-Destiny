using System.Collections;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    public int normalDamage = 20;
    public int chargedDamage = 50;
    public float attackRange = 2f;
    public float chargeTime = 4f; // Tiempo de carga del ataque
    public float moveSpeed = 2f;
    public float chargeSpeed = 5f; // Velocidad del dash
    public float cooldownTime = 2f; // Tiempo de recuperación después del ataque

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isCharging = false;
    private bool isChargeAttack = false;
    private Vector2 movementDirection;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isCharging || isChargeAttack) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && !isCharging)
        {
            StartCoroutine(ChargeAttack());
        }
        else
        {
            movementDirection = (player.position - transform.position).normalized;
            UpdateAnimation(movementDirection);
        }
    }

    private void FixedUpdate()
    {
        if (!isCharging && !isChargeAttack)
        {
            rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isChargeAttack)
            {
                collision.GetComponent<CharacterStats>().TakeDamage(chargedDamage);
            }
            else
            {
                collision.GetComponent<CharacterStats>().TakeDamage(normalDamage);
            }
        }
    }

    private IEnumerator ChargeAttack()
    {
        isCharging = true;
        animator.SetTrigger("Charge"); // Inicia la animación de carga
        yield return new WaitForSeconds(chargeTime);

        isChargeAttack = true;
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * chargeSpeed;
        animator.SetTrigger("Dash"); // Inicia la animación de dash

        yield return new WaitForSeconds(0.5f); // Tiempo de duración del ataque cargado
        rb.velocity = Vector2.zero;

        animator.SetTrigger("Recover"); // Inicia la animación de recuperación
        yield return new WaitForSeconds(cooldownTime); // Tiempo de recarga antes de poder cargar de nuevo

        isCharging = false;
        isChargeAttack = false;

        ResetTriggers(); // Desactivar todos los triggers para volver al estado de idle
    }

    private void UpdateAnimation(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", direction.sqrMagnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
            ResetTriggers(); // Desactivar todos los triggers para volver al estado de idle
        }
    }

    private void ResetTriggers()
    {
        animator.ResetTrigger("Charge");
        animator.ResetTrigger("Dash");
        animator.ResetTrigger("Recover");
    }
}