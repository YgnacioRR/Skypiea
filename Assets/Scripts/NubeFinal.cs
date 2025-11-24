using UnityEngine;
using UnityEngine.SceneManagement;

public class NubeFinal : MonoBehaviour
{
    [Tooltip("Nombre de la escena de fin de demo")]
    public string escenaFinal = "FinDemo";

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            IrAFinDemo();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IrAFinDemo();
        }
    }

    void IrAFinDemo()
    {
        Time.timeScale = 1f; // por si acaso vienes de una muerte pausada
        SceneManager.LoadScene(escenaFinal);
    }
}
