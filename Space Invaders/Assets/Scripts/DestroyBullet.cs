using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    public delegate void BulletDestroyed();  // Delegado para notificar cuando la bala es destruida
    public event BulletDestroyed OnBulletDestroyed;

    void OnCollisionEnter(Collision collision)
    {
        // Puedes añadir más condiciones si necesitas que se destruya por ciertas colisiones
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            DestroyBulletObject();
        }
    }

    // Método para destruir la bala
    void DestroyBulletObject()
    {
        OnBulletDestroyed?.Invoke(); // Llamar al evento para reducir el contador de balas activas
        Destroy(gameObject); // Destruir la bala
    }
}
