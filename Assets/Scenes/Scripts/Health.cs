using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    [Header("UI Settings")]
    public Image healthImage;

    private void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            
        }
    }

    private void UpdateHealthBar()
    {
        float fillAmount = (float)currentHealth / maxHealth;
        healthImage.fillAmount = fillAmount;
    }
}
