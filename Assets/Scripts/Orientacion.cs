using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlatformEffector2D))]
public class OneWayPlatformOrientationFix : MonoBehaviour
{
    public bool invertirLadoSolido = false; // si ves que sigue al revés, actívalo

    void Awake()
    {
        // Asegurar orientación visual correcta
        transform.rotation = Quaternion.identity;
        var s = transform.localScale;
        transform.localScale = new Vector3(Mathf.Abs(s.x), Mathf.Abs(s.y), 1f);

        // Effector: lado sólido hacia arriba
        var eff = GetComponent<PlatformEffector2D>();
        eff.useOneWay = true;
        eff.surfaceArc = 180f;
        eff.rotationalOffset = invertirLadoSolido ? 180f : 0f;

        // Collider usa el effector
        var col = GetComponent<BoxCollider2D>();
        col.isTrigger = false;
        col.usedByEffector = true;
    }
}
