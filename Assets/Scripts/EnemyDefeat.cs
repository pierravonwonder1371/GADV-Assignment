using UnityEngine;

public class EnemyDefeat : MonoBehaviour
{
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private float deathEffectDuration = 1f;

    public void Die()
    {
        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDuration);
        }

        Destroy(gameObject);
    }
}
//Made with assistance from ChatGPT. ChatGPT was asked to provide improvements to the code to make it more efficient.
