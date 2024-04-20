using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StealthSentry.Inpectors
{
    [CustomEditor(typeof(NoiseHazard))]

    public class NoiseHazardInspector : Editor
    {
        // Incapacitation Attributes
        private SerializedProperty AudioSourceProperty;
        private SerializedProperty AudioClipProperty;
        private SerializedProperty DelayProperty;
        private SerializedProperty AttractionRangeProperty;

        private void OnEnable()
        {
            // Retrieve the properties we want to edit
            AudioSourceProperty = serializedObject.FindProperty("AudioSource");
            AudioClipProperty = serializedObject.FindProperty("AudioClip");
            DelayProperty = serializedObject.FindProperty("Delay");
            AttractionRangeProperty = serializedObject.FindProperty("AttractionRange");
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
            // Draw the attraction properties
            EditorGUILayout.LabelField("Attraction", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(AudioSourceProperty);
            EditorGUILayout.PropertyField(AudioClipProperty);
            EditorGUILayout.PropertyField(DelayProperty);
            DelayProperty.intValue = Mathf.Clamp(DelayProperty.intValue, 0, 999999);
            EditorGUILayout.PropertyField(AttractionRangeProperty);
            AttractionRangeProperty.intValue = Mathf.Clamp(AttractionRangeProperty.intValue, 1, 999999);
            EditorGUILayout.Space(10);
        }
        #endregion
    }
}
