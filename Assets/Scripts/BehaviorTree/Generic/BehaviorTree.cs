using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
     public class Tree
     {
        protected Node _root = null;
        public bool Selected = false;
        private List<UIBehaviorTreeNode> _uiNodes;

        public Tree()
        {
            _uiNodes = new List<UIBehaviorTreeNode>();
        }

        public Node GetRoot()
        {
            return _root;
        }

        public void Select()
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(_root);
            int index = 1;
            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                _uiNodes[index].ChangeColor(current.GetStatus());
                List<Node> children = current.GetChildren();
                if (children != null)
                {
                    for (int i = 0; i < children.Count; i++)
                    {
                        queue.Enqueue(children[i]);
                    }
                }

                index++;
            }
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

        public void SetUIPointers(GameObject uiTreeObject) {
            Transform uiTreeTransform = uiTreeObject.transform;
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(_root);
            int index = 1;
            while (queue.Count > 0) {
                Node current = queue.Dequeue();
                UIBehaviorTreeNode uiNode = uiTreeTransform.GetChild(index).gameObject.GetComponent<UIBehaviorTreeNode>();
                _uiNodes.Add(uiNode);
                current.SetUIPointer(uiNode);
                List<Node> children = current.GetChildren();
                if (children != null) {
                    for (int i = 0; i < children.Count; i++)
                    {
                        queue.Enqueue(children[i]);
                    }
                }

                index++;
            }
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

        private void ResetUINodeColors() {
            for (int i = 0; i < _uiNodes.Count; i++) {
                _uiNodes[i].ChangeColor(NodeStatus.UNEXECUTED);
            }
        }
    }
}