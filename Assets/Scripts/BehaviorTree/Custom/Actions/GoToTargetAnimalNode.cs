using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public class GoToTargetAnimalNode : AnimalActionNode
    {

        private const float _targetEpsilon = 3.0f;

        public GoToTargetAnimalNode() : base("Chase Animal")
        {
        }

        protected override void Enter()
        {
            _referencePoint = _ownerAnimal.GetTargetObject().transform.position;
        }

        protected override NodeStatus Execute()
        {
            if (!_ownerAnimal.MoveToPosition(_referencePoint))
            {
                return NodeStatus.FAILURE;
            }

            if (_ownerAnimal.IsNearPosition(_referencePoint, _targetEpsilon))
            {
                return NodeStatus.SUCCESS;
            }

            return NodeStatus.RUNNING;
        }
    }
}
