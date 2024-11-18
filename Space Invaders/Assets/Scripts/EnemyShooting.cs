using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;      // Prefab de la bala
    public float bulletSpeed = 10f;      // Velocidad de las balas
    public int bulletsPerWave = 10;      // Número de balas en cada onda
    public float fireRate = 0.9f;        // Intervalo entre ondas de disparo en segundos
    public float snakeFrequency = 10f;    // Frecuencia del patrón serpenteante
    public float snakeAmplitude = 0f;    // Amplitud del patrón serpenteante
    public float bulletDelay = 0.3f;     // Aumenta el retardo entre balas para dispararlas más lentamente

    private float fireTimer; // Temporizador para controlar los intervalos de disparo
    private float patternChangeTimer = 0f; // Temporizador para el cambio de patrón
    private float patternChangeTime = 10f; // Tiempo después del cual cambia el patrón
    private bool isSnakePattern = true;    // Indica si estamos usando el patrón serpenteante o circular

    void Update()
    {
        // Actualiza el temporizador de disparo
        fireTimer += Time.deltaTime;
        patternChangeTimer += Time.deltaTime;

        // Si el temporizador alcanza el tiempo de disparo, dispara una nueva ola
        if (fireTimer >= fireRate)
        {
            StartCoroutine(ShootWave());
            fireTimer = 0f;
        }

        // Cambiar el patrón después de 10 segundos
        if (patternChangeTimer >= patternChangeTime)
        {
            isSnakePattern = !isSnakePattern; // Cambia entre el patrón serpenteante y circular
            patternChangeTimer = 0f; // Reinicia el temporizador para el próximo cambio
            Debug.Log("Pattern changed! Current pattern: " + (isSnakePattern ? "Snake" : "Circular"));
        }
    }

    System.Collections.IEnumerator ShootWave()
    {
        for (int i = 0; i < bulletsPerWave; i++)
        {
            // Posición inicial de la bala en y = 0
            Vector3 bulletStartPosition = new Vector3(transform.position.x, 0, transform.position.z);

            // Instancia la bala en la posición del enemigo
            GameObject bullet = Instantiate(bulletPrefab, bulletStartPosition, Quaternion.identity);

            // Configura el movimiento de la bala
            CoordinatedBulletPattern bulletPattern = bullet.AddComponent<CoordinatedBulletPattern>();
            bulletPattern.Initialize(-Vector3.forward, bulletSpeed, snakeFrequency, snakeAmplitude, bulletDelay * i);

            // Cambiar el patrón dependiendo del tiempo
            if (!isSnakePattern)
            {
                bulletPattern.SetCircularPattern(); // Cambia el patrón de la bala a circular si estamos en el patrón circular
            }

            // Retardo antes de crear la siguiente bala para mantener el efecto de serpiente
            yield return new WaitForSeconds(bulletDelay);
        }
    }
}
