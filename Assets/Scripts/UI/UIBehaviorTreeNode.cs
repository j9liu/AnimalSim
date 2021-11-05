using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorSim {
    public class UIBehaviorTreeNode : MonoBehaviour
    {
        public static readonly Color[] StatusColors = { new Color(1, 0, 0),
                                                        new Color(1, 1, 1),
                                                        new Color(1, 1, 0),
                                                        new Color(0.8f, 0.4f, 0.4f),
                                                        new Color(0, 1, 0)};
        private Image _image;

        private void Start()
        {
            _image = gameObject.GetComponent<Image>();
        }

        public void ChangeColor(BehaviorTree.NodeStatus status) {
            int index = (int)status;
            _image.color = StatusColors[index];
        }
    }
}

