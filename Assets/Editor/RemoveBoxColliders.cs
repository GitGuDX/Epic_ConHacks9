using UnityEngine;
using UnityEditor;

public class RemoveBoxColliders : EditorWindow
{
    [MenuItem("Tools/Remove All BoxColliders")]
    public static void RemoveAllBoxColliders()
    {
        if (EditorUtility.DisplayDialog("Remove All BoxColliders",
            "Are you sure you want to remove all BoxCollider components from all GameObjects in the scene?",
            "Yes", "No"))
        {
            BoxCollider[] colliders = FindObjectsOfType<BoxCollider>();
            int count = colliders.Length;

            for (int i = 0; i < count; i++)
            {
                DestroyImmediate(colliders[i]);
            }

            Debug.Log($"Removed {count} BoxCollider(s) from the scene.");
        }
    }
}
