using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree {
    public class AnimalActionNode : ActionNode{

        protected Animal     _ownerAnimal;
        protected Vector3    _referencePoint;
        protected GameObject _targetObject;

        public AnimalActionNode(string name) : base(name)
        {
        }


        public override void SetOwner(GameObject owner)
        {
            base.SetOwner(owner);
            _ownerAnimal = owner.GetComponent<Animal>();
        }
    }

}

