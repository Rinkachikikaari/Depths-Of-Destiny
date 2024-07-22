using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Stats
{
    public int maxHealth = 6;
    public int currentHealth;
    public int attackPower = 10;
    public int moveSpeed = 5;
    public float attackSpeed = 2;
    public float AttackTime = 1;
}

public class CharacterStats : MonoBehaviour
{
    public Stats currentStats = new Stats();
    public static Stats MainStats = new Stats();
    private HUD hud;

    private Renderer renderer;
    private Color originalColor;
    private bool isInvulnerable = false;

    private void Awake()
    {
        currentStats = MainStats;
        currentStats.currentHealth = currentStats.maxHealth;
        hud = FindObjectOfType<HUD>();
        if (hud == null)
        {
            Debug.LogError("HUD no encontrado en la escena.");
        }
        else
        {
            Debug.Log("HUD encontrado: " + hud.name);
        }
        renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            originalColor = renderer.material.color;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
        {
            return;
        }

        currentStats.currentHealth -= damage;
        currentStats.currentHealth = Mathf.Clamp(currentStats.currentHealth, 0, currentStats.maxHealth);

        if (currentStats.currentHealth <= 0)
        {
            Die();
            SceneManager.LoadScene(6);
        }

        if (hud != null)
        {
            hud.UpdateHearts();
        }

        StartCoroutine(InvulnerabilityRoutine());
    }

    public void Heal(int amount)
    {
        currentStats.currentHealth += amount;
        currentStats.currentHealth = Mathf.Clamp(currentStats.currentHealth, 0, currentStats.maxHealth);

        if (hud != null)
        {
            hud.UpdateHearts();
        }
    }

    void Die()
    {
        // Implementar l�gica de muerte del personaje
        Debug.Log("Character Died");
        MainStats = new Stats();
    }

    void OnDestroy()
    {
        MainStats = currentStats;
    }

    public void AddStats(Stats addStats)
    {
        currentStats.maxHealth += addStats.maxHealth * 2;
        currentStats.currentHealth += addStats.currentHealth;
        currentStats.attackPower += addStats.attackPower;
        currentStats.moveSpeed += addStats.moveSpeed;
        currentStats.AttackTime -= addStats.AttackTime;

        if (hud != null)
        {
            hud.UpdateHearts();
        }
    }
    private IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;
        if (renderer != null)
        {
            renderer.material.color = Color.red; // Cambia el color del personaje al recibir daño
        }
        yield return new WaitForSeconds(0.1f); // Espera 1 segundo
        if (renderer != null)
        {
            renderer.material.color = originalColor; // Restaura el color original del personaje
        }
        yield return new WaitForSeconds(0.9f); // Espera 1 segundo
        isInvulnerable = false;
    }
}
