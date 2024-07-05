using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private CharacterStats characterStats;
    private Vector2 mov;
    private PlayerAttack PlayerAttack;
    private bool CanAttack = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        PlayerAttack = GetComponent<PlayerAttack>();
        characterStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        MovePj();
        if (Input.GetMouseButtonDown(0) && CanAttack) // Si se presiona el botón de ataque y se puede atacar
        {
            PlayerAttack.Attack();
        }
    }

    void FixedUpdate()
    {
        // Mover el personaje usando la velocidad del characterStats
        rb.MovePosition(rb.position + mov * characterStats.moveSpeed * Time.fixedDeltaTime);

    }

    void MovePj()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        mov = new Vector2(hor, ver).normalized;

        if (mov != Vector2.zero)
        {
            CanAttack = false;
            // Configurar animaciones
            animator.SetFloat("Horizontal", hor);
            animator.SetFloat("Vertical", ver);
            animator.SetFloat("Speed", mov.sqrMagnitude);
        }
        else
        {
            CanAttack = true;
            animator.SetFloat("Speed", 0);
        }
    }
}
