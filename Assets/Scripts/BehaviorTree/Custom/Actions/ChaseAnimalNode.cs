using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public class ChaseAnimalNode : AnimalActionNode
    {

        private float _targetEpsilon;

        public ChaseAnimalNode() : base("Chase Animal")
        {
        }

        protected override void Enter()
        {
            _targetEpsilon = 8.0f;
            _targetObject = _ownerAnimal.GetTargetAnimal().gameObject;
        }

        protected override NodeStatus Execute()
        {
            Vector3 targetPosition = _targetObject.transform.position;
            if (!_ownerAnimal.MoveToPosition(targetPosition, _ownerAnimal.Stats.MaxSpeed))
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
