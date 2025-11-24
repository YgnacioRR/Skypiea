using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlatformEffector2D))]
public class OneWayPlatformAutofix : MonoBehaviour
{
    public float sizeX = 1.7f;   // ajusta a tu sprite
    public float sizeY = 0.24f;
    public float offsetY = 0.06f;

    void Awake()
    {
        // Orientación correcta
        transform.rotation = Quaternion.identity;
        var sc = transform.localScale;
        if (sc.y < 0) sc.y = Mathf.Abs(sc.y);
        transform.localScale = sc;

        // Collider + Effector
        var col = GetComponent<BoxCollider2D>();
        col.isTrigger = false;
        col.usedByEffector = true;
        col.edgeRadius = 0f;
        col.size = new Vector2(sizeX, sizeY);
        col.offset = new Vector2(0f, offsetY);

        var eff = GetComponent<PlatformEffector2D>();
        eff.useOneWay = true;
        eff.surfaceArc = 180f;
        eff.rotationalOffset = 0f;
    }
}
