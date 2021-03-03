using UnityEngine;
using UnityEngine.AI;


namespace MultiPlayRPG
{
    public sealed class EnemyAnimation : MonoBehaviour
    {
        #region Fields

        private const float NOT_MOVE_THRESHOLD = 0.01f;

        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;

        #endregion


        #region UnityMethods

        private void FixedUpdate()
        {
            if (_agent.velocity.magnitude < NOT_MOVE_THRESHOLD)
            {
                _animator.SetBool("Move", false);
            }
            else
            {
                _animator.SetBool("Move", true);
            }
        }

        #endregion
    }
}
