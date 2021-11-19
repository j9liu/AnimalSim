using System.Collections;
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

        private bool _behaviorTreePanelActive;

        private StatusBar _healthBar;
        private StatusBar _foodBar;
        private StatusBar _waterBar;

        private Text _behaviorTreeButtonText;

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

            Transform buttonTransform = _statsPanel.transform.GetChild(2);
            _behaviorTreeButtonText = buttonTransform.GetChild(0).gameObject.GetComponent<Text>();
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
                _behaviorTreePanel.SetActive(true);
                _behaviorTreeButtonText.text = "Hide Behavior Tree";
            }
        }

        public void HideBehaviorTreePanel()
        {
            _behaviorTreePanel.SetActive(false);
            _behaviorTreeButtonText.text = "Show Behavior Tree";
        }

        public void Deselect()
        {
            if (_selectedAnimal != null)
            {
                _selectedAnimal.Deselect();
            }
            _selectedAnimal = null;
            _statsPanel.SetActive(false);
            HideBehaviorTreePanel();
        }

        public GameObject GetUITree(AnimalType animal)
        {
            int index = (int)animal;
            return _uiTrees[index];
        }
    }

}