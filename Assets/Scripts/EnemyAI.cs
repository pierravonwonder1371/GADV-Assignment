using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float range = 2f;
    [SerializeField] private int damage = 1;
    [SerializeField] private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;

    /*As seen here, the enemy can only attack if the player is in sight (ie. PlayerInSight returns true).
     There is also a cooldown timer to prevent enemies from constantly damaging the player. Every update,
     enemies will check whether or not the player is in their sight.*/
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

    /*PlayerInSight is determined by the range of the enemy. The range uses CircleCast, since the game is 2D.
    Should this circle collide with an object tagged player, True is returned. The enemy now has the player
    in its sight.*/
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

    /*Similar conditions are used to determine whether or not the enemy should attack, which of course
     updates the player's health value and the health bar visual. However, DamagePlayer is only triggered
     when PlayerInSight returns True. So despite their similarities, one cannot function without the other.*/
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
//Based on this tutorial by Pandemonium: https://www.youtube.com/playlist?list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV