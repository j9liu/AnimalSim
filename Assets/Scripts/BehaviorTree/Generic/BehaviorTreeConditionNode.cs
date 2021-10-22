using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public class ConditionNode : Node
    {
        public ConditionNode(string name) : base(name, true) {
            _children = null;
        }

        // Defines what the condition is, i.e. when the condition node should evaluate to true or false
        protected virtual bool Condition() {
            return true;
        }

        public override NodeStatus Tick()
        {
            return Condition() ? NodeStatus.SUCCESS : NodeStatus.FAILURE;
        }
    }

}