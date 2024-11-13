using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala
    public float bulletSpeed = 10f; // Velocidad de las balas
    public int bulletCount = 10; // Número de balas en cada disparo
    public float fireRate = 1f; // Intervalo entre disparos en segundos

    private float fireTimer; // Temporizador para controlar los intervalos de disparo

    void Update()
    {
        // Actualiza el temporizador
        fireTimer += Time.deltaTime;

        // Si el temporizador alcanza el tiempo de disparo, dispara
        if (fireTimer >= fireRate)
        {
            ShootBullets();
            fireTimer = 0f;
        }
    }

    void ShootBullets()
    {
        // Divide el ángulo completo en partes iguales para cada bala
        float angleStep = 360f / bulletCount;
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            // Calcula la dirección de cada bala en un patrón circular
            float bulletDirX = transform.position.x + Mathf.Sin(angle * Mathf.Deg2Rad);
            float bulletDirZ = transform.position.z + Mathf.Cos(angle * Mathf.Deg2Rad);

            Vector3 bulletMoveDirection = new Vector3(bulletDirX, 0, bulletDirZ).normalized;

            // Instancia la bala
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = bulletMoveDirection * bulletSpeed; // Asigna la velocidad en la dirección calculada
            }

            angle += angleStep;
        }
    }
}
