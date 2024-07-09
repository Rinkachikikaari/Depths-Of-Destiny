using UnityEngine;

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
    }

    public void TakeDamage(int damage)
    {
        currentStats.currentHealth -= damage;
        currentStats.currentHealth = Mathf.Clamp(currentStats.currentHealth, 0, currentStats.maxHealth);

        if (currentStats.currentHealth <= 0)
        {
            Die();
        }

        if (hud != null)
        {
            hud.UpdateHearts();
        }
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
        // Implementar lógica de muerte del personaje
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
        currentStats.attackSpeed += addStats.attackSpeed;

        if (hud != null)
        {
            hud.UpdateHearts();
        }
    }
}
