using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float hurtAnimationDuration = 0.4f;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Slider slider;
    [SerializeField] private Vector3 healthBarDistance;

    private new Camera camera;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider2D;
    private new Rigidbody2D rigidbody2D;

    private float currentHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = camera;

        currentHealth = health;
        slider.value = currentHealth / health;
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        updateHealthBar();
        animator.SetBool("Hurt", true);
        Invoke(nameof(resetCharacter), hurtAnimationDuration);
        if (currentHealth <= 0)
        {
            animator.SetBool("Death", true);
            rigidbody2D.simulated = false;
            Invoke(nameof(killMethod), hurtAnimationDuration + 1f);
        }
    }

    private void killMethod()
    {
        Destroy(this.gameObject);

        if (this.gameObject.tag == "Player") SceneManager.LoadScene("Gameover");
    }

    private void resetCharacter()
    {
        animator.SetBool("Hurt", false);
    }

    private void FixedUpdate()
    {
        slider.transform.position = transform.position + healthBarDistance;
    }

    private void updateHealthBar()
    {
        slider.value = currentHealth / health;
    }
}
