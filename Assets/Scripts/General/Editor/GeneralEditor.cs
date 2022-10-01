using UnityEditor;
using UnityEngine;
using UnityEditor.Rendering;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 2.0.0
namespace General.Editor
{
    public abstract class GeneralEditor : UnityEditor.Editor
    {
        private readonly GUIContent _defaultInspectorLabel = new("Default Inspector");
        
        private EditorPrefBool _isDefaultInspector;

        public sealed override void OnInspectorGUI()
        {
            _isDefaultInspector.value = EditorGUILayout.Toggle(_defaultInspectorLabel, _isDefaultInspector.value);
            EditorGUILayout.Space();
            
            if (!_isDefaultInspector.value)
            {
                serializedObject.Update();
                DrawFields();
                serializedObject.ApplyModifiedProperties();
            }
            else base.OnInspectorGUI();
        }

        protected abstract void DrawFields();
    }
}
