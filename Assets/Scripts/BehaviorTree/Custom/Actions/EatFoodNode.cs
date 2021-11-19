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
            if (_ownerAnimal.EatTargetFood()) {
                return NodeStatus.RUNNING;
            }

            return NodeStatus.SUCCESS;
        }
    }
}

