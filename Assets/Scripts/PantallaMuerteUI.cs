using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaMuerteUI : MonoBehaviour
{
    [Header("Escenas")]
    public string escenaJuego = "Mapa_Base";          // nombre de tu escena de juego
    public string escenaMenuPrincipal = "Menu_Principal"; // nombre de tu menú

    public void VolverAJugar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(escenaJuego);
    }

    public void VolverAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(escenaMenuPrincipal);
    }
}
