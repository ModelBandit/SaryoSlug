using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OutlineRenderer))]
public class OutlineRendererEditor : Editor
{
    OutlineRenderer module = null;
    private void OnEnable()
    {
        module = target as OutlineRenderer;
    }
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Create Meshs Object"))
        {
            module.MeshMaker();
        }
        if(GUILayout.Button("Create Vertex Object"))
        {
            module.VertexMaker();
        }
        base.OnInspectorGUI();
        if(GUILayout.Button("Vector Calcurator"))
        {
            module.VectorCalcu();
        }
    }
}
