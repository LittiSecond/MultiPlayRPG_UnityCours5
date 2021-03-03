﻿using UnityEngine;
using UnityEngine.AI;


namespace MultiPlayRPG
{
    public sealed class CharacterOfPlr : Unit
    {
        #region Fields

        [SerializeField] GameObject _gfx;
        [SerializeField] private float _revievDelay = 5.0f;

        private Vector3 _startPosition;
        private float _revievTime;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _startPosition = transform.position;
            _revievTime = _revievDelay;
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
                        _focus.Interact(gameObject);
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


        #endregion
    }
}
