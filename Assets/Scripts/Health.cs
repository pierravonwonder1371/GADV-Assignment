using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    [Header("UI Settings (Player Only)")]
    public Image healthImage;

    [Header("Invulnerability Settings")]
    [SerializeField] private float invulnerabilityDuration = 1f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isInvulnerable = false;

    [Header("Player Control (Player Only)")]
    [SerializeField] private MonoBehaviour entityControls;

    [Header("Enemy Settings")]
    [SerializeField] private bool isEnemy = false;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void AddHealth(int value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
        UpdateHealthBar();
    }

    public void Respawn()
    {
        currentHealth = maxHealth;

        if (animator != null)
            animator.Play("playerneutral");

        UpdateHealthBar();

        if (entityControls != null)
            entityControls.enabled = true;
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth > 0)
        {
            StartCoroutine(Invulnerability());
        }
        else
        {
            Die();
        }
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;

        if (spriteRenderer != null)
        {
            for (float i = 0; i < invulnerabilityDuration; i += 0.2f)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                yield return new WaitForSeconds(0.2f);
            }
            spriteRenderer.enabled = true;
        }
        else
        {
            yield return new WaitForSeconds(invulnerabilityDuration);
        }

        isInvulnerable = false;
    }

    private void UpdateHealthBar()
    {
        if (healthImage != null)
        {
            float fillAmount = (float)currentHealth / maxHealth;
            healthImage.fillAmount = fillAmount;
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + "died!");
        if (entityControls != null)
            entityControls.enabled = false;

        if (isEnemy)
        {
            Destroy(gameObject, 0.5f);
        }
        else
        {
            Respawn();
        }
    }
}
