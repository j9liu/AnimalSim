using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public class Tree
    {
        protected Node _root = null;
        public bool Selected = false;
        private List<UIBehaviorTreeNode> _uiNodes;
        private bool _uiNodesLoaded = false;

        public Tree()
        {
            _uiNodes = new List<UIBehaviorTreeNode>();
        }

        private void ChangeUIColor(Node node, UIBehaviorTreeNode uiNode) {
            uiNode.ChangeColor(node.GetStatus());
        }

        public Node GetRoot()
        {
            return _root;
        }

        public void Select()
        {

            Traverse(null, new ManageUI(ChangeUIColor));
        }

        public void SetRoot(Node root)
        {
            _root = root;
        }

        public void SetOwner(GameObject owner)
        {
            if (_root != null) {
                _root.SetOwner(owner, this);
            }
        }

        public delegate void ManageUI(Node node, UIBehaviorTreeNode uiNode);

        public void SetUIPointer(Node node, UIBehaviorTreeNode uiNode) {
            _uiNodes.Add(uiNode);
            node.SetUIPointer(uiNode);
        }

        public void SetUIPointers(GameObject uiTreeObject) {
            Traverse(uiTreeObject, new ManageUI(SetUIPointer));
        }

        public void Tick()
        {
            if (_root != null)
            {
                NodeStatus result = _root.Tick();
                if (Selected && (result != NodeStatus.RUNNING))
                {
                    ResetUINodeColors();
                }
            }
        }

        protected void Traverse(GameObject uiTreeObject, ManageUI manageFunction)
        {
            Transform uiTreeTransform = uiTreeObject.transform;

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(_root);
            int index = 1;
            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                UIBehaviorTreeNode uiNode = null;
                if (_uiNodesLoaded)
                {
                    uiNode = _uiNodes[index];
                }
                else {
                    uiNode = uiTreeTransform.GetChild(index).gameObject.GetComponent<UIBehaviorTreeNode>();
                }

                manageFunction(current, uiNode);

                switch (current.Type)
                {
                    case NodeType.CONTROL:
                        List<Node> children = ((ControlNode)current).GetChildren();
                        for (int i = 0; i < children.Count; i++)
                        {
                            queue.Enqueue(children[i]);
                        }
                        break;
                    case NodeType.DECORATOR:
                        queue.Enqueue(((DecoratorNode)current).GetChild());
                        break;
                    default:
                        break;
                }

                index++;
            }
        }

        private void ResetUINodeColors() {
            for (int i = 0; i < _uiNodes.Count; i++) {
                _uiNodes[i].ChangeColor(NodeStatus.UNEXECUTED);
            }
        }
    }
}