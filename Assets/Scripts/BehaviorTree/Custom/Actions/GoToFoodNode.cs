using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree {
    public class GoToFoodNode : AnimalActionNode
    {

        private const float _goToFoodEpsilon = 3.0f;
        public GoToFoodNode() : base("Go To Food")
        {
        }

        protected override void Enter()
        {
            Debug.Log("Going to food...");
            _referencePoint = _ownerAnimal.GetTargetObject().transform.position;
        }

        protected override NodeStatus Execute()
        {
            if (!_ownerAnimal.MoveToPosition(_referencePoint))
            {
                return NodeStatus.FAILURE;
            }

            if (_ownerAnimal.IsNearPosition(_referencePoint, _goToFoodEpsilon))
            {
                return NodeStatus.SUCCESS;
            }

            return NodeStatus.RUNNING;
        }
    }
}
