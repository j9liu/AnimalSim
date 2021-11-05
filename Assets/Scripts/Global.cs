using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BehaviorSim
{
    public class Global : MonoBehaviour
    {
        [SerializeField]
        private GameObject acorn;

        [SerializeField]
        private GameObject squirrel;

        private Ground _ground;
        private UIManager _uiManager;
        private GameObject _animalsObject;
        private GameObject _foodObject;

        private EventSystem _eventSystem;

        private List<Animal> _animals;

        private float _acornSpawnTimer;
        public float acornSpawnRate; // The rate at which acorns spawns, in seconds

        private const int _animalLayerMask = 1 << 8;

        // Start is called before the first frame update
        void Start()
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
            acornSpawnRate = 5.0f;

            // Populate the world with animals
            SpawnAnimals();

            // Hardcoded spawn of some acorns at the beginning of the simulation
            for (int i = 0; i < 5; i++) {
                Vector3 spawnPosition = _ground.GetRandomPositionForFood(acorn);
                Instantiate(acorn, spawnPosition, Quaternion.identity);
            }
        }

        // Update is called once per frame
        void Update()
        {
            HandleMouseInput();
            SpawnFood();
        }

        public Ground GetGround()
        {
            return _ground;
        }

        private void HandleMouseInput() {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000.0f, _animalLayerMask))
                {
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

        private void SpawnFood()
        {
            _acornSpawnTimer += Time.deltaTime;
            if (_acornSpawnTimer >= acornSpawnRate)
            {
                Vector3 spawnPosition = _ground.GetRandomPositionForFood(acorn);
                Instantiate(acorn, spawnPosition, Quaternion.identity);
                _acornSpawnTimer = 0.0f;
            }
        }

        // TODO: add UI so that the player can specify the initial populations
        // of animals and reuse this function
        public void SpawnAnimals() {
            if (_animals == null) {
                _animals = new List<Animal>();
            }

            /*if (_animals.Count > 0) {
                // TODO: Clear list properly
            }*/

            GameObject currentAnimal = Instantiate(squirrel, new Vector3(0, 3.5f, 0), Quaternion.identity);
            Squirrel currentComponent = currentAnimal.GetComponent<Squirrel>();
            currentComponent.global = this;
            _animals.Add(currentComponent);
        }

    }
}
