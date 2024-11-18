using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 4f; // Tiempo de vida de la bala

    void Start()
    {
        // Destruye la bala después de `lifeTime` segundos
        Destroy(gameObject, lifeTime);
    }
}
