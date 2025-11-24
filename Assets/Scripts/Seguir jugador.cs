using UnityEngine;

public class SeguirJugador : MonoBehaviour
{
    public Transform jugador;    // A quién debe seguir
    public float suavizado = 0.125f;  // Qué tan suave se mueve la cámara
    public Vector3 offset;       // Distancia entre cámara y jugador

    void LateUpdate()
    {
        if (jugador != null)
        {
            Vector3 posicionDeseada = jugador.position + offset;
            Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, suavizado);
            transform.position = new Vector3(posicionSuavizada.x, posicionSuavizada.y, transform.position.z);
        }
    }
}