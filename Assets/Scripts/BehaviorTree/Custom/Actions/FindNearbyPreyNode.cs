using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public class IsPreyNearbyNode : AnimalConditionNode
    {
        private const int _animalLayerMask = 1 << 8;

        public IsPreyNearbyNode() : base("Is Prey Nearby?")
        {
        }

        protected override bool Condition()
        {
            Animal targetAnimal = _ownerAnimal.GetClosestAnimal(AnimalTypeFilter.PREY_ONLY);
            if (targetAnimal != null)
            {
                _ownerAnimal.SetTargetAnimal(targetAnimal);
                return true;
            }

            return false;
        }
    }
}

