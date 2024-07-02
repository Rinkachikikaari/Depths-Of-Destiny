using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnBoss : MonoBehaviour
{
    bool bossvivo = true;
    [SerializeField]private Collider2D col;
    Animator animator;
    [SerializeField] GameObject Boss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && bossvivo)
        {
            Boss.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            bossvivo = false;
            animator.SetBool("Abierto", true);
            col.enabled = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Boss.SetActive(false);
        animator = GetComponent<Animator>();
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
