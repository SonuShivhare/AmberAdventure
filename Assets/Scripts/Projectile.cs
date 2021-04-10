﻿using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float hitImpectDuration = 0.5f;
    [SerializeField] private int damageValue = 10;

    private new Rigidbody2D rigidbody;
    private Animator animator;

    public enum Character
    {
        Amber,
        DevourerHopper,
        LaserFlea
    }

    public Character characterType;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rigidbody.velocity = transform.right * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (characterType)
        {
            case Character.Amber:
                if (collision.gameObject.tag == "Enemy")
                {
                    transform.position = collision.gameObject.transform.position;
                    rigidbody.velocity = Vector2.zero;
                    animator.SetBool("Impect", true);
                    Damage damage = collision.gameObject.GetComponent<Damage>();
                    damage.takeDamage(damageValue);
                    Invoke(nameof(Destroy), hitImpectDuration);
                }
                break;

            case Character.DevourerHopper:
                if (collision.gameObject.tag == "Player")
                {
                    transform.position = collision.gameObject.transform.position;
                    rigidbody.velocity = Vector2.zero;
                    animator.SetBool("Impect", true);
                    Damage damage = collision.gameObject.GetComponent<Damage>();
                    damage.takeDamage(damageValue);
                    Invoke(nameof(Destroy), hitImpectDuration);
                }
                break;

            case Character.LaserFlea:
                if (collision.gameObject.tag == "Player")
                {
                    rigidbody.velocity = Vector2.zero;
                    Damage damage = collision.gameObject.GetComponent<Damage>();
                    damage.takeDamage(damageValue);
                    Invoke(nameof(Destroy), hitImpectDuration);
                }
                break;
        }


    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }


}
