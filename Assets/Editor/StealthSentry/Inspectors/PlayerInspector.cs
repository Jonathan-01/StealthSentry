using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StealthSentry.Inpectors
{
    using Player;

    [CustomEditor(typeof(PlayerController))]
    public class PlayerInspector : Editor
    {
        private SerializedProperty RunSpeedProperty;
        private SerializedProperty WalkSpeedProperty;
        private SerializedProperty CrouchSpeedProperty;
        private SerializedProperty CamXProperty;
        private SerializedProperty CamYProperty;

        // Start is called before the first frame update
        void OnEnable()
        {
            RunSpeedProperty = serializedObject.FindProperty("RunSpeed");
            WalkSpeedProperty = serializedObject.FindProperty("WalkSpeed");
            CrouchSpeedProperty = serializedObject.FindProperty("CrouchSpeed");
            CamXProperty = serializedObject.FindProperty("CameraSensitivityX");
            CamYProperty = serializedObject.FindProperty("CameraSensitivityY");
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
            EditorGUILayout.LabelField("Movement", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(RunSpeedProperty);
            EditorGUILayout.PropertyField(WalkSpeedProperty);
            EditorGUILayout.PropertyField(CrouchSpeedProperty);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Camera", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(CamXProperty);
            EditorGUILayout.PropertyField(CamYProperty);
        }
        #endregion
    }
}
