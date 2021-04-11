using UnityEngine;

public class LaserFlea : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float patrolingRange;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate;
    private float nextFire;

    private float playerDisapearTime;

    private new Rigidbody2D rigidbody;
    private Animator animator;
    private Transform player;

    private ContactFilter2D contactFilter;

    private Vector2 enemyIntialPosition;

    private bool isFacingRight = false;
    private bool isAlreadyAttacked = false;
    private bool isAttacking = false;

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

        enemyIntialPosition = transform.position;
    }

    private void Update()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround) && !isGrounded)
        {
            rigidbody.velocity = Vector2.zero;
            isGrounded = true;
        }
    }

    private void FixedUpdate()
    {
        if (isFollowingPlayer)
        {
            if (player.position.x > transform.position.x && !isFacingRight && !isAttacking) Flip();
            if (player.position.x < transform.position.x && isFacingRight && !isAttacking) Flip();
            attackPlayer();
        }
        else
        {
            patroling();
        }
        LookForPlayer();

    }

    private void Move()
    {
        if (isGrounded)
        {
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
            animator.SetBool("Walk", true);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        speed = -speed;

        transform.Rotate(0, 180, 0);
    }

    private void LookForPlayer()
    {
        RaycastHit2D[] hitBuffer = new RaycastHit2D[20];

        int count = Physics2D.Raycast(transform.position, -transform.right, contactFilter, hitBuffer, 15f);

        for (int i = 0; i < count; i++)
        {
            if (hitBuffer[i].collider.gameObject.tag == "Player")
            {
                isFollowingPlayer = true;
                playerDisapearTime = Time.time + 5f;
            }
            else
            {
                if (Time.time > playerDisapearTime) isFollowingPlayer = false;
            }
        }

        count = Physics2D.Raycast(transform.position, transform.right, contactFilter, hitBuffer, 15f);

        for (int i = 0; i < count; i++)
        {
            if (hitBuffer[i].collider.gameObject.tag == "Player")
            {
                isFollowingPlayer = true;
                playerDisapearTime = Time.time + 5f;
            }
            else
            {
                if (Time.time > playerDisapearTime) isFollowingPlayer = false;
            }
        }
    }

    private void attackPlayer()
    {
        if (!isAlreadyAttacked && Time.time > nextFire)
        {
            isAlreadyAttacked = true;
            animator.SetBool("Attack", true);
            rigidbody.mass = 50;
            isAttacking = true;
            nextFire = Time.time + fireRate;
        }
    }

    private void LaunchAttack()
    {
        Singleton.instance.laser.PlaceIntoScene(firePoint);
    }

    private void ResetAttack()
    {
        animator.SetBool("Attack", false);
        isAttacking = false;
        rigidbody.mass = 1;
        isAlreadyAttacked = false;
    }

    private void patroling()
    {
        if (isGrounded)
        {
            if (transform.position.x > (enemyIntialPosition.x + patrolingRange) && isFacingRight) Flip();
            if (transform.position.x < (enemyIntialPosition.x - patrolingRange) && !isFacingRight) Flip();
        }
        Move();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, patrolingRange);
    }
}
