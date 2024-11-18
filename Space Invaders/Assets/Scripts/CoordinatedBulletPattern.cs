using UnityEngine;

public class CoordinatedBulletPattern : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private float frequency;
    private float amplitude;
    private float spawnDelay;
    private float timeOffset;
    private int patternStage;  // 0 = serpiente, 1 = circular, 2 = espiral

    // Inicialización
    public void Initialize(Vector3 moveDirection, float moveSpeed, float waveFrequency, float waveAmplitude, float delay, int stage)
    {
        direction = moveDirection.normalized;
        speed = moveSpeed;
        frequency = waveFrequency;
        amplitude = waveAmplitude;
        spawnDelay = delay;
        timeOffset = Time.time;  // Guardamos el tiempo inicial para los cálculos
        patternStage = stage; // Guardamos el patrón actual
    }

    void Update()
    {
        // Movimiento de la bala: avanzar en la dirección y agregar movimiento en el patrón correspondiente
        Vector3 forwardMovement = direction * speed * Time.deltaTime;

        switch (patternStage)
        {
            case 0:
                // Patrón serpenteante (senoidal)
                amplitude = Mathf.Sin(Time.time * frequency);  // La amplitud varía con el tiempo
                float wave = Mathf.Sin((Time.time - timeOffset) * frequency) * amplitude;
                Vector3 snakeMovement = transform.right * wave;
                transform.position += forwardMovement + snakeMovement;
                break;

            case 1:
                // Patrón circular
                float radius = 1f;
                float angle = (Time.time - timeOffset) * frequency;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;
                Vector3 circularMovement = new Vector3(x, 0, z);
                transform.position += forwardMovement + circularMovement;
                break;

            case 2:
                // Patrón espiral
                float spiralRadius = 0.2f + Mathf.Sin(Time.time - timeOffset) * 0.5f; // Radio que crece con el tiempo
                float spiralAngle = (Time.time - timeOffset) * frequency; // Ángulo creciente

                // Cálculo del movimiento en espiral
                float xSpiral = Mathf.Cos(spiralAngle) * spiralRadius;
                float zSpiral = Mathf.Sin(spiralAngle) * spiralRadius;

                Vector3 spiralMovement = new Vector3(xSpiral, 0, zSpiral);
                transform.position += forwardMovement + spiralMovement;
                break;
        }
    }

    // Este método se llama cuando la bala se destruye
    private void OnDestroy()
    {
        if (FindObjectOfType<EnemyShooting>() != null)
        {
            FindObjectOfType<EnemyShooting>().RemoveBulletFromList(gameObject);
        }
    }
}
