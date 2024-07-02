using UnityEditor;
using UnityEngine;

public class FollowAI : MonoBehaviour
{
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    [SerializeField] private float Speed;
    //[SerializeField] private Transform player;

    private bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }

        bool isPlayerRight = transform.position.x < target.transform.position.x;
        Flip(isPlayerRight);

    }
    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * Speed;
        }
    }
    private void Flip(bool isPlayerRight)
    {
        if((isFacingRight && !isPlayerRight) || (!isFacingRight && isPlayerRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
