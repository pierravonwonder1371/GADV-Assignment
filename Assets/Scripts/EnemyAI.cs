using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float range = 2f;
    [SerializeField] private int damage = 1;
    [SerializeField] private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                DamagePlayer();
                cooldownTimer = 0;
            }
        }
    }

    private bool PlayerInSight()
    {
        Vector2 origin = transform.position;
        float radius = range;

        Collider2D hit = Physics2D.OverlapCircle(origin, radius, playerLayer);

        if (hit != null && hit.CompareTag("Player"))
        {
            return true;
        }
        return false;
    }

    private void DamagePlayer()
    {
        Vector2 origin = transform.position;
        Collider2D hit = Physics2D.OverlapCircle(origin, range, playerLayer);

        if (hit != null && hit.CompareTag("Player"))
        {
            Health playerHealth = hit.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
//Made with assistance from ChatGPT. ChatGPT was asked to provide improvements to the code to make it more efficient.