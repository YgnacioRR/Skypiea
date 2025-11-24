using UnityEngine;

public class MovimientoNube : MonoBehaviour
{
    [Header("Movimiento")]
    public float distancia = 3f;      // Cuánto se mueve hacia cada lado
    public float velocidad = 1f;      // Qué tan rápido se mueve

    private Vector3 posicionInicial;  // Guarda la posición original
    private bool moviendoDerecha = true;

    void Start()
    {
        posicionInicial = transform.position; // Guarda donde empieza
    }

    void Update()
    {
        // Calcula la posición objetivo dependiendo de la dirección
        float desplazamiento = Mathf.PingPong(Time.time * velocidad, distancia * 2) - distancia;
        transform.position = new Vector3(posicionInicial.x + desplazamiento, transform.position.y, transform.position.z);
    }
}

