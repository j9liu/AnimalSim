using UnityEngine;

namespace BehaviorSim
{
    // This enum is used to quickly identify animals,
    // as well as index them in the food chain data arrays.
    public enum AnimalType: int{
        SQUIRREL = 0,
        FOX = 1
    }

    public class AnimalStats
    {
        public float MaxFood; // Not const; different species will have different stomach capacities
        public float MaxWater;
        public float MaxHealth;

        public float Speed;
        public float AngularSpeed;
        public float SightFOV;
        public float SightRadius;
        public float CollisionRadius;
    }

    public partial class AnimalFoodChainData
    {
        public bool[] FoodSources;
        public bool[] Predators;
    }

    public abstract class Animal : MonoBehaviour
    {
        public abstract AnimalType Type { get; }
        public abstract AnimalStats Stats { get; }
        public abstract AnimalFoodChainData FoodChainData { get; }

        protected BehaviorTree.Tree _behaviorTree;

        protected GameObject _targetObject;
        protected Food _targetFood;

        public Global global;

        protected float _food;
        public float Food {
            get {
                return _food;
            }
        }

        protected float _water;
        public float Water {
            get {
                return _water;
            }
        }

        protected float _health;
        public float Health {
            get {
                return _health;
            }
        }

        private float _healthDecayTimer;
        private const float _healthDecayRate = 15.0f;
        private const float _healthDecayAmount = 10.0f;

        private float _healthRegenTimer;
        private const float _healthRegenRate = 20.0f;
        private const float _healthRegenAmount = 5.0f;

        private float _foodDecayTimer;
        private const float _foodDecayRate = 10.0f;
        private const float _foodDecayAmount = 5.0f;

        private float _eatTimer;

        protected Vector3 _direction;

        protected GameObject _debugArrowObject;

        public Animal()
        {
        }

        protected void Update()
        {
            UpdateStats();

            if (_health == 0)
            {
                Die();
            }
            else {
                _behaviorTree.Tick();
            }
        }

        public Vector3 GetDirection() {
            return _direction;
        }
        public Food GetTargetFood()
        {
            return _targetFood;
        }

        public GameObject GetTargetObject() {
            return _targetObject;
        }

        public void ResetTarget() {
            _targetFood = null;
            _targetObject = null;
        }

        public void SetTargetFood(Food food) {
            _targetFood = food;
            _targetObject = food.gameObject;
        }

        public void SetTargetObject(GameObject obj) {
            _targetObject = obj;
        }

        private void UpdateStats()
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

        public bool IsNearPosition2D(Vector3 position, float epsilon)
        {
            Vector2 xzPosition = new Vector2(transform.position.x, transform.position.z);
            return Vector2.Distance(xzPosition, new Vector2(position.x, position.z)) <= epsilon;
        }

        public bool IsNearPosition(Vector3 position, float epsilon) {
            return Vector3.Distance(transform.position, position) <= epsilon;
        }

        // TODO: add pathfinding algorithm so it avoids other creatures
        // Attempt to move this animal towards the given position.
        // If the animal cannot successfully move, return false.
        public bool MoveToPosition(Vector3 targetPosition)
        {
            Vector3 currentPosition = transform.position;
            Vector3 targetDirection = targetPosition - currentPosition;

            Collider[] colliders = Physics.OverlapSphere(currentPosition, Stats.CollisionRadius, Global.AnimalLayerMask);
            if (colliders.Length > 1)
            {
                targetDirection = Vector3.zero;
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject == this.gameObject) {
                        continue;
                    }
                    targetDirection -= colliders[i].transform.position - currentPosition;
                }
            }

            RotateTowardsDirection(targetDirection);

            Vector3 desiredPosition = currentPosition + (Stats.Speed * Time.deltaTime * _direction);   
            if (!global.GetGround().IsValidPosition(desiredPosition))
            {
                return false;
            }

            transform.position = desiredPosition;
            return true;

        }

        public void RotateTowardsDirection(Vector3 targetDirection) {
            float angle = Vector3.SignedAngle(_direction, Vector3.Normalize(targetDirection), transform.up);
            if (Mathf.Abs(angle) > 0.5f) {
                Quaternion rotation = Quaternion.AngleAxis(angle * Stats.AngularSpeed * Time.deltaTime, transform.up);
                _direction = rotation * _direction;
                transform.rotation = rotation * transform.rotation;
            }
        }

        public void FaceTargetObject() {
            if (_targetObject == null) {
                return;
            }

            Vector3 targetDirection = _targetObject.transform.position - transform.position;
            float angle = Vector3.SignedAngle(_direction, Vector3.Normalize(targetDirection), transform.up);
            Quaternion rotation = Quaternion.AngleAxis(angle, transform.up);
            _direction = rotation * _direction;
            transform.rotation = rotation * transform.rotation;
        }

        /*protected void RunFromTargetObject() {
        
        }*/


        public bool CanEatFood(Food food) {
            int index = (int)food.Type;
            return FoodChainData.FoodSources[index];
        }

        public bool EatTargetFood() {
            if (_targetFood == null) {
                return false;
            }
            
            _eatTimer += Time.deltaTime;
            if (_eatTimer > _targetFood.PortionConsumeRate) {
                _eatTimer = 0.0f;

                _food = Mathf.Min(_food + _targetFood.GetPortion(), Stats.MaxFood);
            }

            return true;
        }

        public bool IsEatenBy(Animal animal) {
            int index = (int)animal.Type;
            return FoodChainData.Predators[index];
        }

        public void Deselect() {
            _behaviorTree.Selected = false;
            _debugArrowObject.SetActive(false);
            if (_targetFood != null) {
                _targetFood.SetHighlighted(false);
            }
        }

        // TODO: add FOV debug view
        public void Select() {
            _behaviorTree.Selected = true;
            _debugArrowObject.SetActive(true);
            if (_targetFood != null) {
                _targetFood.SetHighlighted(true);
            }
        }

        // When an animal dies, it leaves a corpse for predators to eat.
        public void Die() {
            gameObject.SetActive(false);
            global.SpawnCorpse(Type, transform.position);
            Destroy(gameObject);
        }

        // Simply destroys the game object
        public void Delete() {
            Destroy(gameObject);
        }
    }

}
