using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxLives = 3;
    public int currentLives;

    public Image[] heartImages;
    public Sprite fullHeartSprite;
    public Sprite halfHeartSprite;
    public Sprite emptyHeartSprite;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentLives = maxLives;
        UpdateHeartUI();
    }

    public void TakeDamage()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            // Aquí puedes manejar la lógica de fin de juego o reiniciar el nivel, etc.
            Debug.Log("Game Over");
        }

        UpdateHeartUI();
    }

    void UpdateHeartUI()
    {
        for (int i = 0; i < maxLives; i++)
        {
            if (i < currentLives)
            {
                heartImages[i].sprite = fullHeartSprite;
            }
            else if (i == currentLives && currentLives % 2 != 0)
            {
                heartImages[i].sprite = halfHeartSprite;
            }
            else
            {
                heartImages[i].sprite = emptyHeartSprite;
            }
        }
    }
}
