using UnityEngine;


namespace MultiPlayRPG
{
    public class UnitTriggerAnimation : MonoBehaviour
    {
        #region Fields

        [SerializeField] Animator _animator;
        [SerializeField] Unit _unit;
        [SerializeField] UnitStats _stats;
        [SerializeField] CombatSystem _combat;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _stats.EventOnDamage += Damage;
            _unit.EventOnDie += Die;
            _unit.EventOnRevive += Revive;
            _combat.EventOnAttack += Attack;
        }

        #endregion


        #region Methods

        private void Damage()
        {
            _animator.SetTrigger("Damage");
        }

        private void Die()
        {
            _animator.SetTrigger("Die");
        }

        private void Revive()
        {
            _animator.ResetTrigger("Damage");
            _animator.ResetTrigger("Attack");
            _animator.SetTrigger("Revive");
        }

        private void Attack()
        {
            _animator.SetTrigger("Attack");
        }

        #endregion
    }
}
