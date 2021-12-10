using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorSim.BehaviorTree {
    public class SequenceNode : ControlNode
    {
        private Node _activeChild;
        private int  _activeChildIndex;

        public SequenceNode(string name) : base(name) {
            _activeChild = null;
            _activeChildIndex = -1;
        }

        public override NodeStatus Tick()
        {
            int childrenCount = _children.Count;
            if (childrenCount > 0) {
                if (_activeChildIndex < 0)
                {
                    _activeChildIndex = 0;
                }

                while (_activeChildIndex < childrenCount) {
                    _activeChild = _children[_activeChildIndex];
                    NodeStatus result = _activeChild.Tick();
                    switch (result) {
                        case NodeStatus.RUNNING:
                            SetStatus(result);
                            return result;
                        case NodeStatus.SUCCESS:
                            _activeChildIndex++;
                            break;
                        case NodeStatus.FAILURE:
                        case NodeStatus.HALT:
                            _activeChild = null;
                            _activeChildIndex = -1;
                            SetStatus(result);
                            return result;
                    }
                }
            }

            _activeChild = null;
            _activeChildIndex = -1;
            SetStatus(NodeStatus.SUCCESS);
            return NodeStatus.SUCCESS;
        }
    }
}
