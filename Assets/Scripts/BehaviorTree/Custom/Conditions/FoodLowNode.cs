using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public class FoodLowNode : AnimalConditionNode
    {
        private float _threshold;
        public FoodLowNode(float threshold) : base("Food Low?")
        {
            _threshold = threshold;
        }

        protected override bool Condition()
        {
            return (_ownerAnimal.Food / _ownerAnimal.Stats.MaxFood) <= _threshold;
        }
    }
}

