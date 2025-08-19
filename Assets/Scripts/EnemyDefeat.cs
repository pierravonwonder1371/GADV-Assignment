using UnityEngine;

public class EnemyDefeat : MonoBehaviour
{
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private float deathEffectDuration = 1f;

    public void Die()
    {
        /*Die() is called by PlayerAttack whenever an enemy with this attached script is hit
         while within the range of the player during PerformAttack. Upon death, the enemy
         object is destroyed. In its place, a particle effect plays for a few seconds before
         the particles are also destroyed. The duration of the effect is set in the inspector.*/
        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDuration);
        }

        Destroy(gameObject);
    }
}
//Made with assistance from ChatGPT. ChatGPT was asked to provide improvements to the code to make it more efficient.
