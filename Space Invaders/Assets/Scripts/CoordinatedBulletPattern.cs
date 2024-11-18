using UnityEngine;

public class CoordinatedBulletPattern : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private float frequency;
    private float amplitude;
    private float spawnDelay;
    private float timeOffset; // Desfase en el tiempo para el movimiento de serpiente
    private float timeSinceStart; // Tiempo que ha pasado desde el inicio

    private bool isSinePattern = true; // Para saber si estamos en el patrón de serpiente
    private float changePatternTime = 10f; // Tiempo después del cual cambia el patrón
    private float patternChangeTimer = 0f; // Temporizador para el cambio de patrón

    public void Initialize(Vector3 moveDirection, float moveSpeed, float waveFrequency, float waveAmplitude, float delay)
    {
        direction = moveDirection.normalized;
        speed = moveSpeed;
        frequency = waveFrequency;
        amplitude = waveAmplitude;
        spawnDelay = delay;
        timeOffset = Time.time; // Guardar el tiempo inicial para desfase
    }

    void Update()
    {
        timeSinceStart = Time.time - timeOffset; // Tiempo desde que comenzó la bala

        // Mueve la bala en la dirección recta
        Vector3 forwardMovement = direction * speed * Time.deltaTime;

        // Actualiza el temporizador para controlar cuándo cambiar el patrón
        patternChangeTimer += Time.deltaTime;

        // Comprobar si han pasado 10 segundos y cambiar el patrón
        if (patternChangeTimer >= changePatternTime && isSinePattern)
        {
            isSinePattern = false;
            patternChangeTimer = 0f; // Reiniciar el temporizador para el nuevo patrón
            timeOffset = Time.time; // Reiniciar el tiempo para la nueva fórmula
        }

        // Patrón de onda senoidal (serpiente)
        if (isSinePattern)
        {
            // Cambiar la amplitud en función del tiempo usando Sin
            amplitude = Mathf.Sin(Time.time * frequency); // Esto hará que la amplitud varíe de lado a lado

            // Aplica la onda con la nueva amplitud
            float wave = Mathf.Sin((Time.time - timeOffset) * frequency) * amplitude;
            Vector3 snakeMovement = transform.right * wave;

            // Combina el movimiento hacia adelante y el movimiento de ondulación
            transform.position += forwardMovement + snakeMovement;
        }
        else
        {
            // Patrón circular después de 10 segundos
            float radius = 1f; // Radio del círculo
            float angle = (patternChangeTimer * frequency); // Ángulo que avanza con el tiempo

            // Movimiento en un patrón circular
            float x = Mathf.Cos(angle) * radius; // Movimiento en el eje X
            float z = Mathf.Sin(angle) * radius; // Movimiento en el eje Z

            // Aplica el movimiento circular
            Vector3 circularMovement = new Vector3(x, 0, z);
            transform.position += forwardMovement + circularMovement;
        }
    }

    // Método para cambiar el patrón a circular desde afuera
    public void SetCircularPattern()
    {
        isSinePattern = false; // Cambiar el patrón a circular
    }
}
