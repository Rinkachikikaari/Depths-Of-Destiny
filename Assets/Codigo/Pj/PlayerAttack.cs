using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator; // Referencia al Animator del personaje
    public GameObject attackCollider; // Referencia al GameObject del collider de ataque
    private CharacterStats characterStats;
    public bool canAttack = true;
    public bool isAttacking = false;

    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();
    }

    public void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            isAttacking = true;
            animator.SetTrigger("Attack");
        }
        animator.SetTrigger("NotAttack");

    }

    public void OnAttackEnd()
    {
        isAttacking = false;
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        // Esperar el tiempo de cooldown antes de permitir otro ataque
        yield return new WaitForSeconds(characterStats.currentStats.attackSpeed);
        canAttack = true;
    }
}
