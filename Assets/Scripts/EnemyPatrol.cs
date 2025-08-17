using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Vector3 initScale;
    private bool movingLeft;
    private float moveOffset;

    private void Awake()
    {
        initScale = transform.localScale;
        movingLeft = Random.value > 0.5f;
        moveOffset = Random.Range(0f, 1f);
    }

    private void Update()
    {
        int direction = movingLeft ? -1 : 1;

        // Flip sprite based on direction
        transform.localScale = new Vector3(
            Mathf.Abs(initScale.x) * direction,
            initScale.y,
            initScale.z
        );

        // Move
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Reverse direction if the collided object is NOT the player
        if (!collision.CompareTag("Player"))
        {
            movingLeft = !movingLeft;
        }
    }
}
//Made with assistance from ChatGPT. ChatGPT was asked to provide improvements to the code to make it more efficient.
