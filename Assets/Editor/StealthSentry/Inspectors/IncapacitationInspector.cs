using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StealthSentry.Inpectors
{
    [CustomEditor(typeof(Incapacitation))]
    public class IncapacitationScript : Editor
    {
        // Incapacitation Attributes
        private SerializedProperty IsLethalProperty;
        private SerializedProperty HealthProperty;
        private SerializedProperty IsNonLethalProperty;
        private SerializedProperty IsWakableProperty;
        private SerializedProperty WakeAfterProperty;

        private void OnEnable()
        {
            // Retrieve the properties we want to edit
            IsLethalProperty = serializedObject.FindProperty("IsLethalable");
            HealthProperty = serializedObject.FindProperty("Health");
            IsNonLethalProperty = serializedObject.FindProperty("IsNonLethalable");
            IsWakableProperty = serializedObject.FindProperty("IsWakeable");
            WakeAfterProperty = serializedObject.FindProperty("TimeUntilWakeSelf");
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
            // Draw the lethal properties
            EditorGUILayout.LabelField("Lethal", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(IsLethalProperty);
            // If lethalable then get a health value
            if (IsLethalProperty.boolValue)
            {
                EditorGUILayout.PropertyField(HealthProperty);
                HealthProperty.intValue = Mathf.Clamp(HealthProperty.intValue, 1, 999999);
            }
            EditorGUILayout.Space(10);

            // Draw the non-lethal properties
            EditorGUILayout.LabelField("Non-Lethal", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(IsNonLethalProperty);
            // If lethalable then get a health value
            if (IsNonLethalProperty.boolValue)
            {
                EditorGUILayout.PropertyField(IsWakableProperty);
                EditorGUILayout.PropertyField(WakeAfterProperty);
                WakeAfterProperty.intValue = Mathf.Clamp(WakeAfterProperty.intValue, 0, 999999);
            }
        }
        #endregion
    }
}
