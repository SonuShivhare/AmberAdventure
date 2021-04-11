using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    public ObjectPooling projectile1;
    public ObjectPooling projectile2;
    public ObjectPooling poisonProjectile;
    public ObjectPooling laser;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
}
