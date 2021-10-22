using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public enum NodeStatus
    {
        RUNNING = 0,
        SUCCESS = 1,
        FAILURE = 2
    }

    public abstract class Node
    {
        public readonly string name;

        protected readonly bool _isLeaf;
        protected List<Node> _children;

        protected GameObject _owner;
        public Node(string n, bool isLeaf)
        {
            name = n;
            _isLeaf = isLeaf;
        }

        public virtual NodeStatus Tick()
        {
            return NodeStatus.SUCCESS; 
        }
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

        public virtual void SetOwner(GameObject owner) {
            _owner = owner;
            if (_children != null) {
                foreach(Node child in _children) {
                    child.SetOwner(owner);
                }
            }
        }
    }
}