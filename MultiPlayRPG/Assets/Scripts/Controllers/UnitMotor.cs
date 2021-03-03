using UnityEngine;
using UnityEngine.AI;


namespace MultiPlayRPG
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class UnitMotor : MonoBehaviour
    {
        #region Fields

        private NavMeshAgent _agent;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        #endregion


        #region Methods

        public void MoveToPoint(Vector3 point)
        {
            _agent.SetDestination(point);
        }

        #endregion
    }
}
