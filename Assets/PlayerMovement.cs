using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float speed = 5f;
    private Vector2 mov;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MovePj();
    }

    void FixedUpdate()
    {
        // Mover el personaje
        rb.MovePosition(rb.position + mov * speed * Time.fixedDeltaTime);
    }

    void MovePj()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        mov = new Vector2(hor, ver).normalized;

        if (mov != Vector2.zero)
        {
            // Configurar animaciones
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
