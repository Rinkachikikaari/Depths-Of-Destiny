using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator; // Referencia al Animator del personaje
    public GameObject attackCollider; // Referencia al GameObject del collider de ataque
    private CharacterStats characterStats;


    private bool canAttack = true;

    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();

    }

    void Update()
    {

    }

    public void Attack()
    {
        // Reproducir la animación de ataque
        animator.SetTrigger("Attack");
        StartCooldown();
        animator.SetTrigger("NotAttack");
    }

    // Este método será llamado desde la animación de ataque cuando termine (a través de un evento de animación)
    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(characterStats.AttackTime);
    }
}
