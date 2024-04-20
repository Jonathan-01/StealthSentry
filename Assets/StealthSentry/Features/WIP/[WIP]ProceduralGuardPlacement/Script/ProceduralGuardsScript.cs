using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace StealthSentry
{
    [ExecuteInEditMode]
    public class ProceduralGuardsScript : MonoBehaviour
    {
        // Enumerator for guard list
        private enum GuardType
        {
            roaming,
            wallMounted,
            stationary,
            flying
        }

        // Serialised struct for guard list
        [Serializable]
        private struct GuardInfo
        {
            [Tooltip("What Guard should be spawned")]
            [SerializeField] private GameObject GuardToSpawn;
            [Tooltip("How the guard should be placed")]
            [SerializeField] private GuardType GuardType;
            [Tooltip("How many of this type should be placed")]
            [SerializeField] private int SpawnAmount;
        }

        // List to set data on what guards to spawn, how to spawn them and how many to spawn
        [Tooltip("What to spawn and how many should be spawned")]
        [SerializeField] private List<GuardInfo> GuardsToSpawn;

        // Hide mesh on start-up
        private void Start()
        {
            MeshRenderer mesh = (MeshRenderer)GetComponent(typeof(MeshRenderer));
            mesh.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            // If editor isn't running the application then show the mesh
            if (UnityEditor.EditorApplication.isPlaying == false)
            {
                MeshRenderer mesh = (MeshRenderer)GetComponent(typeof(MeshRenderer));
                mesh.enabled = true;
            }
        }

        public void GenerateGuards()
        {
            // No current functionality but send a debug line to confirm the function is correctly called
            Debug.Log("Button Clicked");
        }
    }

}
