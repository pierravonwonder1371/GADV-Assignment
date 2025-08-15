using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float range = 3f;
    [SerializeField] private int damage = 1;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstacleLayer;

    private float cooldownTimer = Mathf.Infinity;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight() && cooldownTimer >= attackCooldown)
        {
            DamagePlayer();
            cooldownTimer = 0;
        }
    }

    private bool PlayerInSight()
    {
        // Check if player is within range
        Collider2D hit = Physics2D.OverlapCircle(transform.position, range, playerLayer);

        if (hit != null && hit.CompareTag("Player"))
        {
            // Calculate direction to player
            Vector2 direction = (hit.transform.position - transform.position).normalized;

            // Cast a ray to check for obstacles
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, direction, range,
                                                    playerLayer | obstacleLayer);

            // If ray hits something
            if (rayHit.collider != null)
            {
                // Check if the thing hit is actually the player
                if (rayHit.collider.CompareTag("Player"))
                {
                    return true; // Player is in sight
                }
            }
        }

        return false;
    }

    private void DamagePlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, range, playerLayer);

        if (hit != null && hit.CompareTag("Player"))
        {
            Health playerHealth = hit.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Damaged player for " + damage);
            }
        }
    }
}