namespace BehaviorSim.BehaviorTree {
    public class IsFoodNearbyNode : AnimalConditionNode
    {
        public IsFoodNearbyNode() : base("Is Food Nearby")
        {
        }


        protected override bool Condition()
        {
            Food targetFood = _ownerAnimal.GetClosestFood(_ownerAnimal.Stats.SightRadius);

            if (targetFood != null)
            {
                _ownerAnimal.SetTargetFood(targetFood);
                targetFood.SetOwner(_ownerAnimal);
                if (_ownerAnimal.Selected)
                {
                    targetFood.SetHighlighted(true);
                }

                return true;
            }

            return false;
        }

    }
}

