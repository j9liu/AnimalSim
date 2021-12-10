using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public class RunFromAnimalNode : AnimalActionNode
    {
        public RunFromAnimalNode() : base("Run from Animal")
        {
        }

        protected override NodeStatus Execute()
        {
            _ownerAnimal.RunFromTargetAnimal();
            
            return NodeStatus.SUCCESS;
        }

        protected override void Exit()
        {
            _ownerAnimal.ResetTarget();
        }
    }

}
