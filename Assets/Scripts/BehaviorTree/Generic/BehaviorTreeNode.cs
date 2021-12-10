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
        HALT = 5,
    }

    public enum NodeType
    {
        LEAF = 0,
        DECORATOR = 1,
        CONTROL = 2
    }

    public abstract class Node
    {

        public readonly string Name;

        public readonly NodeType Type;

        protected GameObject _owner;
        protected Tree _ownerTree;

        protected NodeStatus _status = NodeStatus.UNEXECUTED;

        private UIBehaviorTreeNode _uiNode = null;

        public Node(string n, NodeType type)
        {
            Name = n;
            Type = type;
        }

        public abstract NodeStatus Tick();

        public NodeStatus GetStatus()
        {
            return _status;
        }

        public virtual void SetOwner(GameObject owner, Tree tree)
        {
            _owner = owner;
            _ownerTree = tree;
        }

        protected void SetStatus(NodeStatus status)
        {
            _status = status;
            if (_uiNode != null && _ownerTree != null && _ownerTree.Selected)
            {
                _uiNode.ChangeColor(_status);
            }
        }

        public void SetUIPointer(UIBehaviorTreeNode uiNode)
        {
            _uiNode = uiNode;
        }
    }
}