using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public class UnitStats : NetworkBehaviour, IHealth, ITakerDamag
    {
        #region Fields

        public Stat Damag;

        [SerializeField] private int _maxHealth;
        [SyncVar] private int _currentHealth;

        #endregion


        #region UnityMethods

        public override void OnStartServer()
        {
            _currentHealth = _maxHealth;
        }

        #endregion


        #region Methods

        public void SetHealthRate(float rate)
        {
            _currentHealth = rate == 0.0f ? 0 : (int)(_maxHealth / rate);
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