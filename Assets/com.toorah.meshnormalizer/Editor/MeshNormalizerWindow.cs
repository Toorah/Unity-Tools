using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Toorah.Tools
{
    public class MeshNormalizerWindow : EditorWindow
    {
#pragma warning disable CS0649
        [SerializeField] VisualTreeAsset m_visualTree;
#pragma warning restore CS0649

        [SerializeField]
        Transform m_transform;

        [SerializeField]
        SerializedObject obj;

        [SerializeField]
        SerializedProperty prop;

        private void OnEnable()
        {
            obj = new SerializedObject(this);
            prop = obj.FindProperty(nameof(prop));

            rootVisualElement.Add(m_visualTree.CloneTree());
            var objField = rootVisualElement.Q<ObjectField>("transform");
            objField.BindProperty(prop);
        }


        [MenuItem("Toorah/Mesh/Mesh Normalizer")]
        static void Open()
        {
            var window = GetWindow<MeshNormalizerWindow>();
            window.titleContent = new GUIContent("Normalizer", Resources.Load<Texture>("MeshNormalizerIcon"));
            window.Show();
        }
    }

}