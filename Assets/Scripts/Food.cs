using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim {
    public enum FoodType: int {
        SQUIRREL = 0,
        ACORN = 1,
    }

    public class Food : MonoBehaviour
    {
        [SerializeField]
        private FoodType _type;
        public FoodType Type
        {
            get {
                return _type;
            }
        }

        [SerializeField]
        private float _currentFoodValue;
        public float CurrentFoodValue
        {
            get
            {
                return _currentFoodValue;
            }
        }

        [SerializeField]
        private float _portionConsumeRate;
        public float PortionConsumeRate
        {
            get
            {
                return _portionConsumeRate;
            }
        }

        [SerializeField]
        private float _portionAmount;

        private Animal _owner;

        private Outline _outline;

        public void Start()
        {
            if (Type == FoodType.ACORN) {
                _outline = gameObject.AddComponent<Outline>();

                _outline.OutlineMode = Outline.Mode.OutlineAll;
                _outline.OutlineColor = Color.yellow;
                _outline.OutlineWidth = 2.0f;
                _outline.enabled = false;
            }
        }

        public float GetPortion()
        {
            float eatenAmount = Mathf.Min(_portionAmount, _currentFoodValue);
            _currentFoodValue -= eatenAmount;
            if (_currentFoodValue == 0)
            {
                _owner.ResetTarget();
                Destroy(gameObject);
            }

            return _portionAmount;
        }

        public bool HasOwner()
        {
            return _owner != null;
        }

        public void ResetOwner()
        {
            if (_owner.Selected && _outline != null)
            {
                _outline.enabled = false;
            }
            _owner = null;
        }

        public bool SetOwner(Animal animal)
        {
            if (_owner != null)
            {
                return false;
            }

            _owner = animal;
            return true;
        }

        public void SetHighlighted(bool value)
        {
            if (_outline != null)
            {
                _outline.enabled = value;
            }
        }

    }
}