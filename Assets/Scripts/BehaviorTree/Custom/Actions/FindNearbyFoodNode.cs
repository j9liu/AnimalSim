using UnityEngine;

namespace BehaviorSim.BehaviorTree {
    public class FindNearbyFoodNode : AnimalActionNode
    {
        public FindNearbyFoodNode() : base("Find Nearby Food")
        {
        }

        /*
         * This is essentially a condition node with extra steps. If a food object is found,
         * the tree ensures it remains in the Animal's memory for the next step in the tree.
         */
        protected override NodeStatus Execute()
        {
            Food targetFood = _ownerAnimal.GetClosestFood(_ownerAnimal.Stats.SightRadius);

            if (targetFood != null)
            {
                _ownerAnimal.SetTargetFood(targetFood);
                targetFood.SetOwner(_ownerAnimal);
                if (_ownerAnimal.Selected) {
                    targetFood.SetHighlighted(true);
                }

                return NodeStatus.SUCCESS;
            }

            return NodeStatus.FAILURE;
        }

    }
}

