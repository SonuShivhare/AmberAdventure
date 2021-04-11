using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadPoint : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                PlayerPrefs.SetString("Result", "Lose");
                SceneManager.LoadScene("Gameover");
                break;

            case "Projectile1":
                Singleton.instance.projectile1.PlaceBackToList(collision.gameObject);
                break;

            case "Projectile2":
                Singleton.instance.projectile2.PlaceBackToList(collision.gameObject);
                break;

            case "PoisonProjectile":
                Singleton.instance.poisonProjectile.PlaceBackToList(collision.gameObject);
                break;
        }
    }
}
