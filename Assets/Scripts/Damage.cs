using UnityEngine;
using UnityEngine.SceneManagement;

public class Damage : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float hurtAnimationDuration = 0.4f;

    private Animator animator;
    private CapsuleCollider2D capsuleCollider2D;
    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        animator.SetBool("Hurt", true);
        Debug.Log(health);
        Invoke(nameof(resetCharacter), hurtAnimationDuration);
        if (health <= 0)
        {
            animator.SetBool("Death", true);
            rigidbody2D.simulated = false;
            Invoke(nameof(killMethod), hurtAnimationDuration + 1f);
        }
    }

    private void killMethod()
    {
        Destroy(this.gameObject);

        if(this.gameObject.tag == "Player") SceneManager.LoadScene("Gameover");
    }

    private void resetCharacter()
    {
        animator.SetBool("Hurt", false);
    }
}
