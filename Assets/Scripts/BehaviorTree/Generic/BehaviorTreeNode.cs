using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public enum NodeStatus
    {
        ERROR = 0,
        UNEXECUTED = 1,
        RUNNING = 2,
        FAILURE = 3,
        SUCCESS = 4,
    }

    public abstract class Node
    {
        public readonly string Name;

        protected readonly bool _isLeaf;
        protected List<Node> _children;

        protected GameObject _owner;
        protected Tree _ownerTree;

        protected NodeStatus _status = NodeStatus.UNEXECUTED;

        private UIBehaviorTreeNode _uiNode = null;

        public Node(string n, bool isLeaf)
        {
            Name = n;
            _isLeaf = isLeaf;
        }

        public abstract NodeStatus Tick();

        public virtual void AddChild(Node node)
        {
            if (_isLeaf)
            {
                Debug.LogError("Error: attempted to add a child to a leaf node.");
            }

            if (_children == null)
            {
                _children = new List<Node>();
            }

            if (node != null)
            {
                _children.Add(node);
            }
        }

        public bool HasChildren()
        {
            return (!_isLeaf && _children != null && _children.Count > 0);
        }

        public Node GetChild(int index)
        {
            if (_isLeaf)
            {
                Debug.LogError("Error: attempted to get a child of a leaf node.");
            }

            if (index < 0 || _children == null || index > _children.Count) {
                Debug.LogError("Error: attempted to get a child with out of range index.");
            }

            return _children[index];
        }

        public List<Node> GetChildren()
        {
            return _children;
        }

        public NodeStatus GetStatus() {
            return _status;
        }
        public virtual void RemoveChild(Node node)
        {
            if (_isLeaf)
            {
                Debug.LogError("Error: attempted to remove a child from a leaf node.");
            }

            if (_children != null && node != null)
            {
                _children.Remove(node);
            }
        }

        public virtual void SetOwner(GameObject owner, Tree tree)
        {
            _owner = owner;
            _ownerTree = tree;
            if (_children != null) {
                foreach(Node child in _children) {
                    child.SetOwner(owner, tree);
                }
            }
        }

        protected void SetStatus(NodeStatus status)
        {
            _status = status;
            if (_ownerTree.Selected && _uiNode != null)
            {
                _uiNode.ChangeColor(_status);
            }
        }

        public void SetUIPointer(UIBehaviorTreeNode uiNode) {
            _uiNode = uiNode;
        }
    }
}