using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Island))]
public class IslandEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Island islandTarget = (Island)target;

        if (DrawDefaultInspector())
        {
            if (islandTarget.autoUpdate)
                islandTarget.GenerateIsland();
        }

        if (GUILayout.Button("Generate"))
        {
            islandTarget.GenerateIsland();
        }
    }
}
