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

        private int _damageHash = Animator.StringToHash("Damage");
        private int _attackHash = Animator.StringToHash("Attack");
        private int _dieHash = Animator.StringToHash("Die");
        private int _reviveHash = Animator.StringToHash("Revive");

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
            _animator.SetTrigger(_damageHash);
        }

        private void Die()
        {
            _animator.SetTrigger(_dieHash);
        }

        private void Revive()
        {
            _animator.ResetTrigger(_damageHash);
            _animator.ResetTrigger(_attackHash);
            _animator.SetTrigger(_reviveHash);
        }

        private void Attack()
        {
            _animator.SetTrigger(_attackHash);
        }

        #endregion
    }
}
