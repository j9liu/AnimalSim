using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public abstract class AnimalDecoratorNode : DecoratorNode
    {
        protected Animal _ownerAnimal;

        public AnimalDecoratorNode(string name, Node child) : base(name, child)
        {
        }

        public override void SetOwner(GameObject owner, Tree tree)
        {
            base.SetOwner(owner, tree);
            _ownerAnimal = owner.GetComponent<Animal>();
        }
    }
}

