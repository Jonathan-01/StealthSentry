using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace StealthSentry.Graph.Elements
{
    public class BaseNode : Node
    {
        public string identifier;
        public NodeType nodeType { get; set; }

        public void Init(Vector2 position)
        {
            identifier = "BGNode-WIP";
            SetPosition(new Rect(position, Vector2.zero));
        }

        public void Draw()
        {
            // Title Container
            TextField nodeNameTextField = new TextField()
            {
                value = identifier
            };

            titleContainer.Insert(0, nodeNameTextField);

            // Input continer
            Port inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));

            inputPort.portName = "Input";

            inputContainer.Add(inputPort);

            // Output Container
            Port outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));

            outputPort.portName = "Output";

            outputContainer.Add(outputPort);
        }
    }
}