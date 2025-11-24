using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlatformEffector2D))]
public class OneWayPlatformAutoFit : MonoBehaviour
{
    [Header("Franja superior")]
    public float thickness = 0.24f;     // alto del “piso”
    public float topOffset = 0.06f;     // desplazar un poco hacia arriba
    public float sidePadding = 0.08f;   // margen extra a cada lado

    void Awake()
    {
        var sr = GetComponent<SpriteRenderer>();
        var col = GetComponent<BoxCollider2D>();
        var eff = GetComponent<PlatformEffector2D>();

        // orientación correcta
        transform.rotation = Quaternion.identity;

        // ancho del sprite en MUNDO
        float worldWidth = sr.bounds.size.x;

        // convertir a espacio LOCAL del collider
        float localWidth = worldWidth / transform.lossyScale.x;

        // configurar collider como franja superior ancha
        col.isTrigger = false;
        col.usedByEffector = true;
        col.edgeRadius = 0f;
        col.size = new Vector2(localWidth + sidePadding * 2f, thickness);
        col.offset = new Vector2(0f, topOffset);

        // effector de un solo lado
        eff.useOneWay = true;
        eff.surfaceArc = 175f;
        eff.rotationalOffset = 0f;
    }
}
