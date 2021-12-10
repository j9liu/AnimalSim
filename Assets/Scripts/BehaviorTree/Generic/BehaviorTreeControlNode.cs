using System.Collections.Generic;
using UnityEngine;


namespace BehaviorSim.BehaviorTree {
    public abstract class ControlNode : Node
    {
        protected List<Node> _children;

        public ControlNode(string name) : base(name, NodeType.CONTROL)
        {
            _children = new List<Node>();
        }

        public virtual void AddChild(Node node)
        {
            if (node != null)
            {
                _children.Add(node);
            }
        }

        public bool HasChildren()
        {
            return _children.Count > 0;
        }

        public Node GetChild(int index)
        {
            if (index < 0 || index > _children.Count)
            {
                Debug.LogError("Error: attempted to get a child with out of range index.");
            }

            return _children[index];
        }

        public List<Node> GetChildren()
        {
            return _children;
        }

        public virtual void RemoveChild(Node node)
        {
            if (node != null)
            {
                _children.Remove(node);
            }
        }

        public override void SetOwner(GameObject owner, Tree tree)
        {
            base.SetOwner(owner, tree);
            foreach (Node child in _children)
            {
                child.SetOwner(owner, tree);
            }
        }
    }

}