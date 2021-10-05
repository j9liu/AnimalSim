﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree {
    public class SequenceNode : Node
    {
        private Node _activeChild;
        private int  _activeChildIndex;

        public SequenceNode() : base() {
            _isLeaf = false;
            _children = new List<Node>();
            _activeChild = null;
            _activeChildIndex = -1;
        }

        public override NodeStatus tick()
        {
            int childrenCount = _children.Count;
            if (childrenCount > 0) {
                if (_activeChildIndex < 0)
                {
                    _activeChildIndex = 0;
                }

                while (_activeChildIndex < childrenCount) {
                    _activeChild = _children[_activeChildIndex];
                    NodeStatus result = _activeChild.tick();
                    switch (result) {
                        case NodeStatus.RUNNING:
                            return result;
                        case NodeStatus.SUCCESS:
                            _activeChildIndex++;
                            break;
                        case NodeStatus.FAILURE:
                            _activeChild = null;
                            _activeChildIndex = -1;
                            return result;

                    }
                }
            }

            return NodeStatus.SUCCESS;
        }
    }
}
