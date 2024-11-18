using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del jugador
    public float horizontalLimit = 8f; // Límite horizontal para el movimiento (izquierda/derecha)
    public float depthLimit = 5f; // Límite de profundidad para el movimiento (adelante/atrás)

    void Update()
    {
        // Captura el input horizontal (teclas de flecha o A y D)
        float horizontalInput = Input.GetAxis("Horizontal");
        
        // Captura el input de profundidad (teclas de flecha arriba/abajo o W y S)
        float verticalInput = Input.GetAxis("Vertical");

        // Calcula la nueva posición del jugador
        Vector3 newPosition = transform.position + new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, verticalInput * moveSpeed * Time.deltaTime);

        // Limita la posición del jugador dentro de los bordes de la plataforma
        newPosition.x = Mathf.Clamp(newPosition.x, -horizontalLimit, horizontalLimit);
        newPosition.z = Mathf.Clamp(newPosition.z, -depthLimit, depthLimit);

        // Aplica la nueva posición
        transform.position = newPosition;
    }
}