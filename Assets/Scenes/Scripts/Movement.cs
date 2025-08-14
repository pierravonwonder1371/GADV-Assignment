using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 60f;
    private Rigidbody2D body;
    private bool isGrounded = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float move = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(move * speed, body.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, (speed / 2));
        }
    }
    //private void Update()
    //{
        //body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

        //if (Input.GetKey(KeyCode.Space))
            //body.linearVelocity = new Vector2(body.linearVelocity.x, (speed / 2));
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
