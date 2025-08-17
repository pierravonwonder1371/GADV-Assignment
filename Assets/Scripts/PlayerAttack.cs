using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackOffset = 0f;
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
        cooldownTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.CanAttack())
        {
            Attack();
        }
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        playerMovement.SetAttacking(true);
    }

    public void PerformAttack()
    {
        Vector2 attackDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Vector2 attackPos = (Vector2)transform.position + (attackDirection * attackOffset);

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPos, attackRange, LayerMask.GetMask("Enemy"));
        
        foreach (Collider2D hit in hits)
        {
            EnemyDefeat enemy = hit.GetComponent<EnemyDefeat>();
            if (enemy != null)
            {
                enemy.Die();
                Debug.Log("Enemy destroyed: " + hit.name);
            }
        }

        Debug.Log("Attack performed");
    }
    public void EndAttack()
    {
        playerMovement.SetAttacking(false);
    }
}
//Made with assistance from ChatGPT. ChatGPT was asked to provide improvements to the code to make it more efficient.
//Additional assistance from Gemini. The prompt is the same.

