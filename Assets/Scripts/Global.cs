using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BehaviorSim
{
    public class GlobalParameters {
        public int InitialSquirrelPopulation = 6;
        public int InitialFoxPopulation = 1;
        public int InitialAcornAmount = 7;
        public float AcornSpawnRate = 5.0f; // The rate at which acorns spawn, in seconds
    }

    public class Global : MonoBehaviour
    {
        [SerializeField]
        private GameObject _acorn;
        [SerializeField]
        private GameObject _squirrel;
        [SerializeField]
        private GameObject _fox;
        [SerializeField]
        private GameObject _squirrelCorpse;
        [SerializeField]
        private GameObject _foxCorpse;

        private Ground _ground;
        private UIManager _uiManager;
        private GameObject _animalsObject;
        private GameObject _foodObject;

        private GlobalParameters _parameters;

        private EventSystem _eventSystem;

        private List<GameObject> _food;
        private List<Animal> _animals;

        private float _acornSpawnTimer;

        public const int UILayer = 5;
        public const int AnimalLayerMask = 1 << 8;
        public const int FoodLayerMask = 1 << 9;

        // Start is called before the first frame update
        public void Start()
        {
            GameObject groundObject = GameObject.Find("Ground");
            _ground = groundObject.GetComponent<Ground>();

            GameObject canvasObject = GameObject.Find("Canvas");
            _uiManager = canvasObject.GetComponent<UIManager>();

            _animalsObject = GameObject.Find("Animals");
            _foodObject = GameObject.Find("Food");

            GameObject eventSystemObject = GameObject.Find("EventSystem");
            _eventSystem = eventSystemObject.GetComponent<EventSystem>();

            // Set up timers
            _acornSpawnTimer = 0.0f;
            _parameters = new GlobalParameters();

            _animals = new List<Animal>();
            _food = new List<GameObject>();

            // Populate the world with animals
            SpawnAnimals();
            SpawnFood();

            _uiManager.SetInitialAnimalPopulation(AnimalType.SQUIRREL, _parameters.InitialSquirrelPopulation);
            _uiManager.SetInitialAnimalPopulation(AnimalType.FOX, _parameters.InitialFoxPopulation);
        }

        // Update is called once per frame
        public void Update()
        {
            HandleMouseInput();
            RegenerateFood();
        }

        public Ground GetGround()
        {
            return _ground;
        }

        private void HandleMouseInput() {
            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 1000.0f, AnimalLayerMask))
                    {
                        _uiManager.Deselect();
                        Animal selectedAnimal = hit.collider.gameObject.GetComponent<Animal>();
                        _uiManager.SelectAnimal(selectedAnimal);
                    }
                    else
                    {
                        GameObject clickedObject = _eventSystem.currentSelectedGameObject;
                        if (clickedObject == null || clickedObject.GetComponent<Ground>() != null)
                        {
                            _uiManager.Deselect();
                        }
                    }
                }
            }
        }

        private void RegenerateFood()
        {
            _acornSpawnTimer += Time.deltaTime;
            if (_acornSpawnTimer >= _parameters.AcornSpawnRate)
            {
                Vector3 spawnPosition = _ground.GetRandomPositionForObject(_acorn);
                GameObject currentAcorn = Instantiate(_acorn, spawnPosition, Quaternion.identity, _foodObject.transform);
                _food.Add(currentAcorn);
                _acornSpawnTimer = 0.0f;
            }
        }

        private void SpawnFood()
        {
            for (int i = 0; i < _parameters.InitialAcornAmount; i++)
            {
                Vector3 spawnPosition = _ground.GetRandomPositionForObject(_acorn);
                GameObject food = Instantiate(_acorn, spawnPosition, Quaternion.identity, _foodObject.transform);
                _food.Add(food);
            }
        }

        public GameObject SpawnCorpse(AnimalType type, Vector3 spawnPosition) {
            GameObject corpse = null;
            switch (type) {
                case AnimalType.SQUIRREL:
                    corpse = Instantiate(_squirrelCorpse, spawnPosition, Quaternion.identity, _foodObject.transform);
                    break;
                case AnimalType.FOX:
                    corpse = Instantiate(_foxCorpse, spawnPosition, Quaternion.identity, _foodObject.transform);
                    break;
                default:
                    return null;
            }
            _food.Add(corpse);
            return corpse;
        }

        public void SpawnAnimals() {

            for (int i = 0; i < _parameters.InitialSquirrelPopulation; i++) {
                Vector3 spawnPosition = _ground.GetRandomPositionForObject(_squirrel);
                float spawnAngle = Random.Range(-180, 180);
                Quaternion spawnRotation = Quaternion.AngleAxis(spawnAngle, _squirrel.transform.up);
                GameObject currentAnimal = Instantiate(_squirrel, spawnPosition, spawnRotation, _animalsObject.transform);
                Squirrel currentComponent = currentAnimal.GetComponent<Squirrel>();
                currentComponent.global = this;
                _animals.Add(currentComponent);
            }

            for (int i = 0; i < _parameters.InitialFoxPopulation; i++)
            {
                Vector3 spawnPosition = _ground.GetRandomPositionForObject(_fox);
                float spawnAngle = Random.Range(-180, 180);
                Quaternion spawnRotation = Quaternion.AngleAxis(spawnAngle, _fox.transform.up);
                GameObject currentAnimal = Instantiate(_fox, spawnPosition, spawnRotation, _animalsObject.transform);
                Fox currentComponent = currentAnimal.GetComponent<Fox>();
                currentComponent.global = this;
                _animals.Add(currentComponent);
            }
        }

        public void ResetSimulation() {

            _uiManager.Deselect();

            for (int i = _food.Count - 1; i >= 0; i--)
            {
                Destroy(_food[i]);
            }

            _food.Clear();

            for (int i = _animals.Count - 1; i >= 0; i--)
            {
                if (_animals[i] != null)
                {
                    _animals[i].Delete();
                }
            }
            
            _animals.Clear();

            int squirrelPopulation = _uiManager.GetInitialAnimalPopulation(AnimalType.SQUIRREL);
            if (squirrelPopulation >= 0) {
                _parameters.InitialSquirrelPopulation = squirrelPopulation;
            }

            int foxPopulation = _uiManager.GetInitialAnimalPopulation(AnimalType.FOX);
            if (foxPopulation >= 0)
            {
                _parameters.InitialFoxPopulation = foxPopulation;
            }

            SpawnAnimals();
            SpawnFood();
        }
    }
}
