using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StealthSentry.Inpectors
{
    [CustomEditor(typeof(PatrolRoute))]

    public class PatrolRouteInspector : Editor
    {
        // Incapacitation Attributes
        private SerializedProperty PatrolListProperty;
        private SerializedProperty PatrolPatternProperty;

        private void OnEnable()
        {
            // Retrieve the properties we want to edit
            PatrolListProperty = serializedObject.FindProperty("PatrolList");
            PatrolPatternProperty = serializedObject.FindProperty("PatrolPattern");
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
            // Draw the patrol properties
            EditorGUILayout.LabelField("Patrols", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(PatrolListProperty); 
            EditorGUILayout.PropertyField(PatrolPatternProperty);
        }
        #endregion
    }
}
