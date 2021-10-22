using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim {
    public enum FoodType {
        ACORN = 0
    }

    public class Food : MonoBehaviour
    {
        public FoodType type;
        public float foodValue;
    }
}