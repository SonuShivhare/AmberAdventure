using UnityEngine;

public class DevourerHopper : MonoBehaviour
{
    [SerializeField] private Vector2 jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float patrolingRange;
    [SerializeField] private float enemyDetectionRange;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate;

    [SerializeField] private float jumpDelay = 2f;
    private float nextJump;

    private float playerDisapearTime;

    private new Rigidbody2D rigidbody;
    private Animator animator;
    private Transform player;

    private ContactFilter2D contactFilter;

    private Vector2 playerIntialPosition;

    private bool isFacingRight = false;
    private bool isAlreadyAttacked = false;
    private bool isGrounded = false;
    private bool isFollowingPlayer = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;

        playerIntialPosition = transform.position;
    }

    private void Update()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround) && !isGrounded)
        {
            rigidbody.velocity = Vector2.zero;
            isGrounded = true;
            nextJump = Time.time + jumpDelay;
        }

        if (isGrounded) animator.SetBool("Jump", false);
    }

    private void FixedUpdate()
    {
        if (isFollowingPlayer)
        {
            if (player.position.x > transform.position.x && !isFacingRight) Flip();
            if (player.position.x < transform.position.x && isFacingRight) Flip();
            attackPlayer();
        }
        else
        {
            patroling();
        }
        LookForPlayer();
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        jumpForce.x = -jumpForce.x;

        transform.Rotate(0, 180, 0);
    }

    private void Jump()
    {
        if (isGrounded && Time.time > nextJump)
        {
            rigidbody.velocity = jumpForce;
            animator.SetBool("Jump", true);
            isGrounded = false;
        }
    }

    private void LookForPlayer()
    {
        RaycastHit2D[] hitBuffer = new RaycastHit2D[20];

        int count = Physics2D.Raycast(transform.position, -transform.right, contactFilter, hitBuffer, enemyDetectionRange);

        for (int i = 0; i < count; i++)
        {
            if (hitBuffer[i].collider.gameObject.tag == "Player")
            {
                isFollowingPlayer = true;
                playerDisapearTime = Time.time + 2f;
            }
            else
            {
                if (Time.time > playerDisapearTime) isFollowingPlayer = false;
            }
        }

        count = Physics2D.Raycast(transform.position, transform.right, contactFilter, hitBuffer, enemyDetectionRange);

        for (int i = 0; i < count; i++)
        {
            if (hitBuffer[i].collider.gameObject.tag == "Player")
            {
                isFollowingPlayer = true;
                playerDisapearTime = Time.time + 2f;
            }
            else
            {
                if (Time.time > playerDisapearTime) isFollowingPlayer = false;
            }
        }
    }

    private void attackPlayer()
    {
        if (!isAlreadyAttacked)
        {
            isAlreadyAttacked = true;
            animator.SetBool("Attack", true);
            Invoke("ResetAttack", fireRate);
        }
    }

    private void LaunchAttack()
    {
        Singleton.instance.poisonProjectile.PlaceIntoScene(firePoint);
    }

    private void StopAttack()
    {
        animator.SetBool("Attack", false);
    }

    private void ResetAttack()
    {
        isAlreadyAttacked = false;
    }

    private void patroling()
    {
        if (isGrounded)
        {
            if (transform.position.x > (playerIntialPosition.x + patrolingRange) && isFacingRight) Flip();
            if (transform.position.x < (playerIntialPosition.x - patrolingRange) && !isFacingRight) Flip();
        }
        Jump();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, patrolingRange);
    }
}
