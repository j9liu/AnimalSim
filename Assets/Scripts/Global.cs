using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim
{

    public class Global : MonoBehaviour
    {
        public GameObject food;
        public GameObject squirrel;

        public float foodSpawnTimer;
        public float foodSpawnRate; // The rate at which food spawns, in seconds

        private Ground _ground;

        private const int _animalLayerMask = 1 << 8;
        private Animal _selectedAnimal;

        private StatusBar _healthBar;
        private StatusBar _hungerBar;
        private StatusBar _thirstBar;

        // Start is called before the first frame update
        void Start()
        {
            // Find and set variables corresponding to ground
            GameObject groundObject = GameObject.Find("Ground");
            _ground = groundObject.GetComponent<Ground>();

            // Food spawn rate
            foodSpawnTimer = 0.0f;
            foodSpawnRate = 5.0f;

            // Empty variables related to animal selection
            _selectedAnimal = null;

            GameObject healthBarObject = GameObject.Find("HealthBar");
            _healthBar = healthBarObject.GetComponent<StatusBar>();
            _healthBar.SetMaxValue(Animal.MaxHealth);
            _healthBar.SetActive(false);

            GameObject hungerBarObject = GameObject.Find("HungerBar");
            _hungerBar = hungerBarObject.GetComponent<StatusBar>();
            _hungerBar.SetMaxValue(Animal.MaxHunger);
            _hungerBar.SetActive(false);

            // Populate the world with animals
            SpawnAnimals();

            // Hardcoded spawn of some acorns at the beginning of the simulation
            for (int i = 0; i < 5; i++) {
                Vector3 spawnPosition = _ground.GetRandomPositionForFood(food);
                Instantiate(food, spawnPosition, Quaternion.identity);
            }
        }

        // Update is called once per frame
        void Update()
        {
            HandleMouseInput();
            SpawnFood();
            UpdateGUI();
        }

        public Ground GetGround()
        {
            return _ground;
        }

        void HandleMouseInput() {
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000.0f, _animalLayerMask))
                {
                    _selectedAnimal = hit.collider.gameObject.GetComponent<Animal>();
                    _healthBar.SetActive(true);
                    _hungerBar.SetActive(true);
                }
                else {
                    _selectedAnimal = null;
                    _healthBar.SetActive(false);
                    _hungerBar.SetActive(false);
                }
            }
        }

        void SpawnFood()
        {
            foodSpawnTimer += Time.deltaTime;
            if (foodSpawnTimer >= foodSpawnRate)
            {
                Vector3 spawnPosition = _ground.GetRandomPositionForFood(food);
                Instantiate(food, spawnPosition, Quaternion.identity);
                foodSpawnTimer = 0.0f;
            }
        }

        void UpdateGUI() {
            if (_selectedAnimal == null) {
                return;
            }
            _healthBar.SetCurrentValue(_selectedAnimal.GetHealth());
            _hungerBar.SetCurrentValue(_selectedAnimal.GetHunger());
        }

        void SpawnAnimals() {
            GameObject current = Instantiate(squirrel, new Vector3(0, 3.5f, 0), Quaternion.identity);
            current.GetComponent<Squirrel>().global = this;
        }

        // TODO: 
        /*bool ValidatePosition(Vector3 pos) { 

        }*/

    }
}
