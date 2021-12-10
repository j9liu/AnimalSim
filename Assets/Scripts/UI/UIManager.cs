using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorSim {
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _uiBehaviorTreeNode;
        [SerializeField]
        private GameObject _uiBehaviorTreeLine;

        private GameObject _content;
        private List<GameObject> _uiTrees;

        private Animal _selectedAnimal;

        private GameObject _statsPanel;
        private GameObject _behaviorTreePanel;
        private GameObject _controlPanel;

        private bool _behaviorTreePanelActive;

        private StatusBar _healthBar;
        private StatusBar _foodBar;
        private StatusBar _waterBar;

        private Text _statsSpeciesNameText;
        private Text _behaviorTreeSpeciesNameText;

        private Text _behaviorTreeButtonText;

        private InputField _controlSquirrelPopulationInputField;
        private InputField _controlFoxPopulationInputField;
        private InputField _controlAcornRespawnInputField;

        // Start is called before the first frame update
        void Start()
        {
            _selectedAnimal = null;

            _statsPanel = transform.GetChild(0).gameObject;
            _behaviorTreePanel = transform.GetChild(1).gameObject;
            _controlPanel = transform.GetChild(2).gameObject;

            _statsSpeciesNameText = _statsPanel.transform.GetChild(0).GetComponent<Text>();
            _behaviorTreeSpeciesNameText = _behaviorTreePanel.transform.GetChild(0).GetComponent<Text>();

            Transform statusBarsTransform = _statsPanel.transform.GetChild(1);
            _healthBar = statusBarsTransform.GetChild(0).gameObject.GetComponent<StatusBar>();
            _foodBar = statusBarsTransform.GetChild(1).gameObject.GetComponent<StatusBar>();
            _waterBar = statusBarsTransform.GetChild(2).gameObject.GetComponent<StatusBar>();

            Transform scrollViewTransform = _behaviorTreePanel.transform.GetChild(2);
            _content = scrollViewTransform.GetChild(0).GetChild(0).gameObject;

            _uiTrees = new List<GameObject>();
            _uiTrees.Add(_content.transform.GetChild(0).gameObject);
            _uiTrees.Add(_content.transform.GetChild(1).gameObject);

            for (int i = 0; i < _uiTrees.Count; i++)
            {
                _uiTrees[i].SetActive(false);
            }

            Transform buttonTransform = _statsPanel.transform.GetChild(2);
            _behaviorTreeButtonText = buttonTransform.GetChild(0).gameObject.GetComponent<Text>();

            Transform squirrelPopulationTransform = _controlPanel.transform.GetChild(1);
            _controlSquirrelPopulationInputField = squirrelPopulationTransform.GetChild(1).GetComponent<InputField>();

            Transform foxPopulationTransform = _controlPanel.transform.GetChild(2);
            _controlFoxPopulationInputField = foxPopulationTransform.GetChild(1).GetComponent<InputField>();

            _statsPanel.SetActive(false);
            _behaviorTreePanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (_selectedAnimal != null)
            {
                _healthBar.SetCurrentValue(_selectedAnimal.Health);
                _foodBar.SetCurrentValue(_selectedAnimal.Food);
            }
        }

        public bool HasSelectedAnimal()
        {
            return _selectedAnimal != null;
        }

        public void SelectAnimal(Animal animal)
        {
            if (animal == null)
            {
                return;
            }

            _selectedAnimal = animal;
            _selectedAnimal.Select();

            switch (_selectedAnimal.Type) {
                case AnimalType.SQUIRREL:
                    _statsSpeciesNameText.text = "Squirrel";
                    _behaviorTreeSpeciesNameText.text = "Squirrel";
                    _uiTrees[0].SetActive(true);
                    break;
                case AnimalType.FOX:
                    _statsSpeciesNameText.text = "Fox";
                    _behaviorTreeSpeciesNameText.text = "Fox";
                    _uiTrees[1].SetActive(true);
                    break;
            }

            _statsPanel.SetActive(true);
            _healthBar.SetMaxValue(_selectedAnimal.Stats.MaxHealth);
            _foodBar.SetMaxValue(_selectedAnimal.Stats.MaxFood);
            _waterBar.SetMaxValue(_selectedAnimal.Stats.MaxWater);
        }

        public void ToggleBehaviorTreePanel()
        {
            if (_behaviorTreePanelActive)
            {
                HideBehaviorTreePanel();
            }
            else
            {
                ShowBehaviorTreePanel();
            }
        }

        protected void ShowBehaviorTreePanel()
        {
            if (_selectedAnimal != null)
            {
                int index = (int)_selectedAnimal.Type;
                _uiTrees[index].SetActive(true);
            }

            _behaviorTreePanelActive = true;
            _behaviorTreePanel.SetActive(true);
            _behaviorTreeButtonText.text = "Hide Behavior Tree";
            
            _controlPanel.SetActive(false);
        }

        protected void HideBehaviorTreePanel()
        {
            if (_selectedAnimal != null)
            {
                int index = (int)_selectedAnimal.Type;
                _uiTrees[index].SetActive(false);
            }

            _behaviorTreePanelActive = false;
            _behaviorTreePanel.SetActive(false);
            _behaviorTreeButtonText.text = "Show Behavior Tree";
            _controlPanel.SetActive(true);
        }

        public void Deselect()
        {
            _statsPanel.SetActive(false);
            HideBehaviorTreePanel();

            if (_selectedAnimal != null)
            {
                switch (_selectedAnimal.Type)
                {
                    case AnimalType.SQUIRREL:
                        _uiTrees[0].SetActive(false);
                        break;
                    case AnimalType.FOX:
                        _uiTrees[1].SetActive(false);
                        break;
                }
                _selectedAnimal.Deselect();
            }

            _selectedAnimal = null;
        }

        public GameObject GetUITree(AnimalType animal)
        {
            int index = (int)animal;
            return _uiTrees[index];
        }

        public int GetInitialAnimalPopulation(AnimalType type)
        {
            string input = "";
            switch (type) {
                case AnimalType.SQUIRREL:
                    input = _controlSquirrelPopulationInputField.text;
                    break;
                case AnimalType.FOX:
                    input = _controlFoxPopulationInputField.text;
                    break;
                default:
                    return -1;
            }

            try
            {
                int result = Int16.Parse(input);
                return result;
            }
            catch {
                return -1;
            }
        }

        public void SetInitialAnimalPopulation(AnimalType type, int number)
        {
            switch (type)
            {
                case AnimalType.SQUIRREL:
                    _controlSquirrelPopulationInputField.text = number.ToString();
                    break;
                case AnimalType.FOX:
                    _controlFoxPopulationInputField.text = number.ToString();
                    break;
                default:
                    break;
            }
        }
    }

}