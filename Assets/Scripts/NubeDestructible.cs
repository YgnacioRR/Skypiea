using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class NubeDestructible : MonoBehaviour
{
    [Header("Lógica de toques")]
    public int toquesMaximos = 2;
    private int toquesActuales = 0;

    [Header("Caída / Desaparición")]
    public float tiempoDesaparicion = 4f;
    public float distanciaCaida = 1f;

    private bool cayendo = false;

    // Componentes
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private PlatformEffector2D eff;

    void Awake()
    {
        sr  = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();

        // Effector para que sea “one-way”
        eff = GetComponent<PlatformEffector2D>();
        if (eff == null) eff = gameObject.AddComponent<PlatformEffector2D>();
        eff.useOneWay = true;      // clave
        eff.surfaceArc = 170f;     // tolerancia de ángulo “desde arriba”
        // Si ves fricción lateral rara, puedes desactivarla:
        // eff.useSideFriction = false;

        // El collider debe estar “usado por effector”
        col.usedByEffector = true;

        // Rigidbody2D (recomendado Kinematic o incluso prescindir)
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (cayendo) return;
        if (!collision.gameObject.CompareTag("Player")) return;

        // Solo cuenta si la colisión viene desde arriba
        // (normal del contacto apunta hacia fuera de la nube; desde arriba suele ser normal.y < -0.5)
        var cp = collision.contacts[0];
        if (cp.normal.y < -0.5f)
        {
            toquesActuales++;
            if (toquesActuales >= toquesMaximos)
            {
                StartCoroutine(DestruirNube());
            }
        }
    }

    private IEnumerator DestruirNube()
    {
        cayendo = true;

        // Visualmente la “hacemos fantasma” poco a poco
        float t = 0f;
        Vector3 pos0 = transform.position;

        // Opcional: mientras cae, desactivamos la colisión (para que el player no se quede enganchado)
        col.enabled = false;

        // Semitransparente de partida
        if (sr != null) sr.color = new Color(1f, 1f, 1f, 0.6f);

        while (t < tiempoDesaparicion)
        {
            t += Time.deltaTime;
            float k = t / tiempoDesaparicion;

            // Baja en vertical
            transform.position = pos0 + Vector3.down * (k * distanciaCaida);

            // Fade out
            if (sr != null)
            {
                float a = Mathf.Lerp(0.6f, 0f, k);
                sr.color = new Color(1f, 1f, 1f, a);
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}
