using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim.BehaviorTree {
    public class FindNearbyFoodNode : AnimalActionNode
    {
        private const int _foodLayerMask = 1 << 9;
        public FindNearbyFoodNode() : base("Find Nearby Food")
        {     
        }

        /*
         * This is essentially a condition node with extra steps. If a food object is found,
         * the tree ensures it remains in the Animal's memory for the next step in the tree.
         */
        protected override NodeStatus Execute()
        {
            float radius = _ownerAnimal.GetSightRadius();
            float halfFOV    = _ownerAnimal.GetSightFOV() / 2.0f;
            float minDistance = 2.0f * radius;
            GameObject targetFood = null;
            Collider[] foodColliders = Physics.OverlapSphere(_owner.transform.position, radius, _foodLayerMask);
            foreach (Collider collider in foodColliders) {
                Vector3 foodPosition = collider.gameObject.transform.position;
                Vector3 foodDirection = foodPosition - _owner.transform.position;
                float foodAngle = Mathf.Abs(Vector3.Angle(_ownerAnimal.GetDirection(), foodDirection));
                if (foodAngle <= halfFOV)
                {
                    float foodDistance = Vector3.Distance(foodPosition, _owner.transform.position);
                    if (foodDistance < minDistance) {
                        minDistance = foodDistance;
                        targetFood = collider.gameObject;
                    }
                }
            }

            if (targetFood != null) {
                _ownerAnimal.SetTargetObject(targetFood);
                return NodeStatus.SUCCESS;
            }

            return NodeStatus.FAILURE;
        }

    }
}

