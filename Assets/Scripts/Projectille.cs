using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectille : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform player;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        LaunchProjectille();
    }
    private void LaunchProjectille()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        rb.velocity = directionToPlayer * speed;
        StartCoroutine(DestroyProjectile());
    }
    IEnumerator DestroyProjectile()
    {
        float destroytime = 5f;
        yield return new WaitForSeconds(destroytime);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Movimiento>().CausarHerida();
        }
    }
}
