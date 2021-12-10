using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public class AttackAnimalNode : AnimalActionNode
    {
        private const float _attackEpsilon = 8.2f;

        public AttackAnimalNode() : base("Attack Animal")
        {

        }

        protected override NodeStatus Execute()
        {
            if (!_ownerAnimal.IsNearTargetObject2D(_attackEpsilon))
            {
                return NodeStatus.FAILURE;
            }

            Animal targetAnimal = _ownerAnimal.GetTargetAnimal();
            Food targetCorpse = targetAnimal.Die();
            _ownerAnimal.SetTargetFood(targetCorpse);
            targetCorpse.SetOwner(_ownerAnimal);
            
            return NodeStatus.SUCCESS;
        }
    }
}

