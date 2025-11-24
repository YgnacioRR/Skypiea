using UnityEngine;
using UnityEngine.SceneManagement;

public class MuertePorCaida : MonoBehaviour
{
    public float distanciaMaximaCaida = 15f; // Distancia máxima antes de morir
    private float alturaMaxima;               // La altura más alta alcanzada

    void Start()
    {
        alturaMaxima = transform.position.y;
    }

    void Update()
    {
        // Si el jugador sube, actualiza su altura máxima
        if (transform.position.y > alturaMaxima)
        {
            alturaMaxima = transform.position.y;
        }

        // Si cae más allá del límite permitido desde su punto más alto
        if (transform.position.y < alturaMaxima - distanciaMaximaCaida)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("Jugador murió por caída");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}