using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        if (currentHealth > 0)
        {

        }
        else
        {

        }
    }
}
