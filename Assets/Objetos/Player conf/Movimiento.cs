using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movimiento : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public int speed;
    public float rollSpeed;
    public float attackSpeedMultiplier = 0.5f;
    private bool canRoll = true;
    private bool isAttacking = false;
    private float cooldownTime = 1f;
    private float rollDuration = 0.5f;
    private Vector3 mov;
    private int vidaPersonaje = 6;
    private bool invulnerable = false;
    public float invulnerabilityDuration = 2f; // Duración de la invencibilidad en segundos
    private float invulnerabilityTimer = 0f;

    [SerializeField] HUD hud;
    [SerializeField] Collider2D attackCollider;
    [SerializeField] Transform attackPoint; // Punto de ataque

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackCollider.enabled = false;
    }

    void Update()
    {
        ProcesarMovimiento();

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Atacar();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            CausarHerida();
        }
        if (invulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;
            if (invulnerabilityTimer <= 0f)
            {
                invulnerable = false;
                // Reactivar la detección de colisiones con los enemigos
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (Vector2)mov * Time.fixedDeltaTime);
    }

    void ProcesarMovimiento()
    {
        mov = Vector3.zero;
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        if (!isAttacking && (hor != 0 || ver != 0))
        {
            animator.SetFloat("Horizontal", hor);
            animator.SetFloat("Vertical", ver);
            animator.SetFloat("Speed", 1);

            Vector3 direction = new Vector3(hor, ver, 0).normalized;

            if (Input.GetKeyDown(KeyCode.Space) && canRoll)
            {
                canRoll = false;
                StartCoroutine(StartCooldown());
                animator.SetFloat("Roll", 1);
                StartCoroutine(Roll(direction));
                // Activar la invencibilidad
                invulnerable = true;
                invulnerabilityTimer = invulnerabilityDuration;

                // Desactivar la detección de colisiones con los enemigos durante el período de invencibilidad
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
            }
            else if (!Input.GetMouseButton(0))
            {
                float modifiedSpeed = Input.GetMouseButton(0) ? speed * attackSpeedMultiplier : speed;
                mov = direction * modifiedSpeed;
            }
        }
        else
        {
            animator.SetFloat("Speed", 0);
            animator.SetFloat("Roll", 0);
        }
    }

    void Atacar()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            StartCoroutine(ActivateAttackCollider());
        }
    }

    IEnumerator Roll(Vector3 direction)
    {
        float elapsedTime = 0;
        while (elapsedTime < rollDuration)
        {
            mov = direction * rollSpeed;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        animator.SetFloat("Roll", 0);
    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        canRoll = true;
    }

    IEnumerator ActivateAttackCollider()
    {
        yield return new WaitForEndOfFrame();
        attackCollider.enabled = true;

        yield return new WaitForSeconds(0.1f); // Ajusta este tiempo según sea necesario
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        attackCollider.enabled = false;
        isAttacking = false;
    }



    public void CausarHerida()
    {
        if (!invulnerable && vidaPersonaje > 0)
        {
            vidaPersonaje--;
            hud.RestaCorazones(vidaPersonaje);

            if (vidaPersonaje == 0)
            {
                SceneManager.LoadScene(5);
            }

            // Activar la invencibilidad
            invulnerable = true;
            invulnerabilityTimer = invulnerabilityDuration;

            // Desactivar la detección de colisiones con los enemigos durante el período de invencibilidad
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        }
    }
    public void CausarHeridaBoss()
    {
        if (!invulnerable && vidaPersonaje > 0)
        {
            vidaPersonaje--;
            hud.RestaCorazones(vidaPersonaje);
            vidaPersonaje--;
            hud.RestaCorazones(vidaPersonaje);

            if (vidaPersonaje == 0)
            {
                SceneManager.LoadScene(5);
            }

            // Activar la invencibilidad
            invulnerable = true;
            invulnerabilityTimer = invulnerabilityDuration;

            // Desactivar la detección de colisiones con los enemigos durante el período de invencibilidad
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        }
    }
}