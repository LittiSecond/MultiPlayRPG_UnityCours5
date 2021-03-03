using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public class Unit : Interactable
    {
        #region Fields

        [SerializeField] protected UnitMotor _motor;
        [SerializeField] protected UnitStats _stats;

        public delegate void UnitDenegate();
        //[SyncEvent] public event UnitDenegate EventOnDamage;
        [SyncEvent] public event UnitDenegate EventOnDie;
        [SyncEvent] public event UnitDenegate EventOnRevive;

        protected Interactable _focus;
        protected bool _isAlive = true;
        protected bool _haveFocus;

        #endregion


        #region UnityMethods

        private void Update()
        {
            OnUpdate();
        }

        #endregion


        #region Methods

        protected virtual void OnAliveUpdate()
        {

        }

        protected virtual void OnDeadUpdate()
        {

        }

        [ClientRpc]
        private void RpcDie()
        {
            if (!isServer)
            {
                Die();
            }
        }

        [ClientRpc]
        private void RpcRevive()
        {
            if (!isServer)
            {
                Revive();
            }
        }


        protected virtual void Die()
        {
            _isAlive = false;
            if (isServer)
            {
                EventOnDie();
                _hasInteract = false;
                _motor.MoveToPoint(transform.position);
                RpcDie();

                RemoveFocus();
            }
        }

        protected virtual void Revive()
        {
            _isAlive = true;
            if (isServer)
            {
                EventOnRevive();
                _hasInteract = true;
                _stats.SetHealthRate(1);
                RpcRevive();
            }
        }

        protected void OnUpdate()
        {
            if (isServer)
            {
                if (_isAlive)
                {
                    if (_stats.Health == 0)
                    {
                        Die();
                    }
                    else
                    {
                        OnAliveUpdate();
                    }
                }
                else
                {
                    OnDeadUpdate();
                }
            }
        }

        protected virtual void SetFocus( Interactable newFocus )
        {
            if (newFocus != _focus)
            {
                _focus = newFocus;
                _haveFocus = _focus != null; 
                _motor.FollowTarget(newFocus);
            }
        }

        protected virtual void RemoveFocus()
        {
            _focus = null;
            _haveFocus = false;
            _motor.StopFollowingTarget();
        }

        public override bool Interact(GameObject luser)
        {
            //return base.Interact(luser);
            Debug.Log("Unit::Interact: " + gameObject.name + " interact with " +
                luser.name);
            return true;
        }

        #endregion
    }
}
