using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public abstract class ActionNode : Node
    {
        public ActionNode(string name) : base(name, NodeType.LEAF)
        {
        }

        private bool _running = false;

        protected virtual void Enter() { }
        
        protected virtual void Exit() { }

        protected abstract NodeStatus Execute();

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

            SetStatus(result);
            
            return result;
        }
    }
}
