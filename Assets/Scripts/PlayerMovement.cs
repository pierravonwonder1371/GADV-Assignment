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
            /*When moving towards the right (positive X), spriteRenderer.flipX becomes false.
             There is no need to flip on start as my sprite is already facing right,
             and this is only used when flipping the sprite from left back to right.
             However, when moving left (negative X), spriteRenderer.flipX becomes true,
             flipping the sprite left.*/
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

    /*Whenever Jump() is triggered by the space bar, the jumpcount is reduced by 1.
     When it reaches 0, the player cannot jump anymore. They will have to touch the ground
    to reset it back to its maxValue (2).*/
    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jump);
        jumpcount--;
    }

    /*This entire section is to link the movement script to the attack script.
     SetAttacking has its flag set to true by PlayerAttack, and isAttacking is set to true.*/
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
        /*Checks whether the player is on an object tagged "ground" so that it can reset the double-jump
        counter. All the terrain that can be walked on (Roof, Bridge, etc.) use the "Ground" tag.
        If true, the jumpCount is reset back to maxJump (in this case its value is 2), allowing the player
        to double jump. Additionally, it is also treated as acceptable ground by CanAttack, allowing
        the player to attack wherever they want.*/
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
//Based on this tutorial by Pandemonium: https://www.youtube.com/playlist?list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV