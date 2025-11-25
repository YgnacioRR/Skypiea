using UnityEngine;

public class CamaraSeguirY : MonoBehaviour
{
    public Transform objetivo; // Jugador

    float xFija;
    float zFija;

    void Start()
    {
        // Guardamos la X y Z originales de la cámara
        xFija = transform.position.x;
        zFija = transform.position.z;
    }

    void LateUpdate()
    {
        if (objetivo == null) return;

        // La cámara solo sigue la posición Y del jugador
        transform.position = new Vector3(
            xFija,                 // X NO cambia
            objetivo.position.y,   // Y sigue al jugador
            zFija                  // Z NO cambia
        );
    }
}

