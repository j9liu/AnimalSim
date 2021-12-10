using UnityEngine;

namespace BehaviorSim
{
    // This enum is used to quickly identify animals,
    // as well as index them in the food chain data arrays.
    public enum AnimalType: int
    {
        SQUIRREL = 0,
        FOX = 1
    }

    public class AnimalStats
    {
        public float MaxFood; // Not const; different species will have different stomach capacities
        public float MaxWater;
        public float MaxHealth;

        public float MaxSpeed;
        public float AngularSpeed;
        public float SightFOV;
        public float SightRadius;
        public float HearingRadius;
        public float CollisionRadius;
    }

    public partial class AnimalFoodChainData
    {
        public bool[] FoodSources;
        public bool[] Predators;
    }

    public enum AnimalFilter: int {
        SAME_SPECIES = 0,
        PREY_ONLY = 1,
        PREDATOR_ONLY = 2,
        DIFFERENT_SPECIES = 3,
        ALL = 4
    }

    public abstract class Animal : MonoBehaviour
    {
        public abstract AnimalType Type { get; }
        public abstract AnimalStats Stats { get; }
        public abstract AnimalFoodChainData FoodChainData { get; }

        protected BehaviorTree.Tree _behaviorTree;

        protected GameObject _targetObject;
        protected Food _targetFood;
        protected Animal _targetAnimal;
        protected bool _targetIsPredator = false;

        public Global global;

        protected float _food;
        public float Food
        {
            get {
                return _food;
            }
        }

        protected float _water;
        public float Water
        {
            get {
                return _water;
            }
        }

