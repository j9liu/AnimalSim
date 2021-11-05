using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim {
    public class CameraController : MonoBehaviour
    {
        private float _dragSpeed = 1.5f;
        private float _zoomSpeed = 20.0f;

        private float _minimumZoomDistance = 5.0f;
        private float _maximumZoomDistance = 100.0f;

        private Vector3 _dragOrigin;
        private Vector3 _dragDifference;
        private Vector3 _dragMovement;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _dragOrigin = Input.mousePosition;
                return;
            }

            if (Input.GetMouseButton(0)) {
                _dragDifference = _camera.ScreenToViewportPoint(Input.mousePosition - _dragOrigin);
                _dragMovement = -_dragSpeed * new Vector3(_dragDifference.x, 0, _dragDifference.z);

                _camera.transform.Translate(_dragMovement, Space.World);
            }

            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            float distance = Mathf.Abs(transform.position.y - _camera.transform.position.y);
            if ((distance <= _minimumZoomDistance && scrollInput > 0.0f) || (distance > _maximumZoomDistance && scrollInput < 0.0f)) {
                return;
            }

            _camera.transform.position += _camera.transform.forward * scrollInput * _zoomSpeed;
        }
    }
}
