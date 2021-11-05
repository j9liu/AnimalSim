using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public class AnimalConditionNode : ConditionNode
    {
        protected Animal _ownerAnimal;

        public AnimalConditionNode(string name) : base(name)
        {
        }

        public override void SetOwner(GameObject owner, Tree tree)
        {
            base.SetOwner(owner, tree);
            _ownerAnimal = owner.GetComponent<Animal>();
        }
    }
}

