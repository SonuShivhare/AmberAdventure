using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float hitImpectDuration = 0.5f;
    [SerializeField] private int damageValue = 10;

    private new Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider2D;
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
        boxCollider2D = GetComponent<BoxCollider2D>();

        rigidbody.velocity = transform.right * Speed;
    }

    private void OnEnable()
    {
        rigidbody.velocity = transform.right * Speed;
        boxCollider2D.isTrigger = true;
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
                    boxCollider2D.isTrigger = false;
                    Damage damage = collision.gameObject.GetComponent<Damage>();
                    damage.takeDamage(damageValue);
                }
                break;

            case Character.DevourerHopper:
                if (collision.gameObject.tag == "Player")
                {
                    rigidbody.velocity = Vector2.zero;
                    animator.SetBool("Impect", true);
                    boxCollider2D.isTrigger = false;
                    Damage damage = collision.gameObject.GetComponent<Damage>();
                    damage.takeDamage(damageValue);
                }
                break;

            case Character.LaserFlea:
                if (collision.gameObject.tag == "Player")
                {
                    rigidbody.velocity = Vector2.zero;
                    Damage damage = collision.gameObject.GetComponent<Damage>();
                    damage.takeDamage(damageValue);
                }
                break;
        }

        switch (collision.gameObject.tag)
        {
            case "Platform":
            case "Interactable":
            case "Projectile1":
            case "Projectile2":
            case "PoisonProjectile":
                if (collision.gameObject.tag != gameObject.tag)
                {
                    rigidbody.velocity = Vector2.zero;
                    animator.SetBool("Impect", true);
                    boxCollider2D.isTrigger = false;
                }
                break;
        }
    }

    private void Destroy()
    {
        switch (gameObject.tag)
        {
            case "Projectile1":
                Singleton.instance.projectile1.PlaceBackToList(gameObject);
                break;

            case "Projectile2":
                Singleton.instance.projectile2.PlaceBackToList(gameObject);
                break;

            case "PoisonProjectile":
                Singleton.instance.poisonProjectile.PlaceBackToList(gameObject);
                break;

            case "Laser":
                Singleton.instance.laser.PlaceBackToList(gameObject);
                break;
        }
    }
}
