using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public class SneakToAnimalNode : AnimalActionNode
    {
        private float _successRadius;
        private float _sneakRadius;
        private float _failRadius;

        public SneakToAnimalNode() : base("Sneak to Animal")
        {
        }

        protected override void Enter()
        {
            float targetHearingRadius = _ownerAnimal.GetTargetAnimal().Stats.HearingRadius;
            _targetObject = _ownerAnimal.GetTargetAnimal().gameObject;
            _sneakRadius = targetHearingRadius;
            _successRadius = targetHearingRadius * 0.65f;
            _failRadius = Mathf.Max(targetHearingRadius, _ownerAnimal.Stats.SightRadius);
        }

        protected override NodeStatus Execute()
        {
            Vector3 targetPosition = _targetObject.transform.position;
            if (!_ownerAnimal.IsNearPosition2D(targetPosition, _failRadius)) {
                return NodeStatus.FAILURE;
            }

            if (!_ownerAnimal.MoveToPosition(targetPosition, _ownerAnimal.Stats.MaxSpeed))
            {
                return NodeStatus.FAILURE;
            }

            if (_ownerAnimal.IsNearPosition2D(targetPosition, _successRadius))
            {
                return NodeStatus.SUCCESS;
            }

            return NodeStatus.RUNNING;
        }
    }
}
