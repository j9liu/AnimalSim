using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BehaviorSim {
    public class CameraController : MonoBehaviour
    {
        private Camera _camera;
        private Vector3 _groundBounds;

        private float _dragSpeed = 16.0f;
        private float _zoomSpeed = 20.0f;
        private float _rotateSpeed = 2.0f;

        private float _minimumZoomDistance = 5.0f;
        private float _maximumZoomDistance = 100.0f;

        private Vector3 _dragCameraOrigin;
        private Vector3 _dragScreenOrigin;
        private Vector3 _dragDifference;
        private Vector3 _dragMovement;
        private float   _dragMovementCap;

        private Vector3 _rotateOrigin = Vector3.zero;


        private void Awake()
        {
            _camera = Camera.main;

            GameObject groundObject = GameObject.Find("Ground");
            _groundBounds = groundObject.transform.localScale;

            _dragMovementCap = Mathf.Max(_groundBounds.x, _groundBounds.z);
        }

        // Update is called once per frame
        private void Update()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButton(1))
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        _dragCameraOrigin = _camera.transform.position;
                        _dragScreenOrigin = _camera.ScreenToViewportPoint(Input.mousePosition);
                        return;
                    }

                    _dragDifference = _camera.ScreenToViewportPoint(Input.mousePosition) - _dragScreenOrigin;
                    _dragMovement = _dragSpeed * (_dragDifference.x * _camera.transform.right + _dragDifference.z * _camera.transform.forward);

                    _camera.transform.position = Vector3.MoveTowards(_dragCameraOrigin, _dragCameraOrigin - _dragMovement, _dragMovementCap);
                }

                if (Input.GetMouseButton(2))
                {
                    if (Input.GetMouseButtonDown(2))
                    {
                        return;
                    }

                    _camera.transform.RotateAround(_rotateOrigin, transform.up, Input.GetAxis("Mouse X") * _rotateSpeed);
                }

                float scrollInput = Input.GetAxis("Mouse ScrollWheel");
                float distance = Mathf.Abs(transform.position.y - _camera.transform.position.y);
                if ((distance <= _minimumZoomDistance && scrollInput > 0.0f) || (distance > _maximumZoomDistance && scrollInput < 0.0f))
                {
                    return;
                }

                _camera.transform.position += _camera.transform.forward * scrollInput * _zoomSpeed;
            }
        }
    }
}
