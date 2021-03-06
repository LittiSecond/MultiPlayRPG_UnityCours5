﻿using UnityEngine;
using UnityEngine.AI;


namespace MultiPlayRPG
{
    public sealed class UnitAnimation : MonoBehaviour
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
                _animator.SetBool("Moving", false);
            }
            else
            {
                _animator.SetBool("Moving", true);
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