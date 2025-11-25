#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class MissingScriptCleaner
{
    [MenuItem("Tools/Limpiar Missing Scripts (Proyecto)")]
    public static void CleanAll()
    {
        int total = 0;

        // Busca TODOS los GameObjects cargados (escena + prefabs abiertos, etc.)
        var allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (var go in allObjects)
        {
            // Opcional: si quieres, puedes saltarte los objetos internos del editor
            // if ((go.hideFlags & HideFlags.HideInHierarchy) != 0) continue;

            total += GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
        }

        Debug.Log($"Limpieza completa. Componentes Missing eliminados: {total}");
    }
}
#endif


