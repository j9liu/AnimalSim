namespace BehaviorSim.BehaviorTree
{
   public class ExecuteIfConditionNode : AnimalDecoratorNode
    {
        protected bool _conditionChecked;

        /***************
         * A decorator node that executes its child if the condition is met. Only checks
         * the condition once.
         ***************/

        public ExecuteIfConditionNode(string name, ConditionNode condition, Node child) : base(name, condition, child)
        {
            _conditionChecked = false;
        }

        public override NodeStatus Tick()
        {
            if(!_conditionChecked)
            {
                NodeStatus conditionStatus = _condition.Tick();
                if (conditionStatus == NodeStatus.SUCCESS)
                {
                    _conditionChecked = true;
                }
                else {
                    SetStatus(NodeStatus.FAILURE);
                    Exit();
                    return NodeStatus.FAILURE;
                }
            }

            NodeStatus childStatus = _child.Tick();

            if (childStatus != NodeStatus.RUNNING)
            {
                Exit();
                _conditionChecked = false;
            }

            SetStatus(childStatus);
            return childStatus;
        }
    }


}
