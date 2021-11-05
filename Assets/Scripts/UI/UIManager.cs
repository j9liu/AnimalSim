using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorSim.BehaviorTree;

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

        private StatusBar _healthBar;
        private StatusBar _foodBar;
        private StatusBar _waterBar;

        // Start is called before the first frame update
        void Start()
        {
            _selectedAnimal = null;

            _statsPanel = transform.GetChild(0).gameObject;
            _statsPanel.SetActive(false);
            _behaviorTreePanel = transform.GetChild(1).gameObject;
            _behaviorTreePanel.SetActive(false);

            Transform statusBarsTransform = _statsPanel.transform.GetChild(1);
            _healthBar = statusBarsTransform.GetChild(0).gameObject.GetComponent<StatusBar>();
            _foodBar = statusBarsTransform.GetChild(1).gameObject.GetComponent<StatusBar>();
            _waterBar = statusBarsTransform.GetChild(2).gameObject.GetComponent<StatusBar>();

            Transform scrollViewTransform = _behaviorTreePanel.transform.GetChild(2);
            _content = scrollViewTransform.GetChild(0).GetChild(0).gameObject;

            _uiTrees = new List<GameObject>();
            _uiTrees.Add(_content.transform.GetChild(0).gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (_selectedAnimal != null)
            {
                _healthBar.SetCurrentValue(_selectedAnimal.GetHealth());
                _foodBar.SetCurrentValue(_selectedAnimal.GetFood());
            }
        }

        public bool HasSelectedAnimal() {
            return _selectedAnimal != null;
        }

        public void SelectAnimal(Animal animal) {
            if (animal != null) {
                _selectedAnimal = animal;
                _selectedAnimal.Select();
                _statsPanel.SetActive(true);
                _healthBar.SetMaxValue(_selectedAnimal.maxHealth);
                _foodBar.SetMaxValue(_selectedAnimal.maxFood);
                _waterBar.SetMaxValue(_selectedAnimal.maxWater);
            }
        }

        public void SwitchToStatsPanel() {
            _statsPanel.SetActive(true);
            _behaviorTreePanel.SetActive(false);
        }

        public void SwitchToBehaviorTreePanel() {
            _statsPanel.SetActive(false);
            _behaviorTreePanel.SetActive(true);
        }

        public void Deselect() {
            if (_selectedAnimal != null) {
                _selectedAnimal.Deselect();
            }
            _selectedAnimal = null;
            _statsPanel.SetActive(false);
            _behaviorTreePanel.SetActive(false);
        }

        public GameObject GetUITree(AnimalType animal) {
            int index = (int)animal;
            return _uiTrees[index];
        }
    }

}