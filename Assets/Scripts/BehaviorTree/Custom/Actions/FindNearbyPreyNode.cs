using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public class FindNearbyPreyNode : AnimalActionNode
    {
        private const int _animalLayerMask = 1 << 8;

        public FindNearbyPreyNode() : base("Find Nearby Prey")
        {
        }

        /*
         * This is essentially a condition node with extra steps. If a food object is found,
         * the tree ensures it remains in the Animal's memory for the next step in the tree.
         */
        protected override NodeStatus Execute()
        {
            Animal targetAnimal = _ownerAnimal.GetClosestAnimal(AnimalTypeFilter.PREY_ONLY);
            
            if (targetAnimal != null)
            {
                _ownerAnimal.SetTargetAnimal(targetAnimal);
                return NodeStatus.SUCCESS;
            }

            return NodeStatus.FAILURE;
        }

    }
}

