using UnityEngine;
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float CharacterSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;

    private new Rigidbody2D rigidbody;
    private Animator animator;

    private float moveHorizontal;

    private bool isFacingRight = true;

    public bool isGrounded = false;
    public bool isJumping = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround) && !isGrounded) isGrounded = true;
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(moveHorizontal * CharacterSpeed, rigidbody.velocity.y);

        if (isJumping && isGrounded)
        {
            rigidbody.AddForce(Vector2.up * jumpForce);
            isJumping = false;
            isGrounded = false;
        }

        if (moveHorizontal > 0 && !isFacingRight) Flip();
        if (moveHorizontal < 0 && isFacingRight) Flip();
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.Rotate(0, 180, 0);
    }

    public void move(float moveHorizontal)
    {
        this.moveHorizontal = moveHorizontal;
    }
}
