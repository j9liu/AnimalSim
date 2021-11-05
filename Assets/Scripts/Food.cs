using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim {
    public enum FoodType {
        ACORN = 0
    }

    public class Food : MonoBehaviour
    {
        [SerializeField]
        private FoodType _type;
        public readonly float FoodValue;

        private float _consumeRate;
        private float _consuemAmount;

        private GameObject _consumer;

        public void SetConsumer(GameObject consumer) {
            _consumer = consumer;
        }
    }
}