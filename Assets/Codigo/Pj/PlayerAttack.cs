using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator; // Referencia al Animator del personaje
    public GameObject attackCollider; // Referencia al GameObject del collider de ataque

    private bool canAttack = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack) // Si se presiona el bot�n de ataque y se puede atacar
        {
            Attack();
        }
    }

    void Attack()
    {
        // Reproducir la animaci�n de ataque
        animator.SetTrigger("Attack");
        canAttack = false; // No se puede atacar hasta que la animaci�n termine
        new WaitForSeconds(1.5f);
        OnAttackEnd();
    }

    // Este m�todo ser� llamado desde la animaci�n de ataque cuando termine (a trav�s de un evento de animaci�n)
    public void OnAttackEnd()
    {
        canAttack = true; // Permitir atacar nuevamente
        animator.SetTrigger("NotAttack");
    }
}
