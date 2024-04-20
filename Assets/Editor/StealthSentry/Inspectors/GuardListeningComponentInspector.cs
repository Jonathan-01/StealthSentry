using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StealthSentry.Inpectors
{
    [CustomEditor(typeof(GuardListeningComponent))]

    public class GuardListeningComponentInspector : Editor
    {
        // Incapacitation Attributes
        private SerializedProperty ListenRangeProperty;
        private SerializedProperty SearchIntesnsityProperty;
        private SerializedProperty IncreaseSuspicionProperty;

        private void OnEnable()
        {
            // Retrieve the properties we want to edit
            ListenRangeProperty = serializedObject.FindProperty("ListenRange");
            SearchIntesnsityProperty = serializedObject.FindProperty("SearchIntesnsity");
            IncreaseSuspicionProperty = serializedObject.FindProperty("IncreaseSuspicion");
        }
        public override void OnInspectorGUI()
        {
            // Update the properties with the set values
            serializedObject.Update();

            // Draw the properties
            DrawProperties();

            // Apply the properties to the referenced value
            serializedObject.ApplyModifiedProperties();
        }

        #region Draw Methods
        private void DrawProperties()
        {
            // Draw the listening properties
            EditorGUILayout.LabelField("Listening", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(ListenRangeProperty);
            ListenRangeProperty.intValue = Mathf.Clamp(ListenRangeProperty.intValue, 1, 999999);
            EditorGUILayout.Space(10);

            // Draw the searching properties
            EditorGUILayout.LabelField("Searching", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(SearchIntesnsityProperty);
            SearchIntesnsityProperty.intValue = Mathf.Clamp(SearchIntesnsityProperty.intValue, 1, 100);
            EditorGUILayout.PropertyField(IncreaseSuspicionProperty);
        }
        #endregion
    }
}