        protected float _health;
        public float Health
        {
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
        private const float _foodDecayAmount = 10.0f;

        private float _eatTimer;
        protected enum AnimalBehaviorStatus : int
        {
            EATING = 0,
            DRINKING = 1,
            IDLE = 2,
            MOVING = 3
        }

        protected AnimalBehaviorStatus _behaviorStatus = AnimalBehaviorStatus.IDLE;

        protected float _currentSpeed = 0.0f;
        public float CurrentSpeed
        {
            get
            {
                return _currentSpeed;
            }

            set
            {
                _currentSpeed = Mathf.Min(Mathf.Max(value, 0.0f), Stats.MaxSpeed);
            }
        }

        protected Vector3 _direction;
        public Vector3 Direction
        {
            get {
                return _direction;
            }
        }

        protected GameObject _debugArrowObject;

        public bool _selected = false;
        public bool Selected
        {
            get {
                return _selected;
            }
        }

        public Animal()
        {
        }

        protected void Update()
        {
            if (_health == 0)
            {
                Die();
            }
            else {
                _behaviorTree.Tick();
            }

            UpdateStats();
        }

        public float GetFoodPercentage()
        {
            return _food / Stats.MaxFood;
        }

        public float GetHealthPercentage()
        {
            return _health / Stats.MaxHealth;
        }

        public float GetWaterPercentage()
        {
            return _water / Stats.MaxWater;
        }

        public Animal GetTargetAnimal() {
            return _targetAnimal;
        }

        public Food GetTargetFood()
        {
            return _targetFood;
        }

        public GameObject GetTargetObject()
        {
            return _targetObject;
        }

        public void ResetTarget()
        {
            _targetFood.ResetOwner();
            _targetFood = null;
            _targetObject = null;
        }

        public void SetTargetAnimal(Animal animal) {
            _targetAnimal = animal;
            _targetObject = animal.gameObject;
        }

        public void SetTargetFood(Food food)
        {
            _targetFood = food;
            _targetObject = food.gameObject;
        }

        public void SetTargetObject(GameObject obj)
        {
            _targetObject = obj;
        }

        private void UpdateStats()
        {
            // Map the animal's current speed to a modifier between 0.5 and 2.0
            float scalar = (1.5f * _currentSpeed / Stats.MaxSpeed) + 0.5f;

            if (_behaviorStatus != AnimalBehaviorStatus.EATING)
            {
                _foodDecayTimer += scalar * Time.deltaTime;
                if (_foodDecayTimer > _foodDecayRate)
                {
                    _foodDecayTimer = 0;
                    _food = Mathf.Max(_food - _foodDecayAmount, 0.0f);
                }
            }
            else {
                _foodDecayTimer = 0;
            }

            if (_behaviorStatus != AnimalBehaviorStatus.DRINKING) {
                // TODO: implement water decay
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
            else if (GetFoodPercentage() > 0.5f && GetWaterPercentage() > 0.6f)
            {
                _healthRegenTimer += Time.deltaTime;
                if (_healthRegenTimer > _healthRegenRate)
                {
                    _healthRegenTimer = 0;
                    _health = Mathf.Min(_health + _healthRegenAmount, Stats.MaxHealth);
                }
            }
            else
            {
                _healthDecayTimer = 0;
                _healthRegenTimer = 0;
            }
        }

        public bool IsNearPosition2D(Vector3 position, float epsilon)
        {
            Vector2 xzPosition = new Vector2(transform.position.x, transform.position.z);
            return Vector2.Distance(xzPosition, new Vector2(position.x, position.z)) <= epsilon;
        }

        public bool IsNearPosition(Vector3 position, float epsilon)
        {
            return Vector3.Distance(transform.position, position) <= epsilon;
        }

        public bool IsNearTargetObject2D(float epsilon)
        {
            return IsNearPosition2D(_targetObject.transform.position, epsilon);
        }

        public bool IsNearTargetObject(float epsilon)
        {
            return IsNearPosition(_targetObject.transform.position, epsilon);
        }

        // TODO: add pathfinding algorithm so it avoids environmental obstacles
        // Attempt to move this animal towards the given position.
        // If the animal cannot successfully move, return false.
        // ALSO TODO: make radius bigger, perceive predators from a farther distance
        public bool MoveToPosition(Vector3 targetPosition, float speed)
        {
            _behaviorStatus = AnimalBehaviorStatus.MOVING;
            Vector3 currentPosition = transform.position;
            Vector3 targetDirection = targetPosition - currentPosition;
            float collisionRadius = Stats.CollisionRadius;

            Collider[] colliders = Physics.OverlapSphere(currentPosition, collisionRadius, Global.AnimalLayerMask);
            if (colliders.Length > 1)
            {
                targetDirection = Vector3.zero;
                for (int i = 0; i < colliders.Length; i++)
                {
                    GameObject temporaryObject = colliders[i].gameObject;
                    if (temporaryObject == gameObject || temporaryObject == _targetObject) {
                        continue;
                    }

                    Animal neighborAnimal = colliders[i].gameObject.GetComponent<Animal>();
                    if (!CanEatAnimal(neighborAnimal)) {
                        continue;
                    }

                    Vector3 direction = colliders[i].transform.position - currentPosition;
                    float distance = direction.magnitude;
                    targetDirection -= (distance - collisionRadius) / collisionRadius * direction;
                }
            }

            RotateTowardsDirection(targetDirection);

            if (speed < _currentSpeed)
            {

                CurrentSpeed = Mathf.SmoothStep(speed, _currentSpeed, speed * Time.deltaTime);
            }
            else {
                CurrentSpeed = Mathf.SmoothStep(_currentSpeed, speed, speed * Time.deltaTime);
            }

            Vector3 desiredPosition = currentPosition + (_currentSpeed * Time.deltaTime * _direction);   
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
                Quaternion rotation = Quaternion.AngleAxis(angle * (Stats.AngularSpeed + _currentSpeed) * Time.deltaTime, transform.up);
                _direction = rotation * _direction;
                transform.rotation = rotation * transform.rotation;
            }
        }

        public void FaceTargetObject()
        {
            if (_targetObject == null)
            {
                return;
            }

            Vector3 targetDirection = _targetObject.transform.position - transform.position;
            float angle = Vector3.SignedAngle(_direction, Vector3.Normalize(targetDirection), transform.up);
            Quaternion rotation = Quaternion.AngleAxis(angle, transform.up);
            _direction = rotation * _direction;
            transform.rotation = rotation * transform.rotation;
        }


        // TODO: implement smarter running away behavior
        public void RunFromTargetAnimal()
        {
            _behaviorStatus = AnimalBehaviorStatus.MOVING;

            Vector3 targetDirection = transform.position - _targetAnimal.transform.position;

            Vector3 currentPosition = transform.position;

            RotateTowardsDirection(targetDirection);

            CurrentSpeed = Mathf.SmoothStep(_currentSpeed, Stats.MaxSpeed, Stats.MaxSpeed * Time.deltaTime);

            Vector3 desiredPosition = currentPosition + (_currentSpeed * Time.deltaTime * _direction);

            if (global.GetGround().IsValidPosition(desiredPosition))
            {
                transform.position = desiredPosition;
            }
        }

        public bool CanEatAnimal(Animal animal)
        {
            int index = (int)animal.Type;
            return FoodChainData.FoodSources[index];
        }

        public bool CanEatFood(Food food)
        {
            int index = (int)food.Type;
            return FoodChainData.FoodSources[index];
        }


        public Food GetClosestFood(float radius)
        {
            float halfFOV = Stats.SightFOV / 2.0f;
            float minDistance = radius;

            Food targetFood = null;

            Collider[] foodColliders = Physics.OverlapSphere(transform.position, radius, Global.FoodLayerMask);
            foreach (Collider collider in foodColliders)
            {
                Vector3 foodPosition = collider.transform.position;
                Vector3 foodDirection = foodPosition - transform.position;
                float foodAngle = Mathf.Abs(Vector3.Angle(_direction, foodDirection));
                float foodDistance = Vector3.Distance(foodPosition, transform.position);
                if (foodAngle <= halfFOV && foodDistance < minDistance)
                {
                    Food tempFood = collider.gameObject.GetComponent<Food>();
                    if (CanEatFood(tempFood) && !tempFood.HasOwner())
                    {
                        minDistance = foodDistance;
                        targetFood = tempFood;
                    }
                }
            }

            return targetFood;
        }

        /*public Animal GetClosestAnimal(float radius, bool useSight, bool useHearing, AnimalFilter filter)
        {
            Animal targetAnimal = null;
        }*/

        public Animal GetClosestPrey(float radius)
        {
            float halfFOV = Stats.SightFOV / 2.0f;
            float minDistance = radius;

            Animal targetAnimal = null;

            Collider[] animalColliders = Physics.OverlapSphere(transform.position, radius, Global.AnimalLayerMask);
            foreach (Collider collider in animalColliders)
            {
                Vector3 animalPosition = collider.transform.position;
                Vector3 animalDirection = animalPosition - transform.position;
                float animalAngle = Mathf.Abs(Vector3.Angle(_direction, animalDirection));
                float animalDistance = Vector3.Distance(animalPosition, transform.position);
                if (animalAngle <= halfFOV && animalDistance <= minDistance)
                {
                    Animal tempAnimal = collider.gameObject.GetComponent<Animal>();
                    if (CanEatAnimal(tempAnimal))
                    {
                        minDistance = animalDistance;
                        targetAnimal = tempAnimal;
                    }
                }
            }

            return targetAnimal;
        }

        public Animal GetClosestPredator(float radius)
        {
            float minDistance = radius;
            Animal closestAnimal = null;

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, Global.AnimalLayerMask);
            if (colliders.Length > 1)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    GameObject temporaryObject = colliders[i].gameObject;
                    if (temporaryObject == this.gameObject || temporaryObject == _targetObject)
                    {
                        continue;
                    }

                    float distance = Vector3.Distance(colliders[i].transform.position, transform.position);
                    if (distance < minDistance)
                    {
                        Animal animal = temporaryObject.GetComponent<Animal>();
                        if (!IsEatenBy(animal))
                        {
                            continue;
                        }

                        minDistance = distance;
                        closestAnimal = animal;
                    }
                }
            }

            return closestAnimal;
        }

        public bool EatTargetFood()
        {
            _behaviorStatus = AnimalBehaviorStatus.EATING;
            CurrentSpeed = 0;
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

        public bool IsEatenBy(Animal animal)
        {
            int index = (int)animal.Type;
            return FoodChainData.Predators[index];
        }

        public void Deselect()
        {
            _selected = false;
            _behaviorTree.Selected = false;
            _debugArrowObject.SetActive(false);
            if (_targetFood != null) {
                _targetFood.SetHighlighted(false);
            }
        }

        // TODO: add FOV debug view
        public void Select()
        {
            _selected = true;
            _behaviorTree.Selected = true;
            _debugArrowObject.SetActive(true);
            if (_targetFood != null) {
                _targetFood.SetHighlighted(true);
            }
        }

        // When an animal dies, it leaves a corpse for predators to eat.
        public Food Die()
        {
            if (_targetFood != null)
            {
                _targetFood.ResetOwner();
            }

            Destroy(gameObject);

            GameObject corpseObject = global.SpawnCorpse(Type, transform.position);
            return corpseObject.GetComponent<Food>();
        }

        // Simply destroys the game object
        public void Delete()
        {
            Destroy(gameObject);
        }
    }

}
