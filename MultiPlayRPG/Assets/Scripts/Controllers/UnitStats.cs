using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public sealed class UnitStats : NetworkBehaviour
    {
        #region Fields

        [SerializeField] private int _maxHealth;
        [SyncVar] private int _currentHealth;

        #endregion


        #region UnityMethods

        public override void OnStartAuthority()
        {
            _currentHealth = _maxHealth;
        }

        #endregion
    }
}