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
            
            Animal predator = _ownerAnimal.GetClosestPredator(_ownerAnimal.Stats.HearingRadius);
            if (predator != null) {
                _ownerAnimal.SetTargetAnimal(predator);
                return true;
            }

            return false;
        }
    }
}
