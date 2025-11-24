using UnityEngine;

[RequireComponent(typeof(PlatformEffector2D))]
public class OneWayAutoSide : MonoBehaviour
{
    public bool log = false;

    void Awake()
    {
        var eff = GetComponent<PlatformEffector2D>();
        eff.useOneWay = true;
        eff.surfaceArc = 180f;

        // 1) Si algún padre invierte el “up” mundial, volteamos el lado sólido
        float upDot = Vector2.Dot(transform.up, Vector2.up);
        eff.rotationalOffset = (upDot >= 0f) ? 0f : 180f;

        // 2) EdgeCollider: si los puntos están derecha→izquierda, el normal queda hacia abajo;
        //    con esto lo compensamos igual (ya lo hace la prueba del upDot).
        if (log) Debug.Log($"[OneWayAutoSide] upDot={upDot:F3} → rotationalOffset={eff.rotationalOffset}");
    }
}
