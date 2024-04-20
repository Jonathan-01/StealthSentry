using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StealthSentry.Inpectors
{
    using Alarms;

    [CustomEditor(typeof(Alarm))]
    public class AlarmInspector : Editor
    {
        // Incapacitation Attributes
        private SerializedProperty AudioSourceProperty;
        private SerializedProperty AudioClipProperty;
        private SerializedProperty AttractionRangeProperty;
        private SerializedProperty DurationProperty;
        //private SerializedProperty DisarmableProperty;
        //private SerializedProperty DisarmFromProperty;

        private void OnEnable()
        {
            // Retrieve the properties we want to edit
            AudioSourceProperty = serializedObject.FindProperty("AudioSource");
            AudioClipProperty = serializedObject.FindProperty("AudioClip");
            AttractionRangeProperty = serializedObject.FindProperty("AttractionRange");
            DurationProperty = serializedObject.FindProperty("Duration");
            //DisarmableProperty = serializedObject.FindProperty("Disarmable");
            //DisarmFromProperty = serializedObject.FindProperty("DisarmedFrom");
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
            EditorGUILayout.PropertyField(DurationProperty);
            EditorGUILayout.Space(10);

            /*
            // Draw the disarm properties
            EditorGUILayout.LabelField("Disarm", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(DisarmableProperty);
            if (DisarmableProperty.boolValue)
            {
                EditorGUILayout.PropertyField(DisarmFromProperty);
            }
            */
        }
        #endregion
    }
}
