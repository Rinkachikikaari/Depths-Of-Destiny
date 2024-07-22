using UnityEngine;

public class Projectile: MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;

    private Vector2 targetPosition;

    public void SetTarget(Vector2 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    private void Update()
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if ((Vector2)transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CharacterStats>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
