namespace BehaviorSim.BehaviorTree
{
    public class TickUntilPredatorNotNearbyNode : TickUntilConditionNode
    {
        public TickUntilPredatorNotNearbyNode(Node child) : base("Tick Until\n Predator Not Nearby",
                                                                new PredatorNearbyNode(),
                                                                NodeStatus.FAILURE,
                                                                NodeStatus.HALT,
                                                                true,
                                                                child)
        {
        }
        protected override void Exit()
        {
            _ownerAnimal.ResetTarget();
        }
    }
}
