using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator; // Referencia al Animator del personaje
    public GameObject attackCollider; // Referencia al GameObject del collider de ataque

    private bool canAttack = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack) // Si se presiona el botón de ataque y se puede atacar
        {
            Attack();
        }
    }

    void Attack()
    {
        // Reproducir la animación de ataque
        animator.SetTrigger("Attack");
        canAttack = false; // No se puede atacar hasta que la animación termine
        new WaitForSeconds(1.5f);
        OnAttackEnd();
    }

    // Este método será llamado desde la animación de ataque cuando termine (a través de un evento de animación)
    public void OnAttackEnd()
    {
        canAttack = true; // Permitir atacar nuevamente
        animator.SetTrigger("NotAttack");
    }
}
