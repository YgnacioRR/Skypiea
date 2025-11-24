using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    // Este método se llamará al presionar START
    public void IniciarJuego()
    {
        SceneManager.LoadScene("Mapa_Base"); // Nombre exacto de la escena del juego
    }

    // Opcional: salir del juego (para añadir más adelante)
    public void Salir()
    {
        Application.Quit();
    }
}
