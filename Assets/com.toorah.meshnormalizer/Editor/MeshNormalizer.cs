using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(MeshFilter))]
public class MeshNormalizer : MonoBehaviour
{

    [SerializeField] MeshFilter m_filter;
    public Mesh Mesh => m_filter.sharedMesh;
    public MeshFilter Filter => m_filter;

    private void OnValidate()
    {
        m_filter = GetComponent<MeshFilter>();
    }
    private void Reset()
    {
        m_filter = GetComponent<MeshFilter>();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MeshNormalizer))]
public class MeshNormalizerEditor : Editor
{
    MeshNormalizer m_target;

    private void OnEnable()
    {
        m_target = target as MeshNormalizer;
        SceneView.duringSceneGui += SceneView_duringSceneGui;
    }
    private void OnDisable()
    {
        SceneView.duringSceneGui -= SceneView_duringSceneGui;
    }

    private void SceneView_duringSceneGui(SceneView obj)
    {
        int i = 0;
        foreach(var vert in m_target.Mesh.vertices)
        {
            var pos = m_target.transform.TransformPoint(vert);
            Handles.Label(pos, $"Vert: {i}");
            i++;
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Normalize"))
        {
            NormalizeMesh();
        }
    }

    void NormalizeMesh()
    {
        string undoName = $"Normalize Mesh \"{m_target.gameObject.name}\"";

        Undo.RecordObject(m_target.transform, undoName);
        Undo.RecordObject(m_target.Filter, undoName);

        var verts = m_target.Mesh.vertices;
        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] = m_target.transform.TransformPoint(verts[i]);
        }

        Mesh mesh = Instantiate(m_target.Mesh);
        Undo.RegisterCreatedObjectUndo(mesh, undoName);
        m_target.Filter.sharedMesh = mesh;
        m_target.transform.localScale = Vector3.one;
        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] = m_target.transform.InverseTransformPoint(verts[i]);
        }


        mesh.vertices = verts;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
        mesh.RecalculateNormals();
        Undo.FlushUndoRecordObjects();
    }
}
#endif
