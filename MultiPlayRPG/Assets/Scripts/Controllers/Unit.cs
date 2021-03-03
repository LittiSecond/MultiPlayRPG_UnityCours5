using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public class Unit : NetworkBehaviour
    {
        #region Fields

        [SerializeField] protected UnitMotor _motor;
        [SerializeField] protected UnitStats _stats;

        protected bool _isAlive = true;

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
                _motor.MoveToPoint(transform.position);
                RpcDie();
            }
        }

        protected virtual void Revive()
        {
            _isAlive = true;
            if (isServer)
            {
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

        #endregion
    }
}
