using UnityEngine;
using UnityEngine.AI;


namespace MultiPlayRPG
{
    public sealed class UnitAnimation : MonoBehaviour
    {
        #region Fields

        private const float NOT_MOVE_THRESHOLD = 0.01f;

        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;

        private int _movingHash = Animator.StringToHash("Move");

        #endregion


        #region UnityMethods

        private void FixedUpdate()
        {
            if (_agent.velocity.magnitude < NOT_MOVE_THRESHOLD)
            {
                _animator.SetBool(_movingHash, false);
            }
            else
            {
                _animator.SetBool(_movingHash, true);
            }
        }

        #endregion


        #region Methods

        // Can't to delete the events from the animations

        void Hit()
        {
        }

        void FootR()
        {
        }

        void FootL()
        {
        }


        #endregion

    }
}