using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree {
     public class Tree {
        protected Node _root = null;

        public Tree(Node root) {
            _root = root;
        }

        public void Tick() {
            if (_root != null) {
                _root.Tick();
            }
        }

        public void SetOwner(GameObject owner) {
            if (_root != null) {
                _root.SetOwner(owner);
            }
        }
    }
}