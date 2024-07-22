using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject pauseMenuUI; // Arrastra aquí tu Panel del menú de pausa desde el inspector.
    private bool isPaused = false;

    public Rigidbody2D rb;
    public Animator animator;
    private CharacterStats characterStats;
    private Vector2 mov;
    private PlayerAttack playerAttack;
    public bool canTp = true;

    public float dashSpeed = 20f; // Velocidad del dash
    public float dashTime = 0.2f; // Duración del dash
    public float dashCooldown = 1f; // Cooldown del dash
    private bool isDashing;
    private float dashTimeLeft;
    private float dashCooldownTimeLeft;

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
            if (isDashing)
            {
                dashTimeLeft -= Time.deltaTime;
                if (dashTimeLeft <= 0)
                {
                    isDashing = false;
                    dashCooldownTimeLeft = dashCooldown; // Inicia el cooldown después del dash
                }
            }
            else
            {
                dashCooldownTimeLeft -= Time.deltaTime; // Disminuye el tiempo de cooldown
                MovePj();
                if (Input.GetKeyDown(KeyCode.Space) && mov != Vector2.zero && dashCooldownTimeLeft <= 0) // Botón de dash (barra espaciadora)
                {
                    StartDash();
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && playerAttack.canAttack)
        {
            playerAttack.Attack();
            mov = Vector2.zero; // Detener el movimiento durante el ataque
            animator.SetFloat("Speed", 0); // Actualizar la animación a parado
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // Aquí puedes agregar la funcionalidad que desees para la tecla R
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.MovePosition(rb.position + mov * dashSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + mov * characterStats.currentStats.moveSpeed * Time.fixedDeltaTime);
        }
    }

    void MovePj()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        mov = new Vector2(hor, ver).normalized;

        if (mov != Vector2.zero)
        {
            animator.SetFloat("Horizontal", hor == 0 ? 0 : hor < 0 ? -1 : 1);
            animator.SetFloat("Vertical", ver == 0 ? 0 : ver < 0 ? -1 : 1);
            animator.SetFloat("Speed", mov.sqrMagnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
            mov = Vector2.zero;
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        animator.SetTrigger("Dash"); // Asumiendo que tienes una animación de dash
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo.
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pausa el tiempo.
        isPaused = true;
    }
}
