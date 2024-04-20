using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StealthSentry.Inpectors
{
    [CustomEditor(typeof(DistractionStatic))]
    public class DistractionStaticInspector : Editor
    {
        // Incapacitation Attributes
        private SerializedProperty AudioSourceProperty;
        private SerializedProperty AudioClipProperty;
        private SerializedProperty DelayProperty;
        private SerializedProperty AttractionRangeProperty;
        private SerializedProperty TriggerThrowProperty;
        private SerializedProperty TriggerShootProperty;
        private SerializedProperty TriggerDirectProperty;

        private void OnEnable()
        {
            // Retrieve the properties we want to edit
            AudioSourceProperty = serializedObject.FindProperty("AudioSource");
            AudioClipProperty = serializedObject.FindProperty("AudioClip");
            DelayProperty = serializedObject.FindProperty("Delay");
            AttractionRangeProperty = serializedObject.FindProperty("AttractionRange");
            TriggerThrowProperty = serializedObject.FindProperty("ByThrowable");
            TriggerShootProperty = serializedObject.FindProperty("ByShot");
            TriggerDirectProperty = serializedObject.FindProperty("ByInteraction");
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

            // Draw the disarm properties
            EditorGUILayout.LabelField("Triggers", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(TriggerThrowProperty);
            EditorGUILayout.PropertyField(TriggerShootProperty);
            EditorGUILayout.PropertyField(TriggerDirectProperty);
        }
        #endregion
    }
}
