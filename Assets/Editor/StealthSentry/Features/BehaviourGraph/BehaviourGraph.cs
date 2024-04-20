using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace StealthSentry.Graph
{
    public class BehaviourGraph : EditorWindow
    {
        [MenuItem("StealthSentry/BehaviourGraph", false, 2)]
        public static void Open()
        {
            GetWindow<BehaviourGraph>("Behaviour Graph");
        }

        private void OnEnable()
        {
            AddGraphView();
        }

        private void AddGraphView()
        {
            GraphViewer graphViewer = new GraphViewer();
            graphViewer.StretchToParentSize();

            rootVisualElement.Add(graphViewer);
        }
    }
}
