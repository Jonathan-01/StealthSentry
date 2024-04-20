using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StealthSentry.Inpectors
{
    [CustomEditor(typeof(BehaviourTracker))]
    public class BehaviourTrackerInspector : Editor
    {
        // Incapacitation Attributes
        private SerializedProperty BehaviourProperty;
        private SerializedProperty StateProperty;

        private void OnEnable()
        {
            // Retrieve the properties we want to edit
            BehaviourProperty = serializedObject.FindProperty("Behaviour");
            StateProperty = serializedObject.FindProperty("State");
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
            // Draw the behaviour properties
            EditorGUILayout.LabelField("Behaviour", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(BehaviourProperty);
            GUI.enabled = false;
            EditorGUILayout.PropertyField(StateProperty);
        }
        #endregion
    }
}