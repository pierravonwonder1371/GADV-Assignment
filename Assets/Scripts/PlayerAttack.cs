using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.CanAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        playerMovement.SetAttacking(true);

        Vector2 attackPos = (Vector2)transform.position + (Vector2.right * transform.localScale.x * 1f);
        float radius = 0.5f;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPos, radius, LayerMask.GetMask("Enemy"));
        foreach (Collider2D hit in hits)
        {
            Health enemyHealth = hit.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1);
                Debug.Log("Enemy hit!");
            }
        }

        StartCoroutine(ResetAttack());
    }

    private void OnDrawGizmosSelected()
    {
        // Draw attack circle in Scene view for debugging
        Vector2 attackPos = (Vector2)transform.position + (Vector2.right * transform.localScale.x * 1f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos, 0.5f);
    }

    private System.Collections.IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        playerMovement.SetAttacking(false);
    }
}

