using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public class RepeatUntilPredatorNearbyNode : TickUntilConditionNode
    {
        public RepeatUntilPredatorNearbyNode(Node child) : base("Repeat Until\n Predator Nearby",
                                                                new PredatorNearbyNode(),
                                                                NodeStatus.SUCCESS,
                                                                NodeStatus.HALT,
                                                                child)
        {
        }
        protected override void Exit()
        {
            _ownerAnimal.ResetTarget();
        }
    }

}
