using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public class HungerLowNode : AnimalConditionNode
    {
        public HungerLowNode() : base("Hunger Low?")
        {
        }

        protected override bool Condition()
        {
            Debug.Log("Evaluating hunger...");
            return (_ownerAnimal.GetHunger() / Animal.MaxHunger) <= 0.5f;
        }
    }
}

