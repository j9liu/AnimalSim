﻿using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public abstract class AnimalConditionNode : ConditionNode
    {
        protected Animal _ownerAnimal;

        public AnimalConditionNode(string name) : base(name)
        {
        }

        public override void SetOwner(GameObject owner, Tree tree)
        {
            base.SetOwner(owner, tree);
            _ownerAnimal = owner.GetComponent<Animal>();
        }
    }
}

