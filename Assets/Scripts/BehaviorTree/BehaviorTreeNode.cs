using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public enum NodeStatus {
        RUNNING = 0,
        SUCCESS = 1,
        FAILURE = 2
    }

    public abstract class Node {
        public string name;

        protected bool _isLeaf;
        protected List<Node> _children;

        GameObject owner;

        public virtual NodeStatus tick() {
            return NodeStatus.SUCCESS; 
        }
        public Node() {
        }

        public virtual void addChild(Node node) {
            if (_isLeaf) {
                Debug.LogError("Error: attempted to add a child to a leaf node.");
            }

            if (_children == null)
            {
                _children = new List<Node>();
            }

            if (node != null) {
                _children.Add(node);
            }
        }

        public virtual void removeChild(Node node) {
            if (_isLeaf)
            {
                Debug.LogError("Error: attempted to remove a child from a leaf node.");
            }

            if (_children != null && node != null) {
                _children.Remove(node);
            }
        }
    }
}