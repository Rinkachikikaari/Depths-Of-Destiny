using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private CharacterStats characterStats;
    private Vector2 mov;
    private PlayerAttack playerAttack;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
        characterStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        if (!playerAttack.isAttacking) // Permitir movimiento solo si no está atacando
        {
            MovePj();
        }

        if (Input.GetMouseButtonDown(0) && playerAttack.canAttack)
        {
            playerAttack.Attack();
            mov = Vector2.zero; // Detener el movimiento durante el ataque
            animator.SetFloat("Speed", 0); // Actualizar la animación a parado
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + mov * characterStats.moveSpeed * Time.fixedDeltaTime);
    }

    void MovePj()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        mov = new Vector2(hor, ver).normalized;

        if (mov != Vector2.zero)
        {
            animator.SetFloat("Horizontal", hor);
            animator.SetFloat("Vertical", ver);
            animator.SetFloat("Speed", mov.sqrMagnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }
}
