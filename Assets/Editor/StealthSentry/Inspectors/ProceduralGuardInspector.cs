using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace StealthSentry.Inpectors
{
    [CustomEditor(typeof(ProceduralGuardsScript))]
    public class ProceduralGuardInspector : Editor
    {
        // Procedural Guards Attributes
        private SerializedProperty GuardsToSpawnProperty;

        private void OnEnable()
        {
            // Retrieve the properties we want to edit
            GuardsToSpawnProperty = serializedObject.FindProperty("GuardsToSpawn");
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
            // Draw the GuardsToSpawn list
            EditorGUILayout.PropertyField(GuardsToSpawnProperty);
            EditorGUILayout.Space(10);

            if (GUILayout.Button("Generate"))
            {
                ProceduralGuardsScript GuardsScript = (ProceduralGuardsScript)target;

                Debug.Log("Generating Guards...");
                GuardsScript.GenerateGuards();
            }
        }
        #endregion
    }
}