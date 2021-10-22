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
        public static readonly float MaxHunger = 100.0f;
        public static readonly float MaxThirst = 100.0f;
        public static readonly float MaxHealth = 100.0f;

        protected float _speed;
        protected float _angularSpeed;
        protected Vector3 _direction;

        protected float _sightFOV;
        protected float _sightRadius;

        protected float _hunger;
        protected float _thirst;
        protected float _health;

        protected AnimalType _type;
        // If this animal is eaten by another animal, this specifies
        // by how much to fill the predator's hunger bar.
        protected float _foodValue;

        protected GameObject _targetObject;

        protected BehaviorTree.Tree _behaviorTree;

        public Global global;
        
        // Update is called once per frame
        protected void Update()
        {
            _behaviorTree.Tick();
        }

        public float GetHealth() {
            return _health;
        }

        public float GetHunger()
        {
            return _hunger;
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
            _hunger = Mathf.Min(_hunger + targetFood.foodValue, Animal.MaxHunger);
            Destroy(foodObject);
            _targetObject = null;
        }

        /*protected void RunFromPosition() {
        
        }*/
    }

}
