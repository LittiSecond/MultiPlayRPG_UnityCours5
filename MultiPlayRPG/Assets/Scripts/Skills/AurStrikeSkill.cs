using UnityEngine;


namespace MultiPlayRPG
{
    public class AurStrikeSkill : UpgradableSkill
    {
        #region Fields

        private const int BASE_DAMAGE = 10;

        [SerializeField] private int _damage;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _enemyMask;
        [SerializeField] private ParticleSystem _auraEffect;

        #endregion


        #region Properties

        public override int Level 
        {
            set
            {
                base.Level = value;
                _damage = BASE_DAMAGE + Level;
            }
        }

        #endregion


        #region UnityMethods

        protected void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        #endregion


        #region Methods

        protected override void OnUse()
        {
            if (isServer)
            {
                _unit.Motor.StopFollowingTarget();
            }
            base.OnUse();
        }

        protected override void OnCastComplete()
        {
            if (isServer)
            {
                Collider[] colliders = 
                    Physics.OverlapSphere(transform.position, _radius, _enemyMask);
                for (int i = 0; i < colliders.Length; i++)
                {
                    Unit enemy = colliders[i].GetComponent<Unit>();
                    if (enemy != null && enemy.HasInteract)
                    {
                        enemy.TakeDamage(_unit.gameObject, _damage);
                    }
                }
            }
            else
            {
                _auraEffect.Play();
            }
            base.OnCastComplete();
        }

        #endregion
    }
}
