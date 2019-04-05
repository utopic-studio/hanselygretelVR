using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UResourceIndex))]
public class UResourceIndexInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        UResourceIndex Index = (UResourceIndex)target;
        if (GUILayout.Button("Bake ResourceIndex"))
        {
            Index.Bake();
        }
    }
}
