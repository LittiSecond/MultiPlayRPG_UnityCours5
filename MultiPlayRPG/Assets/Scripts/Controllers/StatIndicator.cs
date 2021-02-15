using UnityEngine;


namespace MultiPlayRPG
{
    public sealed class StatIndicator : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform _barTransform;
        private Transform _cameraTransform;

        private IHealth _health;

        [SerializeField] private float _maxXScale;
        [SerializeField] private int _maxValue;
        [SerializeField] private int _currentValue;
        private bool _isRotationEnabled = false;
        private bool _enabled = false;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _maxXScale = _barTransform.localScale.x;
            _maxValue = 1;
        }

        private void OnEnable()
        {
            _cameraTransform = Camera.main.transform;
            _isRotationEnabled = true;
        }

        private void OnDisable()
        {
            _isRotationEnabled = false;
        }

        private void Update()
        {
            if (_isRotationEnabled && _enabled)
            {
                transform.LookAt(_cameraTransform);

                int newHealth = _health.Health;
                if (newHealth != _currentValue)
                {
                    SetValue(newHealth);
                }
            }
        }

        #endregion

        #region Methods

        private void SetValue(int current, int max)
        {
            if (max > 0)
            {
                _maxValue = max;
                SetValue(current);
            }
        }

        private void SetValue(int current)
        {
            _currentValue = Mathf.Clamp(current, 0, _maxValue);
            Vector3 scale = _barTransform.localScale;
            scale.x = (float)_currentValue / _maxValue * _maxXScale;
            _barTransform.localScale = scale;
        }

        public void SetHealth(IHealth h)
        {
            _health = h;
            On();
        }

        private void On()
        {
            if (_health != null)
            {
                _enabled = true;
                SetValue(_health.Health, _health.MaxHealth);
            }
            else
            {
                Off();
            }
        }

        private void Off()
        {
            _enabled = false;
        }

        #endregion

    }
}