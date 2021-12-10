using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public abstract class DecoratorNode : Node
    {
        protected Node _child;

        public DecoratorNode(string name, Node child) : base(name, NodeType.DECORATOR)
        {
            _child = child;
        }

        public Node GetChild()
        {
            return _child;
        }

        protected virtual void Enter() { }

        protected virtual void Exit() { }

        public override void SetOwner(GameObject owner, Tree tree)
        {
            base.SetOwner(owner, tree);
            _child.SetOwner(owner, tree);
        }
    }
}