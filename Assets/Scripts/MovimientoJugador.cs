using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;

    [Header("Salto automático")]
    public bool autoJump = true;         // si lo apagas, vuelve a salto manual
    public float fuerzaSalto = 8f;       // impulso del salto (Impuse)
    public float jumpCooldown = 0.22f;   // mínimo entre saltos
    public float groundStickTime = 0.06f;// micro-pausa al tocar suelo

    [Header("Detección de suelo")]
    public Transform sensorSuelo;        // un hijo debajo de los pies
    public float radioSuelo = 0.28f;
    public LayerMask capaSuelo;          // capa(s) de Nube/Suelo

    Rigidbody2D rb;
    Collider2D col;
    float inputX;
    bool enSuelo;
    float lastJumpTime;
    float lastGroundTime;
    Vector3 escalaInicial;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rb.bodyType = RigidbodyType2D.Dynamic;
        if (rb.gravityScale <= 0f) rb.gravityScale = 2f;
        rb.freezeRotation = true;                  // evita girar al chocar
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        escalaInicial = transform.localScale;
    }

    void Update()
    {
        // 1) LECTURA DE INPUT HORIZONTAL (old Input System)
        inputX = Input.GetAxisRaw("Horizontal");   // A/D o Flechas

        // (Opcional) Si usas el nuevo Input System, activa Both (abajo en pasos)
        // o reemplaza por Keyboard.current.leftArrowKey... si lo prefieres.

        // 2) FLIP VISUAL
        if (inputX > 0)  transform.localScale = new Vector3(Mathf.Abs(escalaInicial.x), escalaInicial.y, escalaInicial.z);
        if (inputX < 0)  transform.localScale = new Vector3(-Mathf.Abs(escalaInicial.x), escalaInicial.y, escalaInicial.z);

        // 3) CHEQUEO DE SUELO
        bool estabaEnSuelo = enSuelo;
        if (sensorSuelo != null)
        {
            enSuelo = Physics2D.OverlapCircle(sensorSuelo.position, radioSuelo, capaSuelo);
        }
        else
        {
            // Fallback si olvidaste asignar sensor: usa el collider del player
            enSuelo = col.IsTouchingLayers(capaSuelo);
        }

        if (enSuelo && !estabaEnSuelo)
            lastGroundTime = Time.time;

        // 4) SALTO AUTOMÁTICO
        if (autoJump && enSuelo)
        {
            bool cooldownOk = Time.time >= lastJumpTime + jumpCooldown;
            bool yaPegadoAlSuelo = Time.time >= lastGroundTime + groundStickTime;
            if (cooldownOk && yaPegadoAlSuelo)
                HacerSalto();
        }

        // 5) SALTO MANUAL (por si quieres probar con Espacio)
        if (!autoJump && enSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            HacerSalto();
        }
    }

    void FixedUpdate()
    {
        // Movimiento horizontal con física (¡OJO: velocity, no linearVelocity!)
        rb.linearVelocity = new Vector2(inputX * velocidad, rb.linearVelocity.y);
    }

    void HacerSalto()
    {
        // “reseteo” vertical para saltos consistentes
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        lastJumpTime = Time.time;
    }

    void OnDrawGizmosSelected()
    {
        if (!sensorSuelo) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(sensorSuelo.position, radioSuelo);
    }
}
