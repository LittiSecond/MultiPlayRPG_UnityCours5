using UnityEngine;


namespace MultiPlayRPG
{
    [RequireComponent(typeof(UnitMotor), typeof(EnemyStats))]
    public class Enemy : Unit
    {
        #region Properties

        [Header("Movement")]
        [SerializeField] private float _moveRadius = 10.0f;
        [SerializeField] private float _minMovementDelay = 4.0f;
        [SerializeField] private float _maxMovementDelay = 12.0f;

        [Header("Behaviour")]

        [SerializeField] private float _viewDistance = 5.0f;
        [SerializeField] private float _revievDelay = 5.0f;
        [SerializeField] private bool _aggressive;

        private Vector3 _startPosition;
        private Vector3 _curDistanation;

        private float _changePosTime;
        private float _revievTime;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _startPosition = transform.position;
            _changePosTime = UnityEngine.Random.Range(_minMovementDelay, _maxMovementDelay);
            _revievTime = _revievDelay;
        }

        private void Update()
        {
            OnUpdate();
        }

        #endregion


        #region Methods

        protected override void OnDeadUpdate()
        {
            base.OnDeadUpdate();
            if (_revievTime > 0)
            {
                _revievTime -= Time.deltaTime;
            }
            else
            {
                _revievTime = _revievDelay;
                Revive();
            }
        }

        protected override void OnAliveUpdate()
        {
            base.OnAliveUpdate();
            Wandering(Time.deltaTime);
        }

        private void Wandering(float deltaTime)
        {
            _changePosTime -= deltaTime;
            if (_changePosTime <= 0)
            {
                RandomMove();
                _changePosTime = UnityEngine.Random.Range(_minMovementDelay, _maxMovementDelay);
            }
        }

        private void RandomMove()
        {
            _curDistanation = Quaternion.AngleAxis(
                UnityEngine.Random.Range(0.0f, 360.0f),  Vector3.up) * 
                new Vector3(_moveRadius, 0, 0) + _startPosition;
            _motor.MoveToPoint(_curDistanation);
        }

        protected override void Revive()
        {
            base.Revive();
            transform.position = _startPosition;
            if (isServer)
            {
                _motor.MoveToPoint(_startPosition);
            }
        }


        #endregion



    }
}
