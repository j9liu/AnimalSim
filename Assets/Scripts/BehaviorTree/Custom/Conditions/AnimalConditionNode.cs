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

        public override void SetOwner(GameObject owner)
        {
            base.SetOwner(owner);
            _ownerAnimal = owner.GetComponent<Animal>();
        }
    }
}

