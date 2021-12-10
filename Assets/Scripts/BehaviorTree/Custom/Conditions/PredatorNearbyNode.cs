using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree {
    public class PredatorNearbyNode : AnimalConditionNode
    {
        public PredatorNearbyNode() : base("Predator Nearby")
        {
        }
        protected override bool Condition()
        {
            if (_ownerAnimal.TargetAnimalIsPredator()) {
                return _ownerAnimal.IsNearTargetObject2D(1.5f * _ownerAnimal.Stats.HearingRadius);
            }

            Animal predator = _ownerAnimal.GetClosestAnimal(AnimalTypeFilter.PREDATOR_ONLY);
            if (predator != null) {
                _ownerAnimal.SetTargetAnimal(predator);
                return true;
            }

            return false;
        }
    }
}
