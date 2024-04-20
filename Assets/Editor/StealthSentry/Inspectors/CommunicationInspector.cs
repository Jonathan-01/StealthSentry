using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StealthSentry.Inpectors
{
    [CustomEditor(typeof(Communication))]
    public class CommunicationInspector : Editor
    {
        // Communication Attributes
        private SerializedProperty IsGlobalProperty;
        private SerializedProperty CommRangeProperty;
        private SerializedProperty CommSiteProperty;

        private void OnEnable()
        {
            // Retrieve the properties we want to edit
            IsGlobalProperty = serializedObject.FindProperty("IsGlobal");
            CommRangeProperty = serializedObject.FindProperty("CommRange");
            CommSiteProperty = serializedObject.FindProperty("CommSite");
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
            // Draw the comm range properties
            EditorGUILayout.LabelField("Communication Range", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(IsGlobalProperty);
            // If global then we don't need to set a range
            if (IsGlobalProperty.boolValue != true)
            {
                EditorGUILayout.PropertyField(CommRangeProperty);
                CommRangeProperty.intValue = Mathf.Clamp(CommRangeProperty.intValue, 1, 999999);
            }
            EditorGUILayout.Space(10);

            // Draw the comm site property
            EditorGUILayout.LabelField("Communication Site", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(CommSiteProperty);
            EditorGUILayout.Space(10);            
        }
        #endregion
    }
}