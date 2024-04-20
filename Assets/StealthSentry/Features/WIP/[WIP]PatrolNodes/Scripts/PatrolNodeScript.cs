using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthSentry
{
    [ExecuteInEditMode]
    public class PatrolNodeScript : MonoBehaviour
    {
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

    }
}
