using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private CharacterStats characterStats;
    private Vector2 mov;
    private PlayerAttack playerAttack;
    public bool canTp = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
        characterStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        if (!playerAttack.isAttacking) // Permitir movimiento solo si no est� atacando
        {
            MovePj();
        }

        if (Input.GetMouseButtonDown(0) && playerAttack.canAttack)
        {
            playerAttack.Attack();
            mov = Vector2.zero; // Detener el movimiento durante el ataque
            animator.SetFloat("Speed", 0); // Actualizar la animaci�n a parado
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + mov * characterStats.currentStats.moveSpeed * Time.fixedDeltaTime);
    }

    void MovePj()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        mov = new Vector2(hor, ver).normalized;

        if (mov != Vector2.zero)
        {
            animator.SetFloat("Horizontal", hor == 0 ? 0 : hor < 0 ? -1 : 1 );
            animator.SetFloat("Vertical", ver == 0 ? 0 : ver < 0 ? -1 : 1);
            animator.SetFloat("Speed", mov.sqrMagnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
            mov = Vector2.zero;
        }
    }
}
