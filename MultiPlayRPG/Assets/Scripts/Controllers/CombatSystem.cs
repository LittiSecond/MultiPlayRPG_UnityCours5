using UnityEngine;
using UnityEngine.Networking;


namespace MultiPlayRPG
{
    [RequireComponent(typeof(UnitStats))]
    public class CombatSystem : NetworkBehaviour
    {
        #region Fields

        [SerializeField] private float _attackSpeed = 1.0f;

        public float AttackDistance = 0.0f;

        public delegate void CombatDenegate();
        [SyncEvent] public event CombatDenegate EventOnAttack;

        private float _attackCooldown = 0.0f;

        private UnitStats _thisStats;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _thisStats = GetComponent<UnitStats>();
        }

        private void Update()
        {
            if (_attackCooldown > 0.0f)
            {
                _attackCooldown -= Time.deltaTime;
            }
        }

        #endregion


        #region Methods

        public bool Attack(ITakerDamag target)
        {
            if (_attackCooldown <= 0.0f)
            {
                int damag = _thisStats.Damag.GetValue();
                target.TakeDamag(damag);
                _attackCooldown = 1.0f / _attackSpeed;
                EventOnAttack();
                return true;
            }
            return false;
        }

        #endregion
    }
}
