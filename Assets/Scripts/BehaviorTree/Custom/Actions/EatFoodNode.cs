using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree {
    public class EatFoodNode : AnimalActionNode
    {
        public EatFoodNode() : base("Eat Food")
        {
        }

        protected override NodeStatus Execute()
        {

            if (_ownerAnimal.GetTargetObject() == null) {
                return NodeStatus.FAILURE;
            }

            _ownerAnimal.Eat(_ownerAnimal.GetTargetObject());
            return NodeStatus.SUCCESS;
        }
    }
}

