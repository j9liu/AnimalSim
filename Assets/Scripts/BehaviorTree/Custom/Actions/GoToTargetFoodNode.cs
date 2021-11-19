using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree {
    public class GoToTargetFoodNode : AnimalActionNode
    {

        private const float _targetEpsilon = 3.0f;

        public GoToTargetFoodNode() : base("Go To Target Food")
        {
        }

        protected override void Enter()
        {
            _referencePoint = _ownerAnimal.GetTargetFood().transform.position;
        }

        protected override NodeStatus Execute()
        {
            if (!_ownerAnimal.MoveToPosition(_referencePoint))
            {
                return NodeStatus.FAILURE;
            }

            if (_ownerAnimal.IsNearPosition2D(_referencePoint, _targetEpsilon))
            {

                return NodeStatus.SUCCESS;
            }

            return NodeStatus.RUNNING;
        }
    }
}
