using UnityEngine;

namespace BehaviorSim.BehaviorTree {
    public class TickUntilConditionNode : AnimalDecoratorNode
    {
        private bool _entered;
        private bool _mustTickOnce;
        private bool _tickedOnce;
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
                                      NodeStatus haltReturnStatus,
                                      bool mustTickOnce,
                                      Node child) : base(name, condition, child)
        {
            _conditionHaltStatus = conditionHaltStatus;
            _haltReturnStatus = haltReturnStatus;
            _entered = false;
            _mustTickOnce = mustTickOnce;
            _tickedOnce = false;
        }

        public override NodeStatus Tick()
        {
            if (!_entered)
            {
                Enter();
                _entered = true;
            }

            NodeStatus conditionStatus = _condition.Tick();
            if (conditionStatus == _conditionHaltStatus)
            {
                Exit();
                _entered = false;
                if (_mustTickOnce && !_tickedOnce)
                {
                    SetStatus(NodeStatus.FAILURE);
                    return NodeStatus.FAILURE;
                }
                else {
                    SetStatus(_haltReturnStatus);
                    return _haltReturnStatus;
                }
            }

            NodeStatus childStatus = _child.Tick();
            _tickedOnce = true;
            if (childStatus != NodeStatus.RUNNING)
            {
                Exit();
                _tickedOnce = false;
                _entered = false;
            }

            SetStatus(childStatus);
            return childStatus;
        }
    }
}
