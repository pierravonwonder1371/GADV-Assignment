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
        /*Every update, three conditions are checked: cooldownTimer, the current player state and the input button.
         If the cooldownTimer has run its course, the player is not moving and there is a left click input,
         Attack will be called.*/
        cooldownTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.CanAttack())
        {
            Attack();
        }
    }

    private void Attack()
    {
        /*The trigger used in the animator is "attack". The cooldownTimer is prepared for after the
         attack is complete and setAttacking is true. Everything is prepared for PerformAttack.*/
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        playerMovement.SetAttacking(true);
    }

    public void PerformAttack()
    {
        /*The attack only hits the layer that the enemies are on. The range of the attack
         is set in the inspector. It is a circle surrounding the player. If the enemy is
         within that circle, EnemyDefeat is called. attackDirection ensures that the player
         can attack regardless of the direction they are facing. Otherwise, they would only
         be able to attack forward.*/
        Vector2 attackDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Vector2 attackPos = (Vector2)transform.position + (attackDirection * attackOffset);

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPos, attackRange, LayerMask.GetMask("Enemy"));
        
        foreach (Collider2D hit in hits)
        {
            EnemyDefeat enemy = hit.GetComponent<EnemyDefeat>();
            if (enemy != null)
            {
                enemy.Die();
                //The console is updated to indicate that the attack has been done, as well as stating any enemies it successfully hit.
                Debug.Log("Enemy destroyed: " + hit.name);
            }
        }
        Debug.Log("Attack performed");
    }
    /*When the attack is done, SetAttacking is returned to a false state
     so that the player does not keep attacking endlessly.*/
    public void EndAttack()
    {
        playerMovement.SetAttacking(false);
    }
    /*PerformAttack and EndAttack are called by animation events triggered by the
     playerattack animation. That means the PerformAttack only applies after a specific
     frame, and EndAttack only applies after the animation ends.*/
}
//Made with assistance from ChatGPT. ChatGPT was asked to provide improvements to the code to make it more efficient.
//Additional assistance from Gemini. The prompt is the same.
//Based on this tutorial by Pandemonium: https://www.youtube.com/playlist?list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV

