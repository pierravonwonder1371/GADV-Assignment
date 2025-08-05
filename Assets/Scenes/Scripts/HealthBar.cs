using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthImage; // Reference to the UI Image
    private int maxHealth = 10;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Call this method to reduce health by one step
    public void LoseHealth()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateHealthBar();
        }
    }

    // Updates the fill amount of the image
    private void UpdateHealthBar()
    {
        float fillAmount = (float)currentHealth / maxHealth;
        healthImage.fillAmount = fillAmount;
    }
}

