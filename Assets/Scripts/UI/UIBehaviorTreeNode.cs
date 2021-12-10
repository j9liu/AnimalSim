using UnityEngine;
using UnityEngine.UI;

namespace BehaviorSim {
    public class UIBehaviorTreeNode : MonoBehaviour
    {
        public static readonly Color[] StatusColors = { new Color(1, 0, 0), // ERROR
                                                        new Color(1, 1, 1), // UNEXECUTED
                                                        new Color(1, 1, 0), // RUNNING
                                                        new Color(0.8f, 0.4f, 0.4f), // FAILURE
                                                        new Color(0, 1, 0),  // SUCCESS
                                                        new Color(1, 0.5f, 0) // HALT
                                                      };
        private Image _image;

        private void Start()
        {
            _image = gameObject.GetComponent<Image>();
        }

        public void ChangeColor(BehaviorTree.NodeStatus status) {
            int index = (int)status;
            if (_image == null)
            {
                _image = gameObject.GetComponent<Image>();
            }
            _image.color = StatusColors[index];
        }
    }
}

