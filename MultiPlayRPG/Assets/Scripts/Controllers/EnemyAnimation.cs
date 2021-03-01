using UnityEngine;
using UnityEngine.AI;


namespace MultiPlayRPG
{
    public sealed class EnemyAnimation : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;

        #endregion


        #region UnityMethods

        private void FixedUpdate()
        {
            if (!_agent.hasPath)
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
