using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private float meleeSpeed;

    [SerializeField] private float damage;

    float timeUnTilMelee;

    private void Update()
    {
        if (timeUnTilMelee <= 0f)
        {
            if(Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Attack");
                timeUnTilMelee = meleeSpeed;
            }
            else
            {
                timeUnTilMelee -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemigo")
        {
            // other.GetComponent<Enemy>().TakeDmage(damage);
            Debug.Log("Enemy hit");
        }
    }
}
