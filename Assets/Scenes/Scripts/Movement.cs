using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 5f;
    [SerializeField] private int maxjump = 2;
    private Rigidbody2D body;
    private int jumpcount;
    private bool isGrounded = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float move = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(move * speed, body.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && jumpcount > 0)
        {
            Jump();
        }
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jump);
        jumpcount--;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            jumpcount = maxjump;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
//Made with assistance from ChatGPT. ChatGPT was asked to provide improvements to the code to make it more efficient.
