using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject projectile1;
    [SerializeField] private Transform firePoint1;
    [SerializeField] private GameObject projectile2;
    [SerializeField] private Transform firePoint2;
    [SerializeField] private CinemachineVirtualCamera CMMoveFront;
    [SerializeField] private CinemachineVirtualCamera CMMoveBack;


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
        if(moveHorizontal > 0.1f)
        {
            CMMoveFront.Priority = 10;
            CMMoveBack.Priority = 5;
        }
        else if(moveHorizontal < -0.1f)
        {
            CMMoveFront.Priority = 5;
            CMMoveBack.Priority = 10;
        }

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
            Singleton.instance.projectile1.PlaceIntoScene(firePoint1);
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
            Singleton.instance.projectile1.PlaceIntoScene(firePoint1);
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
            Singleton.instance.projectile2.PlaceIntoScene(firePoint2);
            isPowerAttacking = false;
            animator.SetBool("Power Attack", false);
        }

        if (controller.isGrounded) animator.SetBool("Jump", false);
        if (Input.GetButtonDown("Jump") && controller.isGrounded) controller.isJumping = true;


        controller.move(moveHorizontal);
        if (controller.isJumping) animator.SetBool("Jump", true);
    }
}
