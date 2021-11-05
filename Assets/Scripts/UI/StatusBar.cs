using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorSim
{
    public class StatusBar : MonoBehaviour
    {
        private const float epsilon = 0.001f;

        private GameObject barObject;
        private float maxValue;
        private float currentValue;

        // Start is called before the first frame update
        private void Start()
        {
            GameObject backgroundObject = gameObject.transform.GetChild(1).gameObject;
            barObject = backgroundObject.transform.GetChild(0).gameObject;
        }

        public void SetMaxValue(float value)
        {
            maxValue = value;
        }

        private bool EqualsEpsilon(float left, float right)
        {
            return Mathf.Abs(left - right) < epsilon;
        }

        public void SetCurrentValue(float value)
        {
            if (!EqualsEpsilon(value, currentValue))
            {
                currentValue = value;
                barObject.transform.localScale = new Vector3(currentValue / maxValue, 1, 1);
            }
        }

        public void SetActive(bool value) {
            gameObject.SetActive(value);
        }
    }

}