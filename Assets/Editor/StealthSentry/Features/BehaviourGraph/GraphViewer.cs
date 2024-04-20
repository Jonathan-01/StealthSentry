using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace StealthSentry.Graph
{
    using Elements;

    public class GraphViewer : GraphView
    {
        public GraphViewer()
        {
            AddGrid();
            AddManipulators();

            AddStyles();
        }

        private BaseNode CreateNode(Vector2 pos)
        {
            BaseNode node = new BaseNode();

            Vector2 viewPos = new Vector2(viewTransform.position.x, viewTransform.position.y);
            node.Init((pos - viewPos) / viewTransform.scale);
            node.Draw();

            return node;
        }

        private void AddManipulators()
        {
            SetupZoom(0.3f, 2.0f);

            this.AddManipulator(CreateNodeMenu());

            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new ContentDragger());
        }

        private IManipulator CreateNodeMenu()
        {
            ContextualMenuManipulator menuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction("Add Node", actionEvent => AddElement(CreateNode(actionEvent.eventInfo.localMousePosition)))
            );

            return menuManipulator;
        }

        private void AddGrid()
        {
            GridBackground grid = new GridBackground();

            grid.StretchToParentSize();

            Insert(0, grid);
        }

        private void AddStyles()
        {
            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/StealthSentry/Styles/GraphStyle.uss");

            styleSheets.Add(styleSheet);
        }
    }
}
