using System.Collections;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    public int normalDamage = 20;
    public int chargedDamage = 50;
    public float attackRange = 2f;
    public float chargeTime = 3f;
    public float moveSpeed = 2f;
    public float chargeSpeed = 5f;
    public float cooldownTime = 2f;

    private Transform player;
    private Rigidbody2D rb;
    private bool isCharging = false;
    private bool isChargeAttack = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isCharging || isChargeAttack) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, player.position) <= attackRange && !isCharging)
        {
            StartCoroutine(ChargeAttack());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(isChargeAttack && isCharging)
            {
                player.GetComponent<CharacterStats>().TakeDamage(chargedDamage);
            }
            if(!isChargeAttack && !isCharging)
            {
                player.GetComponent<CharacterStats>().TakeDamage(normalDamage);
            
            }
        }
    }
    private IEnumerator ChargeAttack()
    {
        isCharging = true;
        // Añadir animación o efecto de carga aquí
        yield return new WaitForSeconds(chargeTime);

        isChargeAttack = true;
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * chargeSpeed;

        yield return new WaitForSeconds(0.5f); // Tiempo de duración del ataque cargado
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(cooldownTime); // Tiempo de recarga antes de poder cargar de nuevo

        isCharging = false;
        isChargeAttack = false;
    }
}
