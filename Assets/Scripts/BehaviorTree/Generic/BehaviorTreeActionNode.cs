using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public class ActionNode : Node
    {
        public ActionNode(string name) : base(name, true) {
            _children = null;
        }

        private bool _running = false;
        protected virtual void Enter() { }
        protected virtual void Exit() { }

        protected virtual NodeStatus Execute() {
            return NodeStatus.SUCCESS;
        }

        public override NodeStatus Tick() {
            NodeStatus result = NodeStatus.SUCCESS;
            if (!_running)
            {
                Enter();
                _running = true;
            }
            if (_running) {
                result = Execute();
                if (result != NodeStatus.RUNNING) {
                    Exit();
                    _running = false;
                }
            }

            return result;
        }
    }
}
