namespace BehaviorSim.BehaviorTree {
    public class InverterNode : AnimalDecoratorNode
    {
        public InverterNode(string name, Node child) : base(name, child)
        {
        }

        public override NodeStatus Tick()
        {
            NodeStatus childStatus = _child.Tick();
            NodeStatus result = NodeStatus.UNEXECUTED;
            switch (childStatus) {
                case NodeStatus.RUNNING:
                    result = NodeStatus.RUNNING;
                    break;
                case NodeStatus.FAILURE:
                    result = NodeStatus.SUCCESS;
                    break;
                case NodeStatus.SUCCESS:
                    result = NodeStatus.FAILURE;
                    break;
                default:
                    break;
            }

            SetStatus(result);
            return result;
        }
    }
}
