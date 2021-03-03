using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public sealed class NetPlayerSetup : NetworkBehaviour
    {
        #region Fields

        [SerializeField] MonoBehaviour[] _disableBehaviours;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            if (!hasAuthority)
            {
                for (int i = 0; i < _disableBehaviours.Length; i++)
                {
                    _disableBehaviours[i].enabled = false;
                }
            }
        }

        public override void OnStartAuthority()
        {
            for (int i = 0; i < _disableBehaviours.Length; i++)
            {
                _disableBehaviours[i].enabled = true;
            }
        }

        #endregion
    }
}