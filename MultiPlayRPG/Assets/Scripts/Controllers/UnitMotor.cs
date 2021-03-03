using UnityEngine;
using UnityEngine.AI;


namespace MultiPlayRPG
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class UnitMotor : MonoBehaviour
    {
        #region Fields

        private NavMeshAgent _agent;
        private Transform _taget;

        private bool _haveTarget;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (_haveTarget)
            { 
                if (_agent.velocity.sqrMagnitude == 0)
                {
                    FaceTarget();
                }
                _agent.SetDestination(_taget.position);
            }
        }

        #endregion


        #region Methods

        public void MoveToPoint(Vector3 point)
        {
            _agent.SetDestination(point);
        }

        public void FollowTarget(Interactable newTarget)
        {
            _agent.stoppingDistance = newTarget.Radius;
            _taget = newTarget.InteractionTransform;
            _haveTarget = _taget != null;
        }

        public void StopFollowingTarget()
        {
            _agent.stoppingDistance = 0.0f;
            _agent.ResetPath();
            _taget = null;
            _haveTarget = false;
        }

        private void FaceTarget()
        {
            if (_haveTarget)
            {
                Vector3 direction = _taget.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(
                    new Vector3(direction.x, 0.0f, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, 
                    lookRotation, Time.deltaTime * 5.0f);
            }
        }

        #endregion
    }
}
