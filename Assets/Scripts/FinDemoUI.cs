using UnityEngine;
using UnityEngine.SceneManagement;

public class FinDemoUI : MonoBehaviour
{
    [Tooltip("Escena del juego principal a la que se vuelve a jugar")]
    public string escenaJuego = "Nivel1";  // o "MenuPrincipal" si prefieres

    public void VolverAJugar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(escenaJuego);
    }
}
