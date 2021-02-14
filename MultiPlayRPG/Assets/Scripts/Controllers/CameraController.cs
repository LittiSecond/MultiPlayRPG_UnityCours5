using UnityEngine;


namespace MultiPlayRPG
{
    public sealed class CameraController : MonoBehaviour
    {
        #region Fields

        [SerializeField] Vector3 _offsetFromCharacter;
        [SerializeField] float _pitch = 2.0f;
        [SerializeField] float _zoomSpeed = 4.0f;
        [SerializeField] float _minZoom = 4.9f;
        [SerializeField] float _maxZoom = 15.2f;

        private Transform _target;

        private float _currentZoom = 10.0f;
        private float _currentRotation = 0.0f;
        private float _previousMouseX;

        #endregion


        #region Properties

        public Transform Target { set => _target = value; }

        #endregion


        #region UnityMethods

        private void Update()
        {
            if (_target)
            {
                _currentZoom -= Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;
                _currentZoom = Mathf.Clamp(_currentZoom, _minZoom, _maxZoom);

                if (Input.GetMouseButton(2))
                {
                    _currentRotation += Input.mousePosition.x - _previousMouseX;
                }
            }

            _previousMouseX = Input.mousePosition.x;
        }

        private void LateUpdate()
        {
            if (_target)
            {
                transform.position = _target.position - _offsetFromCharacter * _currentZoom;
                transform.LookAt(_target.position + Vector3.up * _pitch);
                transform.RotateAround(_target.position, Vector3.up, _currentRotation);
            }
        }

        #endregion


        #region Methods

        #endregion




    }
}