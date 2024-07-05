using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        CharacterStats stats = other.GetComponent<CharacterStats>();
        if (stats != null)
        {
            StatModifier modifier = GetComponent<StatModifier>();
            if (modifier != null)
            {
                modifier.Apply(stats);
                Destroy(gameObject); // Destruir el item después de recogerlo
            }
        }
    }
}
