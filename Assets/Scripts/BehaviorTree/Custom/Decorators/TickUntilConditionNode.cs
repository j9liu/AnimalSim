using UnityEngine;

namespace BehaviorSim.BehaviorTree {
    public class TickUntilConditionNode : AnimalDecoratorNode
    {
        private bool entered;
        protected ConditionNode _condition;
        protected NodeStatus _conditionHaltStatus;
        protected NodeStatus _haltReturnStatus;

        /***************
         * A decorator node that ticks until either the condition is met and stops ticking the child,
         * or until the child returns a terminating status (SUCCESS, FAILURE, ERROR, or HALT).
         * If the condition is met, returns the status specified to be returned.
         ***************/
        public TickUntilConditionNode(string name,
                                      ConditionNode condition,
                                      NodeStatus conditionHaltStatus,
                                      NodeStatus haltReturnStatus, Node child) : base(name, child)
        {
            _condition = condition;
            _conditionHaltStatus = conditionHaltStatus;
            _haltReturnStatus = haltReturnStatus;
            entered = false;
        }

        public override NodeStatus Tick()
        {
            if (!entered)
            {
                Enter();
                entered = true;
            }

            NodeStatus conditionStatus = _condition.Tick();
            if (conditionStatus == _conditionHaltStatus)
            {
                SetStatus(_haltReturnStatus);
                Exit();
                entered = false;
                return _haltReturnStatus;
            }

            NodeStatus childStatus = _child.Tick();

            if (childStatus != NodeStatus.RUNNING)
            {
                Exit();
                entered = false;
            }

            SetStatus(childStatus);
            return childStatus;
        }
    }
}
