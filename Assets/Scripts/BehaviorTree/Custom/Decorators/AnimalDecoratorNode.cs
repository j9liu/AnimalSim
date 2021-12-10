using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public abstract class AnimalDecoratorNode : DecoratorNode
    {
        protected Animal _ownerAnimal;
        protected ConditionNode _condition;

        public AnimalDecoratorNode(string name, ConditionNode condition, Node child) : base(name, child)
        {
            _condition = condition;
        }

        public override void SetOwner(GameObject owner, Tree tree)
        {
            base.SetOwner(owner, tree);
            if (_condition != null) {
                _condition.SetOwner(owner, tree);
            }
            _ownerAnimal = owner.GetComponent<Animal>();
        }
    }
}

