using UnityEngine;
using System.Collections.Generic;


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
        [SerializeField] private float _rewardExpa;
        [SerializeField] private bool _aggressive;

        private List<CharacterOfPlr> _enemies = new List<CharacterOfPlr>();
        private CombatSystem _combatSystem;

        private Vector3 _startPosition;
        private Vector3 _curDistanation;

        private float _changePosTime;
        private float _revievTime;
        private LayerMask _playerMask;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _combatSystem = GetComponent<CombatSystem>();
            _startPosition = transform.position;
            _changePosTime = UnityEngine.Random.Range(_minMovementDelay, _maxMovementDelay);
            _revievTime = _revievDelay;
            _playerMask = LayerManager.GetLayerMask(Layers.PlayersCharacters);
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
            if (!_haveFocus)
            {
                Wandering(Time.deltaTime);
                if (_aggressive)
                {
                    FindEnemy();
                }
            }
            else
            {
                float distance = Vector3.Distance(
                    _focus.InteractionTransform.position, transform.position);
                if (distance > _viewDistance || !_focus.HasInteract)
                {
                    RemoveFocus();
                }
                else if (distance < _focus.Radius)
                {
                    if ( !_focus.Interact(gameObject))
                    {
                        RemoveFocus();
                    }
                    //ITakerDamag takerDamag = _focus.GetComponent<ITakerDamag>();
                    //if (takerDamag != null)
                    //{
                    //    _combatSystem.Attack(takerDamag);
                    //}
                }
            }

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

        private void FindEnemy()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _viewDistance, _playerMask);
            for (int i = 0; i < colliders.Length; i++)
            {
                Interactable interactable = colliders[i].GetComponent<Interactable>();
                if (interactable != null)
                {
                    if (interactable.HasInteract)
                    {
                        SetFocus(interactable);
                        break;
                    }
                }
            }
        }

        protected override void OnDrawGizmosSelected()
        {
            //base.OnDrawGizmosSelected();
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _viewDistance);
        }

        public override bool Interact(GameObject luser)
        {
            if (base.Interact(luser))
            {
                SetFocus(luser.GetComponent<Interactable>());
                return true;
            }
            return false;
        }

        protected override void DamageWithCombat(GameObject luser)
        {
            base.DamageWithCombat(luser);
            CharacterOfPlr character = luser.GetComponent<CharacterOfPlr>();
            if (character != null && !_enemies.Contains(character))
            {
                _enemies.Add(character);
            }
        }

        protected override void Die()
        {
            base.Die();
            if (isServer)
            {
                int count = _enemies.Count;
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        _enemies[i].PlayerScriptsConnectorr.Progress.AddExp(_rewardExpa / count);
                    }
                    _enemies.Clear();
                }
            }
        }

        #endregion



    }
}
