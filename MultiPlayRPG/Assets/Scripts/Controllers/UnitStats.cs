using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public sealed class UnitStats : NetworkBehaviour, IHealth, ITakerDamag
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


        #region IHealth
        public int MaxHealth { get => _maxHealth; }

        public int Health { get => _currentHealth; }

        #endregion


        #region ITakerDamag
        public void TakeDamag(int amount)
        {
            if (!isServer)
            {
                return;
            }

            _currentHealth -= amount;
            if (_currentHealth < 0)
            {
                _currentHealth = 0;
            }
        }

        #endregion
    }
}