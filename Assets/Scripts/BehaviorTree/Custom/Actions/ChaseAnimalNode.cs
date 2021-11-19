using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public class GoToTargetAnimalNode : AnimalActionNode
    {

        private const float _targetEpsilon = 8.0f;

        public GoToTargetAnimalNode() : base("Chase Animal")
        {
        }

        protected override void Enter()
        {
            _targetObject = _ownerAnimal.GetTargetObject();
        }

        protected override NodeStatus Execute()
        {
            Vector3 targetPosition = _targetObject.transform.position;
            if (!_ownerAnimal.MoveToPosition(targetPosition))
            {
                return NodeStatus.FAILURE;
            }

            if (_ownerAnimal.IsNearPosition2D(targetPosition, _targetEpsilon))
            {
                return NodeStatus.SUCCESS;
            }

            return NodeStatus.RUNNING;
        }
    }
}
