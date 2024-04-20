using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StealthSentry.Inpectors
{
    [CustomEditor(typeof(DistractionThrowable))]
    public class DistractionThrowableInspector : Editor
    {
        // Incapacitation Attributes
        private SerializedProperty AudioSourceProperty;
        private SerializedProperty AudioClipProperty;
        private SerializedProperty AttractionRangeProperty;
        private SerializedProperty AmmoMaxProperty;
        private SerializedProperty ForceProperty;

        private void OnEnable()
        {
            // Retrieve the properties we want to edit
            AudioSourceProperty = serializedObject.FindProperty("AudioSource");
            AudioClipProperty = serializedObject.FindProperty("AudioClip");
            AttractionRangeProperty = serializedObject.FindProperty("AttractionRange");
            AmmoMaxProperty = serializedObject.FindProperty("AmmoMax");
            ForceProperty = serializedObject.FindProperty("Force");
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
            EditorGUILayout.PropertyField(AttractionRangeProperty);
            AttractionRangeProperty.intValue = Mathf.Clamp(AttractionRangeProperty.intValue, 1, 999999);
            EditorGUILayout.Space(10);

            // Draw the disarm properties
            EditorGUILayout.LabelField("Throwable", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(AmmoMaxProperty);
            AmmoMaxProperty.intValue = Mathf.Clamp(AmmoMaxProperty.intValue, 1, 999999);
            EditorGUILayout.PropertyField(ForceProperty);
            ForceProperty.intValue = Mathf.Clamp(ForceProperty.intValue, 1, 999999);
        }
        #endregion
    }
}
