﻿namespace BehaviorSim.BehaviorTree
{
    public class TickUntilPredatorNearbyNode : TickUntilConditionNode
    {
        public TickUntilPredatorNearbyNode(Node child) : base("Tick Until\n Predator Nearby",
                                                                new PredatorNearbyNode(),
                                                                NodeStatus.SUCCESS,
                                                                NodeStatus.HALT,
                                                                false,
                                                                child)
        {
        }
        protected override void Exit()
        {
            _ownerAnimal.ResetTarget();
        }
    }

}
