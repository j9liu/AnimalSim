namespace BehaviorSim.BehaviorTree
{
    public abstract class ConditionNode : Node
    {
        public ConditionNode(string name) : base(name, NodeType.LEAF)
        {
        }

        // Defines what the condition is, i.e. when the condition node should evaluate to true or false
        protected abstract bool Condition();

        public override NodeStatus Tick()
        {
            NodeStatus result = Condition() ? NodeStatus.SUCCESS : NodeStatus.FAILURE;
            SetStatus(result);
            return result;
        }
    }
}