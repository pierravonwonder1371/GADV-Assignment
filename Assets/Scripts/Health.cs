using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    [Header("UI Settings")]
    public Image healthImage;

    [Header("Invulnerability Settings")]
    [SerializeField] private float invulnerabilityDuration = 1f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isInvulnerable = false;

    [Header("Player Control")]
    [SerializeField] private MonoBehaviour playerControls;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("References")]
    [SerializeField] private UIManager uiManager;

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

        // Play respawn / neutral animation
        if (animator != null)
            animator.Play("playerneutral");

        UpdateHealthBar();

        if (playerControls != null)
            playerControls.enabled = true;
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth > 0 && healthImage.fillAmount != 0)
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
        Debug.Log("Player died!");
        if (playerControls != null)
            playerControls.enabled = false;

        if (uiManager != null)
            uiManager.TriggerGameOver();

    }
}
//Made with assistance from ChatGPT. ChatGPT was asked to provide improvements to the code to make it more efficient.
