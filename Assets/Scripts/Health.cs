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

    //Whenever the game starts, the player's health is automatically set to the maxHealth value of 10.
    private void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    /*Code that ended up not being used. Touching healing items would have updated the health value
    (and the health bar visual) based on the value attached to the AddHealth function.*/
    public void AddHealth(int value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
        UpdateHealthBar();
    }

    /*More unused code for respawning. It would reset the currentHealth value to maxHealth,
     reset any animations back to the default playerneutral animation and update the health bar
     visual to have maximum fill. It also reenables the player's controls as dying would disable
     them. It would be used at checkpoints, but since this level was too short to have checkpoints,
     it was not used.*/
    public void Respawn()
    {
        currentHealth = maxHealth;

        if (animator != null)
            animator.Play("playerneutral");

        UpdateHealthBar();

        if (playerControls != null)
            playerControls.enabled = true;
    }

    /*The player (in their current state, at least) only has 10 health points. So they can only
     take damage 10 times before Die() is triggered. When touching the enemies, currentHealth is
     updated to subtract the amount of damage dealt from the current value of the variable and
     the player has a window of invulnerability.*/
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

    /*IsInvulnerable starts out false. When the player takes damage, the flag is set to true. The
     duration of invulnerability is set in the inspector. If the spriteRenderer is also assigned,
     the player's sprite will also flash. If you make contact with an enemy in this window, you
     will not take any damage.*/
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

    /*fillAmount is used for the health bar visual. Whenever the player takes damage, the visual
     is updated based on the current value of currentHealth. (e.g. The player takes 1 damage, reducing
     the the value of currentHealth from 10 to 9. This is passed into fillAmount, and changes the health
     bar's visual to only be filled to 9/10th of the whole image.*/
    private void UpdateHealthBar()
    {
        if (healthImage != null)
        {
            float fillAmount = (float)currentHealth / maxHealth;
            healthImage.fillAmount = fillAmount;
        }
    }

    /*When the player dies, the script containing movement inputs (PlayerMovement) is disabled so that
     the player cannot move after losing all their health. Additionally, the Game Over screen from
     UIManager is called.*/
    private void Die()
    {
        //The console is updated to indicate that the player has died.
        Debug.Log("Player died!");
        if (playerControls != null)
            playerControls.enabled = false;

        if (uiManager != null)
            uiManager.TriggerGameOver();

    }
}
//Made with assistance from ChatGPT. ChatGPT was asked to provide improvements to the code to make it more efficient.
//Based on this tutorial by Pandemonium: https://www.youtube.com/playlist?list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV