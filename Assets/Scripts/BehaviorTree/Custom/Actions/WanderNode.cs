using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree {
    public class WanderNode : AnimalActionNode
    {
        private const float _wanderEpsilon = 0.1f;
        
        public WanderNode() : base("Wander")
        {
        }

        /* Identify a point to wander towards, preferencing points in front
         * of the animal.
         */
        protected override void Enter()
        {
            Debug.Log("Wandering...");

            float sightFOV = _ownerAnimal.GetSightFOV();
            float sightRadius = _ownerAnimal.GetSightRadius();
            Vector3 direction = _ownerAnimal.GetDirection();

            float angle = Random.Range(-sightFOV / 2.0f, sightFOV / 2.0f);
            direction = Quaternion.Euler(0, angle, 0) * direction;
            direction = Vector3.Normalize(direction);

            float distance = Random.Range(sightRadius / 2.0f, sightRadius * 1.2f);
            Vector3 desiredPoint = _owner.transform.position + distance * direction;

            if (!_ownerAnimal.global.GetGround().IsValidPosition(desiredPoint))
            {
                direction = Quaternion.Euler(0, 180, 0) * direction;
                direction = Vector3.Normalize(direction);
                desiredPoint = _owner.transform.position + distance * direction;
            }

            _referencePoint = desiredPoint;
        }

        protected override NodeStatus Execute()
        {
            if (!_ownerAnimal.MoveToPosition(_referencePoint))
            {
                return NodeStatus.FAILURE;
            }

            if (_ownerAnimal.IsNearPosition(_referencePoint, _wanderEpsilon))
            {
                return NodeStatus.SUCCESS;                
            }

            return NodeStatus.RUNNING;
        }
    }
}

