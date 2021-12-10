using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree {
    public class GoToTargetFoodNode : AnimalActionNode
    {

        private float _targetEpsilon;
        private float _goToSpeed;

        public GoToTargetFoodNode(float targetEpsilon) : base("Go To Target Food")
        {
            _targetEpsilon = targetEpsilon;
        }

        protected override void Enter()
        {
            _goToSpeed = _ownerAnimal.Stats.MaxSpeed / 2.0f;
            _targetObject = _ownerAnimal.GetTargetFood().gameObject;
        }

        protected override NodeStatus Execute()
        {
            _referencePoint = _targetObject.transform.position;
            if (!_ownerAnimal.MoveToPosition(_referencePoint, _goToSpeed))
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
