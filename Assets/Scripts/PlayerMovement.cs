using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 5f;
    [SerializeField] private int maxjump = 2;

    private Rigidbody2D body;
    private int jumpcount;
    private bool isGrounded = false;
    private bool onGroundObject = false;
    private bool isAttacking = false;

    private SpriteRenderer spriteRenderer;
    private Animator animator;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        float move = Input.GetAxis("Horizontal");

        if (!isAttacking)
        {
            body.linearVelocity = new Vector2(move * speed, body.linearVelocity.y);

            if (move > 0)
                spriteRenderer.flipX = false;
            else if (move < 0)
                spriteRenderer.flipX = true;
        }
        else
        {
            body.linearVelocity = new Vector2(0, body.linearVelocity.y);
        }


        if (Input.GetKeyDown(KeyCode.Space) && jumpcount > 0 && !isAttacking)
        {
            Jump();
        }
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jump);
        jumpcount--; //decreases the jump count by one. you cannot jump anymore when it reaches zero.
    }

    //this entire section is to link the movement script to the attack script.
    public bool CanAttack()
    {
        return !isAttacking && Mathf.Abs(body.linearVelocity.x) < 0.01f && onGroundObject;
    }

    public void SetAttacking(bool value)
    {
        isAttacking = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //checks whether the player is on an object tagged "ground" so that it can reset the double-jump
        if (collision.gameObject.CompareTag("Ground") && collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            onGroundObject = true;
            jumpcount = maxjump;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            onGroundObject = false;
        }
    }

}
//Made with assistance from ChatGPT. ChatGPT was asked to provide improvements to the code to make it more efficient.
