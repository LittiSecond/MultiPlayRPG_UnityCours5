using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public class UnitStats : NetworkBehaviour, IHealth, ITakerDamag
    {
        #region Fields

        public Stat Damag;
        public Stat Armor;
        public Stat MoveSpeed;

        [SerializeField] private int _maxHealth;

        public delegate void StatsDenegate();
        [SyncEvent] public event StatsDenegate EventOnDamage;

        [SyncVar] private int _currentHealth;

        #endregion


        #region Properties

        public virtual int CurrentHealth
        {
            get
            {
                return _currentHealth;
            }
            protected set
            {
                _currentHealth = value;
            }

        }

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
            CurrentHealth = rate == 0.0f ? 0 : (int)(_maxHealth / rate);
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

            amount -= Armor.GetValue();

            if (amount < 0)
            {
                return;
            }

            CurrentHealth -= amount;
            EventOnDamage();
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
        }
        
        public void TakeHealing(int amount)
        {
            if (!isServer)
            {
                return;
            }

            if (CurrentHealth <= 0.0f)
            {
                return;
            }

            if (amount < 0)
            {
                return;
            }

            CurrentHealth += amount;
            if (CurrentHealth >= _maxHealth)
            {
                CurrentHealth = _maxHealth;
            }
        }

        #endregion
    }
}