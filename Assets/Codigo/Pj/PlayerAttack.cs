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
        // Reproducir la animaci�n de ataque
        animator.SetTrigger("Attack");
        StartCooldown();
        animator.SetTrigger("NotAttack");
    }

    // Este m�todo ser� llamado desde la animaci�n de ataque cuando termine (a trav�s de un evento de animaci�n)
    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(characterStats.AttackTime);
    }
}
