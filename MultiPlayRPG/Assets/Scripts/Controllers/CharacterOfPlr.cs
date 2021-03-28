using UnityEngine;


namespace MultiPlayRPG
{
    public sealed class CharacterOfPlr : Unit
    {
        #region Fields

        [SerializeField] GameObject _gfx;
        [SerializeField] private float _revievDelay = 5.0f;

        public PlayerScriptsConnector PlayerScriptsConnectorr;

        private CombatSystem _combatSystem;
        //private Inventory _inventory;
        private Vector3 _startPosition;
        private float _revievTime;

        #endregion


        #region Properties

        new public PlayerStats Stats
        {
            get
            {
                return _stats as PlayerStats;
            }
        }

        #endregion


        #region UnityMethods

        private void Start()
        {
            _combatSystem = GetComponent<CombatSystem>();
            _startPosition = Vector3.zero;
            _revievTime = _revievDelay;

            if (Stats.CurrentHealth == 0)
            {
                transform.position = _startPosition;
                if (isServer)
                {
                    Stats.SetHealthRate(1.0f);
                    _motor.MoveToPoint(_startPosition);
                }
            }

        }

        private void Update()
        {
            OnUpdate();
        }

        #endregion


        #region Methods

        protected override void OnAliveUpdate()
        {
            base.OnAliveUpdate();
            if (_haveFocus)
            {
                if (!_focus.HasInteract)
                {
                    RemoveFocus();
                }
                else
                {
                    float distance = Vector3.Distance(
                        _focus.InteractionTransform.position, transform.position);
                    if (distance <= _focus.Radius)
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
        }

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

        protected override void Die()
        {
            base.Die();
            _gfx.SetActive(false);
        }

        protected override void Revive()
        {
            base.Revive();
            transform.position = _startPosition;
            _gfx.SetActive(true);
            if (isServer)
            {
                _motor.MoveToPoint(_startPosition);
            }
        }

        public void SetMovePoint(Vector3 point)
        {
            if (_isAlive)
            {
                RemoveFocus();
                _motor.MoveToPoint(point);
            }
        }

        public void SetNewFocus(Interactable newFocus )
        {
            if (_isAlive)
            {
                if (newFocus.HasInteract)
                {
                    SetFocus(newFocus);
                }
            }

        }

        //public void SetInventory(Inventory inventory)
        //{
        //    _inventory = inventory;
        //    _inventory.DropPoint = transform;
        //}

        public bool AddToInventory(Item item)
        {
            return PlayerScriptsConnectorr.Inventoryy.AddItem(item);
        }

        #endregion
    }
}
