using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim
{

    // This enum is used to quickly identify animals for various behavior tree functions.
    // It is quicker than getting the component or comparing strings.
    public enum AnimalType {
        SQUIRREL = 0,
        FOX = 1
    }

    public class Animal : MonoBehaviour
    {
        protected AnimalType _type;

        protected BehaviorTree.Tree _behaviorTree;
        protected GameObject _targetObject;

        public Global global;

        protected float _food;
        protected float _water;
        protected float _health;

        public readonly float maxFood; // Not const, different species have diff. stomach capacities
        public readonly float maxWater;
        public readonly float maxHealth;

        private float _healthDecayTimer;
        private const float _healthDecayRate = 10.0f;
        private const float _healthDecayAmount = 5.0f;

        private float _foodDecayTimer;
        private const float _foodDecayRate = 7.0f;
        private const float _foodDecayAmount = 5.0f;

        protected float _speed;
        protected float _angularSpeed;
        protected Vector3 _direction;

        protected float _sightFOV;
        protected float _sightRadius;

        public Animal(float maxFoodValue, float maxWaterValue, float maxHealthValue)
        {
            maxFood = maxFoodValue;
            maxWater = maxWaterValue;
            maxHealth = maxHealthValue;
        }

        protected void Update()
        {
            HandleStatDecay();

            if (_health == 0)
            {
                Die();
            }
            else {
                _behaviorTree.Tick();
            }
        }

        public float GetHealth() {
            return _health;
        }

        public float GetFood()
        {
            return _food;
        }

        public float GetSightFOV() {
            return _sightFOV;
        }

        public float GetSightRadius() {
            return _sightRadius;
        }

        public Vector3 GetDirection() {
            return _direction;
        }

        public GameObject GetTargetObject() {
            return _targetObject;
        }

        public void SetTargetObject(GameObject obj) {
            _targetObject = obj;
        }
        private void HandleStatDecay()
        {
            _foodDecayTimer += Time.deltaTime;
            if (_foodDecayTimer > _foodDecayRate)
            {
                _foodDecayTimer = 0;
                _food = Mathf.Max(_food - _foodDecayAmount, 0.0f);
            }

            if (_food == 0 || _water == 0)
            {
                _healthDecayTimer += Time.deltaTime;
                if (_healthDecayTimer > _healthDecayRate)
                {
                    _healthDecayTimer = 0;
                    _health = Mathf.Max(_health - _healthDecayAmount, 0.0f);
                }
            }
            else
            {
                _healthDecayTimer = 0;
            }
        }

        public bool IsNearPosition(Vector3 position, float epsilon) {
            return Vector3.Distance(gameObject.transform.position, position) <= epsilon;
        }

        // TODO: add pathfinding algorithm so it avoids other creatures
        // Attempt to move this animal towards the given position.
        // If the animal cannot successfully move, return false.
        // In the future use Quaternion.RotateTo to smootly lerp direction changes
        public bool MoveToPosition(Vector3 position)
        {
            _direction = position - gameObject.transform.position;
            Vector3 desiredPosition = Vector3.MoveTowards(gameObject.transform.position, position, _speed * Time.deltaTime);
            if (global.GetGround().IsValidPosition(desiredPosition)) {
                gameObject.transform.position = desiredPosition;
                return true;
            }

            return false;
        }

        public void Eat(GameObject foodObject) {
            Food targetFood = foodObject.GetComponent<Food>();
            _food = Mathf.Min(_food + targetFood.FoodValue, maxFood);
            Destroy(foodObject);
            _targetObject = null;
        }

        /*protected void RunFromPosition() {
        
        }*/

        public void Deselect() {
            _behaviorTree.Selected = false;
        }

        // TODO: add other debug things, such as FOV or direction visualizer.
        public void Select() {
            _behaviorTree.Selected = true;
        }

        // When an animal dies, it leaves a corpse for predators to eat.
        public void Die() {
            // TODO: disable self so it doesn't have collision,
            // then instantiate corpse prefab.
            Destroy(gameObject);
        }

        // Simply destroys the game object
        public void Delete() {
            Destroy(gameObject);
        }
    }

}
