using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject projectile1;
    [SerializeField] private Transform firePoint1;
    [SerializeField] private GameObject projectile2;
    [SerializeField] private Transform firePoint2;



    [SerializeField] private AudioSource audio;

    [SerializeField] private AudioClip Shoot2;
    [SerializeField] private AudioClip Shoot3;

    [SerializeField] private float fireRate = 0.2f;
    private float nextFire;

    private Animator animator;
    private CharacterController2D controller;

    private float moveHorizontal;


    private bool isAttacking = false;
    private bool isWalkAttacking = false;
    private bool isPowerAttacking = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");

        animator.SetFloat("Player Speed", Mathf.Abs(moveHorizontal));

        if (Input.GetButtonDown("Fire1") && !isAttacking && moveHorizontal == 0)
        {
            animator.SetBool("Attack", true);
            audio.PlayOneShot(Shoot2);
            isAttacking = true;
            nextFire = Time.time + fireRate;
        }

        if (isAttacking && Time.time > nextFire)
        {
            Instantiate(projectile1, firePoint1.position, firePoint1.rotation);
            isAttacking = false;
            animator.SetBool("Attack", false);
        }

        if (Input.GetButtonDown("Fire1") && !isWalkAttacking && Mathf.Abs(moveHorizontal) > 0)
        {
            animator.SetBool("Walk Attack", true);
            audio.PlayOneShot(Shoot2);
            isWalkAttacking = true;
            nextFire = Time.time + fireRate;
        }

        if (isWalkAttacking && Time.time > nextFire)
        {
            Instantiate(projectile1, firePoint1.position, firePoint1.rotation);
            isWalkAttacking = false;
            animator.SetBool("Walk Attack", false);
        }

        if (Input.GetButtonDown("Fire2") && !isPowerAttacking)
        {
            animator.SetBool("Power Attack", true);
            audio.PlayOneShot(Shoot3);
            isPowerAttacking = true;
            nextFire = Time.time + fireRate;
        }

        if (isPowerAttacking && Time.time > nextFire)
        {
            Instantiate(projectile2, firePoint2.position, firePoint2.rotation);
            isPowerAttacking = false;
            animator.SetBool("Power Attack", false);
        }

        if (controller.isGrounded) animator.SetBool("Jump", false);
        if (Input.GetButtonDown("Jump") && controller.isGrounded) controller.isJumping = true;


        controller.move(moveHorizontal);
        if (controller.isJumping) animator.SetBool("Jump", true);
    }
}
