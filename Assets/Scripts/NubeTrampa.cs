using System.Collections;
using UnityEngine;

public class NubeTrampa : MonoBehaviour
{
    [Header("Comportamiento")]
    public float retardoDesaparicion = 0.1f;   // Tiempo antes de desaparecer
    public float tiempoReaparicion = -1f;      // Si es >= 0 reaparece
    public bool destruirSiNoReaparece = false;

    [Header("Parpadeo")]
    public int cantidadParpadeos = 5;
    public float velocidadParpadeo = 0.1f;     // velocidad de ON/OFF

    [Header("Opcional")]
    public GameObject efectoAlDesaparecer;

    bool activada = false;
    Collider2D[] colisiones;
    SpriteRenderer[] sprites;
    PlatformEffector2D effector;

    void Awake()
    {
        colisiones = GetComponentsInChildren<Collider2D>(true);
        sprites = GetComponentsInChildren<SpriteRenderer>(true);
        effector = GetComponent<PlatformEffector2D>();
    }

    void OnCollisionEnter2D(Collision2D c) { VerificarJugador(c.collider); }
    void OnTriggerEnter2D(Collider2D other) { VerificarJugador(other); }

    void VerificarJugador(Collider2D col)
    {
        if (activada) return;
        if (!col.CompareTag("Player")) return;

        activada = true;
        StartCoroutine(DesaparecerConParpadeo());
    }

    IEnumerator DesaparecerConParpadeo()
    {
        // 🔥 PARPADEO ANTES DE DESAPARECER 🔥
        for (int i = 0; i < cantidadParpadeos; i++)
        {
            SetVisible(false);
            yield return new WaitForSeconds(velocidadParpadeo);
            SetVisible(true);
            yield return new WaitForSeconds(velocidadParpadeo);
        }

        // 🔥 RETARDO FINAL
        if (retardoDesaparicion > 0)
            yield return new WaitForSeconds(retardoDesaparicion);

        // 🔥 DESAPARECER
        SetVisible(false);
        SetCollision(false);

        if (efectoAlDesaparecer)
            Instantiate(efectoAlDesaparecer, transform.position, Quaternion.identity);

        // 🔥 ¿REAPARECER?
        if (tiempoReaparicion >= 0)
        {
            yield return new WaitForSeconds(tiempoReaparicion);
            SetVisible(true);
            SetCollision(true);
            activada = false;
        }
        else if (destruirSiNoReaparece)
        {
            Destroy(gameObject);
        }
    }

    void SetVisible(bool estado)
    {
        foreach (var s in sprites) s.enabled = estado;
    }

    void SetCollision(bool estado)
    {
        foreach (var c in colisiones) c.enabled = estado;
        if (effector) effector.enabled = estado;
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (tiempoReaparicion >= 0f)
            destruirSiNoReaparece = false;
    }
#endif
}
