using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Vector3 initScale;
    private bool movingLeft;
    private float moveOffset;

    /*To make the enemies feel more natural, a random value is set to
     movingLeft and moveOffset when the game starts. That way, the starting
     position, as well as the first direction they move in is completely randomised.*/
    private void Awake()
    {
        initScale = transform.localScale;
        movingLeft = Random.value > 0.5f;
        moveOffset = Random.Range(0f, 1f);
    }

    /*The direction of the enemy's movement is reversed when they hit the left and right colliders.
     These colliders are invisible and do not affect the player, they solely exist to tell the enemy
     when to turn around. They repeat this process unless they hit the player. There is also code to
     mirror the sprite whenever they hit the aforementioned collider.*/
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
//Based on this tutorial by Pandemonium: https://www.youtube.com/playlist?list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV