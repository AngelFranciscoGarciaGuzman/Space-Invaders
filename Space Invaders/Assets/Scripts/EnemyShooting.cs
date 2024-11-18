using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;      // Prefab de la bala
    public float bulletSpeed = 6f;      // Velocidad de las balas
    public int bulletsPerWave = 10;      // Número de balas en cada onda
    public float fireRate = 0.9f;        // Intervalo entre ondas de disparo en segundos
    public float snakeFrequency = 10f;    // Frecuencia del patrón serpenteante
    public float snakeAmplitude = 0f;    // Amplitud del patrón serpenteante
    public float bulletDelay = 0.3f;     // Aumenta el retardo entre balas para dispararlas más lentamente
    public float changePatternTime = 10f;  // Tiempo en segundos para cambiar el patrón (debe cambiarse después de 10 segundos)

    private float fireTimer;             // Temporizador para controlar los intervalos de disparo
    private List<GameObject> activeBullets = new List<GameObject>(); // Lista de balas activas
    public TextMeshProUGUI bulletCountText;  // Referencia al texto de cuenta de balas en pantalla

    private float gameStartTime;   // Guardar el tiempo de inicio del juego
    private int patternStage = 0;  // Determina en qué patrón estamos: 0 = serpiente, 1 = círculo, 2 = espiral
    private bool patternChanged = false;  // Bandera para saber si el patrón ha cambiado

    void Start()
    {
        // Guardar el tiempo de inicio del juego
        gameStartTime = Time.time;
    }

    void Update()
    {
        // Actualiza el temporizador de disparo
        fireTimer += Time.deltaTime;

        // Cambiar patrón dependiendo del tiempo transcurrido
        if (Time.time - gameStartTime >= 10f && Time.time - gameStartTime < 20f && patternStage == 0)
        {
            patternStage = 1; // Cambiar al patrón circular
            patternChanged = true;
            Debug.Log("¡El patrón ha cambiado a Circular!");
        }
        else if (Time.time - gameStartTime >= 20f && patternStage == 1)
        {
            patternStage = 2; // Cambiar al patrón espiral
            patternChanged = true;
            Debug.Log("¡El patrón ha cambiado a Espiral!");
        }

        // Si el temporizador alcanza el tiempo de disparo, dispara una nueva ola
        if (fireTimer >= fireRate)
        {
            StartCoroutine(ShootPatternWave());
            fireTimer = 0f;
        }

        // Actualizar el contador de balas activas en pantalla
        bulletCountText.text = "Active Bullets: " + activeBullets.Count;
    }

    System.Collections.IEnumerator ShootPatternWave()
    {
        for (int i = 0; i < bulletsPerWave; i++)
        {
            // Posición inicial de la bala en y = 0
            Vector3 bulletStartPosition = new Vector3(transform.position.x, 0, transform.position.z);

            // Instancia la bala en la posición del enemigo
            GameObject bullet = Instantiate(bulletPrefab, bulletStartPosition, Quaternion.identity);

            // Agregar la bala a la lista de balas activas
            activeBullets.Add(bullet);

            // Configura el movimiento de la bala, pasando el patrón y la bandera de cambio
            CoordinatedBulletPattern bulletPattern = bullet.AddComponent<CoordinatedBulletPattern>();
            bulletPattern.Initialize(-Vector3.forward, bulletSpeed, snakeFrequency, snakeAmplitude, bulletDelay * i, patternStage);

            // Destruir la bala después de un tiempo y eliminarla de la lista
            Destroy(bullet, 5f); // El valor 5f es el tiempo en segundos que la bala existirá antes de ser destruida
            yield return new WaitForSeconds(bulletDelay);
        }
    }

    // Método que se llama cuando una bala se destruye, para eliminarla de la lista
    public void RemoveBulletFromList(GameObject bullet)
    {
        activeBullets.Remove(bullet);
    }
}
